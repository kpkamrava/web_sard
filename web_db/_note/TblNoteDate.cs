using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_lib;
namespace web_db._note
{
   public class TblNoteDate
    {
        public enum Kind
        {
            [web_lib.classAttr.KPvalus(Description = "هرروز")] Harroz,
            [web_lib.classAttr.KPvalus(Description = "هرهفته")] HarHafte,
            [web_lib.classAttr.KPvalus(Description = "هر2هفته")] Har2Hafte,
            [web_lib.classAttr.KPvalus(Description = "هر4هفته")] Har4Week,
            [web_lib.classAttr.KPvalus(Description = "هرماه")] HarMah

        }
        [Key]
        public Guid Id { get; set; }
        [Display(Name = "ازتاریخ")]
        [Column(TypeName = "date")]
        public DateTime? FromDate { get; set; }

        [Display(Name = "ازتاریخ")]
        [NotMapped]
        public string _FromDate { get; set; }

        [Display(Name = "تاتاریخ")]
        [Column(TypeName = "date")]
        public DateTime? ToDate { get; set; }
        [NotMapped]
        [Display(Name = "تاتاریخ")]
        public string _ToDate { get; set; }

        [Display(Name = "بصورت")]
        public Kind KindBesorat { get; set; }
        [Display(Name = "ارسال پیامک")]
        public bool  SendSms { get; set; }

        public Guid FkTblNote { get; set; }
        public TblNote TblNote { get; set; }

       

    }
}
