namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using web_lib;
    using web_sard.Models;


    [LoginAuth]
    public class ContractController : Controller
    {
        /// <summary>
        /// Defines the db.
        /// </summary>
        internal web_db.sardweb_Context db;


        public ContractController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        [LoginAuth(_UserRol._Rolls.Contract)]
        public IActionResult Index(Guid idtype, Guid? fkgroup)
        {

            ViewData["type"] = db.TblContractTypes.Find(idtype);
            ViewData["fkgroup"] = fkgroup;


            var x = from n in db.TblContracts.Include(a => a.FkCustomerNavigation).ThenInclude(a => a.TblCustomerGroups)
                    where n.FkContractType == idtype && n.FkSalmali == User._getuserSalMaliDef() &&
                    (
                       fkgroup == Guid.Empty ?
                       n.FkCustomerNavigation.TblCustomerGroups.Any() == false :
                       fkgroup == null ?
                       true :
                        n.FkCustomerNavigation.TblCustomerGroups.Any(a => a.FkGroup == fkgroup)
                    )
                    orderby n.Code
                    select new Models.tbls.contract.contract(n, db, true, false, null);
            return View(x.ToList());
        }


        [LoginAuth(_UserRol._Rolls.Contract)]
        public IActionResult Create(Guid id, Guid idtype)
        {
            var sal = User._getuserSalMaliDef();
            var x = db.TblContracts.Find(id);
            if (x != null)
            {
                idtype = (Guid)x.FkContractType;
                if (x.FkSalmali != sal)
                {
                    return RedirectToAction(nameof(Index), new { idtype = idtype });
                }
            }
            else
            {
                x = new web_db.TblContract();
                var s = db.TblSalMalis.Find(sal);
                if (s != null)
                {

                    x.Azdate = s.SalAz.Date;
                    x.Tadate = s.SalTa.Date;
                }
                x.Code = Convert.ToInt64((db.TblContracts.Where(a => a.FkSalmali == sal).Max(a => (long?)a.Code) ?? 0) + 1);
            }
            ViewData["type"] = db.TblContractTypes.Find(idtype);
            x.FkContractType = idtype;


            ViewData["listcustumer"] = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.IsEnable).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, false)).ToList();

            return View(new Models.tbls.contract.contract(x, db, true));
        }


        [LoginAuth(_UserRol._Rolls.Contract)]
        [LoginAuth(_UserRol._Rolls.ContractEdit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.contract.contract model, bool sendSMS)
        {
            var sal = User._getuserSalMaliDef();
            var ContractType = db.TblContractTypes.Find(model.FkContractType);

            var x = db.TblContracts.Find(model.Id);


            if (ContractType.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {

                var conf = ContractType.ConfigASardKhane();

                
                    ModelState.Remove("CountMaxIn");

                    ModelState.Remove("CountMaxOut");

                    ModelState.Remove("packingsId");
               


              
              

                    if (conf.OutControlByPercent)
                    {
                        ModelState.Remove("WeightMaxOut");

                    }
                    else
                    {
                        ModelState.Remove("PercentForOut");

                    }
               


                if (ModelState.IsValid)
                {
                    {
                        var x2 = db.TblContracts.Where(a => a.Code == model.Code && a.FkSalmali == sal);
                        if (x2.Any() && x2.Single().Id != x.Id)
                        {
                            x.Code = Convert.ToInt64((db.TblContracts.Where(a => a.FkSalmali == sal).Max(a => (long?)a.Code) ?? 0) + 1);

                        }
                    }

                    if (x == null)
                    {
                        x = new web_db.TblContract
                        {

                            Date = DateTime.Now.Date,

                            Dateadd = DateTime.Now,
                            FkContractType = model.FkContractType,
                            FkSalmali = User._getuserSalMaliDef(),
                            FkUsAdd = User._getuserid().Value,
                            Id = Guid.NewGuid(),
                            FkCustomer = model.FkCustomer

                        };
                        db.TblContracts.Add(x);
                    }
                    else
                    {
                        x.FkUsEdit = User._getuserid();
                        x.Dateedit = DateTime.Now;
                    }
                    x.Code = model.Code;
                    x.Azdate = model.Azdate.ToDate();
                    x.Tadate = model.Tadate.ToDate();
                    x.WeightMaxIn = model.WeightMaxIn;
                    x.CountMaxIn = model.CountMaxIn;

                    x.WeightMaxOut = model.WeightMaxOut;
                    x.CountMaxOut = model.CountMaxOut;
                    x.PercentForOut = model.PercentForOut;


                    x.IsEndVrud = model.isEndVrud;
                    x.IsEndXroj = model.isEndXroj;

                    x.Txt = model.Txt ?? "";

                    x.PriceOfBoxIn = model.PriceOfBoxIn ?? 0;
                    x.PriceOfBoxOut = model.PriceOfBoxOut ?? 0;
                    x.PriceOfKiloIn = model.PriceOfKiloIn ?? 0;
                    x.PriceOfKiloOut = model.PriceOfKiloOut ?? 0;
                    
                        db.TblContractProducts.RemoveRange(db.TblContractProducts.Where(a => a.FkContract == x.Id));
                        foreach (var item in model.prodoctsId)
                        {
                            db.TblContractProducts.Add(new web_db.TblContractProduct { FkContract = x.Id, FkProduct = item });
                        }
                     
                    if (sendSMS)
                    {
                        x.SendSms = true;

                    }
                    db.SaveChanges();
                    ViewBag.txt = "ثبت انجام شد";
                    return RedirectToAction(nameof(Index), new { idtype = model.FkContractType });
                }

                ViewData["listcustumer"] = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.IsEnable).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();

                ViewData["type"] = ContractType;
                ViewBag.error = "لطفا موارد را درست پر کنید";
                return View(model);


            }

            else
            if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {

                var conf = ContractType.ConfigASabad();

            
                    ModelState.Remove("WeightMaxIn");

                    ModelState.Remove("WeightMaxOut");

                    ModelState.Remove("prodoctsId");
               


                if (conf.IsEntry == false)
                {
                    ModelState.Remove("CountMaxIn");
                
                }
                if (conf.IsExit == false)
                {
                    ModelState.Remove("CountMaxOut");
                   

                }
              
                {
                     
                        ModelState.Remove("WeightMaxOut"); 
                        ModelState.Remove("PercentForOut");
                     
                }


                if (ModelState.IsValid)
                {
                    {
                        var x2 = db.TblContracts.Where(a => a.Code == model.Code && a.FkSalmali == sal);
                        if (x2.Any() && x2.Single().Id != x.Id)
                        {
                            x.Code = Convert.ToInt64((db.TblContracts.Where(a => a.FkSalmali == sal).Max(a => (long?)a.Code) ?? 0) + 1);

                        }
                    }

                    if (x == null)
                    {
                        x = new web_db.TblContract
                        {

                            Date = DateTime.Now.Date,

                            Dateadd = DateTime.Now,
                            FkContractType = model.FkContractType,
                            FkSalmali = User._getuserSalMaliDef(),
                            FkUsAdd = User._getuserid().Value,
                            Id = Guid.NewGuid(),
                            FkCustomer = model.FkCustomer

                        };
                        db.TblContracts.Add(x);
                    }
                    else
                    {
                        x.FkUsEdit = User._getuserid();
                        x.Dateedit = DateTime.Now;
                    }
                    x.Code = model.Code;
                    x.Azdate = model.Azdate.ToDate();
                    x.Tadate = model.Tadate.ToDate();
                    x.WeightMaxIn = model.WeightMaxIn;
                    x.CountMaxIn = model.CountMaxIn;

                    x.WeightMaxOut = model.WeightMaxOut;
                    x.CountMaxOut = model.CountMaxOut;
                    x.PercentForOut = model.PercentForOut;


                    x.IsEndVrud = model.isEndVrud;
                    x.IsEndXroj = model.isEndXroj;

                    x.Txt = model.Txt ?? "";

                    x.PriceOfBoxIn = model.PriceOfBoxIn ?? 0;
                    x.PriceOfBoxOut = model.PriceOfBoxOut ?? 0;
                    x.PriceOfKiloIn = model.PriceOfKiloIn ?? 0;
                    x.PriceOfKiloOut = model.PriceOfKiloOut ?? 0;
                  
                        db.TblContractPackings.RemoveRange(db.TblContractPackings.Where(a => a.FkContract == x.Id));
                        foreach (var item in model.packingsId)
                        {
                            db.TblContractPackings.Add(new web_db.TblContractPacking { FkContract = x.Id, FkPacking = item });
                        }

                  
                    if (sendSMS)
                    {
                        x.SendSms = true;

                    }
                    db.SaveChanges();
                    ViewBag.txt = "ثبت انجام شد";
                    return RedirectToAction(nameof(Index), new { idtype = model.FkContractType });
                }

                ViewData["listcustumer"] = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.IsEnable).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();

                ViewData["type"] = ContractType;
                ViewBag.error = "لطفا موارد را درست پر کنید";
                return View(model);


            }


            return null;
        }


        [LoginAuth(_UserRol._Rolls.Contract)]
        [LoginAuth(_UserRol._Rolls.ContractEdit)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                db.TblContractPackings.RemoveRange(db.TblContractPackings.Where(a => a.FkContract == id));
                db.TblContractProducts.RemoveRange(db.TblContractProducts.Where(a => a.FkContract == id));
                db.TblContracts.Remove(db.TblContracts.Find(id));
                db.SaveChanges();
            }
            catch
            {


            }


            return Redirect(Request.UrlReferer());
        }


        public IActionResult ViewContractAjax(Guid id, Guid fkportageAdd, bool Table = false, Guid[] kind = null, bool GardeshYes = true, bool GardeshNo = true)
        {

            var rows = new List<Models.tbls.contract.contract>();
            var ddd1 = DateTime.Now;

            var x = db.TblContracts.Find(id);
            var x2 = db.TblContracts.Where(a => a.FkCustomer == id &&
           (kind == null ? true : (
            (kind ?? new Guid[0]).Contains(a.FkContractType)
            )) &&
           ((GardeshYes ? (db.TblPortageRows.Any(s => s.FkContract == a.Id)) : false) ||
           (GardeshNo ? (!db.TblPortageRows.Any(s => s.FkContract == a.Id)) : false))

            );
            if (x != null)
            {

                rows.Add(new Models.tbls.contract.contract(x, db, true, true));



                long? countForIn, countForOut;
                decimal? weightForIn, weightForOut;
                cl.GetMojavez(x.FkCustomer, x.FkContractType, db, true, out countForIn, out weightForIn);
                cl.GetMojavez(x.FkCustomer, x.FkContractType, db, false, out countForOut, out weightForOut);

                ViewBag.countForIn = countForIn;
                ViewBag.countForOut = countForOut;
                ViewBag.weightForIn = weightForIn;
                ViewBag.weightForOut = weightForOut;



            }
            else if (x2.Any())
            {
                if (kind != null && kind.Length == 1)
                {
                    long? countForIn, countForOut;
                    decimal? weightForIn, weightForOut;
                    cl.GetMojavez(id, kind.First(), db, true, out countForIn, out weightForIn);
                    cl.GetMojavez(id, kind.First(), db, false, out countForOut, out weightForOut);

                    ViewBag.countForIn = countForIn;
                    ViewBag.countForOut = countForOut;
                    ViewBag.weightForIn = weightForIn;
                    ViewBag.weightForOut = weightForOut;
                }


                foreach (var item in x2)
                {
                    rows.Add(new Models.tbls.contract.contract(item, db, true, true, fkportageAdd));
                }

            }
            else
            {
                var x3 = db.TblPortageRows.Where(a => a.FkPortage == id).GroupBy(a => a.FkContract).Select(a => a.Key).Distinct();

                var por = db.TblPortages.Find(id);
                long? countForIn, countForOut;
                decimal? weightForIn, weightForOut;
                cl.GetMojavez(por.FkCustomer, por.FkContracttype, db, true, out countForIn, out weightForIn);
                cl.GetMojavez(por.FkCustomer, por.FkContracttype, db, false, out countForOut, out weightForOut);

                ViewBag.countForIn = countForIn;
                ViewBag.countForOut = countForOut;
                ViewBag.weightForIn = weightForIn;
                ViewBag.weightForOut = weightForOut;



                foreach (var item in x3)
                {
                    rows.Add(new Models.tbls.contract.contract(db.TblContracts.Find(item), db, true, true, fkportageAdd));
                }
            }




            var ddd2 = DateTime.Now - ddd1;

            if (rows.Any() == false)
            {
                return Content("");
            }



            if (Table == false)
            {
                return View(rows);
            }
            else
            {
                return View("ViewContractAjaxtable", rows);

            }
        }


       



        [LoginAuth(_UserRol._Rolls.Contract)]
        [LoginAuth(_UserRol._Rolls.ContractEdit)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult reprice(Guid FkContractType, Guid[] id, decimal? WeightMaxIn, int? PercentForOut, decimal? WeightMaxOut, long? CountMaxIn, long? CountMaxOut,
            decimal? PriceOfBoxIn, decimal? PriceOfBoxOut, decimal? PriceOfKiloIn, decimal? PriceOfKiloOut, bool sendSMS)
        {
            var sal = User._getuserSalMaliDef();
            var ContractType = db.TblContractTypes.Find(FkContractType);

            foreach (var idd in id)
            {
                var x = db.TblContracts.Find(idd);

                x.FkUsEdit = User._getuserid();
                x.Dateedit = DateTime.Now;
                if ((WeightMaxIn ?? 0) > 0) x.WeightMaxIn = WeightMaxIn;
                if ((CountMaxIn ?? 0) > 0) x.CountMaxIn = CountMaxIn;
                if ((WeightMaxOut ?? 0) > 0) x.WeightMaxOut = WeightMaxOut;
                if ((CountMaxOut ?? 0) > 0) x.CountMaxOut = CountMaxOut;
                if ((PercentForOut ?? 0) > 0) x.PercentForOut = PercentForOut;


                //x.IsEndVrud = isEndVrud;
                //x.IsEndXroj = isEndXroj;

                //x.Txt = Txt ?? "";

                if ((PriceOfBoxIn ?? 0) > 0) x.PriceOfBoxIn = PriceOfBoxIn ?? 0;
                if ((PriceOfBoxOut ?? 0) > 0) x.PriceOfBoxOut = PriceOfBoxOut ?? 0;
                if ((PriceOfKiloIn ?? 0) > 0) x.PriceOfKiloIn = PriceOfKiloIn ?? 0;
                if ((PriceOfKiloOut ?? 0) > 0) x.PriceOfKiloOut = PriceOfKiloOut ?? 0;

                if (sendSMS)
                {
                    x.SendSms = true;

                }
                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
            }
            return RedirectToAction(nameof(Index), new { idtype = FkContractType });
        }

    }



}

