using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;

namespace web_lib
{
    public static class AllHelper
    {


        public static String betweenStrings(this String text, String start, String end = "")
        {
            int p1 = text.IndexOf(start) + start.Length;
            int p2 = text.IndexOf(end, p1);

            if (end == "") return (text.Substring(p1).Trim());
            else return text.Substring(p1, p2 - p1).Trim();
        }




        static public T Clone2<T>(this T source)
        {

            return source.ToJson().FromJson<T>();



        }
        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        public static decimal? ToDecimal(this string n)
        {
          
            if (decimal.TryParse(n, out var d)) {
                return d;
            
            }

            return null;
        }
        public static int ToInt(this string n)
        {
            int d = 0;
            int.TryParse(n, out d);

            return d;
        }
        public static string RandomColorCss()
        { Random rnd = new Random();
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

            return HexConverter(randomColor);
        }


        public static IEnumerable<T> GetAllItems<T>(this T value) where T : Enum
        {
            return (T[])Enum.GetValues(typeof(T));
        }



        public static string ToJson(this Object obj)
        {

            return JsonSerializer.Serialize(obj, new JsonSerializerOptions { });

        }

        public static T FromJson<T>(this string str)
        {
            if (str.IsEmpty())
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(str);
        }




        public static string ToPersianChars(this string str)
        {
            var charas = new[] { new[] { 'ي', 'ی' }, new[] { 'ك', 'ک' } };

            foreach (var item in charas)
            {
                str = str.Replace(item[0], item[1]);
            }

            return str;

        }
        public static string ToNumberEng(this string data)
        {
            data = data ?? "";
              var ch = '۰';
            int j = 0;
            for (int i = ch; i < ch+10; i++)
            {
                data = data.Replace(Convert.ToChar(i), Convert.ToChar(48 + j));
                j++;
            }
            return data;

        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value.IsEmpty())
            {
                return default;
            }
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }





        public static IHtmlContent _DatePicker(this IHtmlHelper th, string name, string value, bool requ = false, string attrclass = ""/*form-control*/, string attr = ""/*form-control*/  )
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


