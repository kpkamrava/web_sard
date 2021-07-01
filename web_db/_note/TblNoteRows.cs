using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace web_db._note
{
    [Index("Date")]
    [Index("ForUserId")]
    [Index("ForUserroll")]
    public class TblNoteRows
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string ForUserroll { get; set; }
        public bool SendSms { get; set; }
        public Guid? ForUserId { get; set; }

        public Guid FkTblNote { get; set; }
        public TblNote TblNote { get; set; } 
    
    }
}
