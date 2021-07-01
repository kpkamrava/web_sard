namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="LocationController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]

    public class LocationController : Controller
    {
        /// <summary>
        /// Defines the db.
        /// </summary>
        internal web_db.sardweb_Context db;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        public LocationController(web_db.sardweb_Context db)
        {
            this.db = db;
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            var x = from n in db.TblLocations
                    where n.FkP == null
                    orderby n.Code
                    select new Models.tbls.location.locationchart(db, n);
            return View(x);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="idp">The idp<see cref="Guid"/>.</param>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Create(Guid idp, Guid id)
        {

            var model = new Models.tbls.location.location();


            var row = db.TblLocations.Find(id);

            if (row == null)
            {
                row = new web_db.TblLocation();

            }
            else
            {
                model = new Models.tbls.location.location
                {
                    Code = row.Code,
                    FkP = row.FkP,
                    Id = row.Id,
                    Kind = row.Kind,
                    wight = row.Wight,
                    Title = row.Title,
                    OtcodeAnbar = row.OtcodeAnbar,
                    isdell = false
                };
            }

            if (idp == Guid.Empty)
            {
                idp = row.FkP ?? Guid.Empty;
            }

            var rowp = db.TblLocations.Find(idp);

            if (rowp != null)
            {
                model.Kind = rowp.Kind + 1;
                model.pcode = rowp.Code;
                model.ptitle = rowp.Title;
                model.OtcodeAnbar = rowp.OtcodeAnbar;
            }
            else
            {
                model.Kind = 1;

            }

            if (model.Code == 0)
            {
                model.Code = (db.TblLocations.Where(a => (a.FkP ?? Guid.Empty) == idp).Max(a => (int?)a.Code) ?? 0) + 1;

            }
            return View(model);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="idp">The idp<see cref="Guid"/>.</param>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="model">The model<see cref="Models.tbls.location.location"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guid idp, Guid id, Models.tbls.location.location model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "ثبت انجام نشد - فیلد ها را درست پرکنید";
                return View(model);
            }
            try
            {
                var row = db.TblLocations.Find(id);



                if (row == null)
                {
                    row = new web_db.TblLocation();
                    if (idp != Guid.Empty)
                    {
                        row.FkP = idp;

                        row.Kind = db.TblLocations.Find(idp).Kind + 1;

                    }
                    else
                    {
                        row.Kind = 1;

                    }
                    row.Id = Guid.NewGuid();
                    /**/


                    //row.Code = (db.TblLocations.Where(a => (a.FkP ?? Guid.Empty) == idp).Max(a => (int?)a.Code) ?? 0) + 1;

                    db.TblLocations.Add(row);
                }
                row.Code = model.Code;


                row.Title = model.Title;
                row.Isdell = false;
                row.Wight = model.wight;
                row.OtcodeAnbar = model.OtcodeAnbar;
                if (row.Wight == 0)
                {
                    row.Wight = null;
                }

                //     row.CodeFull = row.Code.ToString();

                {

                    var z = db.TblLocations.FirstOrDefault(a => a.CodeFull == row.CodeFull);
                    if (z != null && z.Id != row.Id)
                    {
                        ViewBag.error = "ثبت انجام نشد - کد تکراری میباشد  ";
                        return View(model);
                    }
                }

                var rowp = db.TblLocations.Find(row.FkP);
                web_db.TblLocation rowpp = null;
                if (rowp != null)
                {
                    rowpp = db.TblLocations.Find(rowp.FkP);

                }
                if (rowp != null)
                {
                    row.CodeFull = rowp.Code + "-" + row.Code;

                    if (rowpp != null)
                    {
                        row.CodeFull = rowpp.Code + "-" + row.Code;
                    }
                }
                db.SaveChanges();
                Models.cl._ListLocation = db.TblLocations.OrderBy(a => a.Code).ToList();

                ViewBag.txt = "ثبت انجام شد  ";
                return RedirectToAction(nameof(Index));
            }
            catch { }
            ViewBag.error = "ثبت انجام نشد  ";
            return View(model);
        }
    }
}
