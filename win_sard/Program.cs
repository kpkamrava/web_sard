using System;
using System.Collections.Generic;
using System.Linq;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


             

             Application.Run(new myNotifity());
        }

      
    }
}
