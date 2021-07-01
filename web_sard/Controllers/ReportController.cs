namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Transactions;
    using web_db;
    using web_lib;
    using web_sard.Models.tbls.customer;


    [LoginAuth]
    public class ReportController : Controller
    {
        internal IWebHostEnvironment env;

        internal web_db.sardweb_Context db;
        public ReportController(web_db.sardweb_Context db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }


        public IActionResult customer(Guid fktype)
        {
            var sal = User._getuserSalMaliDef();
            var ctype = db.TblContractTypes.Find(fktype);
            ViewBag.ctype = ctype;
           
            
            if (ctype.KindCotractType==TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var x = from n in db.TblCustomers.Include(a => a.TblContracts)
                        where n.FkSalmali == sal
                        let contr = n.TblContracts.Where(a => a.FkContractType == fktype)

                        let woutmax = contr.Sum(a =>  ((a.SumInWeight * a.PercentForOut / 100)))
                        let coutmax = contr.Sum(a => ((a.SumInCount * a.PercentForOut / 100)))
                        select new customer(n, db, false)
                        {
                            WOutMandeContract = (woutmax + contr.Sum(a => a.SumOutWeight)) ?? 0,
                            WInMaxContract = contr.Sum(a => a.WeightMaxIn) ?? 0,
                            WInSum = contr.Sum(a => a.SumInWeight) ?? 0,
                            WOutSum = contr.Sum(a => a.SumOutWeight) ?? 0,
                            WOutMaxContract = woutmax ?? 0,
                            WInMandeContract = (contr.Sum(a => a.WeightMaxIn - a.SumInWeight) ?? 0),
                            COutMandeContract = (coutmax + contr.Sum(a => a.SumOutCount)) ?? 0,
                            CInMaxContract = contr.Sum(a => a.CountMaxIn) ?? 0,
                            CInSum = contr.Sum(a => a.SumInCount) ?? 0,
                            COutSum = contr.Sum(a => a.SumOutCount) ?? 0,
                            COutMaxContract = coutmax ?? 0,
                            CInMandeContract = (contr.Sum(a => a.CountMaxIn - a.SumInCount) ?? 0)
                        };

                return View(x.ToList());
            }
            else
             if (ctype.KindCotractType == TblContractType.KindCotractTypeEnum.ASabad)
            {
                var x = from n in db.TblCustomers.Include(a => a.TblContracts)
                        where n.FkSalmali == sal
                        let contr = n.TblContracts.Where(a => a.FkContractType == fktype)

                        let woutmax = contr.Sum(a =>  (a.WeightMaxOut))
                        let coutmax = contr.Sum(a => (a.CountMaxOut))
                        select new customer(n, db, false)
                        {
                            WOutMandeContract = (woutmax + contr.Sum(a => a.SumOutWeight)) ?? 0,
                            WInMaxContract = contr.Sum(a => a.WeightMaxIn) ?? 0,
                            WInSum = contr.Sum(a => a.SumInWeight) ?? 0,
                            WOutSum = contr.Sum(a => a.SumOutWeight) ?? 0,
                            WOutMaxContract = woutmax ?? 0,
                            WInMandeContract = (contr.Sum(a => a.WeightMaxIn - a.SumInWeight) ?? 0),
                            COutMandeContract = (coutmax + contr.Sum(a => a.SumOutCount)) ?? 0,
                            CInMaxContract = contr.Sum(a => a.CountMaxIn) ?? 0,
                            CInSum = contr.Sum(a => a.SumInCount) ?? 0,
                            COutSum = contr.Sum(a => a.SumOutCount) ?? 0,
                            COutMaxContract = coutmax ?? 0,
                            CInMandeContract = (contr.Sum(a => a.CountMaxIn - a.SumInCount) ?? 0)
                        };

                return View(x.ToList());
            }

            return null; 
         
        }


        public IActionResult customerPortage(Guid fktype, Guid fkcustomer)
        {


            ViewBag.cus = db.TblCustomers.Find(fkcustomer).Title;
            var sal = User._getuserSalMaliDef();
            var ctype = db.TblContractTypes.Find(fktype);
            var x = from n in db.TblPortages.Include(a => a.FkCustomerNavigation).Include(a => a.FkContracttypeNavigation)

                    where n.FkSalmali == sal && n.FkContracttype == fktype &&
                           n.IsDel == false && n.IsEnd &&
                           n.FkCustomer == fkcustomer

                    orderby n.Date2 descending
                    select new Models.tbls.portage.portage(n, db, false, false, false);

            ViewBag.ctype = ctype;
            return View(x.ToList());
        }


        public IActionResult customerContract(Guid fktype, Guid fkcustomer)
        {
            ViewBag.cus = db.TblCustomers.Find(fkcustomer).Title;
            var sal = User._getuserSalMaliDef();
            var ctype = db.TblContractTypes.Find(fktype);
            ViewBag.ctype = ctype;
            var x = from n in db.TblContracts.Include(a => a.FkCustomerNavigation)
                    where n.FkContractType == fktype && n.FkSalmali == sal & n.FkCustomer == fkcustomer
                    orderby n.Code
                    select new Models.tbls.contract.contract(n, db, true, false, null);
            return View(x.ToList());
        }

        public IActionResult CustomerContractGardeshGr(Guid id)
        {
            ViewBag.con = db.TblContracts.Find(id).Code;

            var x = from n in db.TblPortageRows.Include(a => a.FkPortageNavigation)
                    where n.FkContract == id && n.FkPortageNavigation.IsDel == false && n.FkPortageNavigation.IsEnd
                    orderby n.FkPortageNavigation.Date2
                    select new Models.tbls.portage.PortageRow(n, db);
            return View(x.ToList());
        }




        public IActionResult printGardeshLocation(Models.reports.rep_gardeshlocation.Model model )
        {
          

          
            if (model.d1.IsEmpty())
            {
                model.d1 =db.TblSalMalis.Single(A=>A.Id==User._getuserSalMaliDef()).SalAz.ToPersianDate();
            }
            if (model.d2.IsEmpty())
            {
                model.d2 = db.TblSalMalis.Single(A => A.Id == User._getuserSalMaliDef()).SalTa.ToPersianDate();
            }  
            if (ModelState.IsValid)
            {

                ViewBag.table = printGardeshLocationdata(model);

            }
            ViewBag.custumers = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.IsEnable).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();


            return View(model);
        }

        public List<Models.reports.rep_gardeshlocation.Rows> printGardeshLocationdata(Models.reports.rep_gardeshlocation.Model model)
        {
            model.pakings = model.pakings ?? new Guid[] { };
            model.prodocts = model.prodocts ?? new Guid[] { };
            model.custumer = model.custumer ?? new Guid[] { };
            model.contract = model.contract ?? new Guid[] { };


            var d1 = model.d1.ToDate();
            var d2 = model.d2.ToDate();
            var sal = User._getuserSalMaliDef();

            var x = (from n in db.TblPortageRows.Where(a => a.FkPortage.HasValue).Include(a => a.FkPortageNavigation)

                     let portage = n.FkPortageNavigation

                     where

                    portage.IsEnd &&
                      portage.IsDel == false &&
                      portage.FkSalmali == sal &&
                      (portage.Date1.Date >= d1 && portage.Date1.Date <= d2) &&

                     (
                     model.locations.Contains(n.FkLocation1 ?? Guid.Empty) ||
                     model.locations.Contains(n.FkLocation2 ?? Guid.Empty) ||
                     model.locations.Contains(n.FkLocation3 ?? Guid.Empty)
                     )
                     &&

                     (model.pakings.Any() ? model.pakings.Contains(n.FkPacking ?? Guid.Empty) : true) &&
                     (model.prodocts.Any() ? model.prodocts.Contains(n.FkProduct ?? Guid.Empty) : true) &&
                       (
                          model.contract.Any() ? model.contract.Contains(n.FkContract ?? Guid.Empty) :
                         (
                          model.custumer.Any() ? model.custumer.Contains(portage.FkCustomer) : true)

                         ) &&

                     model.kindPortage.Contains(portage.KindCode)
                     orderby portage.Date1 descending
                     select n);

            if (model.isMajmuh==false)
            {
                return x.Select(n => new Models.reports.rep_gardeshlocation.Rows(n, db)).AsEnumerable().ToList();
            }


            if (model.isTafkik == true)
            {


                var x2 = from n in x.AsEnumerable()
                         group n by new { n.FkContract, n.FkContractType, n.FkPortage, n.FkProduct, n.FkLocation1, n.FkLocation2, n.FkPacking } into m
                         let por = db.TblPortages.Find(m.Key.FkPortage) ?? new TblPortage()
                         let con = db.TblContracts.Find(m.Key.FkContract) ?? new TblContract()
                         let contype = db.TblContractTypes.Find(m.Key.FkContractType)
                         let cus = db.TblCustomers.Find(con.FkCustomer)
                         let loc = db.TblLocations.Find(m.Key.FkLocation2) ?? new TblLocation()
                         let pro = db.TblProducts.Find(m.Key.FkProduct)
                         let pack = db.TblPackings.Find(m.Key.FkPacking)
                         select new Models.reports.rep_gardeshlocation.Rows
                         {



                             Contract = cus.Title + $" ({cus.Code})",
                             Contractcode = con.Code,
                             ContractType = contype.Title,
                             ContractTypecode = contype.Code,
                             Count = m.Sum(a => a.Count),
                             date = por.Date1,
                             datestr = por.Date1.ToPersianDate(),
                             IdPort = m.Key.FkPortage,

                             Location = loc.CodeFull,
                             Packing = m.Key.FkPacking.HasValue ? pack.Title : "",
                             Product = m.Key.FkProduct.HasValue ? pro.Title : "",
                             UserAdd = db.TblUsers.Find(por.FkUsAdd1).Title,
                             Weight = m.Sum(a => a.WeightOne * a.Count) / m.Sum(a => a.Count) ?? 0,
                             Portagekindcode = por.KindCode,
                             Portagekindstr = por.KindTitle,
                             residcode = por.Code,
                             Car = por.CarMashin + " " + por.CarShMashin + " (" + por.CarRanande + por.CarTell + ")"
                         };


                return x2.ToList();
            }
            else
            {
          

                    var x2 = from n in x.AsEnumerable()
                             group n by new {   n.FkContractType, n.FkPortage } into m
                             let por = db.TblPortages.Find(m.Key.FkPortage) ?? new TblPortage()
                              let contype = db.TblContractTypes.Find(m.Key.FkContractType)
                             let cus = db.TblCustomers.Find(por.FkCustomer)
                                select new Models.reports.rep_gardeshlocation.Rows
                             {



                                 Contract = cus.Title + $" ({cus.Code})",
                            
                                 ContractType = contype.Title,
                                 ContractTypecode = contype.Code,
                                 Count = m.Sum(a => a.Count),
                                 date = por.Date1,
                                 datestr = por.Date1.ToPersianDate(),
                                 IdPort = m.Key.FkPortage,

                             
                                 UserAdd = db.TblUsers.Find(por.FkUsAdd1).Title,
                                 Weight = m.Sum(a => a.WeightOne * a.Count) / m.Sum(a => a.Count) ?? 0,
                                 Portagekindcode = por.KindCode,
                                 Portagekindstr = por.KindTitle,
                                 residcode = por.Code,
                                 Car = por.CarMashin + " " + por.CarShMashin + " (" + por.CarRanande + por.CarTell + ")"
                             };


                    return x2.ToList();
              
            }
        }

       










        public IActionResult printJameGarardad(Models.reports.rep_JameGarardad.Model model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.table = printJameGarardaddata(model);

            }
            ViewData["listcustumer"] = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.IsEnable).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();
            return View(model);
        }
        public Models.reports.rep_JameGarardad.Report printJameGarardaddata(Models.reports.rep_JameGarardad.Model model)
        {
            var sal = User._getuserSalMaliDef();
            var x = (from n in db.TblCustomers.Where(a => a.FkSalmali == User._getuserSalMaliDef() &&
                  model.Customers == null ? true : (model.Customers ?? new Guid[0]).Contains(a.Id)

                     ).AsEnumerable()


                     select new Models.reports.rep_JameGarardad.customer { code = n.Code, id = n.Id, name = n.Title });


            return new Models.reports.rep_JameGarardad.Report { customers = x, model = model };
        }

        public IActionResult printGarardadProductPackings(string kind, Models.reports.rep_JameGarardadProductPackings.Model model)
        {
            ViewBag.kind = kind;
            if (ModelState.IsValid)
            {
                ViewBag.table = printGarardadProductPackingsdata(model);

            }
            ViewData["listcustumer"] = db.TblCustomers.Include(a => a.TblContracts).Where(a => a.FkSalmali == User._getuserSalMaliDef()
     &&
     a.TblContracts.Any()).OrderBy(a => a.Code).Select(a => new Models.tbls.customer.customer(a, db, true)).ToList();
            return View(model);
        }
        public Models.reports.rep_JameGarardadProductPackings.Report printGarardadProductPackingsdata(Models.reports.rep_JameGarardadProductPackings.Model model)
        {
            var sal = User._getuserSalMaliDef();

            var x = (from n in db.TblStoreLogs
                     where n.FkSalmali == User._getuserSalMaliDef() &&
                     model.kindContract == n.FkContractType &&
                  (model.pakings == null ? true : (model.pakings ?? new Guid[0]).Contains(n.FkPacking ?? Guid.Empty)) &&
                  (model.prodocts == null ? true : (model.prodocts ?? new Guid[0]).Contains(n.FkProduct ?? Guid.Empty))


                     select new Models.reports.rep_JameGarardadProductPackings.row
                     {
                         Contract = ((double?)(db.TblContracts.SingleOrDefault(a => a.Id == n.FkContract) ?? new web_db.TblContract()).Code),
                         Customer = (db.TblCustomers.SingleOrDefault(a => a.Id == n.FkCustomer) ?? new TblCustomer()).Title,
                         Location1 = (db.TblLocations.SingleOrDefault(a => a.Id == n.FkLocation1) ?? new TblLocation()).CodeFull,
                         Location2 = (db.TblLocations.SingleOrDefault(a => a.Id == n.FkLocation2) ?? new TblLocation()).CodeFull,
                         Product = (db.TblProducts.SingleOrDefault(a => a.Id == n.FkProduct) ?? new TblProduct()).Title,
                         Packing = (db.TblPackings.SingleOrDefault(a => a.Id == n.FkPacking) ?? new TblPacking()).Title,
                         log = n
                     });


            return new Models.reports.rep_JameGarardadProductPackings.Report { rows = x.ToList(), model = model };
        }




        public IActionResult printMavjudiCustomer(Guid fktype)
        {
            var sal = User._getuserSalMaliDef();
            var ctype = db.TblContractTypes.Find(fktype);
            var x = from n in db.TblCustomers.Include(a => a.TblContracts)
                    where n.FkSalmali == sal
                    let port = n.TblPortages.Where(a => a.FkContracttype == fktype && a.IsEnd && a.IsDel == false)

                    let inn = port.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.In || a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                    let outt = port.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.Out || a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                 orderby n.Title
                    select new customer
                    {
                        Title=n.Title,Code=n.Code,Mob=n.Mob,
                        WInSum = inn.Sum(a => a.WeightNet) ?? 0,
                        WOutSum = outt.Sum(a => a.WeightNet) ?? 0,

                        CInSum = inn.Sum(a => a.PackingCount) ?? 0,
                        COutSum = outt.Sum(a => a.PackingCount) ?? 0,
                    };
            ViewBag.ctype = ctype;
            return View(x.ToList());

        }





      
    }
}
