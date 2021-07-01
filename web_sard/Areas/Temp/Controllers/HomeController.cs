using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Areas.Temp.Controllers
{
    [Area("Temp")]
    [LoginAuth]
    public class HomeController : Controller
    {
        web_db.sardweb_Context db;
        public HomeController(web_db.sardweb_Context db) { this.db = db; }
        public IActionResult Index(Guid? location=null, string kind= "MaxDama") 
        {
            ViewBag.kind = kind;
            if (location==null)
            {
                location = Models.cl._ListLocation.FirstOrDefault(a => a.FkP == null && a.ForProduct).Id;
            }
            ViewBag.location = location;

            var x = from n in db.TblTempRows
                    where n.DateAdd<=DateTime.Now && n.DateAdd>=DateTime.Now.Date.AddDays(-30)&&
                    n.FkLocation1==location
                    orderby n.DateAdd
                    select n;

            return View(x.ToList());
        }
    }
}
