using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using web_db;
using web_lib;
using web_sard.Models;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.SuperVisor)]
    public class YearController : Controller
    {
        web_db.sardweb_Context db;
        public YearController(web_db.sardweb_Context db)
        {
            this.db = db;
        }



        public IActionResult Index()
        {
            var x = db.TblSalMalis.OrderBy(a => a.SalAz).ToList();

            return View(x);
        }

        public IActionResult Create(int id)
        {
            var x = db.TblSalMalis.Find(id);

            if (x == null)
            {
                x = new web_db.TblSalMali
                {
                    IsOpen = true,
                };
            }
            ViewBag.us = db.TblUsers.Where(a => a.IsActive).OrderBy(a => a.Title).ToList();
            ViewBag.usS = db.TblUserSals.Where(a => a.FkSal == id).Select(a => a.FkUser).ToList();

            return View(x);
        }
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public IActionResult Create(web_db.TblSalMali model, string az, string ta)
        {
            var row = db.TblSalMalis.Find(model.Id);

            if (row == null)
            {

                row = new web_db.TblSalMali()

                {
                    Id = (db.TblSalMalis.Max(a => (int?)a.Id) ?? 0) + 1

                };
                db.TblSalMalis.Add(row);
                foreach (var item in db.TblContractTypes.Where(a => a.FkSalmali == User._getuserSalMaliDef()))
                {
                    var itc = item.Clone2();
                    itc.Id = Guid.NewGuid();
                    itc.FkSalmali = row.Id;

                    db.TblContractTypes.Add(itc);
                }
            }
            row.IsOpen = model.IsOpen;
            row.IsOrginal = model.IsOrginal;
            row.Sal = model.Sal;
            row.SalAz = az.ToDate();
            row.SalTa = ta.ToDate();
            db.SaveChanges();
            cl._ListSalmali = db.TblSalMalis.OrderBy(a => a.Id).ToList();
            cl.__ListContractType = db.TblContractTypes.OrderBy(a => a.FkSalmali).ThenBy(a => a.Code).ToList();

            return RedirectToAction("index");
        }

        [ValidateAntiForgeryToken()]
        [HttpPost]
        public IActionResult setPer(int id, Guid[] fkus)
        {
            var x = db.TblUserSals.Where(a => a.FkSal == id);

            db.TblUserSals.RemoveRange(x);
            foreach (var item in fkus)
            {
                db.TblUserSals.Add(new web_db.TblUserSal { FkSal = id, FkUser = item });

            }
            db.SaveChanges();

            return RedirectToAction("Create", new { id = id });
        }


        public IActionResult listCusSend(int id)
        {

            var sal = User._getuserSalMaliDef();

            var listcus = from n in db.TblCustomers
                              // let c=db.TblCustomers.Where(a=>a.FkSalmali==setsal)

                          where n.FkSalmali == sal && n.IsEnable //&& (c.Any(a=>a.Code==n.Code)==false)
                          select new Models.tbls.customer.customer(n, db, false);


            ViewBag.setsal = id;
            return View(listcus.ToList());
        }

        public async Task<IActionResult> CusSendAsync(Guid id, int setsal, Guid? fkType, bool portage, bool fagatcus = false)
        {

            var sal = User._getuserSalMaliDef();

            await CloneCustomerAsync(id, setsal);
            if (fagatcus == false)
            {
                using (var transaction = new TransactionScope())
                {
                    foreach (var item in db.TblContracts.Where(a => a.FkCustomer == id && a.FkContractType == fkType))
                    {
                        CloneContract(item.Id, setsal);

                    }

                    if (portage)
                    {
                        List<porret> l = new List<porret>();
                        foreach (var item in db.TblPortages.Where(a => a.FkCustomer == id && a.FkContracttype == fkType))
                        {
                            l.AddRange(ClonePortage(item.Id, setsal));
                        }

                        foreach (var item in l.GroupBy(a => a.Product))
                        {
                            var h = l.Select(a => a.id).Distinct();
                            if (item.Key)
                            {

                                foreach (var item2 in h)
                                {
                                    Models.cl.refTblStoreLogcontract(item2, db);

                                }


                            }
                            else
                            {
                                foreach (var item2 in h)
                                {
                                    Models.cl.refTblStoreLogcontractType(item2, setsal, db);
                                }
                            }
                        }
                        db.SaveChanges();

                    }

                    transaction.Complete();
                }

            }

            return Ok("انتقال  انجام شد");
        }

        private class porret
        {
            public Guid id { get; set; }
            public bool Product { get; set; }


        }

        private async Task<bool> CloneCustomerAsync(Guid id, int setYear)
        {


            var customer = db.TblCustomers.Find(id);
            var customerClone = db.TblCustomers.SingleOrDefault(a => a.Code == customer.Code && a.FkSalmali == setYear); ;
            #region CloneCustomer
            {

                if (customerClone == null)
                {
                    var Mali_KindOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_KindOT) ?? new web_db.TblConf()).Value;

                    if (Mali_KindOT.IsEmpty() == false)
                    {
                        var cusc = await cl.AddPayvastCustomerAsync(db, customer.Mob, setYear);
                        customerClone = db.TblCustomers.Find(cusc);
                        customerClone.Code = customer.Code;


                    }
                    else
                    {
                        customerClone = new TblCustomer
                        {
                            Code = customer.Code,
                            Id = Guid.NewGuid(),
                            Addras = customer.Addras,
                            FkSalmali = setYear,
                            IsEnable = true,
                            NationalCode = customer.NationalCode,
                            Mob = customer.Mob,
                            Title = customer.Title,
                            IdOtherSystem = customer.IdOtherSystem
                        };
                        db.TblCustomers.Add(customerClone);

                    }




                }

            }

            #endregion


            db.SaveChanges();
            return true;
        }
        private bool CloneContract(Guid id, int setYear)
        {

            var con = db.TblContracts.Find(id);
            var contype = db.TblContractTypes.Find(con.FkContractType);
            var contypeclone = db.TblContractTypes.Single(a => a.Code == contype.Code && a.FkSalmali == setYear);
            var conClone = db.TblContracts.SingleOrDefault(a => a.Code == con.Code && a.FkSalmali == setYear);

            var customer = db.TblCustomers.Find(con.FkCustomer);
            var customerClone = db.TblCustomers.SingleOrDefault(a => a.Code == customer.Code && a.FkSalmali == setYear); ;

            #region Clone Contract
            {


                if (conClone == null)
                {
                    conClone = new web_db.TblContract
                    {
                        FkSalmali = setYear,
                        Id = Guid.NewGuid(),
                        FkContractType = contypeclone.Id,
                        FkCustomer = customerClone.Id
                    };

                    db.TblContracts.Add(conClone);
                }
                {
                    conClone.Azdate = con.Azdate;
                    conClone.Code = con.Code;
                    conClone.CountMaxIn = con.CountMaxIn;
                    conClone.CountMaxOut = con.CountMaxOut;
                    conClone.Date = con.Date;
                    conClone.Dateadd = con.Dateadd;
                    conClone.Dateedit = con.Dateedit;
                    conClone.FkContractType = contypeclone.Id;
                    conClone.FkCustomer = customerClone.Id;
                    conClone.FkSalmali = setYear;
                    conClone.FkUsAdd = con.FkUsAdd;
                    conClone.FkUsEdit = con.FkUsEdit;
                    conClone.IsEndVrud = con.IsEndVrud;
                    conClone.IsEndXroj = con.IsEndXroj;
                    conClone.PercentForOut = con.PercentForOut;
                    conClone.PriceOfBoxIn = con.PriceOfBoxIn;
                    conClone.PriceOfBoxOut = con.PriceOfBoxOut;
                    conClone.PriceOfKiloIn = con.PriceOfKiloIn;
                    conClone.PriceOfKiloOut = con.PriceOfKiloOut;
                    conClone.Tadate = con.Tadate;
                    conClone.WeightMaxIn = con.WeightMaxIn;
                    conClone.WeightMaxOut = con.WeightMaxOut;
                    conClone.Txt = con.Txt;

                }




            }
            #endregion

            #region Clone ContractRows
            {
                db.TblContractPackings.RemoveRange(db.TblContractPackings.Where(a => a.FkContract == conClone.Id));
                db.TblContractProducts.RemoveRange(db.TblContractProducts.Where(a => a.FkContract == conClone.Id));
                foreach (var item in db.TblContractPackings.Where(a => a.FkContract == con.Id))
                {
                    db.TblContractPackings.Add(new web_db.TblContractPacking
                    {
                        FkContract = conClone.Id,
                        FkPacking = item.FkPacking
                    });
                }
                foreach (var item in db.TblContractProducts.Where(a => a.FkContract == con.Id))
                {

                    db.TblContractProducts.Add(new web_db.TblContractProduct
                    {
                        FkContract = conClone.Id,
                        FkProduct = item.FkProduct

                    });
                }

            }
            #endregion

            db.SaveChanges();


            return true;
        }
        private porret[] ClonePortage(Guid id, int setYear)
        {
            var portage = db.TblPortages.Find(id);
            var customer = db.TblCustomers.Find(portage.FkCustomer);
            var Clone = db.TblPortages.SingleOrDefault(a => a.Code == portage.Code && a.KindCode == portage.KindCode && a.FkSalmali == setYear);

            var contype = db.TblContractTypes.Find(portage.FkContracttype);
            var contypeclone = db.TblContractTypes.Single(a => a.Code == contype.Code && a.FkSalmali == setYear);
            var customerClone = db.TblCustomers.SingleOrDefault(a => a.Code == customer.Code && a.FkSalmali == setYear); ;


            if (contypeclone.KindCotractType == TblContractType.KindCotractTypeEnum.ASardKhane)
            {

                #region Clone Portage
                {
                    if (Clone == null)
                    {
                        Clone = new TblPortage
                        {
                            FkSalmali = setYear,
                            Id = Guid.NewGuid(),


                        };

                        db.TblPortages.Add(Clone);
                    }
                    Clone.FkContracttype = contypeclone.Id;
                    Clone.FkCustomer = customerClone.Id;
                    Clone.CarMashin = portage.CarMashin;
                    Clone.CarRanande = portage.CarRanande;
                    Clone.CarShMashin = portage.CarShMashin;
                    Clone.CarTell = portage.CarTell;
                    Clone.Code = portage.Code;
                    Clone.Date1 = portage.Date1;
                    Clone.Date2 = portage.Date2;
                    Clone.Dateadd1 = portage.Dateadd1;
                    Clone.Dateadd2 = portage.Dateadd2;
                    Clone.Dateedit1 = portage.Dateedit1;
                    Clone.Dateedit2 = portage.Dateedit2;
                    Clone.FkCar = portage.FkCar;

                    Clone.FkUsAdd1 = portage.FkUsAdd1;
                    Clone.FkUsAdd2 = portage.FkUsAdd2;
                    Clone.FkUsEdit1 = portage.FkUsEdit1;
                    Clone.FkUsEdit2 = portage.FkUsEdit2;
                    Clone.FkUsPermit = portage.FkUsPermit;
                    Clone.IsDel = portage.IsDel;
                    Clone.IsEnd = portage.IsEnd;
                    Clone.IsPermitOk = portage.IsPermitOk;
                    Clone.KindCode = portage.KindCode;
                    Clone.KindTitle = portage.KindTitle;
                    Clone.OtcodeResid = portage.OtcodeResid;
                    Clone.PackingCount = portage.PackingCount;
                    Clone.PackingOfWeight = portage.PackingOfWeight;
                    Clone.SmsOk = portage.SmsOk;
                    // Clone.SaveFaktor = portage.SaveFaktor;
                    Clone.SmsVaziat = portage.SmsVaziat;
                    //    Clone.SumMoney = portage.SumMoney;
                    Clone.Txt = portage.Txt;
                    Clone.Weight1 = portage.Weight1;
                    Clone.Weight1IsBascul = portage.Weight1IsBascul;
                    Clone.TxtPermit = portage.TxtPermit;
                    Clone.Weight2 = portage.Weight2;
                    Clone.Weight2IsBascul = portage.Weight2IsBascul;
                    Clone.WeightNet = portage.WeightNet;







                }
                #endregion


                #region Clone PortageRows


                foreach (var item in db.TblPortageRows.Where(a => a.FkPortage == Clone.Id))
                {
                    db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == item.Id));
                    db.TblPortageRows.Remove(item);

                }
                foreach (var item in db.TblPortageRows.Where(a => a.FkPortage == portage.Id))
                {
                    var cont = db.TblContracts.Find(item.FkContract);
                    var contClone = db.TblContracts.Single(a => a.Code == cont.Code && a.FkSalmali == setYear);

                    var v = new TblPortageRow
                    {
                        Id = Guid.NewGuid(),
                        FkContractType = Clone.FkContracttype,
                        FkContract = contClone.Id,
                        FkPortage = Clone.Id,
                        Code = item.Code,
                        CodeLocation = item.CodeLocation,
                        Count = item.Count,
                        Date = item.Date,
                        FkLocation1 = item.FkLocation1,
                        FkLocation2 = item.FkLocation2,
                        FkLocation3 = item.FkLocation3,
                        FkPacking = item.FkPacking,
                        FkProduct = item.FkProduct,
                        FkUser = item.FkUser,
                        IsNimPalet = item.IsNimPalet,
                        Txt = item.Txt,
                        WeightOne = item.WeightOne

                    };

                    db.TblPortageRows.Add(v);

                    foreach (var iteminj in db.TblPortageRowInjuries.Where(a => a.FkPortageRow == item.Id))
                    {
                        db.TblPortageRowInjuries.Add(new web_db.TblPortageRowInjury { FkInjury = iteminj.FkInjury, FkPortageRow = v.Id });
                    }

                }


                #endregion



                #region Clone images

                db.TblPortageDocuments.RemoveRange(db.TblPortageDocuments.Where(a => a.FkPortage == Clone.Id));
                foreach (var item in db.TblPortageDocuments.Where(a => a.FkPortage == portage.Id))
                {
                    var c = new TblPortageDocument
                    {
                        Id = Guid.NewGuid(),
                        FkPortage = Clone.Id,
                        Date = item.Date,
                        Image = item.Image,
                        Kind = item.Kind

                    };

                    db.TblPortageDocuments.Add(c);
                }

                #endregion
                db.SaveChanges();
                var x = db.TblPortageRows.Where(a => a.FkPortage == Clone.Id);
                return x.Select(a => new porret { id = (Guid)a.FkContract, Product = true }).Distinct().ToArray();

            }
            else if (contypeclone.KindCotractType == TblContractType.KindCotractTypeEnum.ASabad)

            {

                #region Clone Portage
                {
                    if (Clone == null)
                    {
                        Clone = new TblPortage
                        {
                            FkSalmali = setYear,
                            Id = Guid.NewGuid(),


                        };

                        db.TblPortages.Add(Clone);
                    }
                    Clone.FkContracttype = contypeclone.Id;
                    Clone.FkCustomer = customerClone.Id;
                    Clone.CarMashin = portage.CarMashin;
                    Clone.CarRanande = portage.CarRanande;
                    Clone.CarShMashin = portage.CarShMashin;
                    Clone.CarTell = portage.CarTell;
                    Clone.Code = portage.Code;
                    Clone.Date1 = portage.Date1;
                    Clone.Date2 = portage.Date2;
                    Clone.Dateadd1 = portage.Dateadd1;
                    Clone.Dateadd2 = portage.Dateadd2;
                    Clone.Dateedit1 = portage.Dateedit1;
                    Clone.Dateedit2 = portage.Dateedit2;
                    Clone.FkCar = portage.FkCar;

                    Clone.FkUsAdd1 = portage.FkUsAdd1;
                    Clone.FkUsAdd2 = portage.FkUsAdd2;
                    Clone.FkUsEdit1 = portage.FkUsEdit1;
                    Clone.FkUsEdit2 = portage.FkUsEdit2;
                    Clone.FkUsPermit = portage.FkUsPermit;
                    Clone.IsDel = portage.IsDel;
                    Clone.IsEnd = portage.IsEnd;
                    Clone.IsPermitOk = portage.IsPermitOk;
                    Clone.KindCode = portage.KindCode;
                    Clone.KindTitle = portage.KindTitle;
                    Clone.OtcodeResid = portage.OtcodeResid;
                    Clone.PackingCount = portage.PackingCount;
                    Clone.PackingOfWeight = portage.PackingOfWeight;
                    Clone.SmsOk = portage.SmsOk;
                    // Clone.SaveFaktor = portage.SaveFaktor;
                    Clone.SmsVaziat = portage.SmsVaziat;
                    //    Clone.SumMoney = portage.SumMoney;
                    Clone.Txt = portage.Txt;
                    Clone.Weight1 = portage.Weight1;
                    Clone.Weight1IsBascul = portage.Weight1IsBascul;
                    Clone.TxtPermit = portage.TxtPermit;
                    Clone.Weight2 = portage.Weight2;
                    Clone.Weight2IsBascul = portage.Weight2IsBascul;
                    Clone.WeightNet = portage.WeightNet;







                }
                #endregion


                #region Clone PortageRows


                foreach (var item in db.TblPortageRows.Where(a => a.FkPortage == Clone.Id))
                {
                    db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == item.Id));
                    db.TblPortageRows.Remove(item);

                }
                foreach (var item in db.TblPortageRows.Where(a => a.FkPortage == portage.Id))
                {
                    var cont = db.TblContracts.Find(item.FkContract);
                    var contClone = db.TblContracts.Single(a => a.Code == cont.Code && a.FkSalmali == setYear);

                    var v = new TblPortageRow
                    {
                        Id = Guid.NewGuid(),
                        FkContractType = Clone.FkContracttype,
                        FkContract = contClone.Id,
                        FkPortage = Clone.Id,
                        Code = item.Code,
                        CodeLocation = item.CodeLocation,
                        Count = item.Count,
                        Date = item.Date,
                        FkLocation1 = item.FkLocation1,
                        FkLocation2 = item.FkLocation2,
                        FkLocation3 = item.FkLocation3,
                        FkPacking = item.FkPacking,
                        FkProduct = item.FkProduct,
                        FkUser = item.FkUser,
                        IsNimPalet = item.IsNimPalet,
                        Txt = item.Txt,
                        WeightOne = item.WeightOne

                    };

                    db.TblPortageRows.Add(v);

                    foreach (var iteminj in db.TblPortageRowInjuries.Where(a => a.FkPortageRow == item.Id))
                    {
                        db.TblPortageRowInjuries.Add(new web_db.TblPortageRowInjury { FkInjury = iteminj.FkInjury, FkPortageRow = v.Id });
                    }

                }


                #endregion



                #region Clone images

                db.TblPortageDocuments.RemoveRange(db.TblPortageDocuments.Where(a => a.FkPortage == Clone.Id));
                foreach (var item in db.TblPortageDocuments.Where(a => a.FkPortage == portage.Id))
                {
                    var c = new TblPortageDocument
                    {
                        Id = Guid.NewGuid(),
                        FkPortage = Clone.Id,
                        Date = item.Date,
                        Image = item.Image,
                        Kind = item.Kind

                    };

                    db.TblPortageDocuments.Add(c);
                }

                #endregion
                db.SaveChanges();
                return new porret[] { new porret { id = contypeclone.Id, Product = false } };

            }

            return null;

        }

    }
}
