using System;

#nullable disable

namespace web_db
{
    public partial class TblContractProduct
    {
        public Guid FkContract { get; set; }
        public Guid FkProduct { get; set; }

        public virtual TblContract FkContractNavigation { get; set; }
        public virtual TblProduct FkProductNavigation { get; set; }
    }
}
