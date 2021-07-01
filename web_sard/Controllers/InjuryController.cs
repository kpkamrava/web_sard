using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace web_sard.Controllers
{


    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class InjuryController : Controller
    {

        internal web_db.sardweb_Context db;


        public InjuryController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        public IActionResult Index()
        {
            var list = db.TblInjuries.OrderBy(a => a.Ord).Select(a => new Models.tbls.injury.injury(a));
             
            return View(list.ToList());
        }


        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.injury.injury() { Id = Guid.NewGuid() };
            var row = db.TblInjuries.Find(id);
            if (row != null)
            {
                model = new Models.tbls.injury.injury(row);

            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.injury.injury model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblInjuries.Find(model.Id);
                if (x == null)
                {
                    x = new web_db.TblInjury { Id = model.Id };

                    db.TblInjuries.Add(x);
                }

                x.IsActive = model.IsActive;
                x.Title = model.Title;


                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListInjury = db.TblInjuries.OrderBy(a => a.Ord).ToList();
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
