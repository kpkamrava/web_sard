using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblContractProducts = new HashSet<TblContractProduct>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Ord { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public string OtcodeKala { get; set; }
        public int? OtcodeVahedShomaresh { get; set; }
        public string OtcodeKalaAcc { get; set; }
        public bool? IsNotAc { get; set; }
        public float? WightScale { get; set; }
        public virtual ICollection<TblContractProduct> TblContractProducts { get; set; }
    }
}
