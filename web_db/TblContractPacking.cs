using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblContractPacking
    {
        public Guid FkContract { get; set; }
        public Guid FkPacking { get; set; }

        public virtual TblContract FkContractNavigation { get; set; }
        public virtual TblPacking FkPackingNavigation { get; set; }
    }
}
