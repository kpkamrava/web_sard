using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblTemp
    {
        public TblTemp()
        {
            TblTempRows = new HashSet<TblTempRow>();
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid FkuserAdd { get; set; }
        public int FkSalMali { get; set; }
        public Guid? FkuserTaiid { get; set; }
        public DateTime? DateTaiid { get; set; }
        public string Txt { get; set; }
        public byte[] Sign { get; set; }
        public byte[] SignTaiid { get; set; }

        public virtual TblUser FkuserAddNavigation { get; set; }
        public virtual TblUser FkuserTai { get; set; }
        public virtual ICollection<TblTempRow> TblTempRows { get; set; }
    }
}
