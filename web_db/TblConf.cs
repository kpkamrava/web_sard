#nullable disable

using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace web_db
{
    [Index("Key")]
    public partial class TblConf
    {
        
        public enum KeyEnum
        {
            [web_lib.classAttr.KPvalus(Description = "  سیستم پیامکی- سیستم اصلی")]
            Main_ActiceSms = 100000,

            [web_lib.classAttr.KPvalus(Description = "نام کاربری سیستم پیامکی- سیستم اصلی")]
            Main_UserSms = 100001,
            [web_lib.classAttr.KPvalus(Description = "رمز عبور سیستم پیامکی- سیستم اصلی")]
            Main_PassSms = 100002,
            [web_lib.classAttr.KPvalus(Description = "شماره فرستنده سیستم پیامکی- سیستم اصلی")]
            Main_NumSms = 100003,


          
            
            [web_lib.classAttr.KPvalus(Description = "دقت باسکول- سیستم اصلی")]
            Main_DegateBaskul = 100021,
         
            
            [web_lib.classAttr.KPvalus(Description = "نوع دوربین باسکول- سیستم اصلی")]
            Main_NavhDorbinBaskul = 100025,
            [web_lib.classAttr.KPvalus(Description = "آدرس دوربین باسکول- سیستم اصلی")]
            Main_URLDorbinBaskul = 100026,
            [web_lib.classAttr.KPvalus(Description = "نام کاربری دوربین باسکول- سیستم اصلی")]
            Main_UserDorbinBaskul = 100027,
            [web_lib.classAttr.KPvalus(Description = "رمز عبور دوربین باسکول- سیستم اصلی")]
            Main_PassDorbinBaskul = 100028,





            [web_lib.classAttr.KPvalus(Description = "حداقل افت استاندارد سردخانه- سیستم اصلی")]
            Main_HadeagalOft = 100031,
            [web_lib.classAttr.KPvalus(Description = "حداکثر افت استاندارد سردخانه- سیستم اصلی")]
            Main_HadeaxareOft = 100032,





            [web_lib.classAttr.KPvalus(Description = "نام کاربری سایر سیستم ها- سیستم مالی")]
            Mali_UserOT = 200011,
            [web_lib.classAttr.KPvalus(Description = "رمز عبور سایر سیستم ها- سیستم مالی")]
            Mali_PassOT = 200012,
            [web_lib.classAttr.KPvalus(Description = "آدرس دسترسی سایر سیستم ها- سیستم مالی")]
            Mali_UrlOT = 200013,






            [web_lib.classAttr.KPvalus(Description = "نوع سایر سیستم ها- سیستم مالی")]
            Mali_KindOT = 100014,




         

            [web_lib.classAttr.KPvalus(Description = "سفارش مشتریان - سیستم مشتریان")] 
            US_SefareshMoshtarianEnable= 400001,

            [web_lib.classAttr.KPvalus(Description = "نمایش وزن در - سیستم مشتریان")] 
            US_WeightViewEnable = 400002,

            [web_lib.classAttr.KPvalus(Description = "ضریب وزن - سیستم مشتریان")]
            US_WeightZarib = 400003,
          
            [web_lib.classAttr.KPvalus(Description = "واحد شمارش وزن - سیستم مشتریان")]
            US_WeightVahed = 400004,
           
            [web_lib.classAttr.KPvalus(Description = "حداقل وزن برای هرسفارش بر اساس ضریب- سیستم مشتریان")]
            US_WeightMin = 400005,
          
            [web_lib.classAttr.KPvalus(Description = "حداکثر وزن برای هر سفارش بر اساس ضریب- سیستم مشتریان")]
            US_WeightMax = 400006,



            [web_lib.classAttr.KPvalus(Description = "آدرس سایت - سیستم مشتریان")]
            US_UrlSite = 400011,
        }


        [Key]
        public Guid Id { get; set; }
        public KeyEnum Key { get; set; }
        public string KeyStr { get; set; }
        public string Value { get; set; }
     

        public int? ord { get; set; }
        public string k1 { get; set; }
        public string k2 { get; set; }
        public string k3 { get; set; }


    }
}
