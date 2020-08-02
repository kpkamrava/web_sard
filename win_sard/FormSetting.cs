using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static win_sard.ClassBascul;

namespace win_sard
{
    public partial class FormSetting : Form
    {
        public FormSetting()
        {
            InitializeComponent();
        }

       

        private void تنظیماتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormSetting().ShowDialog();
        }

        private void FormSetting_Load(object sender, EventArgs e)
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

            }
            catch
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                urlServer=textBox1.Text.Trim()
            };
            reg.set("config", Newtonsoft.Json.JsonConvert.SerializeObject(row));
             

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
