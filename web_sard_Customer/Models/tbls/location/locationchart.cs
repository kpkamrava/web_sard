using System;
using System.Collections.Generic;
using System.Linq;

namespace web_sard.Models.tbls.location
{
    public class locationchart
    {
        public locationchart()
        { childs = new List<locationchart>(); }
        public locationchart(web_db.sardweb_Context db, web_db.TblLocation row)
        {
            this.code = row.Code;
            this.id = row.Id;
            this.title = row.Title;
            this.wight = row.Wight;
            this.childs = db.TblLocations.Where(a => a.FkP == row.Id).OrderBy(a => a.Code).Select(a => new locationchart(db, a)).ToList();

            this.CodeFull = row.CodeFull;
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public int code { get; set; }
        public decimal? wight { get; set; }

        public string CodeFull { get; set; }
        public List<locationchart> childs { get; set; }
    }
}
