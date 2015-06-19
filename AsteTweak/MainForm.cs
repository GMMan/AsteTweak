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
        Offsets.KeyMapping curKeys;
        TextBox textEditor = new TextBox { Visible = false };
        KeyReaderControl keyEditor = new KeyReaderControl { Visible = false };

        public MainForm()
        {
            InitializeComponent();
            Controls.Add(textEditor);
            Controls.Add(keyEditor);

            verLabel.Text = string.Format(verLabel.Text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
            verLabel.Left = Size.Width - 28 - verLabel.Width;

            // Set restrictions on key mapping bit widths
            int[] narrowKeys = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 24, 27 };
            for (int i = 0; i < mapListView.Items.Count; ++i) mapListView.Items[i].SubItems[0].Tag = 32;
            foreach (int i in narrowKeys) mapListView.Items[i].SubItems[0].Tag = 7;

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
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
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
                    itm.SubItems[1].Text = br.ReadInt16().ToString();
                    itm.SubItems[2].Text = br.ReadInt16().ToString();
                }

                // Read mappings
                Keys key;

                fs.Seek(curKeys.Up, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[0].Tag = key;
                mapListView.Items[0].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Down, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[1].Tag = key;
                mapListView.Items[1].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Left, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[2].Tag = key;
                mapListView.Items[2].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Right, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[3].Tag = key;
                mapListView.Items[3].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Numpad8, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[4].Tag = key;
                mapListView.Items[4].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Numpad2, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[5].Tag = key;
                mapListView.Items[5].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Numpad4, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[6].Tag = key;
                mapListView.Items[6].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Numpad6, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[7].Tag = key;
                mapListView.Items[7].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Oem1, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[8].Tag = key;
                mapListView.Items[8].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Oem102, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[9].Tag = key;
                mapListView.Items[9].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Oem2, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[10].Tag = key;
                mapListView.Items[10].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Oem6, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[11].Tag = key;
                mapListView.Items[11].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.TableBegin, SeekOrigin.Begin);
                for (int i = 12; i < 24; ++i)
                {
                    key = (Keys)br.ReadUInt32();
                    mapListView.Items[i].Tag = key;
                    mapListView.Items[i].SubItems[1].Text = key.ToString();
                }

                fs.Seek(curKeys.Return, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[24].Tag = key;
                mapListView.Items[24].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.LShift, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[25].Tag = key;
                mapListView.Items[25].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.RShift, SeekOrigin.Begin);
                key = (Keys)br.ReadUInt32();
                mapListView.Items[26].Tag = key;
                mapListView.Items[26].SubItems[1].Text = key.ToString();

                fs.Seek(curKeys.Escape, SeekOrigin.Begin);
                key = (Keys)br.ReadByte();
                mapListView.Items[27].Tag = key;
                mapListView.Items[27].SubItems[1].Text = key.ToString();
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
                    bw.Write(ushort.Parse(itm.SubItems[1].Text));
                    bw.Write(ushort.Parse(itm.SubItems[2].Text));
                }

                // Write key mappings
                fs.Seek(curKeys.Up, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[0].Tag);

                fs.Seek(curKeys.Down, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[1].Tag);

                fs.Seek(curKeys.Left, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[2].Tag);

                fs.Seek(curKeys.Right, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[3].Tag);

                fs.Seek(curKeys.Numpad8, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[4].Tag);

                fs.Seek(curKeys.Numpad2, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[5].Tag);

                fs.Seek(curKeys.Numpad4, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[6].Tag);

                fs.Seek(curKeys.Numpad6, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[7].Tag);

                fs.Seek(curKeys.Oem1, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[8].Tag);

                fs.Seek(curKeys.Oem102, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[9].Tag);

                fs.Seek(curKeys.Oem2, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[10].Tag);

                fs.Seek(curKeys.Oem6, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[11].Tag);

                fs.Seek(curKeys.TableBegin, SeekOrigin.Begin);
                for (int i = 12; i < 24; ++i)
                {
                    bw.Write((uint)(Keys)mapListView.Items[i].Tag);
                }

                fs.Seek(curKeys.Return, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[24].Tag);

                fs.Seek(curKeys.LShift, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[25].Tag);

                fs.Seek(curKeys.RShift, SeekOrigin.Begin);
                bw.Write((uint)(Keys)mapListView.Items[26].Tag);

                fs.Seek(curKeys.Escape, SeekOrigin.Begin);
                bw.Write((byte)(Keys)mapListView.Items[27].Tag);

                fs.Flush();
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
