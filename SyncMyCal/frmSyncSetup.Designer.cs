namespace SyncMyCal
{
    partial class FrmSyncSetup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSyncSetup));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numSyncMinutes = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numDaysFuture = new System.Windows.Forms.NumericUpDown();
            this.numDaysPast = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDestinationCalendar = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmdConnectDestination = new System.Windows.Forms.Button();
            this.cboDestinationCalandarId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cboSourceCalendar = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdConnectSource = new System.Windows.Forms.Button();
            this.cboSourceCalendarId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSyncMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaysFuture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaysPast)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numSyncMinutes);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numDaysFuture);
            this.groupBox2.Controls.Add(this.numDaysPast);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 105);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Syncronisation timespan";
            // 
            // numSyncMinutes
            // 
            this.numSyncMinutes.Location = new System.Drawing.Point(218, 75);
            this.numSyncMinutes.Name = "numSyncMinutes";
            this.numSyncMinutes.Size = new System.Drawing.Size(120, 20);
            this.numSyncMinutes.TabIndex = 5;
            this.numSyncMinutes.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Sync every ... minutes";
            // 
            // numDaysFuture
            // 
            this.numDaysFuture.Location = new System.Drawing.Point(218, 49);
            this.numDaysFuture.Name = "numDaysFuture";
            this.numDaysFuture.Size = new System.Drawing.Size(120, 20);
            this.numDaysFuture.TabIndex = 3;
            this.numDaysFuture.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numDaysPast
            // 
            this.numDaysPast.Location = new System.Drawing.Point(218, 23);
            this.numDaysPast.Name = "numDaysPast";
            this.numDaysPast.Size = new System.Drawing.Size(120, 20);
            this.numDaysPast.TabIndex = 2;
            this.numDaysPast.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sync days in the past:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sync days in the future";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDestinationCalendar);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmdConnectDestination);
            this.groupBox1.Controls.Add(this.cboDestinationCalandarId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 102);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Destination calendar";
            // 
            // cboDestinationCalendar
            // 
            this.cboDestinationCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestinationCalendar.Enabled = false;
            this.cboDestinationCalendar.FormattingEnabled = true;
            this.cboDestinationCalendar.Items.AddRange(new object[] {
            "Google"});
            this.cboDestinationCalendar.Location = new System.Drawing.Point(146, 19);
            this.cboDestinationCalendar.Name = "cboDestinationCalendar";
            this.cboDestinationCalendar.Size = new System.Drawing.Size(192, 21);
            this.cboDestinationCalendar.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Select calendar provider";
            // 
            // cmdConnectDestination
            // 
            this.cmdConnectDestination.Location = new System.Drawing.Point(146, 72);
            this.cmdConnectDestination.Name = "cmdConnectDestination";
            this.cmdConnectDestination.Size = new System.Drawing.Size(192, 23);
            this.cmdConnectDestination.TabIndex = 4;
            this.cmdConnectDestination.Text = "Connect to Provider";
            this.cmdConnectDestination.UseVisualStyleBackColor = true;
            this.cmdConnectDestination.Click += new System.EventHandler(this.cmdConnectDestination_Click);
            // 
            // cboDestinationCalandarId
            // 
            this.cboDestinationCalandarId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDestinationCalandarId.FormattingEnabled = true;
            this.cboDestinationCalandarId.Location = new System.Drawing.Point(146, 45);
            this.cboDestinationCalandarId.Name = "cboDestinationCalandarId";
            this.cboDestinationCalandarId.Size = new System.Drawing.Size(192, 21);
            this.cboDestinationCalandarId.TabIndex = 2;
            this.cboDestinationCalandarId.SelectedIndexChanged += new System.EventHandler(this.cboDestinationCalandarId_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select calendar to sync";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Right now only Outlook->Google is supported";
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(285, 349);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 8;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(204, 349);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboSourceCalendar);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cmdConnectSource);
            this.groupBox3.Controls.Add(this.cboSourceCalendarId);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(11, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 96);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Source calendar";
            // 
            // cboSourceCalendar
            // 
            this.cboSourceCalendar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceCalendar.Enabled = false;
            this.cboSourceCalendar.FormattingEnabled = true;
            this.cboSourceCalendar.Items.AddRange(new object[] {
            "Outlook"});
            this.cboSourceCalendar.Location = new System.Drawing.Point(147, 13);
            this.cboSourceCalendar.Name = "cboSourceCalendar";
            this.cboSourceCalendar.Size = new System.Drawing.Size(192, 21);
            this.cboSourceCalendar.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(122, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Select calendar provider";
            // 
            // cmdConnectSource
            // 
            this.cmdConnectSource.Location = new System.Drawing.Point(147, 65);
            this.cmdConnectSource.Name = "cmdConnectSource";
            this.cmdConnectSource.Size = new System.Drawing.Size(192, 23);
            this.cmdConnectSource.TabIndex = 4;
            this.cmdConnectSource.Text = "Connect to Provider";
            this.cmdConnectSource.UseVisualStyleBackColor = true;
            this.cmdConnectSource.Click += new System.EventHandler(this.cmdConnectSource_Click);
            // 
            // cboSourceCalendarId
            // 
            this.cboSourceCalendarId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSourceCalendarId.FormattingEnabled = true;
            this.cboSourceCalendarId.Location = new System.Drawing.Point(147, 38);
            this.cboSourceCalendarId.Name = "cboSourceCalendarId";
            this.cboSourceCalendarId.Size = new System.Drawing.Size(192, 21);
            this.cboSourceCalendarId.TabIndex = 2;
            this.cboSourceCalendarId.SelectedIndexChanged += new System.EventHandler(this.cboSourceCalendarId_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select calendar to sync";
            // 
            // frmSyncSetup
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(371, 381);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSyncSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup Sync";
            this.Load += new System.EventHandler(this.frmSyncSetup_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSyncMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaysFuture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDaysPast)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numSyncMinutes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDaysFuture;
        private System.Windows.Forms.NumericUpDown numDaysPast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cmdConnectDestination;
        private System.Windows.Forms.ComboBox cboDestinationCalandarId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdConnectSource;
        private System.Windows.Forms.ComboBox cboSourceCalendarId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboDestinationCalendar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboSourceCalendar;
        private System.Windows.Forms.Label label7;
    }
}