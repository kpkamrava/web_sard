using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace web_db._temp
{

    public class TblTempRow
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Fktemp { get; set; }
        public Guid? FkLocation1 { get; set; }
        public Guid? FkLocation2 { get; set; }
        public Guid? FkLocation3 { get; set; }
        public string  Location  { get; set; }

        public DateTime DateAdd { get; set; }
        public DateTime? DateEdit { get; set; }
        public Guid FkUserAdd { get; set; }
        public Guid? FkUserEdit { get; set; }
         

        [Display(Name = "حداقل دما")]
       [DisplayFormat(DataFormatString = "{#.##}")]
          public decimal? MinDama { get; set; }

        [Display(Name = "حداکثر دما")]
        [DisplayFormat(DataFormatString = "{#.##}")]
          public decimal? MaxDama { get; set; }

        [Display(Name = "دمای موتورخانه")]
       [DisplayFormat(DataFormatString = "{#.##}")]
     
        public decimal? MotorDama { get; set; }

        [Display(Name = "رطوبت")]
       [DisplayFormat(DataFormatString = "{#.##}")] 
        public decimal? R { get; set; }

        [Display(Name = "ازن")]
        public string O { get; set; }

        [Display(Name = "مه پاش")]
        public bool M { get; set; }

        [Display(Name = "آبپاشی")]
        public bool A { get; set; }

        [Display(Name = "نظافت")]
        public bool Nezafat { get; set; }

        [Display(Name = "سمپاشی")]
        public bool SamPashi { get; set; }


        [Display(Name = "کلرزنی")]
        public bool ColorZani { get; set; }


        [Display(Name = "تهویه")]
    
        public bool Tahvie { get; set; }
        [Display(Name = "فرمالین")]
        public bool Formalin { get; set; }



        [Display(Name = "توضیحات")]
        public string Txt { get; set; }

        //public virtual TblUser FkUserAddNavigation { get; set; }
        [ForeignKey("Fktemp")]

        public virtual TblTemp FktempNavigation { get; set; }
    }
}
