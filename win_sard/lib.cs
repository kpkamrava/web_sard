using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace win_sard
{
   public class lib
    {

        public delegate bool onexitProcessGetCurrentProcess();

        public static void check_for_existing_instance(Process ProcessGetCurrentProcess, onexitProcessGetCurrentProcess onn)
        {
            if (System.Diagnostics.Process.GetProcessesByName(ProcessGetCurrentProcess.ProcessName).Length > 1)
            {


                if (onn.Invoke())
                { 
                    int idThis = Process.GetCurrentProcess().Id;
                    string nameThis = Process.GetCurrentProcess().ProcessName;

                    foreach (var r in Process.GetProcessesByName(nameThis).Where(a => a.Id != idThis))
                    {

                        r.Kill();

                    }
                }

                
            }
        }


        public static void RegisterInStartup(bool isChecked, string ApplicationExecutablePath)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("RAYACold", ApplicationExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("RAYACold");
            }
        }


    }
}
