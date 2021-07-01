using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblPortageRow
    {
        public TblPortageRow()
        {
            TblPortageRowInjuries = new HashSet<TblPortageRowInjury>();
        }

        public Guid Id { get; set; }
        public long Code { get; set; }
        public Guid? FkPortage { get; set; }
        public DateTime Date { get; set; }
        public long Count { get; set; }
        public double? WeightOne { get; set; }
        public Guid? FkPacking { get; set; }
        public Guid? FkProduct { get; set; }
        public string CodeLocation { get; set; }
        public string Txt { get; set; }
        public Guid? FkLocation1 { get; set; }
        public Guid? FkLocation2 { get; set; }
        public Guid? FkLocation3 { get; set; }
        public Guid FkUser { get; set; }
        public Guid? FkContract { get; set; }
        public Guid? FkContractType { get; set; }
        public bool IsNimPalet { get; set; }

        public virtual TblContract FkContractNavigation { get; set; }
        public virtual TblPortage FkPortageNavigation { get; set; }
        public virtual ICollection<TblPortageRowInjury> TblPortageRowInjuries { get; set; }
    }
}
