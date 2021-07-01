using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblCar
    {
        public TblCar()
        {
            TblPortages = new HashSet<TblPortage>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public byte[] Img { get; set; }
        public decimal PriceTowBascol { get; set; }
        public bool IsDel { get; set; }

        public virtual ICollection<TblPortage> TblPortages { get; set; }
    }
}
