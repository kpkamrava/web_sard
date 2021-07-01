namespace web_sard.Models.tbls.location
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="location" />.
    /// </summary>
    public class location
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the FkP.
        /// </summary>
        public Guid? FkP { get; set; }

        /// <summary>
        /// Gets or sets the ptitle.
        /// </summary>
        public string ptitle { get; set; }

        /// <summary>
        /// Gets or sets the pcode.
        /// </summary>
        public int pcode { get; set; }

        /// <summary>
        /// Gets or sets the Kind.
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        [Display(Name = "کد")]
        [Required]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the wight.
        /// </summary>
        [Display(Name = "ظرفیت (کیلو)")]

        public decimal? wight { get; set; }

        /// <summary>
        /// Gets or sets the OtcodeAnbar.
        /// </summary>
        [Display(Name = "کد انبار سایر سیستم ها")]
        public int? OtcodeAnbar { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether isdell.
        /// </summary>
        [Display(Name = "غیر فعال")]
        public bool isdell { get; set; }

        /// <summary>
        /// Gets or sets the CodeFull.
        /// </summary>
        public string CodeFull { get; set; }
    }
}
