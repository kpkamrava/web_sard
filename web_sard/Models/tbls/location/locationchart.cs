namespace web_sard.Models.tbls.location
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="locationchart" />.
    /// </summary>
    public class locationchart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="locationchart"/> class.
        /// </summary>
        public locationchart()
        {
            childs = new List<locationchart>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="locationchart"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        /// <param name="row">The row<see cref="web_db.TblLocation"/>.</param>
        public locationchart(web_db.sardweb_Context db, web_db.TblLocation row)
        {
            this.code = row.Code;
            this.id = row.Id;
            this.title = row.Title;
            this.wight = row.Wight;
            this.childs = db.TblLocations.Where(a => a.FkP == row.Id).OrderBy(a => a.Code).Select(a => new locationchart(db, a)).ToList();

            this.CodeFull = row.CodeFull;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// Gets or sets the wight.
        /// </summary>
        public decimal? wight { get; set; }

        /// <summary>
        /// Gets or sets the CodeFull.
        /// </summary>
        public string CodeFull { get; set; }

        /// <summary>
        /// Gets or sets the childs.
        /// </summary>
        public List<locationchart> childs { get; set; }
    }
}
