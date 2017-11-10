namespace AsteTweak
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "0 (320x240)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "1 (640x480)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "2 (800x600)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "3 (1024x768)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "4 (1280x960)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
            "5 (1600x1200)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
            "6 (640x360)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
            "7 (848x480)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
            "8 (1280x720)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
            "9 (1600x900)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
            "10 (1920x1080)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
            "11 (2560x1440)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
            "12 (3840x2160)",
            "",
            ""}, -1);
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
            "13 (7680x4320)",
            "",
            ""}, -1);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resListView = new ListViewEx.ListViewEx();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mapListView = new ListViewEx.ListViewEx();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.exePathLabel = new System.Windows.Forms.Label();
            this.loadButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.restoreButton = new System.Windows.Forms.Button();
            this.verLabel = new System.Windows.Forms.Label();
            this.cpolLabel = new System.Windows.Forms.LinkLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resListView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(318, 355);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Screen Resolution";
            // 
            // resListView
            // 
            this.resListView.AllowColumnReorder = true;
            this.resListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.resListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resListView.DoubleClickActivation = false;
            this.resListView.FullRowSelect = true;
            this.resListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14});
            this.resListView.Location = new System.Drawing.Point(4, 19);
            this.resListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.resListView.Name = "resListView";
            this.resListView.Size = new System.Drawing.Size(310, 332);
            this.resListView.TabIndex = 0;
            this.resListView.UseCompatibleStateImageBehavior = false;
            this.resListView.View = System.Windows.Forms.View.Details;
            this.resListView.SubItemClicked += new ListViewEx.SubItemEventHandler(this.resListView_SubItemClicked);
            this.resListView.SubItemEndEditing += new ListViewEx.SubItemEndEditingEventHandler(this.resListView_SubItemEndEditing);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index (original)";
            this.columnHeader1.Width = 84;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Horizontal";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Vertical";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mapListView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(330, 4);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(319, 355);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Key Mapping";
            // 
            // mapListView
            // 
            this.mapListView.AllowColumnReorder = true;
            this.mapListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5});
            this.mapListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapListView.DoubleClickActivation = false;
            this.mapListView.FullRowSelect = true;
            this.mapListView.Location = new System.Drawing.Point(4, 19);
            this.mapListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapListView.Name = "mapListView";
            this.mapListView.Size = new System.Drawing.Size(311, 332);
            this.mapListView.TabIndex = 0;
            this.mapListView.UseCompatibleStateImageBehavior = false;
            this.mapListView.View = System.Windows.Forms.View.Details;
            this.mapListView.SubItemClicked += new ListViewEx.SubItemEventHandler(this.mapListView_SubItemClicked);
            this.mapListView.SubItemEndEditing += new ListViewEx.SubItemEndEditingEventHandler(this.mapListView_SubItemEndEditing);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name (original)";
            this.columnHeader4.Width = 84;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Assignment";
            this.columnHeader5.Width = 100;
            // 
            // exePathLabel
            // 
            this.exePathLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exePathLabel.AutoEllipsis = true;
            this.exePathLabel.Location = new System.Drawing.Point(16, 21);
            this.exePathLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.exePathLabel.Name = "exePathLabel";
            this.exePathLabel.Size = new System.Drawing.Size(545, 16);
            this.exePathLabel.TabIndex = 0;
            this.exePathLabel.Text = "Astebreed.exe not loaded.";
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(569, 15);
            this.loadButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(100, 28);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Select EXE…";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 50);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(653, 363);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(16, 421);
            this.saveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(100, 28);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // restoreButton
            // 
            this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restoreButton.Enabled = false;
            this.restoreButton.Location = new System.Drawing.Point(124, 421);
            this.restoreButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(100, 28);
            this.restoreButton.TabIndex = 4;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
            // 
            // verLabel
            // 
            this.verLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.verLabel.AutoSize = true;
            this.verLabel.Location = new System.Drawing.Point(527, 421);
            this.verLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.verLabel.Name = "verLabel";
            this.verLabel.Size = new System.Drawing.Size(141, 17);
            this.verLabel.TabIndex = 5;
            this.verLabel.Text = "Version {0} by cyanic";
            this.verLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cpolLabel
            // 
            this.cpolLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cpolLabel.AutoSize = true;
            this.cpolLabel.Location = new System.Drawing.Point(368, 437);
            this.cpolLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cpolLabel.Name = "cpolLabel";
            this.cpolLabel.Size = new System.Drawing.Size(303, 17);
            this.cpolLabel.TabIndex = 6;
            this.cpolLabel.TabStop = true;
            this.cpolLabel.Text = "This program uses code licensed under CPOL.";
            this.cpolLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cpolLabel_LinkClicked);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Executables|*.exe";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 464);
            this.Controls.Add(this.cpolLabel);
            this.Controls.Add(this.verLabel);
            this.Controls.Add(this.restoreButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.exePathLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(701, 328);
            this.Name = "MainForm";
            this.Text = "Astebreed Tweaker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private ListViewEx.ListViewEx resListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private ListViewEx.ListViewEx mapListView;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label exePathLabel;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Label verLabel;
        private System.Windows.Forms.LinkLabel cpolLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}

