using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.product
{
    public class Product
    {
        public Product() { }
        public Product(web_db.TblProduct row) {

            this.id = row.Id;
            this.isActive = row.IsActive;
            this.title = row.Title;
            this.code = row.Code; 
        }

        public Guid id { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string title { get; set; }
        [Display(Name = "فعال")]
        [Required]
        public bool isActive { get; set; }
         

        [Display(Name = "کد")]
        [Required]
        public int code { get; set; }
        // public IFormFile img { get; set; }
    }
}
