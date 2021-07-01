using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace web_sard.Models.tbls.product
{
    public class Product
    {
        public Product() { }
        public Product(web_db.TblProduct row, web_db.sardweb_Context db, int salmali)
        {

            this.id = row.Id;
            this.isActive = row.IsActive;
            this.title = row.Title;
            this.code = row.Code;

            this.OtcodeKala = row.OtcodeKala;
            this.OtcodeKalaAcc = row.OtcodeKalaAcc;
            this.OtcodeVahedShomaresh = row.OtcodeVahedShomaresh;
            {

                InContract = db.TblContracts.Include(a => a.TblContractProducts).Where(a => a.FkSalmali == salmali && a.TblContractProducts.Any(S => S.FkProduct == this.id)).Sum(a => a.WeightMaxIn) ?? 0;
                OutContract = db.TblContracts.Include(a => a.TblContractProducts).Where(a => a.FkSalmali == salmali && a.TblContractProducts.Any(S => S.FkProduct == this.id)).Sum(a => a.WeightMaxOut) ?? 0;

            }
        }

        public Guid id { get; set; }
        [Display(Name = "عنوان")]
        [MaxLength(100)]
        [Required]
        public string title { get; set; }
        [Display(Name = "فعال")]


        public bool isActive { get; set; }


        [Display(Name = "کد")]
        [Required]
        public int code { get; set; }


        [Display(Name = "کد کالا سایر سیستم")]
        public string OtcodeKala { get; set; }
        [Display(Name = "کد مالی کالا سایر سیستم")]
        public string OtcodeKalaAcc { get; set; }
        [Display(Name = "واحد شمارنده سایر سیستم")]
        public int? OtcodeVahedShomaresh { get; set; }


        [Display(Name = "قرارداد ورودی")]
        public decimal InContract { get; set; }
        [Display(Name = "قرارداد خروجی")]
        public decimal OutContract { get; set; }
        // public IFormFile img { get; set; }
    }
}
