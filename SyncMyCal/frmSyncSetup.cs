using SyncMyCal.Calendars;
using SyncMyCal.Data;
using SyncMyCal.Sync;
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
    public partial class frmSyncSetup : Form
    {
        public SyncSetting newSetting { get; set; }

        public frmSyncSetup()
        {
            InitializeComponent();

            newSetting = new SyncSetting();
        }

        public frmSyncSetup(SyncSetting setting)
        {
            InitializeComponent();
            this.newSetting = setting;

            //Load settings from provided object
            this.numDaysFuture.Value = setting.DaysIntoFuture;
            this.numDaysPast.Value = setting.DaysIntoPast;
            this.numSyncMinutes.Value = setting.MinutesBetweenSync;

            foreach (CalendarId id in setting.Source.getCalendars())
            {
                this.cboSourceCalendarId.Items.Add(id.DsplayName);
            }
            for (int i = 0; i < cboSourceCalendarId.Items.Count; i++)
            {
                if (cboSourceCalendarId.Items[i].ToString().Equals(setting.SourceCalendar.DsplayName))
                {
                    cboSourceCalendarId.SelectedIndex = i;
                    continue;
                }
            }

            foreach (CalendarId id in setting.Destination.getCalendars())
            {
                this.cboDestinationCalandarId.Items.Add(id.DsplayName);
            }
            for (int i = 0; i < cboDestinationCalandarId.Items.Count; i++)
            {
                if (cboDestinationCalandarId.Items[i].ToString().Equals(setting.DestinationCalendar.DsplayName))
                {
                    cboDestinationCalandarId.SelectedIndex = i;
                    continue;
                }
            }

            //TODO: dynamisch machen wenn auch andere syncs als outlook->google unterstützt werden
            this.cboSourceCalendar.SelectedIndex = 0;
            this.cboDestinationCalendar.SelectedIndex = 0;
        }

        private void frmSyncSetup_Load(object sender, EventArgs e)
        {
            this.cboSourceCalendar.SelectedIndex = 0;
            this.cboDestinationCalendar.SelectedIndex = 0;
        }

        private void cmdConnectSource_Click(object sender, EventArgs e)
        {
            if (cboSourceCalendar.Text.Length < 1)
            {
                MessageBox.Show("Please select the provider first", "Select provider", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ICalendar source = null;
            if (cboSourceCalendar.Text.Equals("Google"))
            {
                source = new GoogleCalendar();

            }
            else if (cboSourceCalendar.Text.Equals("Outlook"))
            {
                source = new OutlookCalendar();
            }
            newSetting.Source = source;

            cboSourceCalendarId.Items.Clear();
            foreach (CalendarId cal in source.getCalendars())
            {
                cboSourceCalendarId.Items.Add(cal.DsplayName);
            }
        }

        private void cmdConnectDestination_Click(object sender, EventArgs e)
        {
            if (cboDestinationCalendar.Text.Length < 1)
            {
                MessageBox.Show("Please select the provider first", "Select provider", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ICalendar destination = null;
            if (cboDestinationCalendar.Text.Equals("Google"))
            {
                destination = new GoogleCalendar();

            }
            else if (cboDestinationCalendar.Text.Equals("Outlook"))
            {
                destination = new OutlookCalendar();
            }
            newSetting.Destination = destination;

            cboDestinationCalandarId.Items.Clear();
            foreach (CalendarId cal in destination.getCalendars())
            {
                cboDestinationCalandarId.Items.Add(cal.DsplayName);
            }
        }

        private void cboSourceCalendarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CalendarId id in newSetting.Source.getCalendars())
            {
                if (id.DsplayName.Equals(cboSourceCalendarId.Text))
                {
                    newSetting.SourceCalendar = id;
                    continue;
                }
            }
        }

        private void cboDestinationCalandarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CalendarId id in newSetting.Destination.getCalendars())
            {
                if (id.DsplayName.Equals(cboDestinationCalandarId.Text))
                {
                    newSetting.DestinationCalendar = id;
                    continue;
                }
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            newSetting.MinutesBetweenSync = Convert.ToInt32(this.numSyncMinutes.Value);
            newSetting.DaysIntoFuture = Convert.ToInt32(numDaysFuture.Value);
            newSetting.DaysIntoPast = Convert.ToInt32(numDaysPast.Value);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
