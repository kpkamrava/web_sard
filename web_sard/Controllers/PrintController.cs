using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using web_lib;
namespace web_sard.Controllers
{
    public class PrintController : Controller
    {
        web_db.sardweb_Context db;
        IWebHostEnvironment env;
        public PrintController(web_db.sardweb_Context db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public IActionResult Index(Guid id)
        {
            {
                var t1 = db.TblPortage.Find(id);
            if (t1!=null)
                {
                    var t = Models.printclass.getlistfile("Portage", "in", env);
                    var aa = t.First().Value;
                    return RedirectToAction("Print", new Models.printclass { contolltype ="Portage", id = id, actiontype = ((Models.tbls.portage.kindPortage.kindPortageEnum)t1.KindCode).ToString(), kind = aa });
                } 
            }
            return Content("NotFind" );

        }
        public IActionResult Print(string contolltype,string actiontype, Guid id, string kind)
        {
         

            var t = Models.printclass.getlistfile(contolltype, actiontype, env);
            if (kind.IsEmpty())
            {
                kind = t.First().Value;
                return RedirectToAction("Index", new Models.printclass { contolltype= contolltype, id = id, kind = kind, actiontype = actiontype  }); 
            }
            return View(new Models.printclass { files = t, id = id,contolltype=contolltype,actiontype=actiontype,kind= kind });


        }

        public IActionResult GetReport(string contolltype, string actiontype, Guid id, string kind )
        {
            var x = new Models.tbls.portage.portage(db.TblPortage.Find(id), db, true);
            StiReport report = new StiReport();
            var z = env.WebRootPath + $"/Reports/{contolltype}/rpt_{actiontype}_{kind}.mrt";
            report.Load(StiNetCoreHelper.MapPath(this,z));
            report.Dictionary.Databases.Clear();

            if (contolltype=="Portage")
            {
                report.RegData("data", x);

            } 


            return StiNetCoreViewer.GetReportResult(this, report);
        }
        public IActionResult ViewerEvent()

        {

            return StiNetCoreViewer.ViewerEventResult(this);

        }

    }
}