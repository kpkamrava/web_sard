using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_lib;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class PackingController : Controller
    {
        web_db.sardweb_Context db;
        public PackingController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var x = from n in db.TblPacking
                    orderby n.Code
                    select new Models.tbls.packing.packing(n)  ;
            return View(x);
        } 
        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.packing.packing();
            var x = db.TblPacking.Find(id);
            if (x == null)
            {
                model.Code = (db.TblPacking.Max(a => (int?)a.Code) ?? 0) + 1;
            }
            else
            {
                model = new Models.tbls.packing.packing(x);

            }

            return View(model);


        }
        [HttpPost]
        public IActionResult Create(Guid id, Models.tbls.packing.packing model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblPacking.Find(id);
                if (x == null)
                {
                    x = new web_db.TblPacking { Id = Guid.NewGuid() };

                    db.TblPacking.Add(x);
                }
                x.Code = model.Code;
                x.IsActive = model.IsActive;
                x.Title = model.Title;
                x.WightEmpty = model.WightEmpty;
                x.WightFull = model.WightFull;

                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.error = "ثبت انجام نشد";
            }
            return View(model);
        }

    }
}