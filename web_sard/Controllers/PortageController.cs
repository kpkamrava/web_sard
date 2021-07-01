namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Transactions;
    using web_lib;
    using web_sard.Models.tbls.contract;


    [LoginAuth(_UserRol._Rolls.Bascul)]
    public class PortageController : Controller
    {

        internal web_db.sardweb_Context db;


        internal IWebHostEnvironment env;


        private readonly IConfiguration _mySettings;

        public PortageController(web_db.sardweb_Context db, IWebHostEnvironment env, IConfiguration settings)
        {
            this.db = db;
            this.env = env;
            _mySettings = settings;
        }



        private object cus(web_db.TblContractType typecontract, Models.tbls.portage.portage model) {

            if (typecontract.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ABaskul)
            {
                var conf = typecontract.ConfigABaskul();
                var c =
               db.TblCustomers.Include(a => a.TblContracts).Where(a =>
               a.FkSalmali == User._getuserSalMaliDef()
               && a.IsEnable
               ).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();
              
                if (model.FkCustomer.IsEmpty() && conf.CustomerPishfarz.HasValue)
                {
                    var cc = (c.SingleOrDefault(a => a.Code == conf.CustomerPishfarz) ?? new Models.tbls.customer.customer());
                    model.FkCustomer = cc.Id;
                    model.Customer = cc;
                    model.Customerstr = cc.Title;

                }
                return c;
            }
            else
            {
            return
                             db.TblCustomers.Include(a => a.TblContracts).Where(a =>
                             a.FkSalmali == User._getuserSalMaliDef()
                             && a.IsEnable &&
                             a.TblContracts.Any(s => s.FkContractType == typecontract.Id)
                             ).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();

            }
            
        }

        public IActionResult Index(Guid idtype, int? kindPortage = null, Guid? printid = null, bool? all = null, string search = "")
        {

            if (all.HasValue)
            {
                Request.HttpContext.Session.Set("portageAll", all.Value);
            }

            all = Request.HttpContext.Session.Get<bool?>("portageAll");
            if (all == null)
            {
                all = false;
            }
            ViewData["all"] = all.Value;
            ViewData["search"] = search;
            var sal = User._getuserSalMaliDef();
            var x = from n in db.TblPortages.Include(a => a.FkCustomerNavigation).Include(a => a.FkContracttypeNavigation)

                    where n.FkSalmali == sal && n.FkContracttype == idtype &&
                    (
                    kindPortage > 0 ?


                    ((kindPortage == n.KindCode) &&

                    (string.IsNullOrWhiteSpace(search) == false ? (
                    (n.Code.ToString() == search) ||
                    (n.FkCustomerNavigation.Title.Contains(search)) ||
                    n.TblPortageRows.Any(a => a.FkContractNavigation.Code.ToString() == search)||
                    (n.CarMashin.Contains(search)) ||
                    (n.CarRanande.Contains(search)) ||
 
                    (n.CarShMashin.Contains(search))   

                    ) : true) &&

                    n.IsEnd && n.IsDel == false &&
                        (
                        all.Value ? true : string.IsNullOrWhiteSpace(n.OtcodeResid)
                        )
                    )
                    :
                     (
                       kindPortage == -1 ? n.IsDel == false && n.IsEnd :
                       kindPortage == -2 ? n.IsDel : (n.IsEnd == false && n.IsDel == false)
                     )
                    )
                    orderby n.Code descending
                    select n;// new Models.tbls.portage.portage(n, db, false, false, false);
            ViewData["type"] = db.TblContractTypes.Find(idtype);
            ViewData["kindPortage"] = kindPortage;
            if (printid != null)
            {
                ViewData["printid"] = printid.Value;

            }
            var xx = new List<Models.tbls.portage.portage>();
            if (search.IsEmpty())
            {
                var v = x.Where(a => a.Date2.Value.Date == DateTime.Now.Date);
                if (v.Count() > 100)
                {
                    return View(v.Select(a => new Models.tbls.portage.portage(a, db, false, false, false)).ToList());
                }
                else
                {
                    return View(x.Take(300).Select(a => new Models.tbls.portage.portage(a, db, false, false, false)).ToList());
                }
            }
            else
            {
                return View(x.Select(a => new Models.tbls.portage.portage(a, db, false, false, false)).ToList());
            }

        }


        public IActionResult CreateIN(Guid id, Guid idtype, int kindPortage)
        {

            var tim = DateTime.Now;
            Models.tbls.portage.portage model = new Models.tbls.portage.portage
            {
                Id = Guid.NewGuid(),
                Date1 = tim,
                Date1date = tim.ToPersianDate(),
                Date1time = new TimeSpan(tim.Hour, tim.Minute, 0),
                Weight1IsBascul = true

            };

            var row = db.TblPortages.Find(id);
            if (row != null)
            {
                if (row.IsEnd)
                {
                    return Redirect(Request.UrlReferer());
                }
                model = new Models.tbls.portage.portage(row, db,true);
                idtype = model.FkContractType;
                kindPortage = model.KindCode;
            }
            var typecontract = db.TblContractTypes.Find(idtype);

            if (model.Code == 0)
            {
                model.Code = (db.TblPortages.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype == idtype && a.KindCode == kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;
            }
            ViewData["type"] = typecontract;
            ViewData["kindPortage"] = kindPortage;


            ViewData["listcustumer"] = cus(typecontract, model);



            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateIN(Guid idtype, int kindPortage, Models.tbls.portage.portage model)
        {
            ModelState.Remove("Weight2");
            ModelState.Remove("Date2date");
            ModelState.Remove("Date2time");
            ModelState.Remove("Customer.Mob");
            ModelState.Remove("Customer.NationalCode");
            ModelState.Remove("Customer.Code");
            ModelState.Remove("Customer.Title");
            ModelState.Remove("Customer.Id");

            var typecontract = db.TblContractTypes.Find(idtype);


            if (typecontract.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var conf = typecontract.ConfigASardKhane();
                
                if (ModelState.IsValid)
                {
                    var x = db.TblPortages.Find(model.Id);
                    if (x == null)
                    {

                        model.Code = (db.TblPortages.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype == idtype && a.KindCode == kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;



                        x = new web_db.TblPortage
                        {
                            FkSalmali = User._getuserSalMaliDef(),
                            Code = model.Code,
                            Dateadd1 = DateTime.Now,
                            FkContracttype = idtype,
                            FkUsAdd1 = User._getuserid().Value,
                            Id = model.Id,
                            KindCode = kindPortage,
                            KindTitle = Models.tbls.portage.kindPortage.listkindcontract.Find(a => a.code == kindPortage).txt,

                        };
                        db.TblPortages.Add(x);
                    }
                    else
                    {
                        x.Dateedit1 = DateTime.Now;
                        x.FkUsEdit1 = User._getuserid().Value;

                    }






                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.CarTell = model.CarTell;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.FkCustomer = model.FkCustomer;
                    if (x.Weight1 != model.Weight1)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "InBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight1 = model.Weight1;
                    x.Weight1IsBascul = model.Weight1IsBascul;
                    x.Txt = model.Txt;
                    x.Date1 = model.Date1date.ToDate().AddHours(model.Date1time.Hours).AddMinutes(model.Date1time.Minutes);
                    x.IsDel = false;
                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = "";






                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {
                        db.SaveChanges();
                        _refstoreTow(x.Id);
                        return RedirectToAction("index", new { idtype = x.FkContracttype });
                    }



                }
                ViewData["type"] = db.TblContractTypes.Find(idtype);
                ViewData["kindPortage"] = kindPortage;
                ViewData["listcustumer"] = cus(typecontract, model);


                return View(model);
            }


            else


            if (typecontract.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {
                 ModelState.Remove("Weight1");
              
                
                var conf = typecontract.ConfigASabad();
               
                 
                if (ModelState.IsValid)
                {
                    var x = db.TblPortages.Find(model.Id);
                    if (x == null)
                    {

                        model.Code = (db.TblPortages.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype == idtype && a.KindCode == kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;



                        x = new web_db.TblPortage
                        {
                            FkSalmali = User._getuserSalMaliDef(),
                            Code = model.Code,
                            Dateadd1 = DateTime.Now,
                            FkContracttype = idtype,
                            FkUsAdd1 = User._getuserid().Value,
                            Id = model.Id,
                            KindCode = kindPortage,
                            KindTitle = Models.tbls.portage.kindPortage.listkindcontract.Find(a => a.code == kindPortage).txt,

                        };
                        db.TblPortages.Add(x);
                    }
                    else
                    {
                        x.Dateedit1 = DateTime.Now;
                        x.FkUsEdit1 = User._getuserid().Value;

                    }






                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.CarTell = model.CarTell;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.FkCustomer = model.FkCustomer;
                    if (x.Weight1 != model.Weight1)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "InBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight1 = model.Weight1;
                    x.Weight1IsBascul = model.Weight1IsBascul;
                    x.Txt = model.Txt;
                    x.Date1 = model.Date1date.ToDate().AddHours(model.Date1time.Hours).AddMinutes(model.Date1time.Minutes);
                    x.IsDel = false;
                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = ""; 

                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {
                        db.SaveChanges();
                        _refstoreTow(x.Id);
                        return RedirectToAction("index", new { idtype = x.FkContracttype });
                    }
                     
                }
                ViewData["type"] = db.TblContractTypes.Find(idtype);
                ViewData["kindPortage"] = kindPortage;
                ViewData["listcustumer"] = cus(typecontract, model);


                return View(model);
            } 
            else
             if (typecontract.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ABaskul)
            {
                  var conf = typecontract.ConfigABaskul();

                if (ModelState.IsValid&&((model.ListRows.FirstOrDefault()??new Models.tbls.portage.PortageRow()).FkPacking.IsEmpty()==false))
                {
                    var x = db.TblPortages.Find(model.Id);
                    if (x == null)
                    {

                        model.Code = (db.TblPortages.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype == idtype && a.KindCode == kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;



                        x = new web_db.TblPortage
                        {
                            FkSalmali = User._getuserSalMaliDef(),
                            Code = model.Code,
                            Dateadd1 = DateTime.Now,
                            FkContracttype = idtype,
                            FkUsAdd1 = User._getuserid().Value,
                            Id = model.Id,
                            KindCode = kindPortage,
                            KindTitle = "توزین اول",

                        };
                        db.TblPortages.Add(x);
                    }
                    else
                    {
                        x.Dateedit1 = DateTime.Now;
                        x.FkUsEdit1 = User._getuserid().Value;

                    }






                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.CarTell = model.CarTell;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.FkCustomer = model.FkCustomer;
                    if (x.Weight1 != model.Weight1)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "InBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight1 = model.Weight1;
                    x.Weight1IsBascul = model.Weight1IsBascul;
                    x.Txt = model.Txt;
                    x.Date1 = model.Date1date.ToDate().AddHours(model.Date1time.Hours).AddMinutes(model.Date1time.Minutes);
                    x.IsDel = false;
                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = "";

                  var c=  db.TblPortageRows.FirstOrDefault(a=>a.FkPortage==x.Id);
                    if (c==null)
                    {
                        c = new web_db.TblPortageRow
                        {
                            Id=Guid.NewGuid(),
                            Code=1,
                            Count=0,
                            Date=DateTime.Now,
                            FkPortage=x.Id,
                            FkPacking= model.ListRows.First().FkPacking,
                            FkUser=User._getuserid().Value,
                            FkContractType= typecontract.Id,
                        };
                        db.TblPortageRows.Add(c);

                    }
                    else
                    {
                        c.FkPacking = model.ListRows.First().FkPacking;
                       
                    }




                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {

                        x.Weight1 = model.Weight1;
                        x.WeightNet = model.Weight2.HasValue?(x.Weight1- x.Weight2).gadrmotlagh():null;


                        db.SaveChanges();
                      
                         

                        return RedirectToAction("index", new { idtype = x.FkContracttype });
                    }



                }
                ViewData["type"] = db.TblContractTypes.Find(idtype);
                ViewData["kindPortage"] = kindPortage;


                ViewData["listcustumer"] = cus(typecontract, model);



                return View(model);
            }


            return null;


        }
        public IActionResult CreateOut(Guid id)
        { 
            var x = db.TblPortages.Find(id);
            if (x.Date2.HasValue == false)
            {
                x.Date2 = DateTime.Now;
                x.Weight2IsBascul = true;
            }

            var model = new Models.tbls.portage.portage(x, db, true, false, true);

            return View(model);
        }

        string checkportage(web_db.TblPortage row)
        {


            var x = db.TblPortageRows.Where(a => a.FkPortage == row.Id&&a.FkContract!=null).Include(a => a.FkContractNavigation).Select(a => a.FkContractNavigation).Distinct();
            if (x.Any(a => a.FkCustomer != row.FkCustomer))
            {
                return "اشکال در ساختار -  طرف حساب با قرارداد همخوانی ندارد";
            }



            return "";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOut(Models.tbls.portage.portage model, bool resendSms, string print)
        {
            var x = db.TblPortages.Find(model.Id);




            model.ContractType = Models.cl._ListContractType(User._getuserSalMaliDef()).Single(a => a.Id == model.FkContractType);


            if (model.ContractType.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {

                ModelState.Remove("Date1date");
                ModelState.Remove("Date1time");
                ModelState.Remove("Customer.Mob");
                ModelState.Remove("Customer.NationalCode");
                ModelState.Remove("Customer.Code");
                ModelState.Remove("Customer.Title");
                ModelState.Remove("Customer.Id");
            
                if (ModelState.IsValid)
                {
                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.CarTell = model.CarTell;


                    if (x.Weight2 != model.Weight2)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "OutBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight2 = model.Weight2;
                    x.WeightNet = (x.Weight2 - x.Weight1).gadrmotlagh();

                    x.Weight2IsBascul = model.Weight2IsBascul;
                    x.Txt = model.Txt;
                    x.Date2 = model.Date2date.ToDate().AddHours(model.Date2time.Value.Hours).AddMinutes(model.Date2time.Value.Minutes);
                    x.IsDel = false;
                    if (x.Dateadd2.HasValue == false)
                    {
                        x.Dateadd2 = DateTime.Now;
                        x.FkUsAdd2 = User._getuserid();
                    }
                    else
                    {
                        x.Dateedit2 = DateTime.Now;
                        x.FkUsEdit2 = User._getuserid();
                    }


                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = "";
                    x.IsEnd = false;








                    {
                        x.SmsOk = resendSms == true ? null : (x.SmsOk == true ? (bool?)true : false);
                    }

                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {
                        using (var trans = new TransactionScope())
                        {

                            _refstoreOne(x.Id);
                            x.IsPermitOk = contract.IsPermitOK(x, db);

                            if (x.IsPermitOk == true)
                            {
                                x.IsEnd = true;
                                x.TxtPermit = "";
                                x.FkUsPermit = null;

                            }
                            else
                            {
                                x.IsEnd = false;
                            }
                            db.SaveChanges();


                            _refstoreTow(x.Id);
                            trans.Complete();
                        }







                        if (x.IsPermitOk == false)
                        {
                            return RedirectToAction("CreateOut", x.Id);

                        }


                        if (string.IsNullOrEmpty(print))
                        {
                            return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode });

                        }
                        return RedirectToAction("Print", new { id = x.Id });
                    }
                }
                ViewData["type"] = db.TblContractTypes.Find(x.FkContracttype);
                ViewData["kindPortage"] = x.KindCode;
                model.Documents = db.TblPortageDocuments.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageDocument(a)).ToList();
                model.ListRows = db.TblPortageRows.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageRow(a, db)).ToList();


            }
            else
            if (model.ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

            {
                ModelState.Remove("Date1date");
                ModelState.Remove("Date1time");
                ModelState.Remove("Customer.Mob");
                ModelState.Remove("Customer.NationalCode");
                ModelState.Remove("Customer.Code");
                ModelState.Remove("Customer.Title");
                ModelState.Remove("Customer.Id");
               
                ModelState.Remove("Weight1");
                ModelState.Remove("Weight2");
               
                if (ModelState.IsValid)
                {
                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.CarTell = model.CarTell;


                    if (x.Weight2 != model.Weight2)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "OutBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight2 = model.Weight2;
                    x.WeightNet = (x.Weight2 - x.Weight1).gadrmotlagh();

                    x.Weight2IsBascul = model.Weight2IsBascul;
                    x.Txt = model.Txt;
                    x.Date2 = model.Date2date.ToDate().AddHours(model.Date2time.Value.Hours).AddMinutes(model.Date2time.Value.Minutes);
                    x.IsDel = false;
                    if (x.Dateadd2.HasValue == false)
                    {
                        x.Dateadd2 = DateTime.Now;
                        x.FkUsAdd2 = User._getuserid();
                    }
                    else
                    {
                        x.Dateedit2 = DateTime.Now;
                        x.FkUsEdit2 = User._getuserid();
                    }


                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = "";
                    x.IsEnd = false;








                    {
                        x.SmsOk = resendSms == true ? null : (x.SmsOk == true ? (bool?)true : false);
                    }

                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {
                        using (var trans = new TransactionScope())
                        {

                            _refstoreOne(x.Id);
                            x.IsPermitOk = contract.IsPermitOK(x, db);

                            if (x.IsPermitOk == true)
                            {
                                x.IsEnd = true;
                                x.TxtPermit = "";
                                x.FkUsPermit = null;

                            }
                            else
                            {
                                x.IsEnd = false;
                            }
                            db.SaveChanges();


                            _refstoreTow(x.Id);
                            trans.Complete();
                        }







                        if (x.IsPermitOk == false)
                        {
                            return RedirectToAction("CreateOut", x.Id);

                        }


                        if (string.IsNullOrEmpty(print))
                        {
                            return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode });

                        }
                        return RedirectToAction("Print", new { id = x.Id });
                    }
                }
                ViewData["type"] = db.TblContractTypes.Find(x.FkContracttype);
                ViewData["kindPortage"] = x.KindCode;
                model.Documents = db.TblPortageDocuments.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageDocument(a)).ToList();
                model.ListRows = db.TblPortageRows.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageRow(a, db)).ToList();

            }
            else
                     if (model.ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ABaskul)
            {

                ModelState.Remove("Date1date");
                ModelState.Remove("Date1time");
                ModelState.Remove("Customer.Mob");
                ModelState.Remove("Customer.NationalCode");
                ModelState.Remove("Customer.Code");
                ModelState.Remove("Customer.Title");
                ModelState.Remove("Customer.Id");

                if (ModelState.IsValid)
                {
                    x.CarMashin = model.CarMashin;
                    x.CarRanande = model.CarRanande;
                    x.CarShMashin = model.CarShMashin;
                    x.FkCar = model.FkCar;
                    x.CarMashin = Models.cl._ListCar.Single(a => a.Id == model.FkCar).Title;

                    x.CarTell = model.CarTell;


                    if (x.Weight2 != model.Weight2)
                    {
                        var z = Models.cl.captureDorbinBaskul();
                        if (z != null)
                        {

                            db.TblPortageDocuments.Add(new web_db.TblPortageDocument
                            {
                                Date = DateTime.Now,
                                FkP = x.Id,
                                Id = Guid.NewGuid(),
                                FkPortage = x.Id,
                                Image = z,
                                Kind = "OutBaskul",
                                Format = ""
                            });

                        }
                    }
                    x.Weight2 = model.Weight2;
                    x.WeightNet = (x.Weight2 - x.Weight1).gadrmotlagh();

                    x.Weight2IsBascul = model.Weight2IsBascul;
                    x.Txt = model.Txt;
                    x.Date2 = model.Date2date.ToDate().AddHours(model.Date2time.Value.Hours).AddMinutes(model.Date2time.Value.Minutes);
                    x.IsDel = false;
                    if (x.Dateadd2.HasValue == false)
                    {
                        x.Dateadd2 = DateTime.Now;
                        x.FkUsAdd2 = User._getuserid();
                    }
                    else
                    {
                        x.Dateedit2 = DateTime.Now;
                        x.FkUsEdit2 = User._getuserid();
                    }


                    x.IsPermitOk = null;
                    x.FkUsPermit = null;
                    x.TxtPermit = "";
                    x.IsEnd = false;








                    {
                        x.SmsOk = resendSms == true ? null : (x.SmsOk == true ? (bool?)true : false);
                    }

                    ViewBag.error = checkportage(x);
                    if (((string)ViewBag.error).IsEmpty())
                    {
                        using (var trans = new TransactionScope())
                        {
 
                             x.IsPermitOk =true;

                            if (x.IsPermitOk == true)
                            {
                                x.IsEnd = true;
                              

                            }
                            x.WeightNet = model.Weight2.HasValue ? (x.Weight1 - x.Weight2).gadrmotlagh() : null;

                            db.SaveChanges();


                         
                            trans.Complete();
                        }







                        

                        if (string.IsNullOrEmpty(print))
                        {
                            return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode });

                        }
                        return RedirectToAction("Print", new { id = x.Id });
                    }
                }
                ViewData["type"] = db.TblContractTypes.Find(x.FkContracttype);
                ViewData["kindPortage"] = x.KindCode;
                model.Documents = db.TblPortageDocuments.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageDocument(a)).ToList();
                model.ListRows = db.TblPortageRows.Where(a => a.FkPortage == model.Id).Select(a => new Models.tbls.portage.PortageRow(a, db)).ToList();


            }
           




                return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Permit(Guid id, string txt)
        {

            var x = db.TblPortages.Find(id);
            x.TxtPermit = txt;
            x.FkUsPermit = User._getuserid();
            x.IsEnd = true;
            x.IsDel = false;
            if (User._getRolAny(_UserRol._Rolls.PermitPortage))
            {
                db.SaveChanges();
                _refstoreTow(id);
            }


            return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode });
        }


        public IActionResult Delete(Guid id)
        {

            var x = db.TblPortages.Find(id);
            x.TxtPermit = "";
            x.FkUsPermit = null;

            if (x.IsEnd)
            {

                x.IsEnd = false;
            }
            else
            {
                x.IsDel = true;

            }
            db.SaveChanges();
            _refstoreTow(id);
            return Redirect(Request.UrlReferer());
        }


        private void _refstoreOne(Guid fkportage)
        {
            var x = db.TblPortageRows.Where(a => a.FkPortage == fkportage);
            var por = db.TblPortages.Find(fkportage);
            var contype = Models.cl._ListContractType(User._getuserSalMaliDef()).Single(a => a.Id == por.FkContracttype);
            por.WeightNet = (por.Weight1 - por.Weight2).gadrmotlagh();
            if (Models.tbls.portage.kindPortage.listkindcontract.Single(a => a.code == por.KindCode).Manfi)
            {

                por.WeightNet = -por.WeightNet;
            }

            por.PackingCount = x.Sum(a => a.Count);
        
            por.PackingOfWeight = (double?)(por.WeightNet / por.PackingCount).gadrmotlagh();

             



            por.OtcodeResid = "";
            db.TblPortageMoneys.RemoveRange(db.TblPortageMoneys.Where(a => a.FkPortage == por.Id));


            decimal ss = 0;
            foreach (var item in x)
            {
                var sc = Models.cl.getWeightScale(item.FkProduct, item.FkPacking);


                item.WeightOne = sc * por.PackingOfWeight;
                if (item.WeightOne.HasValue)
                {
                    ss += (decimal)item.WeightOne * item.Count;

                }
            }


            foreach (var item in x)
            {
                if (item.WeightOne.HasValue)
                {
                    item.WeightOne = item.WeightOne * (double)(por.WeightNet / ss);

                }
            }
           
           


            db.SaveChanges();
            _refstoreTow(fkportage);
        }
        private void _refstoreTow(Guid fkportage)
        {
            var x = db.TblPortageRows.Where(a => a.FkPortage == fkportage);
            var por = db.TblPortages.Find(fkportage);
            por.WeightNet = (por.Weight1 - por.Weight2).gadrmotlagh();
            if (Models.tbls.portage.kindPortage.listkindcontract.Single(a => a.code == por.KindCode).Manfi)
            {

                por.WeightNet = -por.WeightNet;
            }
            var contype = Models.cl._ListContractType(User._getuserSalMaliDef()).Single(a => a.Id == por.FkContracttype);
            if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                foreach (var item in x.Select(a => a.FkContract).Distinct())
                {
                    Models.cl.refTblStoreLogcontract(item.Value, db);
                }
            }
            else
             if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {
                Models.cl.refTblStoreLogcontractType(contype.Id, por.FkSalmali, db);

            }




            db.SaveChanges();
        }


        public IActionResult Print(Guid id, string kind)
        {
            var t1 = db.TblPortages.Find(id);
            var tc = Models.cl._ListContractType(User._getuserSalMaliDef()).Single(a => a.Id == t1.FkContracttype).Code;

            var actiontype = /*((Models.tbls.portage.kindPortage.kindPortageEnum)t1.KindCode).ToString()*/tc.ToString();

            var t = Models.printclass.getlistfile("Portage", actiontype, env);


            if (kind.IsEmpty())
            {
                if (t.Any() == false)
                {
                    return Content("فرمت چاپ وجود ندارد");

                }
                kind = t.FirstOrDefault().Value;
                return RedirectToAction("Print", new Models.printclass { id = id, kind = kind, actiontype = actiontype });
            }
            return View(new Models.printclass { files = t, id = id, actiontype = actiontype, kind = kind });
        }
     



               
        public IActionResult PrintAjax(  Guid id, string kind )
        {
                var x = new Models.tbls.portage.portage(db.TblPortages.Find(id), db, true, true, true);
       
            var fileContent = System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Reports\\portage\\rpt_{(int)x.ContractType.KindCotractType}_{kind}.html"));
            x.ContractType.TblPortages = null;
            x.ContractType.TblContracts = null;
            

          
             var p=fileContent;
            return Content(new {json=x,page=p }.ToJson());
        
        }
      

     
    }
}
