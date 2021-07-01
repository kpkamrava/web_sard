using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace web_db._note
{
   public class TblNoteRoll
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string  ForUserroll { get; set; }
        public Guid? ForUserId { get; set; } 
        public Guid FkTblNote { get; set; }
        public TblNote TblNote { get; set; }


    }
}
