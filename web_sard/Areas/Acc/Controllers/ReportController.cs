using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace web_sard.Areas.Acc.Controllers
{
    [Area("Acc")]
    public class ReportController : Controller
    {
        internal IWebHostEnvironment env;

        internal web_db.sardweb_Context db;
        public ReportController(web_db.sardweb_Context db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Acc(Guid fktype, bool refresh = false)
        {
            var sal = User._getuserSalMaliDef();
            var ctype = db.TblContractTypes.Find(fktype);
            if (refresh)
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                       new System.TimeSpan(0, 15, 0)))
                {
                    foreach (var item in db.TblPortages.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype == fktype))
                    {
                     Models.cl.refPortageAccMoney(db, item.Id);
                    }
                    db.SaveChanges();
                    scope.Complete();
                }


            }
            var x = from n in db.TblCustomers.Include(a => a.TblPortageMoneys)
                    where n.FkSalmali == sal

                    let bascul = n.TblPortageMoneys.Where(a => a.FkContractType == fktype && a.Kind == "bascul").Sum(a => (decimal?)a.PriceSum)
                    let kargar = n.TblPortageMoneys.Where(a => a.FkContractType == fktype && a.Kind == "kargar").Sum(a => (decimal?)a.PriceSum)
                    let packing = n.TblPortageMoneys.Where(a => a.FkContractType == fktype && a.Kind == "packing").Sum(a => (decimal?)a.PriceSum)
                    let product = n.TblPortageMoneys.Where(a => a.FkContractType == fktype && a.Kind == "product").Sum(a => (decimal?)a.PriceSum)
                    select new Models.customerAcc(n, db)
                    {
                        bascul = bascul,
                        kargar = kargar,
                        packing = packing,
                        product = product,

                    };
            ViewBag.ctype = ctype;
            return View(x.ToList());
        }

    }
}
