using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace web_db
{
    public partial class TblContractType
    {

        public enum KindCotractTypeEnum
        {
            [web_lib.classAttr.KPvalus(Description = "سردخانه ")] ASardKhane = 1,
            //[web_lib.classAttr.KPvalus(Description = "سورتینگ ")] ASurting = 3,

            [web_lib.classAttr.KPvalus(Description = "باسکول ")] ABaskul = 5,


            [web_lib.classAttr.KPvalus(Description = "انبار جعبه ")] ASabad = 10,


        }
        public enum SanadPortageEnum
        {
            [web_lib.classAttr.KPvalus(Description = "ندارد ")] None = 0,
            [web_lib.classAttr.KPvalus(Description = "به ازای هر ماشین ")] ByPortage = 10,
            [web_lib.classAttr.KPvalus(Description = "به ازای هر شخص ")] ByCustomer = 20,
            [web_lib.classAttr.KPvalus(Description = "به ازای هر شخص و نوع ورود و خروج ")] ByCustomerAndKindPortage = 30 

        }
        public TblContractType()
        {
            TblContracts = new HashSet<TblContract>();
            TblPortages = new HashSet<TblPortage>();
        }
        public Guid Id { get; set; }
        public int FkSalmali { get; set; }
        [Display(Name = "کد")]
        public int Code { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "نوع  ")]
        public KindCotractTypeEnum KindCotractType { get; set; }

        [Display(Name = "محوطه دارد")]
        public bool IsMahvate { get; set; }
        [Display(Name = "جابجایی دارد")]
        public bool IsJabeJaii { get; set; }


        [Display(Name = "قرارداد دارد")]
        public bool IsContract { get; set; }
        [Display(Name = "قرارداد اجباری")]
        public bool IsContractRequred { get; set; }


        [Display(Name = "نوع سند عملیات ")]
        public SanadPortageEnum OtSanadPortageKind { get; set; }

        [Display(Name = "سند بدون گردش")]
        public bool OtSanadByGroup { get; set; }


        [Display(Name = "ریز")]
        public string ConfigJson { get; set; }
       
        
    
        public void Config<T>(T Aclass)
        {

            ConfigJson = Newtonsoft.Json.JsonConvert.SerializeObject(Aclass);

        }
        public ASardKhane ConfigASardKhane()
        {

            try
            {
                var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ASardKhane>(ConfigJson);

                return v;
            }
            catch
            {
                return new ASardKhane();

            }



        }
        public ASabad ConfigASabad()
        {
            try
            {
                var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ASabad>(ConfigJson);

                return v;
            }
            catch { return new ASabad(); }

        }
        public ABaskul ConfigABaskul()
        {
            try
            {
                var v = Newtonsoft.Json.JsonConvert.DeserializeObject<ABaskul>(ConfigJson);

                return v;
            }
            catch { return new ABaskul(); }

        }


        public virtual TblSalMali FkSalmaliNavigation { get; set; }
        public virtual ICollection<TblContract> TblContracts { get; set; }
        public virtual ICollection<TblPortage> TblPortages { get; set; }





        public  class ASardKhane
        {

            [Display(Name = "کد فاکتور ورود")]
            public int? OttypeFaktor { get; set; }

            [Display(Name = "کد فاکتور برگشت ورود")]
            public int? OttypeFaktorInBack { get; set; }
            [Display(Name = "کد فاکتور خروج")]
            public int? OttypeFaktorOut { get; set; }
            [Display(Name = "کد فاکتور برگشت خروج")]
            public int? OttypeFaktorOutBack { get; set; }



            [Display(Name = " باسکول دارد")]
            public bool basculeActive { get; set; }

            [Display(Name = "نرخ باسکول نقدی")]
            public bool  basculeNaghdi { get; set; }

            [Display(Name = "مالی")]
            public bool IsMali { get; set; }
            [Display(Name = "خروجی بر اساس قرارداد کنترل شود")]
            public bool OutControlByContract { get; set; }
            [Display(Name = "خروجی بر اساس موقعیت کنترل شود")]
            public bool OutControlByLocation { get; set; }
            [Display(Name = "خروجی بر اساس درصد مجاز کنترل شود")]
            public bool OutControlByPercent { get; set; }
            [Display(Name = "نیاز به موقعیت")]
            public bool NeedLocation { get; set; }
          
            
            [Display(Name = "سطح موقیت 1تا3")]
            public int LocationLvlRequired { get; set; }
             
            [Display(Name = "کد حساب فاکتور ورود")]
            public string OtcodHesabFactor { get; set; }
            [Display(Name = "کد حساب فاکتور برگشت از ورود")]
            public string OtcodHesabFactorInBack { get; set; }
            [Display(Name = "کد حساب فاکتور خروج")]
            public string OtcodHesabFactorOut { get; set; }
            [Display(Name = "کد حساب فاکتور برگشت از خروج")]
            public string OtcodHesabFactorOutBack { get; set; }
            [Display(Name = "کد حساب مرکز")]
            public int? OtcodeMarkaz { get; set; }
            [Display(Name = "کد حساب باسکول")]
            public string OtbasculkalaCode { get; set; }
            [Display(Name = "نرخ کیلو کارگر ورود")]
            public decimal? PriceOfKiloInKargar { get; set; }
            [Display(Name = "نرخ جعبه کارگر ورود")]
            public decimal? PriceOfBoxInKargar { get; set; }
            [Display(Name = "نرخ کیلو کارگر خروج")]
            public decimal? PriceOfKiloOutKargar { get; set; }
            [Display(Name = "نرخ جعبه کارگر خروج")]
            public decimal? PriceOfBoxOutKargar { get; set; }
            [Display(Name = "کد حساب کارگر")]
            public string OtcodHesabKargar { get; set; }
        }
        public  class ASabad
        {

            [Display(Name = "کد فاکتور ورود")]
            public int? OttypeFaktor { get; set; }

            [Display(Name = "کد فاکتور برگشت ورود")]
            public int? OttypeFaktorInBack { get; set; }
            [Display(Name = "کد فاکتور خروج")]
            public int? OttypeFaktorOut { get; set; }
            [Display(Name = "کد فاکتور برگشت خروج")]
            public int? OttypeFaktorOutBack { get; set; }



            [Display(Name = "فقط ورودی")]
            public bool IsEntry { get; set; }

            [Display(Name = "فقط خروجی")]
            public bool IsExit { get; set; } 

            [Display(Name = "نیاز به باسکول")]
            public bool Needbascule { get; set; }

            [Display(Name = "پولی باسکول")]
            public bool basculeMali { get; set; }

            [Display(Name = "نرخ باسکول نقدی")]
            public bool basculeNaghdi { get; set; }



            [Display(Name = "مالی")]
            public bool IsMali { get; set; }
             [Display(Name = "خروجی بر اساس موقعیت کنترل شود")]
            public bool OutControlByLocation { get; set; }
          


            [Display(Name = "نیاز به موقعیت")]
            public bool NeedLocation { get; set; }
            [Display(Name = "سطح موقیت 1تا3")]
            public int LocationLvlRequired { get; set; }
             
            [Display(Name = "کد حساب فاکتور ورود")]
            public string OtcodHesabFactor { get; set; }
            [Display(Name = "کد حساب فاکتور برگشت از ورود")]
            public string OtcodHesabFactorInBack { get; set; }
            [Display(Name = "کد حساب فاکتور خروج")]
            public string OtcodHesabFactorOut { get; set; }
            [Display(Name = "کد حساب فاکتور برگشت از خروج")]
            public string OtcodHesabFactorOutBack { get; set; }
            [Display(Name = "کد حساب مرکز")]
            public int? OtcodeMarkaz { get; set; } 
           
            [Display(Name = "نرخ جعبه کارگر ورود")]
            public decimal? PriceOfBoxInKargar { get; set; }
           
            [Display(Name = "نرخ جعبه کارگر خروج")]
            public decimal? PriceOfBoxOutKargar { get; set; }
            [Display(Name = "کد حساب کارگر")]
            public string OtcodHesabKargar { get; set; }
        }

        public class ABaskul
        {

            [Display(Name = "کد فاکتور ورود")]
            public int? OttypeFaktor { get; set; }
             

            [Display(Name = "پولی باسکول")]
            public bool basculeMali { get; set; }

            [Display(Name = "نرخ باسکول نقدی")]
            public bool basculeNaghdi { get; set; }
             

            [Display(Name = "مالی")]
            public bool IsMali { get; set; }
             
            [Display(Name = "کد حساب فاکتور ورود")]
            public string OtcodHesabFactor { get; set; }
          
            [Display(Name = "کد حساب مرکز")]
            public int? OtcodeMarkaz { get; set; }

            [Display(Name = "شخص پیشفرض")]
            public int? CustomerPishfarz { get; set; }

        }



    }
}
