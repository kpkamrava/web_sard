using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace win_sard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static System.Windows.Forms.NotifyIcon notifyIcon1;
        private static System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private static System.Windows.Forms.ToolStripMenuItem تنظیماتToolStripMenuItem;

      




        [STAThread]
        static void Main()
        {
             
            lib.check_for_existing_instance(Process.GetCurrentProcess(), () => { MessageBox.Show("برنامه قبلی بسته و دوباره اجرا شد"); return true; });
             

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormTest());

        }


    }
}
