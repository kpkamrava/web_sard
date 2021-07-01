using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using web_lib;

namespace web_sard.Models.tbls.contract
{
    public class contract
    {

        public contract() { }
        public contract(web_db.TblContract row, web_db.sardweb_Context db, bool isProductsPackings = false, bool mohasebeMande = false, Guid? fkportageadd = null, bool kiloview = true)
        {
            var cus = db.TblCustomers.Find(row.FkCustomer) ?? new web_db.TblCustomer();
            this.Azdate = row.Azdate.ToPersianDate();
            this.Code = Convert.ToDouble(row.Code);
            this.Date = row.Date;
            this.FkContractType = row.FkContractType;
            this.FkCustomer = row.FkCustomer;
            this.FkSalmali = row.FkSalmali;
            this.Id = row.Id;
            this.Tadate = row.Tadate.ToPersianDate();
            this.Txt = row.Txt;
            this.CountMaxIn = row.CountMaxIn;
            this.WeightMaxIn = row.WeightMaxIn;
            this.PercentForOut = row.PercentForOut;
            this.CountMaxOut = row.CountMaxOut;
            this.WeightMaxOut = row.WeightMaxOut;

            this.PriceOfBoxIn = row.PriceOfBoxIn;
            this.PriceOfKiloIn = row.PriceOfKiloIn;
            this.PriceOfBoxOut = row.PriceOfBoxOut;
            this.PriceOfKiloOut = row.PriceOfKiloOut;

            this.dateadd = row.Dateadd.ToPersianDatenull();
            this.dateedit = row.Dateedit.ToPersianDatenull();
            this.useradd = (db.TblUsers.Find(row.FkUsAdd) ?? new web_db.TblUser()).Title;
            this.useredit = (db.TblUsers.Find(row.FkUsEdit) ?? new web_db.TblUser()).Title;
            this.Custumer = cus.Title + " (" + cus.Code + ")";


            this._WeightIn = row.SumInWeight;
            this._WeightOut = row.SumOutWeight;




            this._CountOut = -row.SumOutCount;
            this._CountIn = -row.SumInCount;

            prodocts = new List<Models.tbls.alltbl>();
            prodoctsId = new List<Guid>();
            packings = new List<Models.tbls.alltbl>();
            packingsId = new List<Guid>();
            this.ContractType = new ContractType(db.TblContractTypes.Find(row.FkContractType));
            if (isProductsPackings)
            {

                var listp = db.TblContractProducts.Where(a => a.FkContract == this.Id).Include(a => a.FkProductNavigation);

                foreach (var item in listp)
                {
                    prodoctsId.Add(item.FkProduct);
                    prodocts.Add(new alltbl { code = item.FkProductNavigation.Code, key = item.FkProduct, title = item.FkProductNavigation.Title });
                }

                var listpp = db.TblContractPackings.Where(a => a.FkContract == this.Id).Include(a => a.FkPackingNavigation);

                foreach (var item in listpp)
                {
                    packingsId.Add(item.FkPacking);
                    packings.Add(new alltbl { code = item.FkPackingNavigation.Code, key = item.FkPacking, title = item.FkPackingNavigation.Title });
                }

            }

            this.mandeHesab = new List<mandeHesabclass>();

            if (mohasebeMande)
            {
                var x = from n in db.TblPortageRows.Include(a => a.FkPortageNavigation)
                        where n.FkContract == row.Id &&
                       (
                       (n.FkPortage.HasValue ? n.FkPortageNavigation.IsEnd : true) || n.FkPortage == fkportageadd

                       )
                        select n;
                mandeHesab.Add(new mandeHesabclass(x, this, db, true, kiloview));

                var t = x.AsEnumerable().GroupBy(a => new { a.FkPacking, a.FkProduct }).ToList();
                foreach (var item in t)
                {
                    var str = (db.TblProducts.SingleOrDefault(a => a.Id == item.Key.FkProduct) ?? new web_db.TblProduct()).Title
                                 + " " +
                              (db.TblPackings.SingleOrDefault(a => a.Id == item.Key.FkPacking) ?? new web_db.TblPacking()).Title;

                    mandeHesab.Add(new mandeHesabclass(item.Select(a => a), this, db, false, kiloview) { txt = str });

                }

            }
        }

        public class mandeHesabclass

