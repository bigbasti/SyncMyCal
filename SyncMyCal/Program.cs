using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncMyCal
{
    //TODO: Set different icon when sync in progress
    //TODO: tooltip text shows when the next sync will start
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Check if SyncMyCal is already running -> if so, exit
            if (Process.GetProcessesByName(Application.ProductName).Length > 1)
            {
                Environment.Exit(1);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmSettings());
        }
    }
}
