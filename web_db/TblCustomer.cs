using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblContract = new HashSet<TblContract>();
            TblPortage = new HashSet<TblPortage>();
        }

        public Guid Id { get; set; }
        public int Code { get; set; }
        public string NationalCode { get; set; }
        public string Title { get; set; }
        public string Mob { get; set; }
        public string Addras { get; set; }
        public bool IsEnable { get; set; }
        public string IdOtherSystem { get; set; }
        public int FkSalmali { get; set; }

        public virtual ICollection<TblContract> TblContract { get; set; }
        public virtual ICollection<TblPortage> TblPortage { get; set; }
    }
}
