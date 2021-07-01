using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace web_sard_Customer.Controllers
{


    [LoginAuth]
    public class AjController : Controller
    {
        web_db.sardweb_Context db;
        IConfiguration settings;
        public AjController(web_db.sardweb_Context db, IConfiguration settings)
        { this.db = db; this.settings = settings; }


        public IActionResult contracts()
        {
            var kiloview =Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.US_WeightViewEnable && a.Value == true.ToString());

            var x = from n in db.TblContracts
                    let s = db.TblSalMalis.FirstOrDefault(a => a.Id == n.FkSalmali)
                    where n.FkCustomer == Guid.Parse(User.Identity.Name) && (s.IsOrginal == true)
                    select new web_sard.Models.tbls.contract.contract(n, db, true, false, null, kiloview);
            ViewBag.sals = db.TblSalMalis.Where(a => a.IsOrginal == true).ToList();
            return View(x.ToList());
        }
        public IActionResult contract(Guid id)
        {
            var kiloview = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.US_WeightViewEnable && a.Value ==  true.ToString());

            var x = db.TblContracts.Find(id);
            var port = db.TblPortages.Include(a => a.TblPortageRows).Where(a => a.TblPortageRows.Any(s => s.FkContract == id)).Select(a => a.Id).Distinct();

            ViewBag.listiamges = db.TblPortageDocuments.Where(a => port.Contains(a.FkPortage.GetValueOrDefault()) && a.Kind.Contains("Sign") == false).ToList();
            ViewBag.kiloview =  Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.US_WeightViewEnable && a.Value == true.ToString()) ;

            return View(new web_sard.Models.tbls.contract.contract(x, db, true, true, null, kiloview));
        }
        public IActionResult contractAc(Guid id)
        {

            var kiloview = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.US_WeightViewEnable && a.Value == true.ToString());
            var x = from n in db.TblPortageRows.Include(a => a.FkContractNavigation).Include(a => a.TblPortageRowInjuries).ThenInclude(a => a.FkInjuryNavigation)//.ThenInclude(a=>a.FkCustomerNavigation)
                    where n.FkContract == id
                    orderby n.Date descending
                    select
                    new web_sard.Models.tbls.portage.store
                    {
                        date = n.Date,
                        contract = n.FkContractNavigation.Code,
                        // contracttype=n.FkContractNavigation.contr
                        count = n.Count,
                        countin = n.Count > 0 ? (long?)n.Count : null,
                        countout = n.Count < 0 ? (long?)n.Count : null,
                        // customer=n.FkContractNavigation.FkCustomerNavigation.Title,
                        fklocation1 = n.FkLocation1,
                        fklocation2 = n.FkLocation2,
                        fklocation3 = n.FkLocation3,
                        fkPacking = n.FkPacking,
                        fkProdoct = n.FkProduct,
                        location = n.CodeLocation,
                        Packing = (db.TblPackings.SingleOrDefault(a => a.Id == n.FkPacking) ?? new web_db.TblPacking()).Title,
                        Prodoct = (db.TblProducts.SingleOrDefault(a => a.Id == n.FkProduct) ?? new web_db.TblProduct()).Title,
                        Weight = kiloview ? ((decimal?)n.WeightOne * n.Count) : null,
                        Weightin = kiloview ? (n.Count > 0 ? ((decimal?)n.WeightOne * n.Count) : null) : null,
                        Weightout = kiloview ? (n.Count < 0 ? ((decimal?)n.WeightOne * n.Count) : null) : null,
                        txt = string.Join(",", n.TblPortageRowInjuries.Select(a => a.FkInjuryNavigation.Title)) + " " + n.Txt
                    };

            return View(x.AsEnumerable());
        }

        public ActionResult<string> Doc(Guid id)
        {

            var row = db.TblPortageDocuments.Find(id);
            if (row == null)
            {
                return NotFound();
            }
            return File(row.Image, "image/jpeg");


        }
    }
}
