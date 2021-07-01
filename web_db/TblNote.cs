using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblNote
    {
        public TblNote()
        {
            TblNoteDates = new HashSet<TblNoteDate>();
            TblNoteRolls = new HashSet<TblNoteRoll>();
            TblNoteRows = new HashSet<TblNoteRow>();
        }

        public Guid Id { get; set; }
        public string Caption { get; set; }
        public string Txt { get; set; }
        public DateTime DateAdd { get; set; }
        public Guid FkuserAdd { get; set; }

        public virtual TblUser FkuserAddNavigation { get; set; }
        public virtual ICollection<TblNoteDate> TblNoteDates { get; set; }
        public virtual ICollection<TblNoteRoll> TblNoteRolls { get; set; }
        public virtual ICollection<TblNoteRow> TblNoteRows { get; set; }
    }
}
