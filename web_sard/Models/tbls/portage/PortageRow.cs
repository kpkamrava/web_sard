using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_lib;
namespace web_sard.Models.tbls.portage
{
    public class PortageRow
    {
        public PortageRow()
        { }

        public PortageRow( web_db.TblPortageRow row,web_db.sardweb_Context db)
        {
            this.Code = row.Code;
             this.Count = row.Count;
            this.Date = row.Date;
            this.FkPacking = row.FkPacking;
            this.FkProduct = row.FkProduct;

            this.Packing = (Models.cl._ListPacking.SingleOrDefault(a => a.Id == row.FkPacking) ?? new web_db.TblPacking()).Title;
            this.Product = (Models.cl._ListProduct.SingleOrDefault(a => a.Id == row.FkProduct) ?? new web_db.TblProduct()).Title;
             

            this.Id = row.Id;
            this.Txt = row.Txt;
            this.ListInjurys = db.TblPortageRowInjury.Where(a => a.FkPortageRow == row.Id).Select(a => a.FkInjury).ToArray();
            this.ListInjurysTbl = (from n in db.TblPortageRowInjury.Include(a=>a.FkInjuryNavigation)
                                  where n.FkPortageRow == row.Id
                                  let inj=n.FkInjuryNavigation
                                  select new alltbl {code=inj.Ord,key=inj.Id,title=inj.Title }).ToList(); 
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
           
        }
        public Guid Id { get; set; }
        public long? Code { get; set; }
        public DateTime Date { get; set; }
        public long? Count { get; set; }
        public Guid? FkPacking { get; set; }
        public Guid? FkProduct { get; set; }
        public string Packing { get; set; }
        public string Product { get; set; }
        public string CodeLocation { get; set; }
        public string Txt { get; set; }
        public int? L1 { get; set; }
        public int? L2 { get; set; }
        public int? L3 { get; set; }
        public Guid[] ListInjurys { get; set; }
        public List< Models.tbls.alltbl> ListInjurysTbl { get; set; }

    }
}
