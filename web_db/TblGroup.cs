using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblGroup
    {
        public TblGroup()
        {
            TblCustomerGroups = new HashSet<TblCustomerGroup>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
        public Guid? Fklocation { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblCustomerGroup> TblCustomerGroups { get; set; }
    }
}
