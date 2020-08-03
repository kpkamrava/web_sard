using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblStoreLog
    {
        public Guid FkContractType { get; set; }
        public Guid FkLocation { get; set; }
        public int FkSalmali { get; set; }
        public Guid FkCustomer { get; set; }
        public Guid FkPacking { get; set; }
        public Guid FkProduct { get; set; }
        public Guid FkContract { get; set; }
        public decimal? WeightIn { get; set; }
        public decimal? WeightOut { get; set; }
        public long? CountIn { get; set; }
        public long? CountOut { get; set; }
    }
}
