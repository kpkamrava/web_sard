using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_sard.Models.tbls.portage;

namespace web_sard.Areas.Acc.Models
{
    public class cl
    {
        public enum refPortageAccMoneyEnum
        {

            bascul,


            packing,


            product,


            kargar
        }

        public static void refPortageAccMoney(web_db.sardweb_Context db, Guid idportage)
        {
            var rowp = db.TblPortages.Find(idportage);
            if (rowp.IsEnd == false)
            {
                return;

            }
            db.TblPortageMoneys.RemoveRange(db.TblPortageMoneys.Where(a => a.FkPortage == idportage));
            var rowtype = db.TblContractTypes.Find(rowp.FkContracttype);


            var rowc = db.TblCustomers.Find(rowp.FkCustomer);
            decimal sumMoney = 0;


            if (rowtype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var conf = rowtype.ConfigASardKhane();



                foreach (var contr in db.TblPortageRows.Where(a => a.FkPortage == rowp.Id).AsEnumerable().GroupBy(a => a.FkContract))
                {
                    var cont = db.TblContracts.Find(contr.Key);

                    foreach (var item in contr.GroupBy(a => new { a.FkPacking, a.FkProduct }).Select(a => new { a.Key, a.First().Date, count = a.Sum(a => a.Count), Weight = a.Sum(a => a.Count * ((decimal)(a.WeightOne ?? 0))), a.First().FkLocation1 }))
                    {
                        var product = web_sard.Models.cl._ListProduct.SingleOrDefault(a => a.Id == item.Key.FkProduct);
                        var pack = web_sard.Models.cl._ListPacking.SingleOrDefault(a => a.Id == item.Key.FkPacking);

                        if (conf.OttypeFaktor.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                if (cont.PriceOfBoxIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxIn,
                                        PriceSum = cont.PriceOfBoxIn * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxIn * item.count;
                                }

                                if (cont.PriceOfKiloIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.Weight,
                                        PriceOneWeight = cont.PriceOfKiloIn,
                                        PriceSum = cont.PriceOfKiloIn * item.Weight,
                                        Txt = " وزن ",
                                        Kind = refPortageAccMoneyEnum.product.ToString()
                                    });
                                    sumMoney += cont.PriceOfKiloIn * item.Weight;

                                }

                            }
                        if (conf.OttypeFaktorOut.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                if (cont.PriceOfBoxOut > 0)
                                {
                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxOut,
                                        PriceSum = cont.PriceOfBoxOut * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxOut * item.count;

                                }

                                if (cont.PriceOfKiloOut > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.Weight,
                                        PriceOneWeight = cont.PriceOfKiloOut,
                                        PriceSum = cont.PriceOfKiloOut * item.Weight,
                                        Txt = " وزن ",
                                        Kind = refPortageAccMoneyEnum.product.ToString()
                                    });
                                    sumMoney += cont.PriceOfKiloOut * item.Weight;

                                }
                            }
                        if (conf.OttypeFaktorInBack.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                if (cont.PriceOfBoxIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxIn,
                                        PriceSum = cont.PriceOfBoxIn * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxIn * item.count;

                                }

                            }
                        if (conf.OttypeFaktorOutBack.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                if (cont.PriceOfBoxOut > 0)
                                {
                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxOut,
                                        PriceSum = cont.PriceOfBoxOut * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxOut * item.count;

                                }


                            }




                    }
                }

                if (
          (conf.OttypeFaktor.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In) ||
           (conf.OttypeFaktorOut.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out) ||
           (conf.OttypeFaktorInBack.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack) ||
           (conf.OttypeFaktorOutBack.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
          )
                    if (conf.basculeActive && conf.basculeNaghdi == false)
                    {
                        var car = db.TblCars.Find(rowp.FkCar);
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),


                            FkPortage = rowp.Id,
                            FkCar = rowp.FkCar.Value,
                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = 2,

                            PriceOneWeight = car.PriceTowBascol / 2,
                            PriceSum = car.PriceTowBascol,
                            Txt = " باسکول ",
                            Kind = refPortageAccMoneyEnum.bascul.ToString()
                        });
                        sumMoney += car.PriceTowBascol;
                    }





                if (conf.PriceOfBoxInKargar > 0 &&
                    (
                      rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In ||
                      rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack
                    )
                   )
                {


                    var w = rowp.PackingCount.Value;
                    var p = (conf.PriceOfBoxInKargar ?? 0);
                    //         if (p > 0)
                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }


                }
                if (conf.PriceOfBoxOutKargar > 0 && (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out || rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack))
                {
                    var w = rowp.PackingCount.Value;
                    var p = (conf.PriceOfBoxOutKargar ?? 0);


                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }


                }


                if (conf.PriceOfKiloInKargar > 0 && (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In || rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack))
                {


                    var w = rowp.WeightNet.Value;
                    var p = (conf.PriceOfKiloInKargar ?? 0);

                    if (p > 0)
                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }


                }
                if (conf.PriceOfKiloOutKargar > 0 && (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out || rowp.KindCode == (int)kindPortage.kindPortageEnum.OutBack))
                {


                    var w = rowp.WeightNet.Value;
                    var p = (conf.PriceOfKiloOutKargar ?? 0);

                    if (p > 0)
                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }



                }
            }
            else if (rowtype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {

                var conf = rowtype.ConfigASabad();



                foreach (var contr in db.TblPortageRows.Where(a => a.FkPortage == rowp.Id).AsEnumerable().GroupBy(a => a.FkContract))
                {
                    var cont = db.TblContracts.Find(contr.Key);

                    foreach (var item in contr.GroupBy(a => new { a.FkPacking, a.FkProduct }).Select(a => new { a.Key, a.First().Date, count = a.Sum(a => a.Count), Weight = a.Sum(a => a.Count * ((decimal)(a.WeightOne ?? 0))), a.First().FkLocation1 }))
                    {
                        var product = web_sard.Models.cl._ListProduct.SingleOrDefault(a => a.Id == item.Key.FkProduct);
                        var pack = web_sard.Models.cl._ListPacking.SingleOrDefault(a => a.Id == item.Key.FkPacking);

                        if (conf.OttypeFaktor.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                if (cont.PriceOfBoxIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxIn,
                                        PriceSum = cont.PriceOfBoxIn * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxIn * item.count;
                                }

                                if (cont.PriceOfKiloIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.Weight,
                                        PriceOneWeight = cont.PriceOfKiloIn,
                                        PriceSum = cont.PriceOfKiloIn * item.Weight,
                                        Txt = " وزن ",
                                        Kind = refPortageAccMoneyEnum.product.ToString()
                                    });
                                    sumMoney += cont.PriceOfKiloIn * item.Weight;

                                }

                            }
                        if (conf.OttypeFaktorOut.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                if (cont.PriceOfBoxOut > 0)
                                {
                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxOut,
                                        PriceSum = cont.PriceOfBoxOut * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxOut * item.count;

                                }

                                if (cont.PriceOfKiloOut > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.Weight,
                                        PriceOneWeight = cont.PriceOfKiloOut,
                                        PriceSum = cont.PriceOfKiloOut * item.Weight,
                                        Txt = " وزن ",
                                        Kind = refPortageAccMoneyEnum.product.ToString()
                                    });
                                    sumMoney += cont.PriceOfKiloOut * item.Weight;

                                }
                            }
                        if (conf.OttypeFaktorInBack.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                if (cont.PriceOfBoxIn > 0)
                                {

                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxIn,
                                        PriceSum = cont.PriceOfBoxIn * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxIn * item.count;

                                }

                            }
                        if (conf.OttypeFaktorOutBack.HasValue) if (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                if (cont.PriceOfBoxOut > 0)
                                {
                                    db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                                    {
                                        Date = item.Date,
                                        Id = Guid.NewGuid(),
                                        Fklocation = item.FkLocation1,
                                        FkPacking = item.Key.FkPacking,
                                        FkPortage = rowp.Id,
                                        FkProduct = item.Key.FkProduct,
                                        FkCar = rowp.FkCar.Value,
                                        FkContract = contr.Key,
                                        FkContractType = rowtype.Id,
                                        FkCustomer = rowc.Id,
                                        Weight = item.count,

                                        PriceOneWeight = cont.PriceOfBoxOut,
                                        PriceSum = cont.PriceOfBoxOut * item.count,
                                        Txt = " جعبه ها ",
                                        Kind = refPortageAccMoneyEnum.packing.ToString()
                                    });
                                    sumMoney += cont.PriceOfBoxOut * item.count;

                                }


                            }




                    }
                }

                if (
          (conf.OttypeFaktor.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In) ||
           (conf.OttypeFaktorOut.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out) ||
           (conf.OttypeFaktorInBack.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack) ||
           (conf.OttypeFaktorOutBack.HasValue && rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
          )
                    if (conf.Needbascule && conf.basculeMali && conf.basculeNaghdi == false)
                    {
                        var car = db.TblCars.Find(rowp.FkCar);
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),


                            FkPortage = rowp.Id,
                            FkCar = rowp.FkCar.Value,
                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = 2,

                            PriceOneWeight = car.PriceTowBascol / 2,
                            PriceSum = car.PriceTowBascol,
                            Txt = " باسکول ",
                            Kind = refPortageAccMoneyEnum.bascul.ToString()
                        });
                        sumMoney += car.PriceTowBascol;
                    }





                if (conf.PriceOfBoxInKargar > 0 &&
                    (
                      rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In ||
                      rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack
                    )
                   )
                {


                    var w = rowp.PackingCount.Value;
                    var p = (conf.PriceOfBoxInKargar ?? 0);
                    //         if (p > 0)
                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }


                }
                if (conf.PriceOfBoxOutKargar > 0 && (rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out || rowp.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack))
                {
                    var w = rowp.PackingCount.Value;
                    var p = (conf.PriceOfBoxOutKargar ?? 0);


                    {
                        db.TblPortageMoneys.Add(new web_db.TblPortageMoney
                        {
                            Date = rowp.Date2.GetValueOrDefault(),
                            Id = Guid.NewGuid(),

                            FkPortage = rowp.Id,

                            FkCar = rowp.FkCar.Value,

                            FkContractType = rowtype.Id,
                            FkCustomer = rowc.Id,
                            Weight = w,

                            PriceOneWeight = p,
                            PriceSum = w * p,
                            Txt = " هزینه کارگر ",
                            Kind = refPortageAccMoneyEnum.kargar.ToString()
                        });
                        sumMoney += w * p;

                    }


                }


            
     





            }








            rowp.SumMoney = sumMoney == 0 ? null : ((decimal?)sumMoney);

            db.SaveChanges();
        }

    }
}
