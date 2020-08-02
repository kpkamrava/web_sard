using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace win_sard
{
    public class ClassBascul
    {
        public class MS_Tavzin
        {
            public class portrow
            {

                public int portCom { get; set; }
                public int bitData { get; set; }
                public int parity { get; set; }
                public int stopData { get; set; }
                public int speed { get; set; }
                public int cont_addSahih { get; set; }
                public int cont_addAhshari { get; set; }

                public int kindBaskul { get; set; }
                public string urlServer { get; set; }

            }
            public static System.IO.Ports.SerialPort mp_GetSerilPortFromTbl(portrow row)
            {

                System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort();

                int[] speed = new int[] {
                                    110,
                                    300,
                                    1200,
                                    2400,
                                    4800,
                                    9600,
                                    19200,
                                    38400,
                                    57600,
                                    115200,
                                    230400,
                                    460800,
                                    921600,
            };
                double[] stopBit = new double[] {
                                   1,1.5,2
            };
                string[] portCom = new string[25];

                int i = 1;
                foreach (var t in portCom)
                {
                    portCom[i - 1] = "COM" + i.ToString();
                    i++;
                }

                int[] databit = new int[] {
                                   5,6,7,8
            };

                port.PortName = portCom[row.portCom];
                port.DataBits = databit[row.bitData];
                port.Parity = (System.IO.Ports.Parity)row.parity;

                switch (row.parity)
                {
                    case 0:
                        port.Parity = System.IO.Ports.Parity.Even;
                        break;

                    case 1:
                        port.Parity = System.IO.Ports.Parity.Odd;
                        break;
                    case 2:
                        port.Parity = System.IO.Ports.Parity.None;
                        break;
                    case 3:
                        port.Parity = System.IO.Ports.Parity.Mark;
                        break;
                    case 4:
                        port.Parity = System.IO.Ports.Parity.Space;
                        break;
                }


                port.StopBits = (System.IO.Ports.StopBits)stopBit[row.stopData];
                port.BaudRate = speed[row.speed];

                return port;
            }

            public static int ____nohbaskol = 0;
            public class mpBaskol
            {
                //  public int shBas { get; set; }
                // public string Name { get; set; }
                public NohBaskolEnum noh { get; set; }

            }
            public enum NohBaskolEnum : int
            {
                TEC1500_TEC1600 = 1,
                Yaohua_A12 = 2,

                Gama = 3,
                YaohuaGama = 4,
                Alpha10 = 5,
                MicroTowzin = 13,
            }

       

            public static string[] NohBaskolNames = new string[]
               {
              "000 NONE",
              "001 TEC1500_TEC1600",
              "002 Yaohua_A12",
              "003 Gama",
              "004 YaohuaGama",
              "005 Alpha10",
              "905 PLC FATEK",
                };


            public delegate void mpdelegateDataResived(System.IO.Ports.SerialPort Port, mpBaskol Baskol, decimal Vazn);
            public delegate void mpdelegateDataResivedError(System.IO.Ports.SerialPort Port, mpBaskol Baskol, System.IO.Ports.SerialErrorReceivedEventArgs e);

            public class mpClassBaskol
            {
                portrow row;
                public mpClassBaskol(System.IO.Ports.SerialPort port, mpBaskol baskol, portrow row_)
                {

                    Baskol = baskol;

                    Port = port;
                    row = row_;
                }
                public void mpInti(mpBaskol_status status)
                {
                    switch (Baskol.noh)
                    {

                        case NohBaskolEnum.Gama:
                            mp_run_Gama(status);
                            break;

                        case NohBaskolEnum.TEC1500_TEC1600:
                            mp_run_tec1500(status);
                            break;

                        case NohBaskolEnum.YaohuaGama:
                            // mp_run_yahoma(status);
                            YaohuaOutdoor(status);
                            break;

                        case NohBaskolEnum.Yaohua_A12:
                            YaohuaOutdoor(status);
                            break;

                        case NohBaskolEnum.MicroTowzin:
                            MicroTowzin(status);
                            break;

                        case NohBaskolEnum.Alpha10:
                            mp_run_Alpha10(status);
                            break;
                    }
                }
                public mpBaskol Baskol { get; set; }
                public System.IO.Ports.SerialPort Port { get; set; }
                public event mpdelegateDataResived MpSerialDataResived;
                public event mpdelegateDataResivedError mpSerialDataResivedError;
                public mpBaskol_flag mpStatus { get; set; }
                public enum mpBaskol_flag
                {
                    Runing,
                    Connecting,
                    Disconnectd,
                    reading,
                    readed,
                }
                public enum mpBaskol_status
                {
                    Run,
                    Close,
                    Reset,
                }
                void mp_run_yahoma(mpBaskol_status stat)
                {
                    mpStatus = mpBaskol_flag.Connecting;

                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch { return; }
                    }
                    else
                    {

                        return;
                    }
                    var d = row.cont_addSahih + row.cont_addAhshari;
                    if (d == 0)
                        d = 5;
                    string numbers = "0123456789.";
                    Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                    {

                        mpStatus = mpBaskol_flag.reading;
                        var data = Port.ReadByte();
                        var ssd = Port.ReadExisting();

                        int i = 0;
                        string vazn = "";
                        foreach (var r in ssd)
                        {

                            if (numbers.Contains(r.ToString()))
                            {
                                i++;
                                vazn = r.ToString() + vazn;
                            }

                            if (i >= d)
                            {
                                break;
                            }
                        }
                        vazn = vazn.Trim();
                        if (vazn.Length >= d)
                        {
                            mpStatus = mpBaskol_flag.readed;
                            if (MpSerialDataResived != null)
                            {
                                var meg = Convert.ToDecimal(vazn);
                                if (row.cont_addAhshari > 0)
                                {
                                    var s = meg.ToString("0000000000");
                                    string m = "";
                                    m = s.Substring(0, s.Length - row.cont_addAhshari);
                                    var m2 = s.Substring(s.Length - row.cont_addAhshari, row.cont_addAhshari);
                                    m = m + "." + m2;
                                    meg = Convert.ToDecimal(m);

                                }
                                MpSerialDataResived(Port, Baskol, meg);
                            }
                        }
                    };
                    Port.ErrorReceived += (object sender, System.IO.Ports.SerialErrorReceivedEventArgs e) =>
                    {
                        System.Windows.Forms.MessageBox.Show(e.EventType.ToString());
                        if (mpSerialDataResivedError != null)
                            mpSerialDataResivedError(Port, Baskol, e);
                    };



                }


                public void MicroTowzin(mpBaskol_status stat)
                {
                    mpStatus = mpBaskol_flag.Connecting;
                    var d = row.cont_addSahih + row.cont_addAhshari;
                    if (d == 0)
                        d = 5;
                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch { return; }
                    }
                    else
                    {

                        return;
                    }
                    try
                    {
                        int num;
                        string outdoor_str = ""; ;
                        string numbers = "-+0123456789.";
                        Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                        {

                            if (!Port.IsOpen)
                            {
                                return;
                            }

                        lba1:


                            var dddd = Port.ReadChar();

                            if (dddd == 13)
                            {
                                if (outdoor_str.Length > 4)
                                {
                                    try
                                    {
                                        var meg = Convert.ToDecimal(outdoor_str);
                                        MpSerialDataResived(Port, Baskol, meg);
                                        //  this.SetText(outdoor_str);
                                        outdoor_str = "";
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                if (numbers.Contains(((char)dddd).ToString()))
                                {
                                    outdoor_str += ((char)dddd).ToString();
                                }

                            }
                            if (true)//if not form exit
                            {
                                if (Port.BytesToRead != 0)
                                {
                                    goto lba1;
                                }
                            }
                            else
                            {

                            }

                            //Label_0014:



                            //num = Port.ReadByte();
                            //if (!Port.IsOpen)
                            //{
                            //    return;
                            //}

                            //if (num == 0x3d)
                            //{

                            //    int digits = 0;
                            //    string str = "";
                            //    int num3 = 0;
                            //    foreach (char ch in outdoor_str)
                            //    {
                            //        if (((( /*(ch == '.') ||*/ (ch == '-')) || ((ch == '0') || (ch == '1'))) || (((ch == '2') || (ch == '3')) || ((ch == '4') || (ch == '5')))) || (((ch == '6') || (ch == '7')) || ((ch == '8') || (ch == '9'))))
                            //        {
                            //            str = str.Insert(str.Length, ch.ToString());
                            //            switch (ch)
                            //            {
                            //                case '-':
                            //                    //motion1 = false;
                            //                    break;

                            //                case '.':
                            //                    digits = 1;
                            //                    break;
                            //            }
                            //            if (digits != 0)
                            //            {
                            //                digits++;
                            //            }
                            //        }
                            //        num3++;
                            //    }
                            //    double num4 = 0.0;
                            //    try
                            //    {
                            //        num4 = Math.Round(Convert.ToDouble(str), digits);
                            //    }
                            //    catch
                            //    {
                            //        //  motion1 = false;
                            //        outdoor_str = "";
                            //        return;
                            //    }
                            //    outdoor_str = "";

                            //    var meg = Convert.ToDecimal(num4);
                            //    if (row.cont_addAhshari > 0)
                            //    {
                            //        var s = meg.ToString("0000000000");
                            //        string m = "";
                            //        m = s.Substring(0, s.Length - row.cont_addAhshari);
                            //        var m2 = s.Substring(s.Length - row.cont_addAhshari, row.cont_addAhshari);
                            //        m = m + "." + m2;
                            //        meg = Convert.ToDecimal(m);

                            //    }

                            //    MpSerialDataResived(Port, Baskol, (decimal)meg);
                            //    //weightonline1 = num4;
                            //    // weightonline = weightonline1;
                            //    //this.SetText(num4.ToString("#0.##"));
                            //    outdoor_str = "";
                            //}
                            //else
                            //{
                            //    outdoor_str = outdoor_str.Insert(0, Convert.ToChar(num).ToString());
                            //}
                            //Label_019C:
                            //if (Port.BytesToRead != 0)
                            //{
                            //    if (!Port.IsOpen)
                            //    {
                            //        return;
                            //    }
                            //    else
                            //    {
                            //        goto Label_0014;
                            //    }
                            //}
                        };
                    }
                    catch
                    {
                        MpSerialDataResived(Port, Baskol, 0);
                        //outdoor_str = outdoor_str.Insert(0, Convert.ToChar(num).ToString());
                    }
                }
                public void YaohuaOutdoor(mpBaskol_status stat)
                {
                    mpStatus = mpBaskol_flag.Connecting;
                    var d = row.cont_addSahih + row.cont_addAhshari;
                    if (d == 0)
                        d = 5;
                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch { return; }
                    }
                    else
                    {

                        return;
                    }
                    try
                    {
                        int num;
                        string outdoor_str = ""; ;
                        Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                        {

                            if (!Port.IsOpen)
                            {
                                return;
                            }
                        Label_0014:



                            num = Port.ReadByte();
                            if (!Port.IsOpen)
                            {
                                return;
                            }

                            if (num == 0x3d)
                            {

                                int digits = 0;
                                string str = "";
                                int num3 = 0;
                                foreach (char ch in outdoor_str)
                                {
                                    if (((( /*(ch == '.') ||*/ (ch == '-')) || ((ch == '0') || (ch == '1'))) || (((ch == '2') || (ch == '3')) || ((ch == '4') || (ch == '5')))) || (((ch == '6') || (ch == '7')) || ((ch == '8') || (ch == '9'))))
                                    {
                                        str = str.Insert(str.Length, ch.ToString());
                                        switch (ch)
                                        {
                                            case '-':
                                                //motion1 = false;
                                                break;

                                            case '.':
                                                digits = 1;
                                                break;
                                        }
                                        if (digits != 0)
                                        {
                                            digits++;
                                        }
                                    }
                                    num3++;
                                }
                                double num4 = 0.0;
                                try
                                {
                                    num4 = Math.Round(Convert.ToDouble(str), digits);
                                }
                                catch
                                {
                                    //  motion1 = false;
                                    outdoor_str = "";
                                    return;
                                }
                                outdoor_str = "";

                                var meg = Convert.ToDecimal(num4);
                                if (row.cont_addAhshari > 0)
                                {
                                    var s = meg.ToString("0000000000");
                                    string m = "";
                                    m = s.Substring(0, s.Length - row.cont_addAhshari);
                                    var m2 = s.Substring(s.Length - row.cont_addAhshari, row.cont_addAhshari);
                                    m = m + "." + m2;
                                    meg = Convert.ToDecimal(m);

                                }

                                MpSerialDataResived(Port, Baskol, (decimal)meg);
                                //weightonline1 = num4;
                                // weightonline = weightonline1;
                                //this.SetText(num4.ToString("#0.##"));
                                outdoor_str = "";
                            }
                            else
                            {
                                outdoor_str = outdoor_str.Insert(0, Convert.ToChar(num).ToString());
                            }
                        Label_019C:
                            if (Port.BytesToRead != 0)
                            {
                                if (!Port.IsOpen)
                                {
                                    return;
                                }
                                else
                                {
                                    goto Label_0014;
                                }
                            }
                        };
                    }
                    catch
                    {
                        MpSerialDataResived(Port, Baskol, 0);
                        //outdoor_str = outdoor_str.Insert(0, Convert.ToChar(num).ToString());
                    }
                }




                public void mp_run_Alpha10(mpBaskol_status sta)
                {

                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch { return; }
                    }
                    else
                    {

                    }

                    string val = "0";
                    string numbers = "-+0123456789.";

                    int i = 0;
                    StringBuilder sp = new StringBuilder();
                    //MpBaskol.Form1 f = new Form1();
                    //f.Show();
                    Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                    {
                    lba1:


                        var dddd = Port.ReadChar();


                        val += ((char)dddd).ToString();

                        if (Port.BytesToRead != 0)
                        {
                            goto lba1;
                        }



                    };


                }

                void mp_run_Gama(mpBaskol_status stat)
                {
                    mpStatus = mpBaskol_flag.Connecting;

                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch { return; }
                    }
                    else
                    {

                        return;
                    }
                    var d = row.cont_addSahih + row.cont_addAhshari;
                    if (d == 0)
                        d = 5;
                    string numbers = "0123456789.";
                    Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                    {

                        mpStatus = mpBaskol_flag.reading;
                        var data = Port.ReadByte();
                        var ssd = Port.ReadExisting();

                        int i = 0;
                        string vazn = "";
                        foreach (var r in ssd)
                        {

                            if (numbers.Contains(r.ToString()))
                            {
                                i++;
                                vazn += r.ToString();
                            }

                            if (i >= d)
                            {
                                break;
                            }
                        }
                        vazn = vazn.Trim();
                        if (vazn.Length >= d)
                        {
                            mpStatus = mpBaskol_flag.readed;
                            if (MpSerialDataResived != null)
                            {
                                var meg = Convert.ToDecimal(vazn);
                                if (row.cont_addAhshari > 0)
                                {
                                    var s = meg.ToString("0000000000");
                                    string m = "";
                                    m = s.Substring(0, s.Length - row.cont_addAhshari);
                                    var m2 = s.Substring(s.Length - row.cont_addAhshari, row.cont_addAhshari);
                                    m = m + "." + m2;
                                    meg = Convert.ToDecimal(m);

                                }
                                MpSerialDataResived(Port, Baskol, meg);
                            }
                        }
                    };
                    Port.ErrorReceived += (object sender, System.IO.Ports.SerialErrorReceivedEventArgs e) =>
                    {
                        System.Windows.Forms.MessageBox.Show(e.EventType.ToString());
                        if (mpSerialDataResivedError != null)
                            mpSerialDataResivedError(Port, Baskol, e);
                    };



                }
                void mp_run_tec1500(mpBaskol_status stat)
                {
                    mpStatus = mpBaskol_flag.Connecting;

                    if (!Port.IsOpen)
                    {
                        try
                        {
                            Port.Open();
                        }
                        catch (Exception ex)
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                    byte[] data = new byte[1024];
                    byte[] nums = new byte[4];
                    int iii = 0;
                    bool bRdFlag = true;
                    bool motion1;
                    int num = 0;
                    short deci = 0;
                    int k_deci = 1;
                    float weightonline1 = 0;
                    Port.DataReceived += (object sender, System.IO.Ports.SerialDataReceivedEventArgs e) =>
                    {
                    Label_001C:
                        mpStatus = mpBaskol_flag.reading;

                        num = Port.ReadByte();
                        // var ff = Port.ReadExisting();
                        if ((num & 128) == 128)//110 1111111
                        {
                            bRdFlag = true;
                            iii = 0;
                            motion1 = (num & 16) == 16;
                            deci = (short)(((short)num) & 7);
                            k_deci = (int)Math.Pow(10.0, (double)deci);
                        }

                        else if (bRdFlag)
                        {
                            data[iii] = (byte)num;
                            if (++iii >= 4)
                            {
                                long num2 = (7 & data[0]) << 21;
                                num2 += (127 & data[1]) << 14;
                                num2 += (127 & data[2]) << 7;
                                num2 += 127 & data[3];
                                if ((data[0] & 8) == 8)
                                {
                                    num2 = -num2;
                                }
                                weightonline1 = (float)(((double)num2) / k_deci);
                                weightonline1 = (float)Math.Round(weightonline1, deci);
                                mpStatus = mpBaskol_flag.readed;
                                if (MpSerialDataResived != null)
                                {

                                    MpSerialDataResived(Port, Baskol, Convert.ToDecimal(weightonline1));
                                }
                                bRdFlag = false;
                            }
                        }

                    Label_0179:
                        if (Port.BytesToRead != 0)
                        {
                            goto Label_001C;
                        }

                    };
                    Port.ErrorReceived += (object sender, System.IO.Ports.SerialErrorReceivedEventArgs e) =>
                    {
                        if (mpSerialDataResivedError != null)
                            mpSerialDataResivedError(Port, Baskol, e);
                    };
                }
            }
        }





        public class reg
        {
            public reg()
            {



            }
            static string keyy = "MS_Tavzin";


            public static void set(string namee, string valuee)
            {


                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\" + keyy);
                key.SetValue(namee, valuee);
                key.Close();
            }


            public static string get(string namee)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\" + keyy);
                var g = key.GetValue(namee, "");
                key.Close();
                return g.ToString();


            }
            public static void del(string name)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\" + keyy);
                key.DeleteValue(name);
                key.Close();

            }



        }

    }
}
