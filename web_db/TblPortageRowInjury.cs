using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblPortageRowInjury
    {
        public Guid FkInjury { get; set; }
        public Guid FkPortageRow { get; set; }

        public virtual TblInjury FkInjuryNavigation { get; set; }
        public virtual TblPortageRow FkPortageRowNavigation { get; set; }
    }
}
