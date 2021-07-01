using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static win_sard.ClassBascul;

namespace win_sard
{
    public partial class FormTest : Form
    { 
       
        public FormTest()
        {
            InitializeComponent();
        }
        ClassService service;


        private void button1_Click(object sender, EventArgs e)
        {
             service.sendVazn(50000);
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

            try
            {
                var row = Newtonsoft.Json.JsonConvert.DeserializeObject<MS_Tavzin.portrow>(reg.get("config"));
                if (row == null)
                {
                    return;
                }
                cmbKindBaskul.SelectedIndex = row.kindBaskul - 1;
                cmbPortCom.SelectedIndex = row.portCom;
                cmbSpeed.SelectedIndex = row.speed;
                cmbBitData.SelectedIndex = row.bitData;
                cmbStopData.SelectedIndex = row.stopData;
                cmbPariti.SelectedIndex = row.parity;
                mp_TextBox3.Text = row.cont_addSahih.ToString();
                mp_TextBox4.Text = row.cont_addAhshari.ToString();
                textBox1.Text = row.urlServer;
                textBox2.Text = row.urlUser;
                textBox3.Text = row.urlPass;

            }
            catch
            {


            }

              

         
        }

        private void FormTest_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void FormTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lib.RegisterInStartup(true, Application.ExecutablePath);
            var row = new MS_Tavzin.portrow
            {
                kindBaskul = cmbKindBaskul.SelectedIndex + 1,
                portCom = cmbPortCom.SelectedIndex,
                speed = cmbSpeed.SelectedIndex,
                bitData = cmbBitData.SelectedIndex,
                stopData = cmbStopData.SelectedIndex,
                parity = cmbPariti.SelectedIndex,
                cont_addSahih = Convert.ToInt32(mp_TextBox3.Text),
                cont_addAhshari = Convert.ToInt32(mp_TextBox4.Text),
                urlServer = textBox1.Text.Trim(),
                urlUser = textBox2.Text.Trim(),
                urlPass = textBox3.Text.Trim(),

            };
            reg.set("config", Newtonsoft.Json.JsonConvert.SerializeObject(row));

            groupBox1.Visible = false;

             
            service = new ClassService();
          
           

            service.StrInterfacedelegate += (str) =>
            {

                richTextBox1.Text = DateTime.Now + "   " + str + System.Environment.NewLine + richTextBox1.Text;

                if (richTextBox1.Text.Length>1500)
                {
                   richTextBox1.Text = richTextBox1.Text.Substring(0, 1500);
                }
            


            };
            service.STRVaznInterfacedelegate += (decimal? vazn) =>
            {
                label1.Text = "وزن :" + vazn.ToString();
                  
            };
            service.startbascul();
              

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var z=MessageBox.Show("آیا میخواهید سرویس باسکول بسته شود؟","خروج",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (z == DialogResult.Yes)
            {
                lib.RegisterInStartup(false, Application.ExecutablePath);
                string nameThis = Process.GetCurrentProcess().ProcessName;

                foreach (var r in Process.GetProcessesByName(nameThis))
                {

                    r.Kill();

                }
            }
         
        }
    }
}
