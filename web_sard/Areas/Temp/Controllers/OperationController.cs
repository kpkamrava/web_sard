using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using web_lib;
namespace web_sard.Areas.Temp.Controllers
{
    [Area("Temp")]
    [LoginAuth(_UserRol._Rolls.Temp)]
    public class OperationController : Controller
    {
        
        web_db.sardweb_Context db;
        public OperationController(web_db.sardweb_Context db) { this.db = db; }
        public IActionResult Index()
        {
            var x = db.TblTemps.Where(a => a.FkSalMali == User._getuserSalMaliDef()).OrderByDescending(a => a.Date).Include(a=>a.UserAdd).Include(a=>a.UserTaiid).ToList();

            return View(x);
        }
        public IActionResult Create(Guid? id)
        {
            var x = db.TblTemps.Find(id);
            if (x==null)
            {
                x = new web_db._temp.TblTemp();
            }
            return View(x);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(web_db._temp.TblTemp temp)
        {
            if (ModelState.IsValid&&( temp.FkuserTaiid.HasValue==false))
            { 
                var xx = db.TblTemps.Any(a =>
                ( a.Date == temp._Date.ToDate()&&(a.Id!=temp.Id))
              
                &&a.FkSalMali==User._getuserSalMaliDef()
                );
                if (xx == false)
                {
                    var x = db.TblTemps.Any(a => a.Id == temp.Id);


                    if (x == false)
                    {
                        temp.Id = Guid.NewGuid();
                        temp.FkuserAdd = User._getuserid().Value;
                        temp.FkSalMali = User._getuserSalMaliDef();
                        db.TblTemps.Add(temp);
                    }
                    else
                    {
                        db.TblTemps.Update(temp);
                    }
                    db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
             
            }
            
            return View(temp);

        }
        public IActionResult Delete(Guid id)
        {
            var x = db.TblTemps.Find(id);
            if (x!=null)
            {
                db.TblTemps.Remove(x);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Temp(Guid? id)
        {
            if (id==null)
            {
                var z= db.TblTemps.OrderByDescending(a=>a.Date).FirstOrDefault();
                if (z==null)
                {
                    return RedirectToAction("index","Home");

                }
                return RedirectToAction(nameof(Temp),new { id = z.Id });
            }

            var x = db.TblTemps.Include(a=>a.TblTempRows).Include(a=>a.UserAdd).Include(a=>a.UserTaiid).Single(a=>a.Id==id);


            return View(x);
        }

        public IActionResult addTempRow(Guid? id, Guid? idTemp, string? location)
        {
        
            var model = db.TblTempRows.Include(a=>a.FktempNavigation).SingleOrDefault(a=>a.Id==id);
             
            if (model==null)
            { 
                var lll = Models.cl._ListLocation.Single(a => a.CodeFull == location);
                var l = Models.cl.GuidToLocationId(lll.Id);

                  model = new web_db._temp.TblTempRow
                {
                    FkLocation1 = l[0],
                    FkLocation2 = l[1],
                    FkLocation3 = l[2],
                    Fktemp = idTemp.Value,
                    FktempNavigation=db.TblTemps.Find(idTemp),
                    Location = location, 
                };
            }
            
            var ll = new[] { model.FkLocation1, model.FkLocation2, model.FkLocation3 };

            var rows = db.TblTempRows.Where(a => a.Fktemp == model.Fktemp && ll.Contains(a.FkLocation1) && ll.Contains(a.FkLocation2) && ll.Contains(a.FkLocation3)).AsEnumerable();

            if ((id .HasValue==false)&& db.TblTemps.Find(model.Fktemp).FkuserTaiid.HasValue == true)
            {
                if (rows.Any())
                {
                   return RedirectToAction("addTempRow",new {id= rows.First().Id});

                }
                else
                {
                    return Content("داده وجود ندارد");
                }

            }

            ViewBag.rows = rows;

            return View(model);
        }
        [HttpPost] 
        public IActionResult addTempRow(web_db._temp.TblTempRow model)
        {
            if (db.TblTemps.Find(model.Fktemp).FkuserTaiid.HasValue == false)
            {
                if (db.TblTempRows.Any(a => a.Id == model.Id))
                {

                    model.FkUserEdit = User._getuserid();
                    model.DateEdit = DateTime.Now;
                    db.TblTempRows.Update(model);
                }
                else
                {
                    model.Id = Guid.NewGuid();
                    model.FkUserAdd = User._getuserid().Value;
                    model.DateAdd = DateTime.Now;
                    db.TblTempRows.Add(model);
                }
            }
          
            db.SaveChanges();
             
            return RedirectToAction("Temp",new {id= model.Fktemp });
        }


        [RequestFormLimits(MultipartBodyLengthLimit = 100L * 1024L * 1024L)]
        
        public IActionResult addDoc(Guid id,  string kind, IFormFile image  )
        {
              
                var x = db.TblTemps.Find(id);

             
            if (x != null)
            {
                if (kind == "Sign")
                {

                    x.Sign = image.ToByteArray().imgTosmall(web_lib.FilesHelper.imageSize.s450);

                }
                else if (User._getRolAny(_UserRol._Rolls.TempAdmin))
                {
                    if (kind == "SignTaiid")
                    {
                        x.FkuserTaiid = User._getuserid();
                        x.DateTaiid = DateTime.Now;
                        x.SignTaiid = image.ToByteArray().imgTosmall(web_lib.FilesHelper.imageSize.s450);

                    }
                    else if (kind == "RemoveSignTaiid")
                    {
                        x.FkuserTaiid = null;
                        x.DateTaiid = null;
                        x.SignTaiid = null;

                    }
                }

                db.SaveChanges();

            }




      


            return RedirectToAction("temp",new {id=id });
        }

    }
}
