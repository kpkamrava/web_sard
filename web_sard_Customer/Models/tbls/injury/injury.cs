using System;
using System.ComponentModel.DataAnnotations;

namespace web_sard.Models.tbls.injury
{
    public class injury
    {
        public injury() { }
        public injury(web_db.TblInjury row)
        {
            Id = row.Id;
            this.IsActive = row.IsActive;
            this.Ord = row.Ord;
            this.Title = row.Title;



        }
        public Guid Id { get; set; }
        [Display(Name = "عنوان")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "فعال")]

        public bool IsActive { get; set; }
        public int Ord { get; set; }
    }
}
