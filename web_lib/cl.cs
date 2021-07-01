using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;

namespace web_lib
{
    public class stat
    {
        public static bool sendmail(String Host, int? port, string MailFrom, string user, string pass, string mailresive, string subject, string body)
        {

            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(Host);

                mail.From = new System.Net.Mail.MailAddress(MailFrom);
                mail.To.Add(mailresive);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = port ?? 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(user, pass);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public class Captcha
        {
            //public static bool CheckCaptcha(string Captcha)
            //{
            //    //var t = new SessionContext<string>("CK").GetUserData();
            //    //if (t == null)
            //    //{
            //    //    return false;
            //    //}

            //    //return (t).ToLower() == Captcha.ToLower();
            //}
            private static Random Randomizer = new Random(DateTime.Now.Second);
            private string Text { get; set; }
            public string ImageAsBaseSrc { get; set; }

            public Captcha() // constructor
            {
                Text = GetRandomText();
                var t = CreateCaptcha(Text);
                var base64 = Convert.ToBase64String(t);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                ImageAsBaseSrc = imgSrc;
                //new SessionContext<string>("CK").SetAuthenticationToken(Text);
            }

            private static string GetRandomText()
            {
                string text = "";
                const string chars = "ABCEFGHJKNPRSTUVXYZ0123456789";
                for (int i = 0; i < 4; i++) text += chars.Substring(Randomizer.Next(0, chars.Length), 1);
                return text;
            }
            private static Brush PickBrush()
            {
                /*  Brush result = Brushes.Transparent;

                  Random rnd = new Random();

                  Type brushesType = typeof(Brushes);

                  PropertyInfo[] properties = brushesType.GetProperties();

                  int random = rnd.Next(properties.Length);
                  result = (Brush)properties[random].GetValue(null, null);
                  */
                return Brushes.Green;
            }
            private static byte[] CreateCaptcha(string text)
            {
                byte[] byteArray = null;

                Font[] fonts = {
            new Font("Arial", 24, FontStyle.Bold),
            new Font("Courier New", 22, FontStyle.Bold),
            new Font("Calibri", 20, FontStyle.Bold),
            new Font("Tahoma", 24, FontStyle.Italic | FontStyle.Bold) };

                using (var bmp = new Bitmap(130, 40))
                {
                    using (var graphic = Graphics.FromImage(bmp))
                    {
                        using (var hb = new HatchBrush(HatchStyle.DarkUpwardDiagonal, Color.Silver, Color.White))
                            graphic.FillRectangle(hb, 0, 0, bmp.Width, bmp.Height);


                        for (int i = 0; i < text.Length; i++)
                        {
                            var point = new PointF(2 + (i * 20), 25 + Randomizer.Next(-5, 5));
                            var f = fonts[Randomizer.Next(0, 4)];
                            f = new Font(f.FontFamily.Name, f.Size + Randomizer.Next(-2, 2), f.Style);
                            graphic.DrawString(text.Substring(i, 1), f, PickBrush(), point, new StringFormat { LineAlignment = StringAlignment.Center });
                        }
                    }
                    using (var stream = new MemoryStream())
                    {
                        bmp.Save(stream, ImageFormat.Png);
                        byteArray = stream.ToArray();
                    }
                }

                // Cleanup Fonts (they are disposable)
                foreach (var font in fonts) font.Dispose();

                return byteArray;

            }

        }

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
                registryKey.SetValue("ApplicationName", ApplicationExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("ApplicationName");
            }
        }





        public enum CaptureCameraKind
        {
            HikvisionISAPI

        }
        public static MemoryStream CaptureCamera(CaptureCameraKind kind, string IP, String User, String Pass)
        {
            try
            {
                if (kind == CaptureCameraKind.HikvisionISAPI)
                {
                    int read, total = 0;
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create($"http://{IP}/ISAPI/Streaming/channels/1/picture");
                    req.Method = "GET";
                    //req.Timeout = 500;
                    NetworkCredential cred = new NetworkCredential(User, Pass/*"admin", "13666631keyvan"*/);
                    req.Credentials = cred;
                    WebResponse resp = req.GetResponse();
                    // get response stream

                    Stream stream = resp.GetResponseStream();
                    // read data from stream
                    byte[] buffer = new byte[10000000];

                    while ((read = stream.Read(buffer, total, 1000)) != 0)
                    {
                        total += read;
                    }


                    var f = new MemoryStream(buffer, 0, total);
                    // return File(f.ToArray(), "Image/jpeg");

                    return f;
                }
            }
            catch (Exception e)
            {


            } 

            return null;

        }


    }

}
