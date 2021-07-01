using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace web_sard.Models.tbls.user
{
    public class user
    {
        public user() { Roles = new string[0]; }
        public user(web_db.TblUser model, web_db.sardweb_Context db)
        {

            Id = model.Id;
            Mob = model.Mob;
            Password = model.Password;
            Roles = model.Roles.Split(",");
            Title = model.Title;
            Username = model.Username;
            this.isActive = model.IsActive;

            Permis = db.TblUserPermis.Where(a => a.FkUser == model.Id).ToArray();


        }

        public Guid Id { get; set; }
        [Display(Name = "نام کاربری")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Username { get; set; }
        [Display(Name = "رمز عبور")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Password { get; set; }
        [Display(Name = "شماره موبایل")]
        [Required]
        [MinLength(11, ErrorMessage = "حداقل باید 11 حرف باشد")]
        public string Mob { get; set; }
        [Display(Name = "نام")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Title { get; set; }

        [Display(Name = "فعال")]
        public bool isActive { get; set; }

        [Display(Name = "دسترسی")]
        public string[] Roles { get; set; }
        [Display(Name = "سایر دسترسیها")]
        public web_db.TblUserPermi[] Permis { get; set; }
    }
}
