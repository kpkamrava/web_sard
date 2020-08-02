using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_lib;

namespace web_sard.Controllers
{

    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]

    public class LocationController : Controller
    {
        web_db .sardweb_Context db;
        public LocationController(web_db.sardweb_Context db)
        { this.db = db; }
        public IActionResult Index()
        {
            var x = from n in db.TblLocation
                    where n.FkP == null
                    orderby n.Code
                    select new Models.tbls.location.locationchart(db, n);
            return View(x);
        }

        public IActionResult Create(Guid idp,Guid id)
        {

            var model = new Models.tbls.location.location();


            var row = db.TblLocation.Find(id);

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
                    isdell = false 
                };
            }

            if (idp==Guid.Empty)
            {
                idp = row.FkP??Guid.Empty;
            }

            var rowp = db.TblLocation.Find(idp);
            
            if (rowp != null)
            {
                model.Kind = rowp.Kind+1;
                model.pcode = rowp.Code;
                model.ptitle = rowp.Title;

            }
            else
            {
                model.Kind =  1;

            }

            if (model.Code==0)
            {
                model.Code =( db.TblLocation.Where(a=>(a.FkP??Guid.Empty)==idp).Max(a => (int?)a.Code) ?? 0)+1;
                   
            } 
            return View(model);
          
        }
        [HttpPost]
        public IActionResult Create(Guid idp, Guid id,Models.tbls.location.location model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.error = "ثبت انجام نشد - فیلد ها را درست پرکنید";
                return View(model);
            }
            try
            {
                var row = db.TblLocation.Find(id);
            


                if (row == null)
                {
                    row = new web_db.TblLocation();
                    if (idp != Guid.Empty)
                    {
                        row.FkP = idp;

                        row.Kind = db.TblLocation.Find(idp).Kind + 1;

                    }
                    else
                    {
                        row.Kind = 1;
                     
                    }
                    row.Id = Guid.NewGuid();
                 /**/   row.Code = (db.TblLocation.Where(a => (a.FkP ?? Guid.Empty) == idp).Max(a => (int?)a.Code) ?? 0) + 1;

                    db.TblLocation.Add(row);
                }
               //row.Code = model.Code;


                row.Title = model.Title;
                row.Isdell = false;
                row.Wight = model.wight;
                if (row.Wight==0)
                {
                    row.Wight = null;
                } 
                row.CodeFull = row.Code.ToString();
                 
                var rowp = db.TblLocation.Find(row.FkP);
                web_db.TblLocation rowpp = null;
                if (rowp != null)
                {
                    rowpp = db.TblLocation.Find(rowp.FkP);

                }
                if (rowp!=null)
                {
                    row.CodeFull = rowp.Code + "-" + row.CodeFull;

                    if (rowpp != null)
                    {
                        row.CodeFull = rowpp.Code + "-" + row.CodeFull;
                    }
                }  
                db.SaveChanges(); 
                ViewBag.txt = "ثبت انجام شد  "; 
                return RedirectToAction(nameof(Index));
            }
            catch { }
            ViewBag.error = "ثبت انجام نشد  ";
            return View(model);


        }

    }
}