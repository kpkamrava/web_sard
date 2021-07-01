using System;

#nullable disable

namespace web_db
{
    public partial class TblUserSal
    {
        public Guid FkUser { get; set; }
        public int FkSal { get; set; }

        public virtual TblSalMali FkSalNavigation { get; set; }
        public virtual TblUser FkUserNavigation { get; set; }
    }
}
