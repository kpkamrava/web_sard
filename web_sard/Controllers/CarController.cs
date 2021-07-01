namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="CarController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class CarController : Controller
    {

        internal web_db.sardweb_Context db;


        public CarController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        public IActionResult Index()
        {
            var list = db.TblCars.OrderBy(a => a.Title).Select(a => new Models.tbls.car.car(a));
            return View(list.ToList());
        }


        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.car.car() { Id = Guid.NewGuid(), IsDel = false };
            var row = db.TblCars.Find(id);
            if (row != null)
            {
                model = new Models.tbls.car.car(row);

            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.car.car model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblCars.Find(model.Id);
                if (x == null)
                {
                    x = new web_db.TblCar { Id = model.Id, PriceTowBascol = 0 };

                    db.TblCars.Add(x);
                }



                x.Title = model.Title;
                x.IsDel = model.IsDel;
                x.PriceTowBascol = model.PriceTowBascol;


                x.Img = model.Img.ToByteArray(true);



                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListCar = db.TblCars.OrderBy(a => a.Title).ToList();
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
