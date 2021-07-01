namespace web_sard.Models.tbls.car
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="car" />.
    /// </summary>
    public class car
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="car"/> class.
        /// </summary>
        public car()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="car"/> class.
        /// </summary>
        /// <param name="row">The row<see cref="web_db.TblCar"/>.</param>
        public car(web_db.TblCar row)
        {
            this.Id = row.Id;

            this.Title = row.Title;
            //this.Img =  row.Img;
            this.IsDel = row.IsDel;
            this.PriceTowBascol = row.PriceTowBascol;
        }

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
        /// Gets or sets the Img.
        /// </summary>
        [Display(Name = "تصویر")]
        public IFormFile Img { get; set; }

        /// <summary>
        /// Gets or sets the PriceTowBascol.
        /// </summary>
        [Display(Name = "نرخ باسکول"),]
        public decimal PriceTowBascol { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsDel.
        /// </summary>
        [Display(Name = "حذف")]
        public bool IsDel { get; set; }
    }
}
