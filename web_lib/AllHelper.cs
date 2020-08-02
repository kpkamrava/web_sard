using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace web_lib
{
    public static class AllHelper
    {
        static string chaz = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+ ";
        static string chbe = "!@#$%^&*()_+ ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
        public static string _DatePicker(this HtmlHelper th, string name, string value, bool requ = false, string attrclass = ""/*form-control*/, string attr = ""/*form-control*/  )
        {
            if (string.IsNullOrEmpty(value))
            {
                value = DateTime.Now.ToPersianDate();

            }

            var str = string.Format(@"");


            str += @"<input  " + attr + "  datePR=true autocomplete='off' value='" + value + "' id='" + name + "' name='" + name + "' type='text'   class='text-box single-line datepicker " + (requ ? " required " : " ") + attrclass + "'>";

            str += $@"
         <script>
         $('#{name}').persianDatepicker({{ theme: 'latoja' }});
         </script>";
           

            return str;

        }
        public static string IsSelected(this HtmlHelper htmlHelper, string controllers, string actions="", string cssClass = " active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static decimal gadrmotlagh(this decimal number)
        {
            return number<0?-number:number;
        }
        public static double gadrmotlagh(this double number)
        {
            return number < 0 ? -number : number;
        }
        public static long gadrmotlagh(this long number)
        {
            return number < 0 ? -number : number;
        }
        public static int gadrmotlagh(this int number)
        {
            return number < 0 ? -number : number;
        }
        public static string UrlReferer(this Microsoft.AspNetCore.Http.HttpRequest  request)
        {  
            return request.Headers["Referer"].ToString();
        }
        public static string TextAreaLinerBR(this string txt)
        {
            return txt.Replace("\n", "<br />").Replace("\r\n", "<br />");
        }
       
        public static string Encode(this string serverName)
        {
            for (int i = 0; i < chaz.Length; i++)
            {
                serverName = serverName.Replace(chaz[i], chbe[i]);

            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }

        public static string sizestr(this long size)
        {
            var s = size > 0 ? (size / 1024) : 0;
            return s > 1000 ? s / 1000 + " MB " : s + " KB ";
        }


        public static string Decode(this string encodedServername)
        {
            var str = Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
            for (int i = 0; i < chaz.Length; i++)
            {
                str = str.Replace(chbe[i], chaz[i]);

            }
            return str;
        }
        class typefile
        {
            public string aztype { get; set; }
            public string totype { get; set; }


        }


        static List<typefile> listtype = new List<typefile> {
            new typefile {aztype=".application",totype="exe" },
            new typefile {aztype=".app",totype="exe" },
            new typefile {aztype=".exe",totype="exe" },
            new typefile {aztype=".xls",totype="xls" },
            new typefile {aztype=".pdf",totype="pdf" },
            new typefile {aztype=".docx",totype="doc" },
            new typefile {aztype=".txt",totype="txt" },
            new typefile {aztype=".bmp",totype="jpg" },
            new typefile {aztype=".jpg",totype="jpg" },
            new typefile {aztype=".jpeg",totype="jpg" },
        };
        public static string typeFileToImage(this string type)
        {
            // var type = typefile.Replace(".","");
            var l = listtype.SingleOrDefault(a => a.aztype.ToLower() == type.ToLower());
            if (l != null)
            {
                return l.totype + ".png";
            }
            return "file.png";

        }


        public static byte[] imgTosmall(this byte[] byteImageIn)
        {
            if (byteImageIn == null)
            {
                return null;
            }
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;


            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 50000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }
        public static byte[] imgTo100Kbytes(this byte[] byteImageIn)
        {
            if (byteImageIn == null)
            {
                return null;
            }
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f; 
            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 100000)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }
        public static string ToPersianDateMini(this DateTime dt)
        {
            var tt = (DateTime.Now - dt);



            if (tt.TotalDays >= 1)
            {
                if (tt.TotalDays < 2 && tt.TotalDays >= 1)
                {
                    return "دیروز   " + dt.ToShortTimeString();
                }
                else
                {
                    return dt.ToPersianDate();
                }
            }
            else
            {


                if (tt.TotalHours > 1)
                {
                    return "امروز ساعت " + dt.ToShortTimeString();

                }
                else
                if (tt.TotalMinutes > 2)
                {
                    return Math.Round(tt.TotalMinutes) + " دقیقه قبل ";

                }
                else
                {
                    return "چند لحظه قبل";

                }
            }

        }


        public static string ToPersianDateMini(this DateTime? dt)
        {
            if (dt.HasValue)
            {
                return dt.Value.ToPersianDateMini();
            }
            else return "";
        }
        public static string ToPersianDate(this DateTime dt)
        {
            try
            {
                var dateTime = dt == null ? DateTime.Now : dt;
                System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
                string year = persianCalendar.GetYear(dateTime).ToString();
                string month = persianCalendar.GetMonth(dateTime).ToString()
                               .PadLeft(2, '0');
                string day = persianCalendar.GetDayOfMonth(dateTime).ToString()
                               .PadLeft(2, '0');

                return String.Format("{0}/{1}/{2}", year, month, day);

            }
            catch
            {
                return ToPersianDate(DateTime.Now);

            }
        }
        public static int[] ToPersianDateint(this DateTime dt)
        {

            var dateTime = dt == null ? DateTime.Now : dt;
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            return new int[] { year, month, day };


        }
        public static string ToPersianDatenull(this DateTime dt)
        {
            try
            {
                var dateTime = dt == null ? DateTime.Now : dt;
                System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
                string year = persianCalendar.GetYear(dateTime).ToString();
                string month = persianCalendar.GetMonth(dateTime).ToString()
                               .PadLeft(2, '0');
                string day = persianCalendar.GetDayOfMonth(dateTime).ToString()
                               .PadLeft(2, '0');

                return String.Format("{0}/{1}/{2}", year, month, day);

            }
            catch
            {
                return "";

            }
        }
        public static string ToPersianDate(this DateTime? dt)
        {

            return dt.HasValue ? ToPersianDate(dt.Value) : DateTime.Now.ToPersianDate();
        }
        public static string ToPersianDatenull(this DateTime? dt)
        {

            return dt.HasValue ? ToPersianDate(dt.Value) : "";
        }
        public static string ToPersianDateTime(this DateTime? dt)
        {
            if (dt == null)
            {
                return "";
            }
            try
            {
                var dateTime = dt;
                System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
                string year = persianCalendar.GetYear(dateTime.Value).ToString();
                string month = persianCalendar.GetMonth(dateTime.Value).ToString()
                               .PadLeft(2, '0');
                string day = persianCalendar.GetDayOfMonth(dateTime.Value).ToString()
                               .PadLeft(2, '0');
                string hour = dateTime.Value.Hour.ToString().PadLeft(2, '0');
                string minute = dateTime.Value.Minute.ToString().PadLeft(2, '0');
                string second = dateTime.Value.Second.ToString().PadLeft(2, '0');
                return String.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second);

            }
            catch
            {
                return "";

            }
        }

        public static string ToPersianDateTime(this DateTime dt)
        {
            return ((DateTime?)dt).ToPersianDateTime();
        }
         
        public static bool IsImage(this string exch)
        {
            var str = exch.ToLower();
            if (
                str.EndsWith(".jpg") ||


                 str.EndsWith(".jpeg") ||
                 str.EndsWith(".png") ||
                 str.EndsWith(".ico")
                                )
            {
                return true;


            }
            return false;
        }
 
        public static bool IsCodemeli(this string input)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d{10}$"))
                return false;

            var check = Convert.ToInt32(input.Substring(9, 1));
            var sum = Enumerable.Range(0, 9)
                .Select(x => Convert.ToInt32(input.Substring(x, 1)) * (10 - x))
                .Sum() % 11;

            return (sum < 2 && check == sum) || (sum >= 2 && check + sum == 11);
        }
        public static bool IsEmpty(this string str)
        {
            
                return string.IsNullOrWhiteSpace(str);
            
        }
        public static bool IsMail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public static DateTime ToDate(this String dt)
        {
            try
            {
                System.Globalization.PersianCalendar jc = new System.Globalization.PersianCalendar();

                try
                {
                    string[] date = dt.Split('/');
                    int year = Convert.ToInt32(date[0]);
                    int month = Convert.ToInt32(date[1]);
                    int day = Convert.ToInt32(date[2]);

                    DateTime d = jc.ToDateTime(year, month, day, 0, 0, 0, 0, System.Globalization.PersianCalendar.PersianEra);

                    return d;
                }
                catch
                {
                    throw new FormatException("The input string must be in 0000/00/00 format.");
                }
            }
            catch
            {

                throw;
            }
        }
        public static DateTime? ToDateNull(this String dt)
        {
            try
            {
                System.Globalization.PersianCalendar jc = new System.Globalization.PersianCalendar();

                try
                {
                    string[] date = dt.Split('/');
                    int year = Convert.ToInt32(date[0]);
                    int month = Convert.ToInt32(date[1]);
                    int day = Convert.ToInt32(date[2]);

                    DateTime d = jc.ToDateTime(year, month, day, 0, 0, 0, 0, System.Globalization.PersianCalendar.PersianEra);

                    return d;
                }
                catch
                {
                    return null;
                }
            }
            catch
            {

                return null;
            }
        }
    }

}
