using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblContractProduct = new HashSet<TblContractProduct>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Ord { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<TblContractProduct> TblContractProduct { get; set; }
    }
}
