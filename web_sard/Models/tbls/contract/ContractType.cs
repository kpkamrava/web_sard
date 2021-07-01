namespace web_sard.Models.tbls.contract
{
    using System;

    /// <summary>
    /// Defines the <see cref="ContractType" />.
    /// </summary>
    public class ContractType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractType"/> class.
        /// </summary>
        public ContractType()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContractType"/> class.
        /// </summary>
        /// <param name="r">The r<see cref="web_db.TblContractType"/>.</param>
        public ContractType(web_db.TblContractType r)
        {
            this.Code = r.Code;

            //this.IsEntry = r.IsEntry;
            //this.IsExit = r.IsExit;
            //this.IsProduct1Packing0 = r.IsProduct1Packing0;
            //this.Needbascule = r.Needbascule;
            this.Title = r.Title;
            this.Id = r.Id;
            //this.OutControlByContract = r.OutControlByContract;
            //this.OutControlByLocation = r.OutControlByLocation;
            //this.OutControlByPercent = r.OutControlByPercent;
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsEntry.
        /// </summary>
        public bool IsEntry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsExit.
        /// </summary>
        public bool IsExit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsProduct1Packing0.
        /// </summary>
        public bool IsProduct1Packing0 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Needbascule.
        /// </summary>
        public bool Needbascule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OutControlByContract.
        /// </summary>
        public bool OutControlByContract { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OutControlByLocation.
        /// </summary>
        public bool OutControlByLocation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether OutControlByPercent.
        /// </summary>
        public bool OutControlByPercent { get; set; }
    }
}
