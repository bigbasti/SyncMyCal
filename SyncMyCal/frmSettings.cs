﻿using SyncMyCal.Calendars;
using SyncMyCal.Properties;
using SyncMyCal.Sync;
using SyncMyCal.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncMyCal
{
    public partial class FrmSettings : Form
    {
        private DateTime nextSync = DateTime.Now;
        private SyncManager syncManager = new SyncManager();
        List<Timer> syncTimer = new List<Timer>();

        public FrmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.chkStartOnBoot.Checked = Settings.Default.StartOnWindowsLogon;
            this.chkAlertAfterSync.Checked = Settings.Default.ShowAlertfAfterSync;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - (this.Width + 10), Screen.PrimaryScreen.WorkingArea.Height - (this.Height + 10));

            foreach (SyncSetting setting in syncManager._calendarsToSync)
            {
                lstSyncSettings.Items.Add(setting.ToString());
                nextSync = DateTime.Now.AddMinutes(setting.MinutesBetweenSync);
                this.ntiSystemTray.Text = string.Format("SyncMyCal - Next sync is in {0} minutes", (nextSync - DateTime.Now).Minutes);
            }
        }

        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ntiSystemTray.Dispose();
            Environment.Exit(0);
        }

        private void jetztSynchronisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syncManager.SyncAllCalendars();
        }

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            if (syncManager._calendarsToSync.Count > 0)
            {
                this.Hide();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            cmdAdd.Text = "Please wait...";
            FrmSyncSetup newSetup = new FrmSyncSetup();
            DialogResult result = newSetup.ShowDialog();

            if (result == DialogResult.OK)
            {
                //add only when the user clicked OK
                if (newSetup.NewSetting.ToString() != null)
                {
                    syncManager._calendarsToSync.Add(newSetup.NewSetting);
                    lstSyncSettings.Items.Add(newSetup.NewSetting.ToString());
                    syncManager.SaveSyncSettings();
                    RefreshTimers();
                }
            }
            cmdAdd.Text = "&Add";
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            //TODO: Refactor objektvergleich muss her, da sonst alle syncs die die gleiche quelle und senke haben gleich sind
            if (lstSyncSettings.Text.Length > 0)
            {
                cmdEdit.Text = "waiting...";
                //something was selected
                SyncSetting settingToChange = new SyncSetting();
                int managerIndex = 0;

                for (var index = 0; index < syncManager._calendarsToSync.Count; index++)
                {
                    if (lstSyncSettings.Text.Equals(syncManager._calendarsToSync[index].ToString()))
                    {
                        settingToChange = syncManager._calendarsToSync[index];
                        managerIndex = index;
                        break;
                    }
                }

                FrmSyncSetup newSetup = new FrmSyncSetup(settingToChange);
                DialogResult result = newSetup.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //add only when the user clicked OK
                    syncManager._calendarsToSync[managerIndex] = newSetup.NewSetting;
                    lstSyncSettings.Items[lstSyncSettings.SelectedIndex] = newSetup.NewSetting.ToString();
                    syncManager.SaveSyncSettings();
                    RefreshTimers();
                }
            }
            cmdEdit.Text = "&Edit";
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            if (lstSyncSettings.Text.Length <= 0) return;
            for (var index = 0; index < syncManager._calendarsToSync.Count; index++)
            {
                if (lstSyncSettings.Text.ToString().Equals(syncManager._calendarsToSync[index].ToString()))
                {
                    syncManager._calendarsToSync.Remove(syncManager._calendarsToSync[index]);
                    lstSyncSettings.Items.RemoveAt(lstSyncSettings.SelectedIndex);
                    syncManager.SaveSyncSettings();
                    RefreshTimers();
                    break;
                }
            }
        }

        private void RefreshTimers()
        {
            syncTimer = new List<Timer>();
            foreach (SyncSetting setting in syncManager._calendarsToSync)
            {
                Timer t = new Timer()
                {
                    Enabled = true,
                    Interval = setting.MinutesBetweenSync * 60 * 1000
                };
                t.Tick += (sender, e) =>
                {
                    bool success = false;
                    string errorMessage = "krasser fehler!";
                    try
                    {
                        success = syncManager.SyncCalendar(setting);
                    }catch(Exception ex){
                        errorMessage = ex.Message;
                    }

                    if (Settings.Default.ShowAlertfAfterSync)
                    {
                        if (success)
                        {
                            this.ntiSystemTray.ShowBalloonTip(1000, "Sync finished successful", "Just synced changes from " + setting.SourceCalendar.DsplayName + " to " + setting.DestinationCalendar.DsplayName + 
                                                                    Environment.NewLine + "Next sync in " + setting.MinutesBetweenSync + " minutes", ToolTipIcon.Info);
                        }
                        else
                        {
                            this.ntiSystemTray.ShowBalloonTip(1000, "Sync failed", "Could not sync " + setting.ToString() + 
                                                                    Environment.NewLine + "Because: " + errorMessage, ToolTipIcon.Error);
                        }
                        
                    }
                    if (nextSync.AddMinutes(setting.MinutesBetweenSync) < DateTime.Now)
                    {
                        nextSync = DateTime.Now.AddMinutes(setting.MinutesBetweenSync);
                        this.ntiSystemTray.Text = string.Format("Next sync is in {0} minutes", (nextSync - DateTime.Now).Minutes);
                    }
                };
                syncTimer.Add(t);
                t.Start();
            }
        }

        private void chkStartOnBoot_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.StartOnWindowsLogon = chkStartOnBoot.Checked;
            RegistryTools.RegisterInStartup(chkStartOnBoot.Checked);
            Settings.Default.Save();
        }

        private void chkAlertAfterSync_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ShowAlertfAfterSync = chkAlertAfterSync.Checked;
            Settings.Default.Save();
        }

        private void lstSyncSettings_DoubleClick(object sender, EventArgs e)
        {
            cmdEdit_Click(this, e);
        }

        private void ntiSystemTray_DoubleClick(object sender, EventArgs e)
        {
            einstellungenToolStripMenuItem_Click(this, e);
        }
    }
}
