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
    public partial class FrmSyncSetup : Form
    {
        public SyncSetting NewSetting { get; set; }

        public FrmSyncSetup()
        {
            InitializeComponent();

            NewSetting = new SyncSetting();
        }

        public FrmSyncSetup(SyncSetting setting)
        {
            InitializeComponent();
            this.NewSetting = setting;

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
            NewSetting.Source = source;

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
            NewSetting.Destination = destination;

            cboDestinationCalandarId.Items.Clear();
            foreach (CalendarId cal in destination.getCalendars())
            {
                cboDestinationCalandarId.Items.Add(cal.DsplayName);
            }
        }

        private void cboSourceCalendarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CalendarId id in NewSetting.Source.getCalendars())
            {
                if (id.DsplayName.Equals(cboSourceCalendarId.Text))
                {
                    NewSetting.SourceCalendar = id;
                    continue;
                }
            }
        }

        private void cboDestinationCalandarId_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CalendarId id in NewSetting.Destination.getCalendars())
            {
                if (id.DsplayName.Equals(cboDestinationCalandarId.Text))
                {
                    NewSetting.DestinationCalendar = id;
                    continue;
                }
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            NewSetting.MinutesBetweenSync = Convert.ToInt32(this.numSyncMinutes.Value);
            NewSetting.DaysIntoFuture = Convert.ToInt32(numDaysFuture.Value);
            NewSetting.DaysIntoPast = Convert.ToInt32(numDaysPast.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
