using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyncMyCal.Tools
{
    class RegistryTools
    {
        public static void RegisterInStartup(bool autostart)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (autostart)
            {
                registryKey.SetValue("SyncMyCal", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("SyncMyCal");
            }
        }
    }
}
