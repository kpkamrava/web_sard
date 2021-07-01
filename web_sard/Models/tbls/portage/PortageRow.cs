namespace web_sard.Models.tbls.portage
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="PortageRow" />.
    /// </summary>
    public class PortageRow
    {
 
        public PortageRow()
        {
            ListInjurysTbl = new List<alltbl>();
        }

      
        public PortageRow(web_db.TblPortageRow row, web_db.sardweb_Context db)
        {
            this.Code = row.Code;
            this.Count = row.Count.gadrmotlagh();
            this.Date = row.Date;
            this.FkPacking = row.FkPacking;
            this.FkProduct = row.FkProduct;

            this.Packing = (Models.cl._ListPacking.SingleOrDefault(a => a.Id == row.FkPacking) ?? new web_db.TblPacking()).Title;
            this.Product = (Models.cl._ListProduct.SingleOrDefault(a => a.Id == row.FkProduct) ?? new web_db.TblProduct()).Title;


            this.Id = row.Id;
            this.FkContract = row.FkContract;
            this.CodeContract = (db.TblContracts.Find(row.FkContract)??new web_db.TblContract()).Code;
            this.Txt = row.Txt;
            this.IsNimPalet = row.IsNimPalet;
            this.ListInjurys = db.TblPortageRowInjuries.Where(a => a.FkPortageRow == row.Id).Select(a => a.FkInjury).ToArray();
            this.ListInjurysTbl = (from n in db.TblPortageRowInjuries.Include(a => a.FkInjuryNavigation)
                                   where n.FkPortageRow == row.Id
                                   let inj = n.FkInjuryNavigation
                                   select new alltbl { code = inj.Ord, key = inj.Id, title = inj.Title }).ToList();
            this.CodeLocation = row.CodeLocation;
            if (this.CodeLocation.IsEmpty() == false)
            {
                var z = this.CodeLocation.Split("-");
                if (z.Count() > 0)
                {
                    L1 = Convert.ToInt32(z[0]);
                }
                if (z.Count() > 1)
                {
                    L2 = Convert.ToInt32(z[1]);
                }
                if (z.Count() > 2)
                {
                    L3 = Convert.ToInt32(z[2]);
                }
            }
            UserAddStr = db.TblUsers.Find(row.FkUser).Title;
            this.fkportage = row.FkPortage;
            tblportage = new portage();

        }
        //public PortageRow(web_db.TblPortageRow row, web_db.sardweb_Context db, bool portage)
        //{
        //    this.Code = row.Code;
        //    this.Count = row.Count;
        //    this.WeightOne = row.WeightOne;
        //    this.Date = row.Date;
        //    this.FkPacking = row.FkPacking;
        //    this.FkProduct = row.FkProduct;

        //    this.Packing = (Models.cl._ListPacking.SingleOrDefault(a => a.Id == row.FkPacking) ?? new web_db.TblPacking()).Title;
        //    this.Product = (Models.cl._ListProduct.SingleOrDefault(a => a.Id == row.FkProduct) ?? new web_db.TblProduct()).Title;


        //    this.Id = row.Id;
        //    this.FkContract = row.FkContract;
        //    this.CodeContract = db.TblContracts.Find(row.FkContract).Code;
        //    this.Txt = row.Txt;
        //    this.IsNimPalet = row.IsNimPalet;
        //    this.ListInjurys = db.TblPortageRowInjuries.Where(a => a.FkPortageRow == row.Id).Select(a => a.FkInjury).ToArray();
        //    this.ListInjurysTbl = (from n in db.TblPortageRowInjuries.Include(a => a.FkInjuryNavigation)
        //                           where n.FkPortageRow == row.Id
        //                           let inj = n.FkInjuryNavigation
        //                           select new alltbl { code = inj.Ord, key = inj.Id, title = inj.Title }).ToList();
        //    this.CodeLocation = row.CodeLocation;
        //    if (this.CodeLocation.IsEmpty() == false)
        //    {
        //        var z = this.CodeLocation.Split("-");
        //        if (z.Count() > 0)
        //        {
        //            L1 = Convert.ToInt32(z[0]);
        //        }
        //        if (z.Count() > 1)
        //        {
        //            L2 = Convert.ToInt32(z[1]);
        //        }
        //        if (z.Count() > 2)
        //        {
        //            L3 = Convert.ToInt32(z[2]);
        //        }
        //    }

        //    this.fkportage = row.FkPortage;
        //    var v = db.TblPortages.Find(row.FkPortage);
        //    this.tblportage = v == null ? null : new portage(v, db);
        //}


        public portage? tblportage { get; set; }
        public Guid? fkportage { get; set; }
        public Guid Id { get; set; }


        public long? Code { get; set; }


        public DateTime Date { get; set; }


        public long? Count { get; set; }
        public double? WeightOne { get; set; }


        public Guid? FkPacking { get; set; }


        public Guid? FkProduct { get; set; }


        public Guid? FkContract { get; set; }


        public double CodeContract { get; set; }


        public string Packing { get; set; }


        public string Product { get; set; }


        public string CodeLocation { get; set; }

        public string UserAddStr { get; set; }

        public bool IsNimPalet { get; set; }


        public string Txt { get; set; }


        public int? L1 { get; set; }


        public int? L2 { get; set; }


        public int? L3 { get; set; }


        public Guid[] ListInjurys { get; set; }


        public List<Models.tbls.alltbl> ListInjurysTbl { get; set; }
    }
}
