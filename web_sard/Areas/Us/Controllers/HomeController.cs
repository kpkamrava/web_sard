using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Areas.Us.Controllers
{
    [Area("Us")]
    [LoginAuth(_UserRol._Rolls.Us)]
    public class HomeController : Controller
    {
        web_db.sardweb_Context db;
        public HomeController(web_db.sardweb_Context db) { this.db = db; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List(web_db._queu.TblQueu.QueuEnum id=web_db._queu.TblQueu.QueuEnum.Save)
        {
            ViewBag.id = id;
            var x = db.TblQueus.Where(a => a.KindQueu==id).OrderBy(a => a.date).ToList();
            return View(x);
        }
    }
}
