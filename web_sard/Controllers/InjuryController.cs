using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_lib;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class InjuryController : Controller
    {
        web_db.sardweb_Context db;
        public InjuryController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var list = db.TblInjury.OrderBy(a=>a.Ord).Select(a=>new Models.tbls.injury.injury(a));
            return View(list.ToList());
        }
        public IActionResult Create( Guid id)
        {
            var model = new Models.tbls.injury.injury();
            var row = db.TblInjury.Find(id);
            if (row!=null)
            {
                model = new Models.tbls.injury.injury(row);

            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Guid id, Models.tbls.injury.injury model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblInjury.Find(id);
                if (x == null)
                {
                    x = new web_db.TblInjury { Id = Guid.NewGuid() };

                    db.TblInjury.Add(x);
                }
               
                x.IsActive = model.IsActive;
                x.Title = model.Title;
                

                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListInjury = db.TblInjury.OrderBy(a=>a.Ord).ToList();
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