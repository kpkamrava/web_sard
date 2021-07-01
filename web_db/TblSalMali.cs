using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblSalMali
    {
        public TblSalMali()
        {
            TblContractTypes = new HashSet<TblContractType>();
            TblUserSals = new HashSet<TblUserSal>();
        }

        public int Id { get; set; }
        public string Sal { get; set; }
        public bool IsOpen { get; set; }
        public DateTime SalAz { get; set; }
        public DateTime SalTa { get; set; }
        public bool IsOrginal { get; set; }

        public virtual ICollection<TblContractType> TblContractTypes { get; set; }
        public virtual ICollection<TblUserSal> TblUserSals { get; set; }
    }
}
