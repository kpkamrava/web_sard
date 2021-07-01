using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace web_db
{
    public partial class TblPortage
    {
        public TblPortage()
        {
            TblPortageDocuments = new HashSet<TblPortageDocument>();
            TblPortageInjuries = new HashSet<TblPortageInjury>();
            TblPortageMoneys = new HashSet<TblPortageMoney>();
            TblPortageRows = new HashSet<TblPortageRow>();
        }

        public Guid Id { get; set; }
        public int FkSalmali { get; set; }
        public Guid FkContracttype { get; set; }
        public Guid FkCustomer { get; set; }
        [Display(Name = "کد")]
        public long Code { get; set; }
        [Display(Name = "زمان ورود")]
        public DateTime Date1 { get; set; }
        [Display(Name = "زمان خروج")]
        public DateTime? Date2 { get; set; }
        [Display(Name = "توزین اول")]
        public decimal? Weight1 { get; set; }
        [Display(Name = "توزین دوم")]
        public decimal? Weight2 { get; set; }
        public bool? Weight1IsBascul { get; set; }
        public bool? Weight2IsBascul { get; set; }
        [Display(Name = "وزن خالص")]
        public decimal? WeightNet { get; set; }
        [Display(Name = "تعداد")]
        public long? PackingCount { get; set; }
        [Display(Name = "وزن هر عدد")]
        public double? PackingOfWeight { get; set; }
        [Display(Name = "توضیحات")]
        public string Txt { get; set; }
        public Guid FkUsAdd1 { get; set; }
        public Guid? FkUsEdit1 { get; set; }
        public Guid? FkUsAdd2 { get; set; }
        public Guid? FkUsEdit2 { get; set; }
        public DateTime Dateadd1 { get; set; }
        public DateTime? Dateedit1 { get; set; }
        public DateTime? Dateadd2 { get; set; }
        public DateTime? Dateedit2 { get; set; }
        [Display(Name = "راننده")]
        public string CarRanande { get; set; }
        [Display(Name = "شماره ماشین")]
        public string CarShMashin { get; set; }
        [Display(Name = "تماس راننده")]
        public string CarTell { get; set; }
        [Display(Name = "ماشین")]
        public string CarMashin { get; set; }
        public Guid? FkCar { get; set; }
        [Display(Name = "نوع")]
        public int KindCode { get; set; }
        [Display(Name = "نوع")]
        public string KindTitle { get; set; }
        public bool IsDel { get; set; }
        public bool IsEnd { get; set; }
        public string TxtPermit { get; set; }
        public bool? IsPermitOk { get; set; }
        public Guid? FkUsPermit { get; set; }
        public bool? SmsOk { get; set; }
        public string SmsVaziat { get; set; }
        public string OtcodeResid { get; set; }
        public decimal? SumMoney { get; set; }
        public bool? SaveFaktor { get; set; }

        public virtual TblCar FkCarNavigation { get; set; }
        public virtual TblContractType FkContracttypeNavigation { get; set; }
        public virtual TblCustomer FkCustomerNavigation { get; set; }
        public virtual ICollection<TblPortageDocument> TblPortageDocuments { get; set; }
        public virtual ICollection<TblPortageInjury> TblPortageInjuries { get; set; }
        public virtual ICollection<TblPortageMoney> TblPortageMoneys { get; set; }
        public virtual ICollection<TblPortageRow> TblPortageRows { get; set; }
    }
}
