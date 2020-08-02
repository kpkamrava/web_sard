using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblPortage
    {
        public TblPortage()
        {
            TblPortageDocument = new HashSet<TblPortageDocument>();
            TblPortageRow = new HashSet<TblPortageRow>();
        }

        public Guid Id { get; set; }
        public int FkSalmali { get; set; }
        public Guid FkContracttype { get; set; }
        public Guid FkCustomer { get; set; }
        public long Code { get; set; }
        public DateTime Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public decimal? Weight1 { get; set; }
        public decimal? Weight2 { get; set; }
        public bool? Weight1IsBascul { get; set; }
        public bool? Weight2IsBascul { get; set; }
        public decimal? WeightNet { get; set; }
        public long? PackingCount { get; set; }
        public decimal? PackingOfWeight { get; set; }
        public string Txt { get; set; }
        public Guid FkUsAdd1 { get; set; }
        public Guid? FkUsEdit1 { get; set; }
        public Guid? FkUsAdd2 { get; set; }
        public Guid? FkUsEdit2 { get; set; }
        public DateTime Dateadd1 { get; set; }
        public DateTime? Dateedit1 { get; set; }
        public DateTime? Dateadd2 { get; set; }
        public DateTime? Dateedit2 { get; set; }
        public string CarRanande { get; set; }
        public string CarShMashin { get; set; }
        public string CarTell { get; set; }
        public string CarMashin { get; set; }
        public Guid? FkCar { get; set; }
        public int KindCode { get; set; }
        public string KindTitle { get; set; }
        public bool IsDel { get; set; }
        public bool IsEnd { get; set; }
        public string TxtPermit { get; set; }
        public bool? IsPermitOk { get; set; }
        public Guid? FkUsPermit { get; set; }

        public virtual TblCar FkCarNavigation { get; set; }
        public virtual TblContractType FkContracttypeNavigation { get; set; }
        public virtual TblCustomer FkCustomerNavigation { get; set; }
        public virtual ICollection<TblPortageDocument> TblPortageDocument { get; set; }
        public virtual ICollection<TblPortageRow> TblPortageRow { get; set; }
    }
}
