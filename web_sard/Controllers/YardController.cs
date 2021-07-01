namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using web_lib;
    using web_sard.Models;

 
    [LoginAuth(_UserRol._Rolls.SakoBargiriMahvate)]
    public class YardController : Controller
    {
    
        internal web_db.sardweb_Context db;

      
        public YardController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        public IActionResult Index(Guid idtype, int? kindPortage, Guid? fkgroup)
        {
            if (fkgroup == null)
            {
                fkgroup = HttpContext.Session.Get<Guid?>("fkgroup");
            }

            var x = (from n in db.TblPortages.Include(a => a.FkCustomerNavigation).ThenInclude(a => a.TblCustomerGroups)

                     let gr = n.FkCustomerNavigation.TblCustomerGroups

                     where n.FkSalmali == User._getuserSalMaliDef() && n.FkContracttype == idtype && n.IsEnd == false && n.IsDel == false &&
                   (
                   kindPortage.HasValue ?
                   (
                   n.KindCode == kindPortage
                   )
                   : true
                   ) &&
                     (
                   fkgroup.HasValue ?
                   (
                      fkgroup == Guid.Empty ?
                       gr.Any() == false
                       :
                       gr.Any(a => a.FkGroup == fkgroup)

                   )
                   : false
                   )
                     orderby n.Dateadd1 ascending
                     select new Models.tbls.portage.portage(n, db, false, false, false)).ToList();




            ViewData["type"] = db.TblContractTypes.Find(idtype);
            ViewData["kindPortage"] = kindPortage;
            ViewData["fkgroup"] = fkgroup;
            ViewData["ListGroup"] = db.TblGroups.Where(a => a.IsActive).ToList();


            HttpContext.Session.Set("fkgroup", fkgroup ?? null);

            return View(x);
        }

        public IActionResult View(Guid id, Guid defContract, int openkind = -1)
        {
            ViewBag.openkind = openkind;
            var x = db.TblPortages.Find(id);

            if (x.IsEnd || x.IsDel)
            {
                return RedirectToAction("index", new { idtype = x.FkContracttype });
            }
            ViewBag.ListContract = new Dictionary<string, Guid>(db.TblContracts.Where(a => a.FkContractType == x.FkContracttype && x.FkCustomer == a.FkCustomer).Select(a => new KeyValuePair<string, Guid>(a.Code.ToString(), a.Id)));
            ViewBag.defContract = defContract;


            ViewBag.injurys = db.TblPortageInjuries.Where(a => a.FkPortage == id).Select(a => a.FkInjury).ToList();


            return View(new Models.tbls.portage.portage(x, db, true, false, true));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult setinjuryDef(Guid id, Guid defContract, Guid[] FkInjury, string submitall = "")
        {

            var pp = db.TblPortages.Find(id);
            if ((pp.FkSalmali != User._getuserSalMaliDef()) || (pp.IsEnd) || (pp.IsDel))
            {
                return RedirectToAction("index");

            }



            db.TblPortageInjuries.RemoveRange(db.TblPortageInjuries.Where(a => a.FkPortage == id));
            foreach (var item in FkInjury)
            {
                db.TblPortageInjuries.Add(new web_db.TblPortageInjury { FkPortage = id, FkInjury = item });
            }
            if (submitall.IsEmpty() == false)
            {
                foreach (var itemr in db.TblPortageRows.Where(a => a.FkPortage == id && a.FkContract == defContract))
                {
                    db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == itemr.Id));
                    foreach (var item in FkInjury)
                    {
                        db.TblPortageRowInjuries.Add(new web_db.TblPortageRowInjury { FkPortageRow = itemr.Id, FkInjury = item });
                    }
                }




            }




            db.SaveChanges();



            return RedirectToAction("view", new { id = id, openkind = 1 });
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 100L * 1024L * 1024L)]
        public IActionResult addDoc(Guid id, Guid idp, IFormFile image = null, bool del = false, string kind = "Yard")
        {

            if (del)
            {
                var x = db.TblPortageDocuments.Find(id);



                var pp = db.TblPortages.Find(x.FkPortage);
                if ((pp.FkSalmali != User._getuserSalMaliDef()) || (pp.IsEnd) || (pp.IsDel))
                {
                    return RedirectToAction("index");

                }




                db.TblPortageDocuments.Remove(x);
            }
            else
            {

                var pp = db.TblPortages.Find(idp);
                if ((pp.FkSalmali != User._getuserSalMaliDef()) || (pp.IsEnd) || (pp.IsDel))
                {
                    return RedirectToAction("index");

                }
                web_db.TblPortageDocument x = null;

                if (kind == "Sign")
                {
                    x = db.TblPortageDocuments.FirstOrDefault(a => a.FkPortage == idp && a.Kind == kind);
                }
                if (kind == "SignPermit")
                {
                    x = db.TblPortageDocuments.FirstOrDefault(a => a.FkPortage == idp && a.Kind == kind);
                }
                if (x == null)
                {
                    x = new web_db.TblPortageDocument
                    {

                        Id = Guid.NewGuid(),
                        FkPortage = idp,
                        Kind = kind
                    };
                    db.TblPortageDocuments.Add(x);

                }
                x.Date = DateTime.Now;


                using (var target = new MemoryStream())
                {
                    image.CopyTo(target);
                    x.Image = target.ToArray();
                }
                x.Image = x.Image.imgTosmall(web_lib.FilesHelper.imageSize.s800);

            }

            db.SaveChanges();



            return RedirectToAction("view", new { id = idp, openkind = 1 });
        }


        public async Task<JsonResult> getlistRowsAsync(Guid id, Guid fkcontract)
        {


            var portage = db.TblPortages.Find(id);
            var portageContractType = db.TblContractTypes.Find(portage.FkContracttype);

            if (portageContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var xrows = await (from n in db.TblPortageRows
                                   where n.FkPortage == id && n.FkContract == fkcontract
                                   orderby n.Date descending
                                   select new Models.tbls.portage.PortageRow(n, db)).ToListAsync();

                Models.tbls.portage.PortageRow z = xrows.FirstOrDefault();

                if (z == null)
                {
                    var fkgroup = HttpContext.Session.Get<Guid?>("fkgroup");

                    var k = cl.GuidToLocationCode((cl._ListGroup.SingleOrDefault(a => a.Id == fkgroup) ?? new web_db.TblGroup()).Fklocation);
                    z = new Models.tbls.portage.PortageRow()
                    {
                        L1 = k[0],
                        L2 = k[1],
                        L3 = k[2]

                    };
                }

                var listinjdef = db.TblPortageInjuries.Where(a => a.FkPortage == id).Select(a => a.FkInjury).ToArray();
                xrows.Insert(0,
                    new Models.tbls.portage.PortageRow
                    {
                        CodeLocation = z.CodeLocation,
                        FkPacking = z.FkPacking,
                        FkProduct = z.FkProduct,
                        ListInjurys = listinjdef,
                        L1 = z.L1,
                        L2 = z.L2,
                        L3 = z.L3
                    });

                List<web_db.TblInjury> xinjury = null;
                List<string> nimpalets = null;

                var xpackings = Models.cl._ListPacking.Where(a => a.ForContractType().Contains(web_db.TblContractType.KindCotractTypeEnum.ASardKhane)).ToList();




                List<web_db.TblProduct>   xproducts = await db.TblContractProducts.Where(a => a.FkContract == fkcontract).Include(a => a.FkProductNavigation).Select(a => a.FkProductNavigation).ToListAsync();
        

                
                xinjury = Models.cl._ListInjury.Where(a => a.IsActive).ToList();
                var plast = db.TblPortages.Include(a => a.TblPortageRows).OrderByDescending(a => a.Date2).FirstOrDefault(a => a.Id != portage.Id && a.FkCustomer == portage.FkCustomer && a.TblPortageRows.Any(s => s.FkContract == fkcontract));
                if (plast != null)
                {
                    nimpalets = db.TblPortageRows.Where(a => a.FkPortage == plast.Id && a.FkContract == fkcontract && a.IsNimPalet).Select(a =>
                    new
                    {
                        aa = "(" + a.CodeLocation + ")" + a.Count.ToString()
                    }).Select(a => a.aa).ToList();
                }

                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { rows = xrows, packings = xpackings, products = xproducts, injurys = xinjury, nimpalets = nimpalets }));


            }
            else
              if (portageContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {
                var xrows = await (from n in db.TblPortageRows
                                   where n.FkPortage == id && n.FkContract == fkcontract
                                   orderby n.Date descending
                                   select new Models.tbls.portage.PortageRow(n, db)).ToListAsync();

                Models.tbls.portage.PortageRow z = xrows.FirstOrDefault();

                if (z == null)
                {
                    var fkgroup = HttpContext.Session.Get<Guid?>("fkgroup");

                    var k = cl.GuidToLocationCode((cl._ListGroup.SingleOrDefault(a => a.Id == fkgroup) ?? new web_db.TblGroup()).Fklocation);
                    z = new Models.tbls.portage.PortageRow()
                    {
                        L1 = k[0],
                        L2 = k[1],
                        L3 = k[2]

                    };
                }

                var listinjdef = db.TblPortageInjuries.Where(a => a.FkPortage == id).Select(a => a.FkInjury).ToArray();
                xrows.Insert(0,
                    new Models.tbls.portage.PortageRow
                    {
                        CodeLocation = z.CodeLocation,
                        FkPacking = z.FkPacking,
                        FkProduct = z.FkProduct,
                        ListInjurys = listinjdef,
                        L1 = z.L1,
                        L2 = z.L2,
                        L3 = z.L3
                    });


                var   xpackings = await (from n in db.TblContractPackings.Include(a => a.FkPackingNavigation)
                                        where (n.FkPackingNavigation.ForContractTypeJson??"").Contains(((int)web_db.TblContractType.KindCotractTypeEnum.ASabad).ToString())
                                         && n.FkContract == fkcontract
                                         select n.FkPackingNavigation
                                         
                                         
                                         )  .ToListAsync();




                List<web_db.TblProduct> xproducts = null;
                List<web_db.TblInjury> xinjury = null;
                List<string> nimpalets = null;
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { rows = xrows, packings = xpackings, products = xproducts, injurys = xinjury, nimpalets = nimpalets }));

            }
            return null;




        }

        public JsonResult AddlistRow(Guid idp, Guid id, int? L1, int? L2, int? L3, long Count, Guid fkcontract, Guid? FkPacking, Guid? FkProduct, Guid[] FkInjurys, string Txt, bool isNimPalet = false)
        {


            if (Count < 1)
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "تعداد اشتباه است - ثبت انجام نشد" }));

            }
            if ((FkPacking == Guid.Empty) || (FkPacking == null))
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "سبد انتخاب نشده است - ثبت انجام نشد" }));

            }


            var pp = db.TblPortages.Find(idp);

            if ((pp.FkSalmali != User._getuserSalMaliDef()) || (pp.IsEnd) || (pp.IsDel))
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "ثبت انجام نشد - غیر مجاز" }));

            }
            var ppp = db.TblContractTypes.Find(pp.FkContracttype);



            if (ppp.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var conf = ppp.ConfigASardKhane();
                var pkind = Models.tbls.portage.kindPortage.listkindcontract.Single(a => a.code == pp.KindCode);

                if ((FkProduct == Guid.Empty) || (FkProduct == null))
                {
                    return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "محصول انتخاب نشده است - ثبت انجام نشد" }));

                }

                if (conf.NeedLocation)
                {
                    if (conf.LocationLvlRequired > 0)
                    {
                        if ((L1 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "فاز اشتباه است - ثبت انجام نشد" }));

                        }
                    }
                    if (conf.LocationLvlRequired > 1)
                    {
                        if ((L2 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "سالن اشتباه است - ثبت انجام نشد" }));

                        }
                    }
                    if (conf.LocationLvlRequired > 2)
                    {
                        if ((L3 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "طبقه اشتباه است - ثبت انجام نشد" }));

                        }
                    }

                }

                var row = db.TblPortageRows.Find(id);

                if (row == null)
                {
                    row = new web_db.TblPortageRow
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Code = 1,
                        FkPortage = idp,
                        FkContract = fkcontract,
                        FkContractType = ppp.Id,
                        FkUser = User._getuserid().Value

                    };
                    db.TblPortageRows.Add(row);

                }



                if (L1.HasValue)
                {
                    row.CodeLocation = L1 + (L2.HasValue ? ("-" + L2 + (L3.HasValue ? ("-" + L3) : "")) : "");

                    var ll1 = Models.cl._ListLocation.SingleOrDefault(a => a.CodeFull == row.CodeLocation && a.Isdell == false);
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
                            row.FkLocation2 = ll1.FkP;
                            row.FkLocation3 = ll1.Id;

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

                }


                row.Count = (pkind.Manfi ? -1 : 1) * Count;
                row.FkPacking = FkPacking;
                row.FkProduct = FkProduct;
                row.Txt = Txt;
                row.IsNimPalet = isNimPalet;
                db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == row.Id));
                foreach (var item in FkInjurys)
                {
                    db.TblPortageRowInjuries.Add(new web_db.TblPortageRowInjury { FkPortageRow = row.Id, FkInjury = item });
                }
                db.SaveChanges();
            }
            if (ppp.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {
                var conf = ppp.ConfigASabad();
                var pkind = Models.tbls.portage.kindPortage.listkindcontract.Single(a => a.code == pp.KindCode);

                if (conf.NeedLocation)
                {
                    if (conf.LocationLvlRequired > 0)
                    {
                        if ((L1 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "فاز اشتباه است - ثبت انجام نشد" }));

                        }
                    }
                    if (conf.LocationLvlRequired > 1)
                    {
                        if ((L2 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "سالن اشتباه است - ثبت انجام نشد" }));

                        }
                    }
                    if (conf.LocationLvlRequired > 2)
                    {
                        if ((L3 ?? 0) < 1)
                        {
                            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = false, txt = "طبقه اشتباه است - ثبت انجام نشد" }));

                        }
                    }

                }

                var row = db.TblPortageRows.Find(id);

                if (row == null)
                {
                    row = new web_db.TblPortageRow
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Code = 1,
                        FkPortage = idp,
                        FkContract = fkcontract,
                        FkContractType = ppp.Id,
                        FkUser = User._getuserid().Value

                    };
                    db.TblPortageRows.Add(row);

                }



                if (L1.HasValue)
                {
                    row.CodeLocation = L1 + (L2.HasValue ? ("-" + L2 + (L3.HasValue ? ("-" + L3) : "")) : "");

                    var ll1 = Models.cl._ListLocation.SingleOrDefault(a => a.CodeFull == row.CodeLocation && a.Isdell == false);
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
                            row.FkLocation2 = ll1.FkP;
                            row.FkLocation3 = ll1.Id;

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

                }


                row.Count = (pkind.Manfi ? -1 : 1) * Count;
                row.FkPacking = FkPacking;
                row.FkProduct = FkProduct;
                row.Txt = Txt;
                row.IsNimPalet = isNimPalet;
                db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == row.Id));
                foreach (var item in FkInjurys)
                {
                    db.TblPortageRowInjuries.Add(new web_db.TblPortageRowInjury { FkPortageRow = row.Id, FkInjury = item });
                }
                db.SaveChanges();

            }


            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = true, txt = "ثبت انجام شد" }));
        }


        public JsonResult removelistRow(Guid id)
        {
            var row = db.TblPortageRows.Find(id);

            var pp = db.TblPortages.Find(row.FkPortage);
            if ((pp.FkSalmali != User._getuserSalMaliDef()) || (pp.IsEnd) || (pp.IsDel))
            {
                return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = true, txt = "حذف انجام نشد - غیر مجاز" }));

            }

            db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == id));
            db.TblPortageRows.Remove(row);
            db.SaveChanges();
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(new { ok = true, txt = "حذف انجام شد" }));
        }
        [LoginAuth(_UserRol._Rolls.Movement)]
        public IActionResult Movement(Guid fklocation)
        {
            ViewBag.fklocation = fklocation;

            var x = Models.tbls.portage.store.getByListByLocation(fklocation, User._getuserSalMaliDef(), db).ToList();

            return View(x);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Movement(Guid id, long Count, Guid FkNewLocation)
        {
            var x = db.TblStoreLogs.Find(id);
            var contype = Models.cl._ListContractType(User._getuserSalMaliDef()).Single(a => a.Id == x.FkContractType);


            if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {


                if (x.Count < 0 || Count > x.Count || Count < 0)
                {
                    return Redirect(Request.UrlReferer());
                }
                var row1 = new web_db.TblPortageRow
                {

                    FkContract = x.FkContract,
                    Date = DateTime.Now,
                    FkPacking = x.FkPacking,
                    FkProduct = x.FkProduct,
                    Id = Guid.NewGuid(),
                    Txt = "جابجایی",
                    FkUser = User._getuserid().Value,
                    FkContractType = contype.Id
                };


                {
                    row1.FkLocation1 = x.FkLocation1;
                    row1.FkLocation2 = x.FkLocation2;
                    row1.FkLocation3 = x.FkLocation3;
                    row1.CodeLocation = (
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation3) ??
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation2) ??
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation1) ?? new web_db.TblLocation()).CodeFull;
                    row1.Count = -Count;
                    row1.WeightOne = (double?)(x.Weight / x.Count);
                    db.TblPortageRows.Add(row1);
                }


                var row2 = new web_db.TblPortageRow
                {

                    FkContract = x.FkContract,
                    Date = DateTime.Now,
                    FkPacking = x.FkPacking,
                    FkProduct = x.FkProduct,
                    Id = Guid.NewGuid(),
                    Txt = "جابجایی",
                    FkUser = User._getuserid().Value,
                    FkContractType = x.FkContractType,

                };
                {
                    {

                        var ll1 = Models.cl._ListLocation.SingleOrDefault(a => a.Id == FkNewLocation);
                        row2.CodeLocation = ll1.CodeFull;

                        if (ll1.FkP.HasValue)
                        {
                            var ll2 = Models.cl._ListLocation.SingleOrDefault(a => a.Id == ll1.FkP);

                            if (ll2.FkP.HasValue)
                            {
                                row2.FkLocation1 = ll2.FkP.Value;
                                row2.FkLocation2 = ll1.FkP;
                                row2.FkLocation3 = ll1.Id;

                            }
                            else
                            {
                                row2.FkLocation1 = ll1.FkP.Value;
                                row2.FkLocation2 = ll1.Id;
                            }

                        }
                        else
                        {
                            row2.FkLocation1 = ll1.Id;
                        }
                    }

                    row2.Count = Count;
                    row2.WeightOne = (double?)(x.Weight / x.Count);
                    db.TblPortageRows.Add(row2);
                }
                db.SaveChanges();


                Models.cl.refTblStoreLogcontract(x.FkContract.Value, db);





                db.SaveChanges();
                return Redirect(Request.UrlReferer());

            }
            else
             if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {

                if (x.Count < 0 || Count > x.Count || Count < 0)
                {
                    return Redirect(Request.UrlReferer());
                }
                var row1 = new web_db.TblPortageRow
                {

                    FkContract = x.FkContract,
                    Date = DateTime.Now,
                    FkPacking = x.FkPacking,
                    FkProduct = x.FkProduct,
                    Id = Guid.NewGuid(),
                    Txt = "جابجایی",
                    FkUser = User._getuserid().Value,
                    FkContractType = contype.Id
                };


                {
                    row1.FkLocation1 = x.FkLocation1;
                    row1.FkLocation2 = x.FkLocation2;
                    row1.FkLocation3 = x.FkLocation3;
                    row1.CodeLocation = (
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation3) ??
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation2) ??
                        Models.cl._ListLocation.SingleOrDefault(a => a.Id == row1.FkLocation1) ?? new web_db.TblLocation()).CodeFull;
                    row1.Count = -Count;
                    row1.WeightOne = (double?)(x.Weight / x.Count);
                    db.TblPortageRows.Add(row1);
                }


                var row2 = new web_db.TblPortageRow
                {

                    FkContract = null,
                    Date = DateTime.Now,
                    FkPacking = x.FkPacking,
                    FkProduct = x.FkProduct,
                    Id = Guid.NewGuid(),
                    Txt = "جابجایی",
                    FkUser = User._getuserid().Value,
                    FkContractType = x.FkContractType,

                };

                {
                    {

                        var ll1 = Models.cl._ListLocation.SingleOrDefault(a => a.Id == FkNewLocation);
                        row2.CodeLocation = ll1.CodeFull;

                        if (ll1.FkP.HasValue)
                        {
                            var ll2 = Models.cl._ListLocation.SingleOrDefault(a => a.Id == ll1.FkP);

                            if (ll2.FkP.HasValue)
                            {
                                row2.FkLocation1 = ll2.FkP.Value;
                                row2.FkLocation2 = ll1.FkP;
                                row2.FkLocation3 = ll1.Id;

                            }
                            else
                            {
                                row2.FkLocation1 = ll1.FkP.Value;
                                row2.FkLocation2 = ll1.Id;
                            }

                        }
                        else
                        {
                            row2.FkLocation1 = ll1.Id;
                        }
                    }

                    row2.Count = Count;
                    row2.WeightOne = (double?)(x.Weight / x.Count);
                    db.TblPortageRows.Add(row2);
                }
                db.SaveChanges();


                Models.cl.refTblStoreLogcontractType(x.FkContractType, User._getuserSalMaliDef(), db);





                db.SaveChanges();
                return Redirect(Request.UrlReferer());


            }



            return null;





        }
    }
}
