using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.packing
{
    public class packing
    {
        public packing()
        { }
            public packing(web_db.TblPacking row)
        {
            this.Code = row.Code;
            this.Id = row.Id; 
            this.IsActive = row.IsActive;
            this.Title = row.Title;
            this.WightEmpty = row.WightEmpty;
            this.WightFull = row.WightFull;
          

        }
        public Guid Id { get; set; }
        [Display(Name = "کد")]
        [Required]
        public int Code { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "وزن خالی")]
        [Required]
           public decimal WightEmpty { get; set; }

        [Display(Name = "وزن پر")]
        [Required]
        public decimal WightFull { get; set; }

        [Display(Name = "فعال")]

        public bool IsActive { get; set; }

    }
}
