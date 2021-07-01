using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace web_sard.Areas.Acc.Models 
{
    public class customerAcc
    {

        public class customerAccRow
        {
            public string Kind { get; set; }
            public decimal Weight { get; set; }
            public decimal PriceSum { get; set; }

        }

        public customerAcc()
        {
        }


        public customerAcc(web_db.TblCustomer row, web_db.sardweb_Context db)
        {

            this.Code = row.Code;
            this.Id = row.Id;
            this.IdOtherSystem = row.IdOtherSystem;
            this.Mob = row.Mob;
            this.NationalCode = row.NationalCode;
            this.Title = row.Title;
            this.codesContract = (row.TblContracts ?? (ICollection<web_db.TblContract>)db.TblContracts.Where(a => a.FkCustomer == row.Id)).Select(a => a.Code).ToArray();


            //var x = from n in db.TblPortageMoneys
            //        where n.FkContractType == contype && n.FkCustomer == row.Id
            //        group n by n.Kind into m
            //        select new customerAccRow { Kind =m.Key, Weight=m.Sum(a=>a.Weight), PriceSum=m.Sum(a=>a.PriceSum)};

        }

        public Guid Id { get; set; }


        [DisplayName("کد")]
        public int Code { get; set; }


        [DisplayName("کد ملی")]

        public string NationalCode { get; set; }

        [DisplayName("عنوان")]

        public string Title { get; set; }


        [DisplayName("موبایل")]

        public string Mob { get; set; }

        public double[] codesContract { get; set; }

        [DisplayName("آیدی سایر سیستمها")]
        public string IdOtherSystem { get; set; }


        [DisplayName("باسکول")]
        public decimal? bascul { get; set; }
        [DisplayName("جعبه ها")]
        public decimal? packing { get; set; }
        [DisplayName("وزن")]
        public decimal? product { get; set; }
        [DisplayName("کارگر")]
        public decimal? kargar { get; set; }


    }
}
