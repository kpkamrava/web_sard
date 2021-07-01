using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace web_sard.Models.tbls.car
{
    public class car
    {

        public car() { }
        public car(web_db.TblCar row)
        {
            this.Id = row.Id;

            this.Title = row.Title;
            //this.Img =  row.Img;
            this.IsDel = row.IsDel;
            this.PriceTowBascol = row.PriceTowBascol;




        }
        public Guid Id { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Img { get; set; }
        [Display(Name = "نرخ باسکول"),]
        public decimal PriceTowBascol { get; set; }
        [Display(Name = "حذف")]
        public bool IsDel { get; set; }

    }
}
