using System;

#nullable disable

namespace web_db
{
    public partial class TblPortageDocument
    {
        public Guid Id { get; set; }
        public Guid? FkPortage { get; set; }
        public Guid? FkP { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }
        public string Kind { get; set; }
        public string Format { get; set; }

        public virtual TblPortage FkPortageNavigation { get; set; }
    }
}
