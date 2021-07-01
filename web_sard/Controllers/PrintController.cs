namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="PrintController" />.
    /// </summary>
    public class PrintController : Controller
    {
        /// <summary>
        /// Defines the db.
        /// </summary>
        internal web_db.sardweb_Context db;

        /// <summary>
        /// Defines the env.
        /// </summary>
        internal IWebHostEnvironment env;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrintController"/> class.
        /// </summary>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        /// <param name="env">The env<see cref="IWebHostEnvironment"/>.</param>
        public PrintController(web_db.sardweb_Context db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index(Guid id)
        {
            {
                var t1 = db.TblPortages.Find(id);
                if (t1 != null)
                {
                    var t = Models.printclass.getlistfile("Portage", "in", env);
                    var aa = t.First().Value;
                    return RedirectToAction("Print", new Models.printclass { contolltype = "Portage", id = id, actiontype = ((Models.tbls.portage.kindPortage.kindPortageEnum)t1.KindCode).ToString(), kind = aa });
                }
            }
            return Content("NotFind");
        }


        //public IActionResult Print(string contolltype, string actiontype, Guid id, string kind)
        //{


        //    var t = Models.printclass.getlistfile(contolltype, actiontype, env);
        //    if (kind.IsEmpty())
        //    {
        //        kind = t.First().Value;
        //        return RedirectToAction("Index", new Models.printclass { contolltype = contolltype, id = id, kind = kind, actiontype = actiontype });
        //    }
        //    return View(new Models.printclass { files = t, id = id, contolltype = contolltype, actiontype = actiontype, kind = kind });
        //}


        //public IActionResult GetReport(string contolltype, string actiontype, Guid id, string kind)
        //{
        //    var x = new Models.tbls.portage.portage(db.TblPortages.Find(id), db, true);
        //    StiReport report = new StiReport();
        //    var z = env.WebRootPath + $"/Reports/{contolltype}/rpt_{actiontype}_{kind}.mrt";
        //    report.Load(StiNetCoreHelper.MapPath(this, z));
        //    report.Dictionary.Databases.Clear();

        //    if (contolltype == "Portage")
        //    {
        //        report.RegData("data", x);

        //    }


        //    return StiNetCoreViewer.GetReportResult(this, report);
        //}


        //public IActionResult ViewerEvent()
        //{

        //    return StiNetCoreViewer.ViewerEventResult(this);
        //}
    }
}
