using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblNoteRow
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string ForUserroll { get; set; }
        public Guid? ForUserId { get; set; }
        public Guid FkTblNote { get; set; }
        public bool? SendSms { get; set; }

        public virtual TblNote FkTblNoteNavigation { get; set; }
    }
}
