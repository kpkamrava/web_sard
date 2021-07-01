using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblInjury
    {
        public TblInjury()
        {
            TblPortageInjuries = new HashSet<TblPortageInjury>();
            TblPortageRowInjuries = new HashSet<TblPortageRowInjury>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public int Ord { get; set; }

        public virtual ICollection<TblPortageInjury> TblPortageInjuries { get; set; }
        public virtual ICollection<TblPortageRowInjury> TblPortageRowInjuries { get; set; }
    }
}
