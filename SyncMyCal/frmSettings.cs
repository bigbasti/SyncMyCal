using SyncMyCal.Calendars;
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
    public partial class frmSettings : Form
    {
        private SyncManager syncManager = new SyncManager();
        List<Timer> syncTimer = new List<Timer>();

        public frmSettings()
        {
            InitializeComponent();
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            this.chkStartOnBoot.Checked = Settings.Default.StartOnWindowsLogon;
            this.chkAlertAfterSync.Checked = Settings.Default.ShowAlertfAfterSync;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - (this.Width + 10), Screen.PrimaryScreen.WorkingArea.Height - (this.Height + 10));

            foreach (SyncSetting setting in syncManager.calendarsToSync)
            {
                lstSyncSettings.Items.Add(setting.ToString());
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
            Environment.Exit(0);
        }

        private void jetztSynchronisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            syncManager.syncAllCalendars();
        }

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            if (syncManager.calendarsToSync.Count > 0)
            {
                this.Hide();
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            cmdAdd.Text = "Please wait...";
            frmSyncSetup newSetup = new frmSyncSetup();
            DialogResult result = newSetup.ShowDialog();

            if (result == DialogResult.OK)
            {
                //add only when the user clicked OK
                syncManager.calendarsToSync.Add(newSetup.newSetting);
                lstSyncSettings.Items.Add(newSetup.newSetting.ToString());
                syncManager.saveSyncSettings();
                refreshTimers();
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

                for (var index = 0; index < syncManager.calendarsToSync.Count; index++)
                {
                    if (lstSyncSettings.Text.Equals(syncManager.calendarsToSync[index].ToString()))
                    {
                        settingToChange = syncManager.calendarsToSync[index];
                        managerIndex = index;
                        break;
                    }
                }

                frmSyncSetup newSetup = new frmSyncSetup(settingToChange);
                DialogResult result = newSetup.ShowDialog();

                if (result == DialogResult.OK)
                {
                    //add only when the user clicked OK
                    syncManager.calendarsToSync[managerIndex] = newSetup.newSetting;
                    lstSyncSettings.Items[lstSyncSettings.SelectedIndex] = newSetup.newSetting.ToString();
                    syncManager.saveSyncSettings();
                    refreshTimers();
                }
            }
            cmdEdit.Text = "&Edit";
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            if (lstSyncSettings.Text.Length > 0)
            {
                for (var index = 0; index < syncManager.calendarsToSync.Count; index++)
                {
                    if (lstSyncSettings.Text.ToString().Equals(syncManager.calendarsToSync[index].ToString()))
                    {
                        syncManager.calendarsToSync.Remove(syncManager.calendarsToSync[index]);
                        lstSyncSettings.Items.RemoveAt(lstSyncSettings.SelectedIndex);
                        syncManager.saveSyncSettings();
                        refreshTimers();
                        break;
                    }
                }
            }
        }

        private void refreshTimers()
        {
            syncTimer = new List<Timer>();
            foreach (SyncSetting setting in syncManager.calendarsToSync)
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
                        success = syncManager.syncCalendar(setting);
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
    }
}
