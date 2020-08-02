using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblInjury
    {
        public TblInjury()
        {
            TblPortageRowInjury = new HashSet<TblPortageRowInjury>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int Ord { get; set; }

        public virtual ICollection<TblPortageRowInjury> TblPortageRowInjury { get; set; }
    }
}
