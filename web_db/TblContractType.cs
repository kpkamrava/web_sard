using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblContractType
    {
        public TblContractType()
        {
            TblContract = new HashSet<TblContract>();
            TblPortage = new HashSet<TblPortage>();
        }

        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }
        public bool IsProduct1Packing0 { get; set; }
        public bool Needbascule { get; set; }
        public bool IsMali { get; set; }
        public bool OutControlByContract { get; set; }
        public bool OutControlByLocation { get; set; }
        public bool OutControlByPercent { get; set; }
        public bool NeedLocation { get; set; }
        public int LocationLvlRequired { get; set; }

        public virtual ICollection<TblContract> TblContract { get; set; }
        public virtual ICollection<TblPortage> TblPortage { get; set; }
    }
}