            return th.Raw( str);

        }
        public static string IsSelected(this HtmlHelper htmlHelper, string controllers, string actions = "", string cssClass = " active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : String.Empty;
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        public static string ToMoney(this decimal number)
        {

            return number.ToString("###,###,##0");
        }

        public static string ToMoneyStr(this decimal number, string simb = "تومان")
        {
            var str = "";



            if (number >= 1000000000)
            {
                var t = ((long)number / 1000000000);
                str += (((decimal)t).ToMoney()) + " میلیارد ";
                number = number - t * 1000000000;
                if (number > 0)
                {
                    str += " و ";
                }
            }




            if (number >= 1000000)
            {
                var t = ((long)number / 1000000);
                str += (((decimal)t).ToMoney()) + " میلیون ";
                number = number - t * 1000000;
                if (number > 0)
                {
                    str += " و ";
                }
            }

            if (number >= 1000)
            {
                var t = ((long)number / 1000);
                str += (((decimal)t).ToMoney()) + " هزار ";
                number = number - t * 1000;
            }
            return str + simb;
        }
        public static string ToMoneyStr(this decimal? number, string simb = "تومان", string nul = "")
        {
            return number.HasValue ? number.Value.ToMoneyStr(simb) : nul;
        }

        public static string ToMoney(this decimal? number)
        {
            return number.HasValue ? number.Value.ToMoney() : "";
        }

        public static decimal Round(this decimal  number)
        {
            return Math.Round( number);
        }
        public static string ToKilo(this decimal number)
        {
            return number.ToString("######,##0");
        }
        public static string ToKilo(this decimal? number)
        {
            return number.HasValue ? number.Value.ToKilo() : "";
        }
        public static string ToKilo(this long? number)
        {
            return number.HasValue? number.Value.ToString("######,##0"):"";
        }
        public static string ToDama(this decimal number)
        {
            return number.ToString("#####0.##");
        }
        public static string ToDama(this decimal? number)
        {
            return number.HasValue ? number.Value.ToDama() : "";
        }
    
        public static string ToKilo(this long number)
        {
            return ((decimal)number).ToKilo();
        }
        public static long? gadrmotlagh(this long? number)
        {
            return number.HasValue ? (long?)number.Value.gadrmotlagh() : null;
        }
        public static decimal? gadrmotlagh(this decimal? number)
        {
            return number.HasValue ? (decimal?)number.Value.gadrmotlagh() : null;
        }
        public static decimal gadrmotlagh(this decimal number)
        {
            return number < 0 ? -number : number;
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
        public static string UrlReferer(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return request.Headers["Referer"].ToString();
        }
        public static string UrlMyDomin(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host.Value.ToString()}{request.PathBase.Value.ToString()}"; ;
        }
        public static string UrlMy(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
          var v=  string.Format("{0}://{1}{2}{3}", request.Scheme, request.Host, request.Path, request.QueryString);
            return v ;
        }
        public static string UrlMyControlAction(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            var v = string.Format("{0}{1}",  request.Path, request.QueryString);
            return v;
        }
        public static string LinkShareTelegram(this Microsoft.AspNetCore.Http.HttpRequest request, string url = "", string text = "")
        {
            if (url.IsEmpty())
            {
                url = UrlMy(request);
            }
            var str = "https://t.me/share/url?url=" + url + "&text=" + text;
            return str;
        }
        public static string LinkShareWhatsapp(this Microsoft.AspNetCore.Http.HttpRequest request, string url = "", string text = "")
        {
            if (url.IsEmpty())
            {
                url = UrlMy(request);
            }
            var str = "https://wa.me/?text=" + text + url;
            return str;
        }
        public static string LinkShareInstagram(this Microsoft.AspNetCore.Http.HttpRequest request, string url = "", string text = "")
        {
            if (url.IsEmpty())
            {
                url = UrlMy(request);
            }
            var str = "https://www.instagram.com/?url=" + url;
            return str;
        }



        public static string queryChange(this string url, string q, string qvalue)
        {
            var uri = new Uri(url);
            var qs = HttpUtility.ParseQueryString(uri.Query);
            qs.Set(q, qvalue);
            var uriBuilder = new UriBuilder(uri);
            uriBuilder.Query = qs.ToString();
            var newUri = uriBuilder.Uri;
            return newUri.ToString();
        }


        public static string TextAreaLinerBR(this string txt)
        {
            return txt.Replace("\n", "<br />").Replace("\r\n", "<br />");
        }



        public static string sizestr(this long size)
        {
            var s = size > 0 ? (size / 1024) : 0;
            return s > 1000 ? s / 1000 + " MB " : s + " KB ";
        }


        public static string ToPersianAiamHafte(this int roz)
        {
            var str = new string[] { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه" };
            return str[roz]; 
        }
        public static string ToPersianAiamMahha(this int mah)
        {
            var str = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر","آبان","آذر","دی","بهمن","اسفند"  };
            return str[mah-1];
        }
        public static string ToPersianAiamHafte(this DateTime dt)
        {
            var str = new string[] {  "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };
            return str[(int)dt.DayOfWeek];
        }
        public static int ToAiamHafte(this DayOfWeek dt)
        {
            var aiam = ((int)dt) + 1;
            aiam = aiam > 6 ? aiam - 1 : aiam;
            return aiam;
        }

        public static string ToPersianDateMini(this DateTime dt)
        {
            var tt = (DateTime.Now.Date - dt.Date);
            var ttt = (DateTime.Now - dt);



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


                if (ttt.TotalHours > 1)
                {
                    return "ساعت " + dt.ToShortTimeString();

                }
                else
                if (ttt.TotalMinutes > 2)
                {
                    return Math.Round(ttt.TotalMinutes) + " دقیقه قبل ";

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
        public static DateTime ToPersianAddMonths(this DateTime dt,int months)
        {

            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
             

            return persianCalendar.AddMonths(dt, months); ;


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

                if (dateTime.Value.Second>0)
                {
                    string second = dateTime.Value.Second.ToString().PadLeft(2, '0');
                    return String.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second);

                }
                else if (dateTime.Value.Minute+   dateTime.Value.Hour > 0)
                {
                     return String.Format("{0}/{1}/{2} {3}:{4}", year, month, day, hour, minute);

                }
                else
                {
                    return String.Format("{0}/{1}/{2}", year, month, day);

                }

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

        public static bool IsMobile(this string input)
        {

            var s = input.Trim();
            if (s.Length != 11)
            {
                return false;

            }
            return Regex.IsMatch(input, @"^(\0|0)?9\d{9}$");


        }
        public static bool IsNumber(this string input)
        {     
           return decimal.TryParse(input,out var d);
           


        }
        public static bool IsCardBank(this string input)
        {

            var s = input.Trim();
            if (s.Length != 16)
            {
                return false;

            }
            return true;


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
        public static bool IsEmpty(this Guid str)
        {

            return str == null || str == Guid.Empty;

        }
        public static bool IsEmpty(this Guid? str)
        {

            return str == null || str == Guid.Empty;

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


        public static DateTime ToDate(this int[] date)
        {
            try
            {
                System.Globalization.PersianCalendar jc = new System.Globalization.PersianCalendar();

                try
                {
                    
                    int year = Convert.ToInt32(date[0]);
                    int month = Convert.ToInt32(date[1]);
                    int day = Convert.ToInt32(date[2]);

                    DateTime d = jc.ToDateTime(year, month, day, 0, 0, 0, 0, System.Globalization.PersianCalendar.PersianEra);

                    return d;
                }
                catch
                {
                    throw new FormatException("The input string must be in 0000 00 00 format.");
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



        /*
         ////////
         ////
         ////
         ////
         ////
         ////
         /////
         ///


        /
        /
        /
        /
        /
        /



        /
        /

         
         
         
         
         
         
         
         */
        static Regex MobileCheck = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
        static Regex MobileVersionCheck = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);

        public static bool IsMobile(this HttpRequest request)
        {

            if (request != null)
            {
                if (request.Headers["HTTP_USER_AGENT"].ToString() != null)
                {
                    var u = request.Headers["HTTP_USER_AGENT"].ToString();

                    if (u.Length < 4)
                        return false;

                    if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                        return true;
                }
                else if (request.Headers["User-Agent"].ToString() != null)
                {
                    var u = request.Headers["User-Agent"].ToString();

                    if (u.Length < 4)
                        return false;

                    if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                        return true;
                }

            }


            return false;
        }


        static string chch = "abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()_+ ضصثقفغعهخحجچپشسیبلاتنمکگظطزرذدئوآ♂►◄↕‼¶▬§↨↑↓→←∟↔▲▼♣♠•◘○◙♀♪♫☼-";

        public static string Encode(this string serverName)
        {

            string ret = "";
            for (int i = 0; i < serverName.Length; i++)
            {
                var ch = serverName[i];
                if (serverName.Any(a => a == ch))
                {
                    var p = chch.IndexOf(ch);
                    // var chaz = chch[p];
                    var chbe = chch[chch.Length - p - 1];
                    ret += chbe;
                }
                else
                {
                    ret += ch;
                }


            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(ret));
        }

        public static string Decode(this string encodedServername)
        {
            string ret = "";
            var str = Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
            for (int i = 0; i < str.Length; i++)
            {
                var ch = str[i];
                if (str.Any(a => a == ch))
                {
                    var p = chch.IndexOf(ch);
                    var chaz = chch[p];
                    var chbe = chch[chch.Length - p - 1];
                    //str = str.Replace(chbe, chaz);
                    ret += chbe;

                }
                else
                {
                    ret += ch;

                }
            }
            return ret;
        }


        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage;
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                Bitmap bitmap = (Bitmap)Image.FromStream(ms);


                returnImage = bitmap;
            }

            return returnImage;
        }



















    }

}
