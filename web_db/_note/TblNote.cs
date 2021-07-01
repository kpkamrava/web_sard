using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_db._note
{
    public class TblNote
    {
        public TblNote() {
            TblNoteRolls =new List<TblNoteRoll>() ;
            TblNoteDates = new List<TblNoteDate>();
            TblNoteRows = new List<TblNoteRows>();
        }
        public enum KindNote
        {
            [web_lib.classAttr.KPvalus(Description = "خبر")] Khabar=1,
            [web_lib.classAttr.KPvalus(Description = "اخطار")] Ekhtar = 2,
            [web_lib.classAttr.KPvalus(Description = "اطلاعیه")] Eteleh = 3, 
        }
        [Key]
        public Guid Id { get; set; }  

        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "عنوان")]
        [Required]
        public string Caption { get; set; }
        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات")]
        [Required]
        public string Txt { get; set; }

       

        public DateTime DateAdd{ get; set; }
        public Guid FkuserAdd { get; set; } 
        [ForeignKey("FkuserAdd")]
        public TblUser UserAdd { get; set; }

        public IList<TblNoteRoll> TblNoteRolls { get; set; }
        public IList<TblNoteDate> TblNoteDates { get; set; }
        public IList<TblNoteRows> TblNoteRows { get; set; }

    }
}
