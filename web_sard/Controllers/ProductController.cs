namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ProductController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class ProductController : Controller
    {
         /// <summary>
        /// Defines the db.
        /// </summary>
        internal web_db.sardweb_Context db;

 
        public ProductController(web_db.sardweb_Context db)
        {
            this.db = db;
        }

 
        public IActionResult Index()
        {
            var sal = User._getuserSalMaliDef();

            var x = from n in db.TblProducts
                    orderby n.Code
                    select new Models.tbls.product.Product(n, db, sal) { title = n.Title };
            return View(x);
        }

 
        public IActionResult Create(Guid id)
        {
            var sal = User._getuserSalMaliDef();
            var model = new Models.tbls.product.Product() { id = Guid.NewGuid(), isActive = true };
            var x = db.TblProducts.Find(id);
            if (x == null)
            {
                model.code = (db.TblProducts.Max(a => (int?)a.Code) ?? 0) + 1;
            }
            else
            {
                model = new Models.tbls.product.Product(x, db, sal);

            }

            return View(model);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.product.Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblProducts.Find(model.id);
                if (x == null)
                {
                    x = new web_db.TblProduct { Id = model.id };

                    db.TblProducts.Add(x);
                }
                x.Code = model.code;
                x.IsActive = model.isActive;
                x.Title = model.title;


                x.OtcodeKala = model.OtcodeKala;
                x.OtcodeKalaAcc = model.OtcodeKalaAcc;
                x.OtcodeVahedShomaresh = model.OtcodeVahedShomaresh;
                x.IsNotAc = model.IsNotAc;
                x.WightScale = model.WightScale;
                x.WightScale = x.WightScale == 0 ? null : x.WightScale;

                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListProduct = db.TblProducts.OrderBy(a => a.Code).ToList();

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
