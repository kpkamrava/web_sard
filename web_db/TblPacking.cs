using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblPacking
    {
        public TblPacking()
        {
            TblContractPacking = new HashSet<TblContractPacking>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal WightFull { get; set; }
        public decimal WightEmpty { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblContractPacking> TblContractPacking { get; set; }
    }
}
