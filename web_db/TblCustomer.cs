using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace web_db
{
    public partial class TblCustomer
    {
        public TblCustomer()
        {
            TblContracts = new HashSet<TblContract>();
            TblCustomerGroups = new HashSet<TblCustomerGroup>();
            TblPortageMoneys = new HashSet<TblPortageMoney>();
            TblPortages = new HashSet<TblPortage>();
        }

        public Guid Id { get; set; }
        [Display(Name = "کد")]
        public int Code { get; set; }
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "موبایل")]
        public string Mob { get; set; }
        [Display(Name = "آدرس")]
        public string Addras { get; set; }
        public bool IsEnable { get; set; }
        [Display(Name = "کد سایر سیستم")]
        public string IdOtherSystem { get; set; }
        public int FkSalmali { get; set; }
        public string Password { get; set; }
        public DateTime? Datesendpassword { get; set; }
        public DateTime? Dateloginlast { get; set; }

        public virtual ICollection<TblContract> TblContracts { get; set; }
        public virtual ICollection<TblCustomerGroup> TblCustomerGroups { get; set; }
        public virtual ICollection<TblPortageMoney> TblPortageMoneys { get; set; }
        public virtual ICollection<TblPortage> TblPortages { get; set; }
    }
}
