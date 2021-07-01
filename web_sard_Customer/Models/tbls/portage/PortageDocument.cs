using System;

namespace web_sard.Models.tbls.portage
{
    public class PortageDocument
    {

        public PortageDocument() { }
        public PortageDocument(web_db.TblPortageDocument row)
        {
            this.Date = row.Date;
            this.FkPortage = row.FkPortage;
            this.Id = row.Id;
            this.Kind = row.Kind;

        }
        public Guid Id { get; set; }
        public Guid? FkPortage { get; set; }
        public DateTime Date { get; set; }
        public string Kind { get; set; }



    }
}
