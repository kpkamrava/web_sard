namespace web_sard.Models.tbls.portage
{
    using System;

    /// <summary>
    /// Defines the <see cref="PortageDocument" />.
    /// </summary>
    public class PortageDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PortageDocument"/> class.
        /// </summary>
        public PortageDocument()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortageDocument"/> class.
        /// </summary>
        /// <param name="row">The row<see cref="web_db.TblPortageDocument"/>.</param>
        public PortageDocument(web_db.TblPortageDocument row)
        {
            this.Date = row.Date;
            this.FkPortage = row.FkPortage;
            this.Id = row.Id;
            this.Kind = row.Kind;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the FkPortage.
        /// </summary>
        public Guid? FkPortage { get; set; }

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Kind.
        /// </summary>
        public string Kind { get; set; }
    }
}
