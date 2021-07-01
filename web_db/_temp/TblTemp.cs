using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_lib;
#nullable disable

namespace web_db._temp
{

    [Index("FkSalMali")]
    public partial class TblTemp
    {
        public TblTemp()
        {
            TblTempRows = new HashSet<TblTempRow>();
        }
        [Key]
       
        public Guid Id { get; set; }
        [Display(Name = "تاریخ")]
        [Column(TypeName = "date")]
        [Required]
        public DateTime Date { get; set; }
        [NotMapped]
        [Display(Name = "تاریخ")]
        [Required]
        public string _Date { get { return Date.ToPersianDatenull(); } set { Date = value.ToDate(); } }
        [Display(Name = "کاربر ثبت کننده")]
         
        [ForeignKey("FkuserAdd")]
        public Guid FkuserAdd { get; set; }
        [ForeignKey("FkuserAdd")]
        
        public TblUser UserAdd { get; set; }
      
        public int FkSalMali { get; set; }


        [Display(Name = "کاربر تایید کننده")]
        [ForeignKey("FkuserTaiid")]
         
        public Guid? FkuserTaiid { get; set; }
        [ForeignKey("FkuserTaiid")]
        public TblUser UserTaiid { get; set; }



        [Display(Name = "زمان تایید")]
        public DateTime? DateTaiid { get; set; }
        [Display(Name = "توضیحات")]
        [Column(TypeName = "nvarchar(200)")]

        public string Txt { get; set; }
        [Display(Name = "امضا")]
        public byte[] Sign { get; set; }
        [Display(Name = "امضای تایید")]
        public byte[] SignTaiid { get; set; }

   
        public ICollection<TblTempRow> TblTempRows { get; set; }

    }
}
