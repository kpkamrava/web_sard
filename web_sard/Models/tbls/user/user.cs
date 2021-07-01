namespace web_sard.Models.tbls.user
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="user" />.
    /// </summary>
    public class user
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="user"/> class.
        /// </summary>
        public user()
        {
            Roles = new string[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="user"/> class.
        /// </summary>
        /// <param name="model">The model<see cref="web_db.TblUser"/>.</param>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
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

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        [Display(Name = "نام کاربری")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Display(Name = "رمز عبور")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Mob.
        /// </summary>
        [Display(Name = "شماره موبایل")]
        [Required]
        [MinLength(11, ErrorMessage = "حداقل باید 11 حرف باشد")]
        public string Mob { get; set; }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        [Display(Name = "نام")]
        [Required]
        [MinLength(3, ErrorMessage = "حداقل باید 3 حرف باشد")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether isActive.
        /// </summary>
        [Display(Name = "فعال")]
        public bool isActive { get; set; }

        /// <summary>
        /// Gets or sets the Roles.
        /// </summary>
        [Display(Name = "دسترسی")]
        public string[] Roles { get; set; }

        /// <summary>
        /// Gets or sets the Permis.
        /// </summary>
        [Display(Name = "سایر دسترسیها")]
        public web_db.TblUserPermi[] Permis { get; set; }
    }
}
