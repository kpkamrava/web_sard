using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblTempRow
    {
        public Guid Id { get; set; }
        public Guid Fktemp { get; set; }
        public Guid? FkLocation1 { get; set; }
        public Guid? FkLocation2 { get; set; }
        public Guid? FkLocation3 { get; set; }
        public string Location { get; set; }
        public DateTime DateAdd { get; set; }
        public DateTime? DateEdit { get; set; }
        public Guid FkUserAdd { get; set; }
        public Guid? FkUserEdit { get; set; }
        public decimal? MinDama { get; set; }
        public decimal? MaxDama { get; set; }
        public decimal? MotorDama { get; set; }
        public decimal? R { get; set; }
        public string O { get; set; }
        public bool? M { get; set; }
        public bool? A { get; set; }
        public bool? Nezafat { get; set; }
        public bool? SamPashi { get; set; }
        public bool? ColorZani { get; set; }
        public string Txt { get; set; }
        public bool? Formalin { get; set; }
        public bool? Tahvie { get; set; }

        public virtual TblTemp FktempNavigation { get; set; }
    }
}
