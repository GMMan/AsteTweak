using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace AsteTweak
{
    partial class MainForm : Form
    {
        string curPath;
        Offsets.Resolution curRes;
        Offsets.IKeyMapping curKeys;
        uint? curRefreshOffset;
        TextBox textEditor = new TextBox { Visible = false };
        KeyReaderControl keyEditor = new KeyReaderControl { Visible = false };

        public MainForm()
        {
            InitializeComponent();
            Controls.Add(textEditor);
            Controls.Add(keyEditor);

            verLabel.Text = string.Format(verLabel.Text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            verLabel.Left = Size.Width - 28 - verLabel.Width;

            try
            {
                // Attempt to auto-load Steam version of the game
                string steamGamePath = getSteamVersionLocation();
                if (steamGamePath != null) setSelectedExe(steamGamePath);
            }
            catch { }
        }

        uint makeMaxBitMask(int bits)
        {
            if (bits < 0) throw new ArgumentOutOfRangeException("bits", "Number of bits must be positive.");
            if (bits >= 32) return 0xffffffff;
            return (uint)(1 << bits) - 1;
        }

        // Autodetect for Steam version
        string getSteamVersionLocation()
        {
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Default))
            {
                using (RegistryKey appUnistKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 283680"))
                {
                    if (appUnistKey != null)
                    {
                        return Path.Combine((string)appUnistKey.GetValue("InstallLocation"), "Astebreed.exe");
                    }
                }
            }

            return null;
        }

        void setSelectedExe(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                PeLite pe = new PeLite(fs);
                uint timestamp = pe.GetImageTimestamp();
                if (!Offsets.Resolutions.TryGetValue(timestamp, out curRes) || !Offsets.KeyMappings.TryGetValue(timestamp, out curKeys))
                {
                    MessageBox.Show(this, "Could not find offsets for this version of the game.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Offsets.RefreshRateMappings.ContainsKey(timestamp))
                    curRefreshOffset = Offsets.RefreshRateMappings[timestamp];
                else
                    curRefreshOffset = null;
            }

            exePathLabel.Text = curPath = path;

            loadValuesFromExe(path);

            saveButton.Enabled = true;
            if (File.Exists(makeBackupFilename(path))) restoreButton.Enabled = true;
        }

        string makeBackupFilename(string path)
        {
            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + "_orig" + Path.GetExtension(path));
        }

        void loadValuesFromExe(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                BinaryReader br = new BinaryReader(fs);

                // Read resolutions table
                fs.Seek(curRes.TableBegin, SeekOrigin.Begin);
                for (int i = 0; i < resListView.Items.Count; ++i)
                {
                    ListViewItem itm = resListView.Items[i];
                    if (i < curRes.MaxCount)
                    {
                        itm.SubItems[1].Text = (curRes.IsLong ? br.ReadUInt32() : br.ReadUInt16()).ToString();
                        itm.SubItems[2].Text = (curRes.IsLong ? br.ReadUInt32() : br.ReadUInt16()).ToString();
                    }
                    else
                    {
                        itm.SubItems[1].Text = itm.SubItems[2].Text = "N/A";
                    }
                }

                // Read refresh rate
                if (curRefreshOffset.HasValue)
                {
                    fs.Seek(curRefreshOffset.Value, SeekOrigin.Begin);
                    refreshRateNumericUpDown.Value = -br.ReadSByte() + 1;
                    refreshRateNumericUpDown.Enabled = true;
                }
                else
                {
                    refreshRateNumericUpDown.Enabled = false;
                }

                // Read mappings
                mapListView.Items.Clear();
                var propsDict = new Dictionary<int, Tuple<InputKeyAttribute, PropertyDescriptor>>();
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(curKeys))
                {
                    foreach (Attribute attr in prop.Attributes)
                    {
                        if (attr is InputKeyAttribute ikAttr)
                        {
                            propsDict[ikAttr.Order] = new Tuple<InputKeyAttribute, PropertyDescriptor>(ikAttr, prop);
                            break;
                        }
                    }
                }

                foreach (var tup in propsDict.OrderBy(p => p.Key).Select(p => p.Value))
                {
                    var ikAttr = tup.Item1;
                    var prop = tup.Item2;
                    if (prop.Name == nameof(Offsets.IKeyMapping.TableBegin))
                    {
                        fs.Seek(curKeys.TableBegin, SeekOrigin.Begin);
                        for (int i = 0; i < curKeys.TableNames.Length; ++i)
                        {
                            Keys key = (Keys)br.ReadUInt32();
                            var itm = new ListViewItem(new string[] { curKeys.TableNames[i], key.ToString() }) { Tag = key };
                            itm.SubItems[0].Tag = 32;
                            mapListView.Items.Add(itm);
                        }
                    }
                    else
                    {
                        uint offset = (uint)prop.GetValue(curKeys);
                        fs.Seek(offset, SeekOrigin.Begin);
                        Keys key = (Keys)(ikAttr.IsLong ? br.ReadUInt32() : br.ReadByte());
                        var itm = new ListViewItem(new string[] { ikAttr.Name, key.ToString() }) { Tag = key };
                        itm.SubItems[0].Tag = ikAttr.IsLong ? 32 : 7;
                        mapListView.Items.Add(itm);
                    }
                }
            }
        }

        void saveValuesToExe(string path)
        {
            using (FileStream fs = File.OpenWrite(path))
            {
                BinaryWriter bw = new BinaryWriter(fs);

                // Write resolutions table
                fs.Seek(curRes.TableBegin, SeekOrigin.Begin);
                for (int i = 0; i < resListView.Items.Count; ++i)
                {
                    ListViewItem itm = resListView.Items[i];
                    if (i < curRes.MaxCount)
                    {
                        if (curRes.IsLong)
                        {
                            bw.Write(uint.Parse(itm.SubItems[1].Text));
                            bw.Write(uint.Parse(itm.SubItems[2].Text));
                        }
                        else
                        {
                            bw.Write(ushort.Parse(itm.SubItems[1].Text));
                            bw.Write(ushort.Parse(itm.SubItems[2].Text));
                        }
                    }
                }

                // Read refresh rate
                if (curRefreshOffset.HasValue)
                {
                    fs.Seek(curRefreshOffset.Value, SeekOrigin.Begin);
                    bw.Write(((sbyte)-(refreshRateNumericUpDown.Value - 1)));
                }

                // Write key mappings
                var propsDict = new Dictionary<int, Tuple<InputKeyAttribute, PropertyDescriptor>>();
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(curKeys))
                {
                    foreach (Attribute attr in prop.Attributes)
                    {
                        if (attr is InputKeyAttribute ikAttr)
                        {
                            propsDict[ikAttr.Order] = new Tuple<InputKeyAttribute, PropertyDescriptor>(ikAttr, prop);
                            break;
                        }
                    }
                }

                foreach (var tup in propsDict.OrderBy(p => p.Key).Select(p => p.Value))
                {
                    var ikAttr = tup.Item1;
                    var prop = tup.Item2;
                    if (prop.Name == nameof(Offsets.IKeyMapping.TableBegin))
                    {
                        fs.Seek(curKeys.TableBegin, SeekOrigin.Begin);
                        for (int i = 0; i < curKeys.TableNames.Length; ++i)
                        {
                            bw.Write((uint)((Keys)mapListView.Items[ikAttr.Order + i].Tag));
                        }
                    }
                    else
                    {
                        uint offset = (uint)prop.GetValue(curKeys);
                        fs.Seek(offset, SeekOrigin.Begin);
                        Keys key = (Keys)mapListView.Items[ikAttr.Order].Tag;
                        if (ikAttr.IsLong)
                            bw.Write((uint)key);
                        else
                            bw.Write((byte)key);
                    }
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                setSelectedExe(openFileDialog.FileName);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Back up file first. Don't back up if backup file already exists.
                string backupPath = makeBackupFilename(curPath);
                if (!File.Exists(backupPath))
                {
                    File.Copy(curPath, backupPath);
                    restoreButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(this, string.Format("Failed to make backup ({0}). Do you want to save anyway?", ex.Message), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No) return;
            }

            try
            {
                saveValuesToExe(curPath);
                MessageBox.Show(this, "Modifications saved.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error saving modifications. ({0})", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreButton_Click(object sender, EventArgs e)
        {
            string backupPath = makeBackupFilename(curPath);
            if (!File.Exists(backupPath))
            {
                MessageBox.Show(this, "Backup could not be found.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                File.Delete(curPath);
                File.Copy(backupPath, curPath);
                loadValuesFromExe(curPath);
                MessageBox.Show(this, "Backup restored.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format("Error restoring from backup. ({0})", ex.Message), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resListView_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
        {
            if (curPath == null || e.SubItem == 0) return;
            resListView.StartEditing(textEditor, e.Item, e.SubItem);
        }

        private void resListView_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
        {
            if (e.Cancel) return;

            ushort res;
            if (!ushort.TryParse(e.DisplayText, out res))
            {
                MessageBox.Show(this, string.Format("Please enter a number, and check that it is not negative or greater than {0}.", ushort.MaxValue), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }

        private void mapListView_SubItemClicked(object sender, ListViewEx.SubItemEventArgs e)
        {
            if (curPath == null || e.SubItem == 0) return;
            keyEditor.LastPressedKey = Keys.None;
            mapListView.StartEditing(keyEditor, e.Item, e.SubItem);
        }

        private void mapListView_SubItemEndEditing(object sender, ListViewEx.SubItemEndEditingEventArgs e)
        {
            if (e.Cancel) return;

            Keys key = keyEditor.LastPressedKey;
            if (key == Keys.None)
            {
                e.Cancel = true;
                return;
            }
            uint keyValue = (uint)key;
            uint mask = makeMaxBitMask((int)e.Item.SubItems[0].Tag);
            if ((keyValue & ~mask) != 0)
            {
                MessageBox.Show(this, string.Format("The key you have chosen is too wide to be assigned. Please make sure the key code value is less than or equal to 0x{0:X8}.", mask), Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }

            e.Item.Tag = key;
            e.DisplayText = key.ToString();
        }

        private void cpolLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.codeproject.com/info/cpol10.aspx");
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return keyEditor.Visible ? false : base.ProcessDialogKey(keyData);
        }
    }
}
