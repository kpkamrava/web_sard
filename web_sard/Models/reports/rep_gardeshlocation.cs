using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_lib;

namespace web_sard.Models.reports
{
    public class rep_gardeshlocation
    {
        public class gardeshlocationModel
        {
            [Display(Name = " موقعیت")]
            [Required]
            public Guid[] locations { get; set; }
            [Display(Name = "محصولات")]
            public Guid[] prodocts { get; set; }
            public string[] prodoctsSTR { get { return prodocts != null ? prodocts.Select(a => Models.cl._ListProduct.Single(s => s.Id == a).Title).ToArray() : new string[0]; ; } }
            [Display(Name = " بسته بندی ")]
            public Guid[] pakings { get; set; }

            [Display(Name = " نوع عملیات ")]
            [Required]
            public int[] kindPortage { get; set; }


           


            public string[] pakingsSTR { get { return pakings != null ? pakings.Select(a => Models.cl._ListPacking.Single(s => s.Id == a).Title).ToArray() : new string[0]; } }
            [Display(Name = "از تاریخ")]
            [Required]
            public string d1 { get; set; }
            [Display(Name = "تا تاریخ")]
            [Required]
            public string d2 { get; set; }
            [Display(Name = "فرمت چاپ")]
            [Required]
            public string kind { get; set; }
            public bool isvalid { get; set; }
            public Dictionary<string, string> files { get; set; }
        }
        public class gardeshlocationReport
        {
            public gardeshlocationModel model { get; set; }
            public IEnumerable<gardeshlocationRows> rows { get; set; }


        }
        public class gardeshlocationRows
        {
            public static gardeshlocationRows get(web_db.TblPortageRow row, web_db.sardweb_Context db)
            {
                var por = db.TblPortage.Find(row.FkPortage);
                var con = db.TblContract.Find(row.FkContract);
                var contype = db.TblContractType.Find(por.FkContracttype);
                var cus = db.TblCustomer.Find(con.FkCustomer);


                return new gardeshlocationRows {
                    Contract = cus.Title + $" ({cus.Code})",
                Contractcode = con.Code,
                ContractType = contype.Title,
                ContractTypecode = contype.Code,
                Count = row.Count,
                date = por.Date1,
                datestr = por.Date1.ToPersianDate(),
                IdPort = row.FkPortage,
                IdRow=row.Id,
                Location=row.CodeLocation,
                Packing=row.FkPacking.HasValue?(cl._ListPacking.Single(a=>a.Id==row.FkPacking).Title):"",
                Product= row.FkProduct.HasValue ? (cl._ListProduct.Single(a=>a.Id==row.FkProduct).Title) : "",
                UserAdd=db.TblUser.Find(row.FkUser).Title,
                Weight=por.PackingOfWeight??0,
               Portagekindcode=por.KindCode,
               Portagekindstr=por.KindTitle
                 
            };

               






            }
            public Guid IdRow { get; set; }
            public Guid? IdPort { get; set; }
            public string datestr { get; set; }
            public DateTime date { get; set; }
            public long Count { get; set; }
            public decimal Weight { get; set; }
            public string Packing { get; set; }
            public string Product { get; set; }
            public string Contract { get; set; }
            public string Location { get; set; }
            public double Contractcode { get; set; }
            public string ContractType { get; set; }
            public int ContractTypecode { get; set; }
            public int Portagekindcode { get; set; }
            public string Portagekindstr { get; set; }

            public string   UserAdd { get; set; }

        }
    }
}
