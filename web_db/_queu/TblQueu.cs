using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_lib;
namespace web_db._queu
{
   public class TblQueu
    {
        public enum QueuEnum{
             [classAttr.KPvalus(Description = "ثبت نشده")] Empty = -1,
            [classAttr.KPvalus(Description = " درخواست")] Save =0,
            [classAttr.KPvalus(Description = "تایید شده")] IsActive =10,
            [classAttr.KPvalus(Description = "پایان یافته")] End = 100,
            [classAttr.KPvalus(Description = "رد شده")] EndRad = 200
        }
        public Guid Id { get; set; }
        [Display(Name = "شماره درخواست")]
        public long Code { get; set; }
        [Display(Name ="شماره موبایل")]
        public string mob { get; set; }
        [Display(Name = "کد ارسالی به موبایل")]
        public int? codesend { get; set; }
        [Display(Name = "زمان کد")]
        public DateTime? datecodesend { get; set; }

        [Display(Name = "کد ملی")]
        [Required]
        public string codemeli { get; set; }
        [Display(Name = "نام")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "تاریخ")]
        public DateTime date { get; set; }
        [Display(Name = "مرحله")]
        public QueuEnum KindQueu { get; set; }
        [Display(Name = "کد محصولات")]
        public string CodeMahsuls { get; set; }
        [Display(Name = "محصولات")]
        public string  Mahsuls { get; set; }

        [Display(Name = "آدرس")]
        [Required]
        public string Addras { get; set; }
     
        [Display(Name = "مقدار درخواست")]
        [Required]
        public long? Weight { get; set; }


        [Display(Name = "توضیحات")]
        
        public string Txt { get; set; }

        public string TxtReq { get; set; }

        public Guid? ContractID { get; set; }
        [ForeignKey("ContractID")] 
        public TblContract Contract { get; set; }

    }
}
