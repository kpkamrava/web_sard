using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblNoteDate
    {
        public Guid Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int KindBesorat { get; set; }
        public bool SendSms { get; set; }
        public Guid FkTblNote { get; set; }

        public virtual TblNote FkTblNoteNavigation { get; set; }
    }
}
