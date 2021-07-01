using System;

#nullable disable

namespace web_db
{
    public partial class TblPortageInjury
    {
        public Guid FkPortage { get; set; }
        public Guid FkInjury { get; set; }

        public virtual TblInjury FkInjuryNavigation { get; set; }
        public virtual TblPortage FkPortageNavigation { get; set; }
    }
}
