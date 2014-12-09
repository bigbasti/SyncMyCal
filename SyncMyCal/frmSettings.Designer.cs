namespace SyncMyCal
{
    partial class frmSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.ntiSystemTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuSystemTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jetztSynchronisierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.beendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lstSyncSettings = new System.Windows.Forms.ListBox();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.cmdEdit = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.chkStartOnBoot = new System.Windows.Forms.CheckBox();
            this.mnuSystemTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // ntiSystemTray
            // 
            this.ntiSystemTray.ContextMenuStrip = this.mnuSystemTray;
            this.ntiSystemTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntiSystemTray.Icon")));
            this.ntiSystemTray.Text = "SyncMyCal";
            this.ntiSystemTray.Visible = true;
            // 
            // mnuSystemTray
            // 
            this.mnuSystemTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.jetztSynchronisierenToolStripMenuItem,
            this.toolStripMenuItem1,
            this.beendenToolStripMenuItem});
            this.mnuSystemTray.Name = "mnuSystemTray";
            this.mnuSystemTray.Size = new System.Drawing.Size(128, 76);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.einstellungenToolStripMenuItem.Text = "&Settings";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // jetztSynchronisierenToolStripMenuItem
            // 
            this.jetztSynchronisierenToolStripMenuItem.Name = "jetztSynchronisierenToolStripMenuItem";
            this.jetztSynchronisierenToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.jetztSynchronisierenToolStripMenuItem.Text = "Sync &Now";
            this.jetztSynchronisierenToolStripMenuItem.Click += new System.EventHandler(this.jetztSynchronisierenToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // beendenToolStripMenuItem
            // 
            this.beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            this.beendenToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.beendenToolStripMenuItem.Text = "&Exit";
            this.beendenToolStripMenuItem.Click += new System.EventHandler(this.beendenToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Configured syncs:";
            // 
            // lstSyncSettings
            // 
            this.lstSyncSettings.FormattingEnabled = true;
            this.lstSyncSettings.Location = new System.Drawing.Point(15, 34);
            this.lstSyncSettings.Name = "lstSyncSettings";
            this.lstSyncSettings.Size = new System.Drawing.Size(346, 95);
            this.lstSyncSettings.TabIndex = 2;
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(15, 160);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(106, 23);
            this.cmdAdd.TabIndex = 3;
            this.cmdAdd.Text = "&Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // cmdEdit
            // 
            this.cmdEdit.Location = new System.Drawing.Point(127, 160);
            this.cmdEdit.Name = "cmdEdit";
            this.cmdEdit.Size = new System.Drawing.Size(106, 23);
            this.cmdEdit.TabIndex = 4;
            this.cmdEdit.Text = "&Edit";
            this.cmdEdit.UseVisualStyleBackColor = true;
            this.cmdEdit.Click += new System.EventHandler(this.cmdEdit_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Location = new System.Drawing.Point(239, 160);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(106, 23);
            this.cmdRemove.TabIndex = 5;
            this.cmdRemove.Text = "&Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // chkStartOnBoot
            // 
            this.chkStartOnBoot.AutoSize = true;
            this.chkStartOnBoot.Location = new System.Drawing.Point(15, 135);
            this.chkStartOnBoot.Name = "chkStartOnBoot";
            this.chkStartOnBoot.Size = new System.Drawing.Size(207, 17);
            this.chkStartOnBoot.TabIndex = 6;
            this.chkStartOnBoot.Text = "Start SyncMyCal after windows startup";
            this.chkStartOnBoot.UseVisualStyleBackColor = true;
            this.chkStartOnBoot.CheckedChanged += new System.EventHandler(this.chkStartOnBoot_CheckedChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 195);
            this.Controls.Add(this.chkStartOnBoot);
            this.Controls.Add(this.cmdRemove);
            this.Controls.Add(this.cmdEdit);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.lstSyncSettings);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.Shown += new System.EventHandler(this.frmSettings_Shown);
            this.mnuSystemTray.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon ntiSystemTray;
        private System.Windows.Forms.ContextMenuStrip mnuSystemTray;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jetztSynchronisierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem beendenToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstSyncSettings;
        private System.Windows.Forms.Button cmdAdd;
        private System.Windows.Forms.Button cmdEdit;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.CheckBox chkStartOnBoot;
    }
}