using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblPortageRowLocation
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid Fkuser { get; set; }
        public Guid? FkPortageRow { get; set; }
        public Guid FkLocation1 { get; set; }
        public Guid? FkLocation2 { get; set; }
        public Guid? FkLocation3 { get; set; }

        public virtual TblPortageRow FkPortageRowNavigation { get; set; }
    }
}
