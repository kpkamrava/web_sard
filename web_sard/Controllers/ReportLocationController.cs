using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using web_lib;
namespace web_sard.Controllers
{
    public class ReportLocationController : Controller
    {
        IWebHostEnvironment env;

        web_db.sardweb_Context db;
        public ReportLocationController(web_db.sardweb_Context db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }




        public IActionResult printGardeshLocation(Models.reports.rep_gardeshlocation.gardeshlocationModel model,string table="")
        {

             var actiontype = "gardesh";

             model.files = Models.printclass.getlistfile("Location", actiontype, env);
            if (model.kind.IsEmpty())
            {
                model.kind = model.files.FirstOrDefault().Key;
            }
            if (model.d1.IsEmpty())
            {
                model.d1 = DateTime.Now.ToPersianDate();
               }
            if (model.d2.IsEmpty())
            {
                model.d2 = DateTime.Now.ToPersianDate();
            }

            model.isvalid = ModelState.IsValid;

            if (table.IsEmpty()==false&&model.isvalid)
            {

                ViewBag.table = printGardeshLocationdata(model);

            }
            return View(model);
        }
        public Models.reports.rep_gardeshlocation.gardeshlocationReport printGardeshLocationdata(Models.reports.rep_gardeshlocation.gardeshlocationModel model)
        {
            var d1 = model.d1.ToDate();
            var d2 = model.d2.ToDate();
            var x = from n in db.TblPortageRow.Include(a => a.FkPortageNavigation).AsEnumerable()
                   
                    let portage = n.FkPortageNavigation
                    where portage.IsEnd && portage.IsDel == false && (portage.Date1 >= d1 && portage.Date1 <= d2) &&
                    model.locations.Contains(n.FkLocation1.GetValueOrDefault()) || model.locations.Contains(n.FkLocation2.GetValueOrDefault()) || model.locations.Contains(n.FkLocation3.GetValueOrDefault()) &&
                    (model.pakings!=null ? model.pakings.Contains(n.FkPacking.GetValueOrDefault()) : true) &&
                    (model.prodocts != null ? model.prodocts.Contains(n.FkProduct.GetValueOrDefault()) : true)&&
                    model.kindPortage.Contains(portage.KindCode)
                    select   Models.reports.rep_gardeshlocation.gardeshlocationRows.get(n,db);


            return new Models.reports.rep_gardeshlocation.gardeshlocationReport {model=model,rows=x };
        
        }
        public IActionResult GetReportprintGardeshLocation(Models.reports.rep_gardeshlocation.gardeshlocationModel model)
        {
          
          
            
            StiReport report = new StiReport();
            var z = env.WebRootPath + $"/Reports/Location/rpt_gardesh_{model.kind}.mrt";
            report.Load(StiNetCoreHelper.MapPath(this, z));
            report.Dictionary.Databases.Clear();

          

              report.RegData("data.root", printGardeshLocationdata(model));
       
          
             
            
            return StiNetCoreViewer.GetReportResult(this, report);
           
        }
        public IActionResult ViewerEvent()
        {

            return StiNetCoreViewer.ViewerEventResult(this);

        }

    }
}