        {
            public string txt { get; set; }
            public mandeHesabclass() { }
            public mandeHesabclass(IEnumerable<web_db.TblPortageRow> x, contract contr, web_db.sardweb_Context db, bool c = false, bool kiloview = true)
            {

                {
                    var xx = x.Where(a => a.FkPortage.HasValue);
                    var xin = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.In);
                    var xinback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.InBack);
                    var xout = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.Out);
                    var xoutback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.OutBack);

                    this.Count = new mohasebe<long?>
                    {
                        InMaxContract = contr.CountMaxIn.gadrmotlagh(),
                        InSum = (xin.Sum(a => a.Count) + xinback.Sum(a => a.Count)).gadrmotlagh(),
                        OutMaxContract = contr.CountMaxOut.gadrmotlagh(),
                        OutSum = (xout.Sum(a => a.Count) + xoutback.Sum(a => a.Count)).gadrmotlagh()


                    };
                    this.Weight = new mohasebe<Decimal?>();
                    if (kiloview)
                        this.Weight = new mohasebe<Decimal?>
                        {
                            InMaxContract = (contr.WeightMaxIn).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count * (decimal?)a.FkPortageNavigation.PackingOfWeight) + xinback.Sum(a => a.Count * (decimal?)a.FkPortageNavigation.PackingOfWeight)).gadrmotlagh(),
                            OutMaxContract = contr.WeightMaxOut.gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count * (decimal?)a.FkPortageNavigation.PackingOfWeight) + xoutback.Sum(a => a.Count * (decimal?)a.FkPortageNavigation.PackingOfWeight)).gadrmotlagh(),

                        };

                    if (c == false)
                    {
                        Count.InMaxContract = null;
                        if (kiloview) Weight.InMaxContract = null;
                    }

                    if (contr.ContractType.OutControlByPercent)
                    {
                        this.Count.OutMaxContract = this.Count.InSum * contr.PercentForOut / 100;
                        if (kiloview) this.Weight.OutMaxContract = this.Weight.InSum * contr.PercentForOut / 100;

                    }

                    if (contr.ContractType.IsProduct1Packing0)
                    {
                        if (c)
                        {
                            if (kiloview) Weight.InMandeContract = Weight.InMaxContract - Weight.InSum;
                            if (kiloview) Weight.OutMandeContract = Weight.OutMaxContract - Weight.OutSum;
                            Count.OutMandeContract = Count.OutMaxContract - Count.OutSum;

                        }
                        else
                        {
                            if (kiloview) Weight.OutMaxContract = null;
                            Count.OutMaxContract = null;

                        }

                    }
                    else
                    {
                        if (c)
                        {
                            Count.InMandeContract = (Count.InMaxContract - Count.InSum);
                            Count.OutMandeContract = (Count.OutMaxContract - Count.OutSum);
                        }
                    }



                    this.mandelocations = new List<portage.store>();
                }
                {
                    foreach (var item in x.GroupBy(a => new { a.FkContract, a.FkLocation1, a.FkLocation2, a.FkLocation3, a.FkPacking, a.FkProduct }).Select(a => a.Key))
                    {

                        mandelocations.AddRange(portage.store.getByListlog(db.TblStoreLogs.Where(a =>
                        a.FkContract == item.FkContract &&
                        a.FkLocation1 == item.FkLocation1 &&
                        a.FkLocation2 == item.FkLocation2 &&
                        a.FkLocation3 == item.FkLocation3 &&
                        a.FkPacking == item.FkPacking &&
                        a.FkProduct == item.FkProduct

                        ), db));

                    }
                }

            }


            public class mohasebe<T> //where T //:Nullable<struct>
            {
                public T InSum { get; set; }
                public T InMaxContract { get; set; }
                public T InMandeContract { get; set; }
                public T OutSum { get; set; }
                public T OutMaxContract { get; set; }
                public T OutMandeContract { get; set; }

            }

            public mohasebe<Decimal?> Weight { get; set; }
            public mohasebe<long?> Count { get; set; }
            public List<portage.store> mandelocations { get; set; }

        }

        public List<mandeHesabclass> mandeHesab { get; set; }
        public Guid Id { get; set; }
        [Display(Name = "کد")]
        [Required]
        public double Code { get; set; }
        [Display(Name = "طرف قرارداد")]
        public Guid FkCustomer { get; set; }
        [Display(Name = "طرف قرارداد")]
        public string Custumer { get; set; }
        [Display(Name = "نوع قرارداد")]
        public Guid FkContractType { get; set; }

        [Display(Name = "نوع قرارداد")]
        public ContractType ContractType { get; set; }

        [Display(Name = "حداکثر مقدار ورود")]
        [Required]
        public long? CountMaxIn { get; set; }
        [Display(Name = "حداکثر وزن ورود")]
        [Required]
        public decimal? WeightMaxIn { get; set; }

        [Display(Name = "حداکثر مقدار خروج")]
        [Required]
        public long? CountMaxOut { get; set; }
        [Display(Name = "حداکثر وزن خروج")]
        [Required]
        public decimal? WeightMaxOut { get; set; }
        [Display(Name = "حداکثر درصد خروج از مقدار ورودی")]
        [Required]
        public int? PercentForOut { get; set; }


        [Display(Name = "زمان قرارداد")]
        public DateTime Date { get; set; }
        [Display(Name = "سال مالی")]
        public int FkSalmali { get; set; }
        [Display(Name = "زمان تحویل از")]
        [Required]
        public string Azdate { get; set; }
        [Display(Name = "زمان تحویل تا")]
        [Required]
        public string Tadate { get; set; }
        [Display(Name = "توضیحات")]
        public string Txt { get; set; }


        [Display(Name = "نرخ هر کیلو ورود")]
        public decimal? PriceOfKiloIn { get; set; }
        [Display(Name = "نرخ هر جعبه ورود")]
        public decimal? PriceOfBoxIn { get; set; }

        [Display(Name = "نرخ هر کیلو خروج")]
        public decimal? PriceOfKiloOut { get; set; }
        [Display(Name = "نرخ هر جعبه خروج")]
        public decimal? PriceOfBoxOut { get; set; }


        [Display(Name = "تاریخ ثبت")]
        public string dateadd { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public string dateedit { get; set; }
        [Display(Name = "کاربر ثبت کننده ")]
        public string useradd { get; set; }
        [Display(Name = "کاربر ویرایش کننده")]
        public string useredit { get; set; }


        [Display(Name = "محصولات")]
        public List<Models.tbls.alltbl> prodocts { get; set; }
        [Display(Name = "محصولات")]
        [Required]
        public List<Guid> prodoctsId { get; set; }

        [Display(Name = "سبدها")]
        public List<Models.tbls.alltbl> packings { get; set; }
        [Display(Name = "سبدها")]
        [Required]
        public List<Guid> packingsId { get; set; }

        [Display(Name = " مقدار ورود")]

        public long? _CountIn { get; set; }
        [Display(Name = " وزن ورود")]

        public decimal? _WeightIn { get; set; }

        [Display(Name = " مقدار خروج")]

        public long? _CountOut { get; set; }
        [Display(Name = " وزن خروج")]

        public decimal? _WeightOut { get; set; }


        [Display(Name = "  % وزن قابل خروج")]

        public decimal? _WeightForOut
        {
            get
            {
                try
                {
                    return (ContractType.OutControlByPercent ? (_WeightIn * PercentForOut / 100) : ContractType.IsProduct1Packing0 ? _WeightIn : WeightMaxOut).gadrmotlagh();
                }
                catch { return null; }

            }
        }

        [Display(Name = "  % تعداد قابل خروج")]

        public long? _CountForOut
        {

            get
            {
                try
                {
                    return (ContractType.OutControlByPercent ? (((_CountIn * PercentForOut / 100))) : ContractType.IsProduct1Packing0 ? _CountIn : CountMaxOut).gadrmotlagh();
                }
                catch { return null; }
            }
        }






        [Display(Name = " مانده وزن ورودی")]

        public decimal? _WeightForInMande { get { return WeightMaxIn - _WeightIn; } }

        [Display(Name = " مانده تعداد ورودی")]

        public long? _CountForInMande
        {
            get { return CountMaxIn - _CountIn; }


        }




        [Display(Name = " مانده وزن خروج")]

        public decimal? _WeightForOutMande { get { return _WeightForOut - _WeightOut; } }

        [Display(Name = " مانده تعداد خروج")]

        public decimal? _CountForOutMande
        {
            get { return _CountForOut - _CountOut; }


        }




    }
}
