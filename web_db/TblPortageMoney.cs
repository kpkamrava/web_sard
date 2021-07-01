using System;

#nullable disable

namespace web_db
{
    public partial class TblPortageMoney
    {
        public Guid Id { get; set; }
        public Guid FkPortage { get; set; }
        public Guid FkCustomer { get; set; }
        public Guid? FkContract { get; set; }
        public Guid FkContractType { get; set; }
        public DateTime Date { get; set; }
        public Guid? Fklocation { get; set; }
        public Guid? FkProduct { get; set; }
        public Guid? FkPacking { get; set; }
        public Guid FkCar { get; set; }
        public decimal Weight { get; set; }
        public decimal PriceOneWeight { get; set; }
        public decimal PriceSum { get; set; }
        public string Txt { get; set; }
        public string Kind { get; set; }

        public virtual TblContract FkContractNavigation { get; set; }
        public virtual TblCustomer FkCustomerNavigation { get; set; }
        public virtual TblPortage FkPortageNavigation { get; set; }
    }
}
