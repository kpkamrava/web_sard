using System;

#nullable disable

namespace web_db
{
    public partial class TblStoreLog
    {
        public Guid Id { get; set; }
        public Guid FkContractType { get; set; }
        public Guid? FkLocation1 { get; set; }
        public Guid? FkLocation2 { get; set; }
        public Guid? FkLocation3 { get; set; }
        public int FkSalmali { get; set; }
        public Guid? FkCustomer { get; set; }
        public Guid? FkPacking { get; set; }
        public Guid? FkProduct { get; set; }
        public Guid? FkContract { get; set; }
        public decimal? Weight { get; set; }
        public long Count { get; set; }
        public long? CountIn { get; set; }
        public long? CountOut { get; set; }
        public decimal? WeightIn { get; set; }
        public decimal? WeightOut { get; set; }
        public long? CountMovement { get; set; }
        public decimal? WeightMovement { get; set; }
    }
}
