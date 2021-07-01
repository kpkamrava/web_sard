namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="PackingController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class PackingController : Controller
    {
        /// <summary>
        /// Defines the db.
        /// </summary>
        internal web_db.sardweb_Context db;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackingController"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        public PackingController(web_db.sardweb_Context db)
        {
            this.db = db;
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            var sal = User._getuserSalMaliDef();
            var x = from n in db.TblPackings
                    orderby n.Code
                    select new Models.tbls.packing.packing(n, db, sal);
            return View(x);
        }

      
        public IActionResult Create(Guid id)
        {
            var sal = User._getuserSalMaliDef();

            var model = new Models.tbls.packing.packing() { Id = Guid.NewGuid(), IsActive = true };
            var x = db.TblPackings.Find(id);
            if (x == null)
            {
                model.Code = (db.TblPackings.Max(a => (int?)a.Code) ?? 0) + 1;
            }
            else
            {
                model = new Models.tbls.packing.packing(x, db, sal);

            }

            return View(model);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="model">The model<see cref="Models.tbls.packing.packing"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.packing.packing model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblPackings.Find(model.Id);
                if (x == null)
                {
                    x = new web_db.TblPacking { Id = model.Id };

                    db.TblPackings.Add(x);
                }
                x.Code = model.Code;
                x.IsActive = model.IsActive;
                x.Title = model.Title;
                x.WightEmpty = model.WightEmpty;
                x.WightFull = model.WightFull;
                x.ForContractType( model.ForContractType);
                x.WightScale = model.WightScale;
                x.WightScale = x.WightScale == 0 ? null : x.WightScale;
                x.OtcodeKala = model.OtcodeKala;
                x.OtcodeVahedShomaresh = model.OtcodeVahedShomaresh;
                x.OtcodeKalaAcc = model.OtcodeKalaAcc;
                x.IsNotAc = model.IsNotAc;

                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListPacking = db.TblPackings.OrderBy(a => a.Code).ToList();

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
