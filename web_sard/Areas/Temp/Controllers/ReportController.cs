using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Areas.Temp.Controllers
{
    [Area("Temp")]
    [LoginAuth]
    public class ReportController : Controller
    {
        web_db.sardweb_Context db;
        public ReportController(web_db.sardweb_Context db) { this.db = db; }
        public IActionResult Index()
        {
            var x = db.TblTempRows.OrderByDescending(a => a.DateAdd).ToList();
            return View();
        }
        public IActionResult Create(Guid? id)
        {
            var x = db.TblTempRows.Find(id);
            if (x == null)
            {
                x = new web_db._temp.TblTempRow();
            }
            return View(x);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(web_db._temp.TblTempRow temp)
        {
            if (ModelState.IsValid)
            {
                var x = db.TblTempRows.Any(a => a.Id == temp.Id);
                if (x == false)
                {
                    temp.Id = Guid.NewGuid();
                    temp.DateAdd = DateTime.Now;
                    db.TblTempRows.Add(temp);
                }
                else
                {
                    db.TblTempRows.Update(temp);
                }
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(temp);
        }


    }
}
