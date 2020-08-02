using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblLocation
    {
        public TblLocation()
        {
            InverseFkPNavigation = new HashSet<TblLocation>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Ord { get; set; }
        public Guid? FkP { get; set; }
        public int Kind { get; set; }
        public int Code { get; set; }
        public bool Isdell { get; set; }
        public decimal? Wight { get; set; }
        public string CodeFull { get; set; }
        public bool ForProduct { get; set; }

        public virtual TblLocation FkPNavigation { get; set; }
        public virtual ICollection<TblLocation> InverseFkPNavigation { get; set; }
    }
}
