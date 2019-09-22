namespace PhotoEdit
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
            this.currentDirectoryTreeView = new System.Windows.Forms.TreeView();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locateOnDiskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectRootFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.currentDirectoryImagesView = new System.Windows.Forms.ListView();
            this.readImagesProgressBar = new System.Windows.Forms.ProgressBar();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentDirectoryTreeView
            // 
            this.currentDirectoryTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentDirectoryTreeView.Location = new System.Drawing.Point(3, 3);
            this.currentDirectoryTreeView.MinimumSize = new System.Drawing.Size(100, 100);
            this.currentDirectoryTreeView.Name = "currentDirectoryTreeView";
            this.currentDirectoryTreeView.Size = new System.Drawing.Size(324, 694);
            this.currentDirectoryTreeView.TabIndex = 0;
            this.currentDirectoryTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CurrentDirectoryTreeView_AfterSelect);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1016, 30);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateOnDiskToolStripMenuItem,
            this.selectRootFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // locateOnDiskToolStripMenuItem
            // 
            this.locateOnDiskToolStripMenuItem.Name = "locateOnDiskToolStripMenuItem";
            this.locateOnDiskToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+L";
            this.locateOnDiskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.locateOnDiskToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.locateOnDiskToolStripMenuItem.Text = "Locate On Disk";
            this.locateOnDiskToolStripMenuItem.Click += new System.EventHandler(this.LocateOnDiskToolStripMenuItem_Click);
            // 
            // selectRootFolderToolStripMenuItem
            // 
            this.selectRootFolderToolStripMenuItem.Name = "selectRootFolderToolStripMenuItem";
            this.selectRootFolderToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.selectRootFolderToolStripMenuItem.Text = "Select Root Folder";
            this.selectRootFolderToolStripMenuItem.Click += new System.EventHandler(this.SelectRootFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(237, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detailToolStripMenuItem,
            this.smallToolStripMenuItem,
            this.largeToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // detailToolStripMenuItem
            // 
            this.detailToolStripMenuItem.Name = "detailToolStripMenuItem";
            this.detailToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.detailToolStripMenuItem.Text = "Details";
            this.detailToolStripMenuItem.Click += new System.EventHandler(this.ViewSubMenuItem_Click);
            // 
            // smallToolStripMenuItem
            // 
            this.smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            this.smallToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.smallToolStripMenuItem.Text = "Small";
            this.smallToolStripMenuItem.Click += new System.EventHandler(this.ViewSubMenuItem_Click);
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.largeToolStripMenuItem.Text = "Large";
            this.largeToolStripMenuItem.Click += new System.EventHandler(this.ViewSubMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(133, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // currentDirectoryImagesView
            // 
            this.currentDirectoryImagesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentDirectoryImagesView.HideSelection = false;
            this.currentDirectoryImagesView.Location = new System.Drawing.Point(3, 3);
            this.currentDirectoryImagesView.MinimumSize = new System.Drawing.Size(100, 100);
            this.currentDirectoryImagesView.MultiSelect = false;
            this.currentDirectoryImagesView.Name = "currentDirectoryImagesView";
            this.currentDirectoryImagesView.Size = new System.Drawing.Size(652, 694);
            this.currentDirectoryImagesView.TabIndex = 2;
            this.currentDirectoryImagesView.UseCompatibleStateImageBehavior = false;
            this.currentDirectoryImagesView.ItemActivate += new System.EventHandler(this.CurrentDirectoryImagesView_ItemActivate);
            // 
            // readImagesProgressBar
            // 
            this.readImagesProgressBar.Location = new System.Drawing.Point(12, 39);
            this.readImagesProgressBar.Name = "readImagesProgressBar";
            this.readImagesProgressBar.Size = new System.Drawing.Size(992, 10);
            this.readImagesProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.readImagesProgressBar.TabIndex = 3;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 55);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.currentDirectoryTreeView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.currentDirectoryImagesView);
            this.splitContainer.Size = new System.Drawing.Size(992, 700);
            this.splitContainer.SplitterDistance = 330;
            this.splitContainer.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 767);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.readImagesProgressBar);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Photo Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView currentDirectoryTreeView;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ListView currentDirectoryImagesView;
        private System.Windows.Forms.ToolStripMenuItem locateOnDiskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectRootFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ProgressBar readImagesProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}

