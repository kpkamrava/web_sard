using System;
using System.IO;
using System.Net;
using static win_sard.ClassBascul;
 
namespace win_sard
{
    public class ClassService
    {
        public delegate void STRdelegate(string strnotifite);
        public delegate void STRVazndelegate(decimal? vazn);
        public STRdelegate StrInterfacedelegate { get; set; }
        public STRVazndelegate STRVaznInterfacedelegate { get; set; }

        class mpclassTreadBascol
        {

            public MS_Tavzin.mpClassBaskol baskol { get; set; }
            public System.Threading.Thread thread { get; set; }

        }

        DateTime getv1 = DateTime.Now;
        mpclassTreadBascol treadbasA;
        void baskolATread_start()
        {

            treadbasA.baskol.mpInti(MS_Tavzin.mpClassBaskol.mpBaskol_status.Run);
            treadbasA.baskol.MpSerialDataResived += (System.IO.Ports.SerialPort Port, MS_Tavzin.mpBaskol Baskol, decimal Vazn) =>
            {
                STRVaznInterfacedelegate.Invoke(Vazn);

                sendVazn(Vazn);

            };

            treadbasA.baskol.mpSerialDataResivedError += (System.IO.Ports.SerialPort Port, MS_Tavzin.mpBaskol Baskol, System.IO.Ports.SerialErrorReceivedEventArgs e) =>
            {
                STRVaznInterfacedelegate.Invoke(null);
                StrInterfacedelegate.Invoke("خطا درخواندن وزن");


            };
        }




        public void startbascul()
        {
              try
            {
                var row =  Newtonsoft.Json.JsonConvert.DeserializeObject<MS_Tavzin.portrow>(reg.get("config"));
                if (row == null)
                {
                    StrInterfacedelegate.Invoke("تنظیمات باسکول انجام نشده");
                    new FormSetting().ShowDialog();

                }
                row = Newtonsoft.Json.JsonConvert.DeserializeObject<MS_Tavzin.portrow>(reg.get("config"));
                if (row == null)
                {

                    return;
                }

                var port = MS_Tavzin.mp_GetSerilPortFromTbl(row);


                var towz = new MS_Tavzin.mpBaskol();

                towz.noh = (MS_Tavzin.NohBaskolEnum)row.kindBaskul;



                //{
                //    var i = 1;
                //    decimal z = 0;
                //    while (true)
                //    {
                //        if (i < 10)
                //        {
                //            z = new Random().Next(2999, 15050);
                //        }
                //        if (i > 20)
                //        {
                //            i = 0;
                //        }
                //        Application.DoEvents();
                //        sendVazn(z);
                //        i++;
                //        Application.DoEvents();
                //        Thread.Sleep(1500);
                //        Application.DoEvents();
                //    }
                //}





                if (port.IsOpen == true)
                {
                    StrInterfacedelegate.Invoke("ارتباط با باسکول انجام نشد - پورت بسته است");
                    return;
                }
                StrInterfacedelegate.Invoke("پورت باز");

                treadbasA = new mpclassTreadBascol();
                treadbasA.baskol = new MS_Tavzin.mpClassBaskol(port, towz, row);
                treadbasA.thread = new System.Threading.Thread(new System.Threading.ThreadStart(baskolATread_start));
                treadbasA.thread.Start();
                StrInterfacedelegate.Invoke("باسکول شروع به کار");
            }
            catch
            {
                return;

            }
        }

        public string sendVazn(decimal vazn)
        {
            var row = Newtonsoft.Json.JsonConvert.DeserializeObject<MS_Tavzin.portrow>(reg.get("config"));
            var str = row.urlServer + "/Ajax/SetBasculWeight/" + vazn+$"?username={row.urlUser}&password={row.urlPass}";

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(str);
                request.Method = "Get";
                request.ContentType = "application/json";
                // request.ContentLength = DATA.Length;
                //using (Stream webStream = request.GetRequestStream())
                ////using (StreamWriter requestWriter = new StreamWriter(webStream, System.Text.Encoding.Unicode))
                ////{
                ////    requestWriter.Write(DATA);
                ////}

                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                using (StreamReader responseReader = new StreamReader(webStream))
                {
                    string response = responseReader.ReadToEnd();
                  
                    StrInterfacedelegate.Invoke("وزن ارسال شد "+ response);

                    return response;
                }

            }
            catch(Exception e)
            {
                StrInterfacedelegate.Invoke("خطا در ارسال وزن"+e.Message);

            }
            return "";
        }



    }
}
