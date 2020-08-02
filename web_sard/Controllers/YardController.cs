using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_lib;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.SakoBargiriMahvate)]
    public class YardController : Controller
    {
        web_db.sardweb_Context db;
        public YardController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        public IActionResult Index(Guid idtype,int? kindPortage)
        {
            var x = from n in db.TblPortage 
                    where n.FkSalmali == User._getuserSalMaliDef() && n.FkContracttype == idtype && n.IsEnd == false && n.IsDel == false&&
                    (
                    kindPortage.HasValue?
                    (
                    n.KindCode==kindPortage
                    )
                    :true
                    )
                    orderby n.Dateadd1 ascending
                    select new Models.tbls.portage.portage(n, db, false, false,false);
           
                     ViewData["type"] = db.TblContractType.Find(idtype);
                     ViewData["kindPortage"] = kindPortage;
             

            return View(x.ToList());
        }
        public IActionResult View(Guid id)
        {
            var x = db.TblPortage.Find(id);
            
            if (x.IsEnd||x.IsDel)
            {
                return RedirectToAction("index",new {idtype=x.FkContracttype });
            } 
            return View(new Models.tbls.portage.portage(x, db, true,false,true));
        }

        public IActionResult addDoc(Guid id, Guid idp, IFormFile image =null,bool del=false ,string kind= "Yard")
        {

            
            if (del)
            {
                var x = db.TblPortageDocument.Find(id);
                db.TblPortageDocument.Remove(x);
            }
            else {
                web_db.TblPortageDocument x =null;

                if (kind == "Sign")
                {
                    x = db.TblPortageDocument.FirstOrDefault(a => a.FkPortage == idp && a.Kind == "Sign");
                }
                if(x==null)
                {
                    x = new web_db.TblPortageDocument
                    {
                    
                        Id = Guid.NewGuid(),
                        FkPortage = idp,
                        Kind = kind
                    };
                    db.TblPortageDocument.Add(x);

                }
                x.Date = DateTime.Now;
                 
               
                using (var target = new MemoryStream())
                {
                    image.CopyTo(target);
                    x.Image= target.ToArray();
                }
                x.Image = x.Image.imgTo100Kbytes();
                 
           
            }

            db.SaveChanges();



            return RedirectToAction("view",new { id = idp });
        }
        public async Task<JsonResult> getlistRowsAsync(Guid id)
        {


            var portage = db.TblPortage.Find(id);
             var portageContractType = db.TblContractType.Find(portage.FkContracttype);
          
            
            var xrows =await( from n in db.TblPortageRow
                    where n.FkPortage == id
                    orderby n.Date  descending
                    select new Models.tbls.portage.PortageRow(n,db)).ToListAsync();

            var z = (xrows.FirstOrDefault() ?? new Models.tbls.portage.PortageRow());
             

            xrows.Insert(0,new Models.tbls.portage.PortageRow {CodeLocation=z.CodeLocation,FkPacking=z.FkPacking,FkProduct=z.FkProduct,ListInjurys=z.ListInjurys,
            L1=z.L1,
            L2=z.L2,
            L3=z.L3
            });


            var xpackings = Models.cl._ListPacking.ToList();
            if (portageContractType.IsProduct1Packing0==false)
            {
               
                xpackings = await db.TblContractPacking.Where(a => a.FkContract == portage.FkContract).Include(a => a.FkPackingNavigation).Select(a => a.FkPackingNavigation).ToListAsync();
            }



           List<web_db.TblProduct>  xproducts = null;
           List<web_db.TblInjury> xinjury = null;
          if (portageContractType.IsProduct1Packing0 == true)
            {
                
                xproducts =await db.TblContractProduct.Where(a => a.FkContract == portage.FkContract).Include(a=>a.FkProductNavigation).Select(a=>a.FkProductNavigation).ToListAsync();
                xinjury = Models.cl._ListInjury.Where(a=>a.IsActive).ToList();
            }
          

            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { rows=xrows, packings= xpackings, products=xproducts,injurys=xinjury }));
        }

        public JsonResult AddlistRow(Guid idp,Guid id,int? L1, int? L2, int? L3, long Count,Guid FkPacking,Guid FkProduct,Guid[] FkInjurys,  string Txt)
        {


             if (Count<1)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "تعداد اشتباه است - ثبت انجام نشد" }));

            }
            if (FkPacking == Guid.Empty)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "بسته بندی انتخاب نشده است - ثبت انجام نشد" }));

            }

          
            var pp = db.TblPortage.Find(idp);
            var ppp = db.TblContractType.Find(pp.FkContracttype);
            if (ppp.IsProduct1Packing0==true)
            {
                if (FkProduct == Guid.Empty)
                {
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "محصول انتخاب نشده است - ثبت انجام نشد" }));

                }
                 }
            if (ppp.NeedLocation)
            {
                if (ppp.LocationLvlRequired > 0)
                {
                    if ((L1 ?? 0) < 1)
                    {
                        return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "فاز اشتباه است - ثبت انجام نشد" }));

                    }
                }
                if (ppp.LocationLvlRequired > 1)
                {
                    if ((L2 ?? 0) < 1)
                    {
                        return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "سالن اشتباه است - ثبت انجام نشد" }));

                    }
                }
                if (ppp.LocationLvlRequired > 2)
                {
                    if ((L3 ?? 0) < 1)
                    {
                        return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "طبقه اشتباه است - ثبت انجام نشد" }));

                    }
                }

            }

            var row=db.TblPortageRow.Find(id);
            
            if (row==null)
            {
                row = new web_db.TblPortageRow {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Code = 1,
                    FkPortage = idp,
                 
                };
                  db.TblPortageRow.Add(row);
                 
            }

             

            if (L1.HasValue)
            {
                row.CodeLocation = L1 + (L2.HasValue ? ("-" + L2 + (L3.HasValue ? ("-" + L3) : "")) : "");

                var ll1 = Models.cl._ListLocation.SingleOrDefault(a => a.CodeFull == row.CodeLocation&&a.Isdell==false);
                if (ll1 == null)
                {

                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "فاز و سالن و طبقه اشتباه است - ثبت انجام نشد" }));

                }  

                 if (ll1.FkP.HasValue)
                { 
                    var ll2 = Models.cl._ListLocation.SingleOrDefault(a => a.Id == ll1.FkP);

                    if (ll2.FkP.HasValue)
                    {
                        row.FkLocation1 = ll2.FkP.Value;
                        row.FkLocation2 =  ll1.FkP;
                        row.FkLocation3 =  ll1.Id ;

                    }
                    else
                    {
                        row.FkLocation1 = ll1.FkP.Value;
                        row.FkLocation2 = ll1.Id;
                    } 

                }
                else
                {
                    row.FkLocation1 = ll1.Id;
                }

                row.TblPortageRowLocation.Add(new web_db.TblPortageRowLocation
                {
                    Date = DateTime.Now,
                    FkLocation1 = row.FkLocation1.Value,
                    FkLocation2 = row.FkLocation2,
                    FkLocation3 = row.FkLocation3,
                    Fkuser = User._getuserid().Value,
                    Id = Guid.NewGuid()
                });

            }
             

            row.Count = Count; 
            row.FkPacking = FkPacking;
            row.FkProduct = FkProduct;
            row.Txt = Txt;
            db.TblPortageRowInjury.RemoveRange(db.TblPortageRowInjury.Where(a=>a.FkPortageRow==row.Id));
            foreach (var item in FkInjurys)
            {
                db.TblPortageRowInjury.Add(new web_db.TblPortageRowInjury {FkPortageRow=row.Id,FkInjury=item });
            } 
            db.SaveChanges();


            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new {ok=true,txt="ثبت انجام شد" }));
        }

        public JsonResult removelistRow( Guid id )
        { 
            var row = db.TblPortageRow.Find(id);
            db.TblPortageRowInjury.RemoveRange(db.TblPortageRowInjury.Where(a => a.FkPortageRow == id));
            db.TblPortageRowLocation.RemoveRange(db.TblPortageRowLocation.Where(a => a.FkPortageRow == id));
            db.TblPortageRow.Remove(row); 
            db.SaveChanges(); 
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = true, txt = "حذف انجام شد" }));
        }

    }
}