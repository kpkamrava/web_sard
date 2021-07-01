namespace web_sard.Models.tbls.customer
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="customer" />.
    /// </summary>
    public class customer
    {

        public customer()
        {
        }


        public customer(web_db.TblCustomer row, web_db.sardweb_Context db, bool grops = true)
        {

            this.Addras = row.Addras;
            this.Code = row.Code;
            this.FkSalmali = row.FkSalmali;
            this.Id = row.Id;
            this.IdOtherSystem = row.IdOtherSystem;
            this.IsEnable = row.IsEnable;
            this.Mob = row.Mob;
            this.NationalCode = row.NationalCode;
            this.Title = row.Title;
            this.datelastlogin = row.Dateloginlast;
            this.codesContract = (row.TblContracts ?? (ICollection<web_db.TblContract>)db.TblContracts.Where(a => a.FkCustomer == row.Id)).Select(a => a.Code).ToArray();
            this.listfkGroup = new Guid[] { };
            this.listGroup = new string[] { };
            if (grops)
            {
                var v = db.TblCustomerGroups.Where(a => a.FkCustumer == this.Id).Include(a => a.FkGroupNavigation);
                this.listfkGroup = v.Select(a => a.FkGroup).ToArray();
                this.listGroup = v.Select(a => a.FkGroupNavigation.Title).ToArray();

            }


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

        public DateTime? datelastlogin { get; set; }


        public int FkSalmali { get; set; }


        public Guid[] listfkGroup { get; set; }

        public string[] listGroup { get; set; }
        public decimal WOutMandeContract { get; internal set; }
        public decimal WInMaxContract { get; internal set; }
        public decimal WInSum { get; internal set; }
        public decimal WOutSum { get; internal set; }
        public decimal WOutMaxContract { get; internal set; }
        public decimal WInMandeContract { get; internal set; }
        public long COutMandeContract { get; internal set; }
        public long CInMaxContract { get; internal set; }
        public long CInSum { get; internal set; }
        public long COutSum { get; internal set; }
        public long COutMaxContract { get; internal set; }
        public long CInMandeContract { get; internal set; }
    }
}
