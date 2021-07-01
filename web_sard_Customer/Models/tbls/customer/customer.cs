using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace web_sard.Models.tbls.customer
{
    public class customer
    {
        public customer() { }
        public static web_sard.Models.tbls.customer.customer get(web_db.TblCustomer row, web_db.sardweb_Context db)
        {
            var r = new web_sard.Models.tbls.customer.customer();



            if (row != null)
            {
                r = new web_sard.Models.tbls.customer.customer
                {
                    Addras = row.Addras,
                    Code = row.Code,
                    FkSalmali = row.FkSalmali,
                    Id = row.Id,
                    IdOtherSystem = row.IdOtherSystem,
                    IsEnable = row.IsEnable,
                    Mob = row.Mob,
                    NationalCode = row.NationalCode,
                    Title = row.Title,
                    codesContract = (row.TblContracts ?? (ICollection<web_db.TblContract>)db.TblContracts.Where(a => a.FkCustomer == row.Id)).Select(a => a.Code).ToArray()
                };
                var v = db.TblCustomerGroups.Where(a => a.FkCustumer == r.Id).Include(a => a.FkGroupNavigation);
                r.listfkGroup = v.Select(a => a.FkGroup).ToArray();
                r.listGroup = v.Select(a => a.FkGroupNavigation.Title).ToArray();
            }
            return r;

        }

        public Guid Id { get; set; }
        [DisplayName("کد")]
        public int Code { get; set; }
        [DisplayName("کد ملی")]
        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        public string NationalCode { get; set; }
        [DisplayName("عنوان")]
        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string Title { get; set; }
        [DisplayName("موبایل")]
        [Required]
        public string Mob { get; set; }
        [DisplayName("آدرس")]
        [MaxLength(250)]

        public string Addras { get; set; }
        [DisplayName("فعال")]
        public bool IsEnable { get; set; }
        public double[] codesContract { get; set; }
        [DisplayName("آیدی سایر سیستمها")]
        public string IdOtherSystem { get; set; }
        public int FkSalmali { get; set; }
        public Guid[] listfkGroup { get; set; }
        public string[] listGroup { get; set; }

    }
}
