using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace win_sard
{
  
    class myNotifity : ApplicationContext
    {
        //Component declarations
        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        private ToolStripMenuItem SettingMenuItem;
        public static  ClassService service;
        public myNotifity()
        {
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
            InitializeComponent();
            TrayIcon.Visible = true;


            service = new ClassService();
            service.StrInterfacedelegate = (str) => {
                TrayIcon.BalloonTipTitle = "وضعیت "+ DateTime.Now.ToLocalTime();
                TrayIcon.BalloonTipText = str;
                TrayIcon.ShowBalloonTip(2000); 
            };
            service.startbascul();
        }

        private void InitializeComponent()
        {
            TrayIcon = new NotifyIcon();

            TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
            TrayIcon.BalloonTipText =
              "I noticed that you double-clicked me! What can I do for you?";
            TrayIcon.BalloonTipTitle = "You called Master?";
            TrayIcon.Text = "My fabulous tray icon demo application";


            //The icon is added to the project resources.
            //Here I assume that the name of the file is 'TrayIcon.ico'
            TrayIcon.Icon = Properties.Resources._5491974_Lhk_icon;

            //Optional - handle doubleclicks on the icon:
            TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            //Optional - Add a context menu to the TrayIcon:
            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            SettingMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();

            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
            this.CloseMenuItem,this.SettingMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(153, 70);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem1";
            this.CloseMenuItem.Size = new Size(152, 22);
            this.CloseMenuItem.Text = "خروج";
            this.CloseMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);
            // 
            // SettingMenuItem
            // 
            this.SettingMenuItem.Name = "SettingMenuItem1";
            this.SettingMenuItem.Size = new Size(152, 22);
            this.SettingMenuItem.Text = "تنظیمات";
            this.SettingMenuItem.Click += new EventHandler(this.SettingMenuItem_Click);

            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            //Cleanup so that the icon will be removed when the application is closed
            TrayIcon.Visible = false;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            //Here you can do stuff if the tray icon is doubleclicked
            TrayIcon.ShowBalloonTip(10000);
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("شما میخواهید برنامه را ببندید؟",
                    "آیا مطمئن هستید؟", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void SettingMenuItem_Click(object sender, EventArgs e)
        {
            new FormSetting().ShowDialog();
            service.startbascul();
        }
    }
}
