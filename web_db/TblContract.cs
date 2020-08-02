using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblContract
    {
        public TblContract()
        {
            TblContractPacking = new HashSet<TblContractPacking>();
            TblContractProduct = new HashSet<TblContractProduct>();
            TblPortageRow = new HashSet<TblPortageRow>();
        }

        public Guid Id { get; set; }
        public double Code { get; set; }
        public Guid FkCustomer { get; set; }
        public Guid FkContractType { get; set; }
        public long? CountMaxIn { get; set; }
        public decimal? WeightMaxIn { get; set; }
        public int? PercentForOut { get; set; }
        public long? CountMaxOut { get; set; }
        public decimal? WeightMaxOut { get; set; }
        public DateTime Date { get; set; }
        public int FkSalmali { get; set; }
        public DateTime? Azdate { get; set; }
        public DateTime? Tadate { get; set; }
        public string Txt { get; set; }
        public Guid FkUsAdd { get; set; }
        public Guid? FkUsEdit { get; set; }
        public DateTime Dateadd { get; set; }
        public DateTime? Dateedit { get; set; }
        public decimal PriceOfKiloIn { get; set; }
        public decimal PriceOfBoxIn { get; set; }
        public decimal PriceOfKiloOut { get; set; }
        public decimal PriceOfBoxOut { get; set; }
        public decimal? SumInWeight { get; set; }
        public decimal? SumOutWeight { get; set; }
        public long? SumInCount { get; set; }
        public long? SumOutCount { get; set; }

        public virtual TblContractType FkContractTypeNavigation { get; set; }
        public virtual TblCustomer FkCustomerNavigation { get; set; }
        public virtual ICollection<TblContractPacking> TblContractPacking { get; set; }
        public virtual ICollection<TblContractProduct> TblContractProduct { get; set; }
        public virtual ICollection<TblPortageRow> TblPortageRow { get; set; }
    }
}
