namespace web_sard.Models.tbls.packing
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="packing" />.
    /// </summary>
    public class packing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="packing"/> class.
        /// </summary>
        public packing()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="packing"/> class.
        /// </summary>
        /// <param name="row">The row<see cref="web_db.TblPacking"/>.</param>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        /// <param name="salmali">The salmali<see cref="int"/>.</param>
        public packing(web_db.TblPacking row, web_db.sardweb_Context db, int salmali)
        {
            this.Code = row.Code;
            this.Id = row.Id;
            this.IsActive = row.IsActive;
            this.Title = row.Title;
            this.WightEmpty = row.WightEmpty;
            this.WightFull = row.WightFull;
            this.WightScale = row.WightScale;
            this.ForContractType  = row.ForContractType();
            this.OtcodeKala = row.OtcodeKala;
            this.OtcodeKalaAcc = row.OtcodeKalaAcc;
            this.OtcodeVahedShomaresh = row.OtcodeVahedShomaresh;
            this.IsNotAc = row.IsNotAc ?? false;
            {

                InContract = db.TblContracts.Include(a => a.TblContractPackings).Where(a => a.FkSalmali == salmali && a.TblContractPackings.Any(S => S.FkPacking == this.Id)).Sum(a => a.CountMaxIn) ?? 0;
                OutContract = db.TblContracts.Include(a => a.TblContractPackings).Where(a => a.FkSalmali == salmali && a.TblContractPackings.Any(S => S.FkPacking == this.Id)).Sum(a => a.CountMaxOut) ?? 0;


            }
        }

     
        public Guid Id { get; set; }

      
        [Display(Name = "کد")]
        [Required]
        public int Code { get; set; }

      
        [Display(Name = "عنوان")]
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

       
        [Display(Name = "وزن خالی")]
        [Required]
        public decimal WightEmpty { get; set; }

       
        [Display(Name = "وزن پر")]
        [Required]
        public decimal WightFull { get; set; }
      
        [Display(Name = "وزن معیار")] 
        public float? WightScale { get; set; }

       
        [Display(Name = "فعال")]

        public bool IsActive { get; set; }

      
        [Display(Name = "نوع بسته بندی سردخانه")]


        public List<web_db.TblContractType.KindCotractTypeEnum> ForContractType  { get; set; }

       
        [Display(Name = "کد کالا سایر سیستم")]
        public string OtcodeKala { get; set; }

       
        [Display(Name = "کد مالی کالا سایر سیستم")]
        public string OtcodeKalaAcc { get; set; }

       
        [Display(Name = "واحد شمارنده سایر سیستم")]
        public int? OtcodeVahedShomaresh { get; set; }

        
        [Display(Name = "قرارداد ورودی")]
        public long InContract { get; set; }

       
        [Display(Name = "قرارداد خروجی")]
        public long OutContract { get; set; }
        [Display(Name = "بدون محاسبه در تجمیع")]
        public bool IsNotAc { get; set; }
    }
}
