namespace web_sard.Models.tbls.home
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="countport" />.
    /// </summary>
    public class countport
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public Guid location { get; set; }

        /// <summary>
        /// Gets or sets the KindTitle.
        /// </summary>
        public string KindTitle { get; set; }

        /// <summary>
        /// Gets or sets the Kindcode.
        /// </summary>
        public int Kindcode { get; set; }

        /// <summary>
        /// Gets or sets the WeightNet.
        /// </summary>
        public decimal WeightNet { get; set; }

        /// <summary>
        /// Gets or sets the PackingCount.
        /// </summary>
        public decimal PackingCount { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="countEmroz" />.
    /// </summary>
    public class countEmroz
    {
        /// <summary>
        /// Gets or sets the carDakhel1.
        /// </summary>
        public int carDakhel1 { get; set; }

        /// <summary>
        /// Gets or sets the carDakhel10.
        /// </summary>
        public int carDakhel10 { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="mainclass" />.
    /// </summary>
    public class mainclass
    {
        /// <summary>
        /// Gets or sets the countports.
        /// </summary>
        public List<countport> countports { get; set; }
    }
}
