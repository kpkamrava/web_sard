namespace web_sard.Models.tbls.contract
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="contract" />.
    /// </summary>
    public class contract
    {

        public static bool IsPermitOK(web_db.TblPortage por, web_db.sardweb_Context db)
        {


            if (por.KindCode == (int)portage.kindPortage.kindPortageEnum.In)
            {


                long? countForIn;
                decimal? weightForIn;
                cl.GetMojavez(por.FkCustomer, por.FkContracttype, db, true, out countForIn, out weightForIn);


                countForIn -= por.PackingCount;
                weightForIn -= por.WeightNet;


                if (countForIn < 0 || weightForIn < 0)
                {
                    return false;
                }
            }

            if (por.KindCode == (int)portage.kindPortage.kindPortageEnum.Out)
            {

                long? countForOut;
                decimal? weightForOut;
                cl.GetMojavez(por.FkCustomer, por.FkContracttype, db, false, out countForOut, out weightForOut);
                countForOut += por.PackingCount;
                weightForOut += por.WeightNet;


                if (countForOut < 0 || weightForOut < 0)
                {
                    return false;
                }
            }
            return true;
        }


        public contract()
        {
        }


        public contract(web_db.TblContract row, web_db.sardweb_Context db, bool isProductsPackings = false, bool mohasebeMande = false, Guid? fkportageadd = null)
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
            this.Custumer = cus.Title;

            this.isEndVrud = row.IsEndVrud ?? false;
            this.isEndXroj = row.IsEndXroj ?? false;


            this._WeightIn = row.SumInWeight;
            this._WeightOut = row.SumOutWeight;
            this._CountOut = row.SumOutCount;
            this._CountIn = row.SumInCount;

            prodocts = new List<Models.tbls.alltbl>();
            prodoctsId = new List<Guid>();
            packings = new List<Models.tbls.alltbl>();
            packingsId = new List<Guid>();
            this.ContractType =  db.TblContractTypes.Find(row.FkContractType);
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
                mandeHesab.Add(new mandeHesabclass(x, this, db, true));

                var t = x.AsEnumerable().GroupBy(a => new { a.FkPacking, a.FkProduct }).ToList();
                foreach (var item in t)
                {
                    var str = (cl._ListProduct.SingleOrDefault(a => a.Id == item.Key.FkProduct) ?? new web_db.TblProduct()).Title
                                 + " " +
                              (cl._ListPacking.SingleOrDefault(a => a.Id == item.Key.FkPacking) ?? new web_db.TblPacking()).Title;

                    mandeHesab.Add(new mandeHesabclass(item.Select(a => a), this, db) { txt = str });

                }

            }
        }


        public class mandeHesabclass
        {

            public string txt { get; set; }


            public mandeHesabclass()
            {
            }


            public mandeHesabclass(IEnumerable<web_db.TblPortageRow> x, contract contr, web_db.sardweb_Context db, bool c = false)
            {


                if (contr.ContractType.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {
                    var conf = contr.ContractType.ConfigASardKhane();
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

                        this.Weight = new mohasebe<Decimal?>
                        {
                            InMaxContract = (contr.WeightMaxIn).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count * (decimal?)a.WeightOne) + xinback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),
                            OutMaxContract = contr.WeightMaxOut.gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count * (decimal?)a.WeightOne) + xoutback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),

                        };

                        if (c == false)
                        {
                            Count.InMaxContract = null;
                            Weight.InMaxContract = null;
                        }

                        if (conf.OutControlByPercent)
                        {
                            this.Count.OutMaxContract = this.Count.InSum * contr.PercentForOut / 100;
                            this.Weight.OutMaxContract = this.Weight.InSum * contr.PercentForOut / 100;

                        }

                        
                            if (c)
                            {
                                Weight.InMandeContract = Weight.InMaxContract - Weight.InSum;
                                Weight.OutMandeContract = Weight.OutMaxContract - Weight.OutSum;
                                Count.OutMandeContract = Count.OutMaxContract - Count.OutSum;

                            }
                            else
                            {
                                Weight.OutMaxContract = null;
                                Count.OutMaxContract = null;

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
                else
                  if (contr.ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

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

                        this.Weight = new mohasebe<Decimal?>
                        {
                            InMaxContract = (contr.WeightMaxIn).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count * (decimal?)a.WeightOne) + xinback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),
                            OutMaxContract = contr.WeightMaxOut.gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count * (decimal?)a.WeightOne) + xoutback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),

                        };

                        if (c == false)
                        {
                            Count.InMaxContract = null;
                            Weight.InMaxContract = null;
                        }

                        

                   
                      
                            if (c)
                            {
                                Count.InMandeContract = (Count.InMaxContract - Count.InSum);
                                Count.OutMandeContract = (Count.OutMaxContract - Count.OutSum);
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


              
            }


            public mandeHesabclass(IEnumerable<web_db.TblPortageRow> x, web_db.sardweb_Context db, bool c = false)
            {
                if (x.Any() == false)
                {
                    return;
                }
                var contrtt = x.Select(a => a.FkContract).Distinct();
                var contr = db.TblContracts.Where(a => contrtt.Contains(a.Id));
                var ContractType = db.TblContractTypes.Single(a => a.Id == contr.First().FkContractType);
                    if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {
                    var conf = ContractType.ConfigASardKhane();
                    {
                        var xx = x.Where(a => a.FkPortage.HasValue);
                        var xin = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.In);
                        var xinback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.InBack);
                        var xout = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.Out);
                        var xoutback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.OutBack);

                        this.Count = new mohasebe<long?>
                        {
                            InMaxContract = contr.Sum(z => z.CountMaxIn).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count) + xinback.Sum(a => a.Count)).gadrmotlagh(),
                            OutMaxContract = contr.Sum(z => z.CountMaxOut).gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count) + xoutback.Sum(a => a.Count)).gadrmotlagh()


                        };

                        this.Weight = new mohasebe<Decimal?>
                        {
                            InMaxContract = (contr.Sum(z => z.WeightMaxIn)).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count * (decimal?)a.WeightOne) + xinback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),
                            OutMaxContract = contr.Sum(z => z.WeightMaxOut).gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count * (decimal?)a.WeightOne) + xoutback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),

                        };

                        if (c == false)
                        {
                            Count.InMaxContract = null;
                            Weight.InMaxContract = null;
                        }
                        if (conf.OutControlByPercent)
                        {
                            this.Count.OutMaxContract = this.Count.InSum * contr.Sum(z => z.PercentForOut / 100);
                            this.Weight.OutMaxContract = this.Weight.InSum * contr.Sum(z => z.PercentForOut / 100);

                        }

                        
                            if (c)
                            {
                                Weight.InMandeContract = Weight.InMaxContract - Weight.InSum;
                                Weight.OutMandeContract = Weight.OutMaxContract - Weight.OutSum;
                                Count.OutMandeContract = Count.OutMaxContract - Count.OutSum;

                            }
                            else
                            {
                                Weight.OutMaxContract = null;
                                Count.OutMaxContract = null;

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
                else if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
                {
                    var conf = ContractType.ConfigASardKhane();
                    {
                        var xx = x.Where(a => a.FkPortage.HasValue);
                        var xin = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.In);
                        var xinback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.InBack);
                        var xout = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.Out);
                        var xoutback = xx.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.OutBack);

                        this.Count = new mohasebe<long?>
                        {
                            InMaxContract = contr.Sum(z => z.CountMaxIn).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count) + xinback.Sum(a => a.Count)).gadrmotlagh(),
                            OutMaxContract = contr.Sum(z => z.CountMaxOut).gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count) + xoutback.Sum(a => a.Count)).gadrmotlagh()


                        };

                        this.Weight = new mohasebe<Decimal?>
                        {
                            InMaxContract = (contr.Sum(z => z.WeightMaxIn)).gadrmotlagh(),
                            InSum = (xin.Sum(a => a.Count * (decimal?)a.WeightOne) + xinback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),
                            OutMaxContract = contr.Sum(z => z.WeightMaxOut).gadrmotlagh(),
                            OutSum = (xout.Sum(a => a.Count * (decimal?)a.WeightOne) + xoutback.Sum(a => a.Count * (decimal?)a.WeightOne)).gadrmotlagh(),

                        };

                        if (c == false)
                        {
                            Count.InMaxContract = null;
                            Weight.InMaxContract = null;
                        }
                        if (conf.OutControlByPercent)
                        {
                            this.Count.OutMaxContract = this.Count.InSum * contr.Sum(z => z.PercentForOut / 100);
                            this.Weight.OutMaxContract = this.Weight.InSum * contr.Sum(z => z.PercentForOut / 100);

                        }



                        if (c)
                        {
                            Count.InMandeContract = (Count.InMaxContract - Count.InSum);
                            Count.OutMandeContract = (Count.OutMaxContract - Count.OutSum);
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
              
            }

            /// <summary>
            /// Defines the <see cref="mohasebe{T}" />.
            /// </summary>
            /// <typeparam name="T">.</typeparam>
            public class mohasebe<T>  // where T  :struct
            {
                /// <summary>
                /// Gets or sets the InSum.
                /// </summary>
                public T InSum { get; set; }

                /// <summary>
                /// Gets or sets the InMaxContract.
                /// </summary>
                public T InMaxContract { get; set; }

                /// <summary>
                /// Gets or sets the InMandeContract.
                /// </summary>
                public T InMandeContract { get; set; }

                /// <summary>
                /// Gets or sets the OutSum.
                /// </summary>
                public T OutSum { get; set; }

                /// <summary>
                /// Gets or sets the OutMaxContract.
                /// </summary>
                public T OutMaxContract { get; set; }

                /// <summary>
                /// Gets or sets the OutMandeContract.
                /// </summary>
                public T OutMandeContract { get; set; }
            }

            /// <summary>
            /// Gets or sets the Weight.
            /// </summary>
            public mohasebe<Decimal?> Weight { get; set; }

            /// <summary>
            /// Gets or sets the Count.
            /// </summary>
            public mohasebe<long?> Count { get; set; }

            /// <summary>
            /// Gets or sets the mandelocations.
            /// </summary>
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
        public web_db.TblContractType ContractType { get; set; }


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
        [Display(Name = "پایان ورود قرارداد")]
        public bool isEndVrud { get; set; }
        [Display(Name = "پایان خروج قرارداد")]
        public bool isEndXroj { get; set; }


        [Display(Name = "زمان تحویل تا")]
        [Required]
        public string Tadate { get; set; }

        /// <summary>
        /// Gets or sets the Txt.
        /// </summary>
        [Display(Name = "توضیحات")]
        public string Txt { get; set; }

        /// <summary>
        /// Gets or sets the PriceOfKiloIn.
        /// </summary>
        [Display(Name = "نرخ هر کیلو ورود")]
        public decimal? PriceOfKiloIn { get; set; }

        /// <summary>
        /// Gets or sets the PriceOfBoxIn.
        /// </summary>
        [Display(Name = "نرخ هر جعبه ورود")]
        public decimal? PriceOfBoxIn { get; set; }

        /// <summary>
        /// Gets or sets the PriceOfKiloOut.
        /// </summary>
        [Display(Name = "نرخ هر کیلو خروج")]
        public decimal? PriceOfKiloOut { get; set; }

        /// <summary>
        /// Gets or sets the PriceOfBoxOut.
        /// </summary>
        [Display(Name = "نرخ هر جعبه خروج")]
        public decimal? PriceOfBoxOut { get; set; }

        /// <summary>
        /// Gets or sets the dateadd.
        /// </summary>
        [Display(Name = "تاریخ ثبت")]
        public string dateadd { get; set; }

        /// <summary>
        /// Gets or sets the dateedit.
        /// </summary>
        [Display(Name = "تاریخ ویرایش")]
        public string dateedit { get; set; }

        /// <summary>
        /// Gets or sets the useradd.
        /// </summary>
        [Display(Name = "کاربر ثبت کننده ")]
        public string useradd { get; set; }

        /// <summary>
        /// Gets or sets the useredit.
        /// </summary>
        [Display(Name = "کاربر ویرایش کننده")]
        public string useredit { get; set; }

        /// <summary>
        /// Gets or sets the prodocts.
        /// </summary>
        [Display(Name = "محصولات")]
        public List<Models.tbls.alltbl> prodocts { get; set; }

        /// <summary>
        /// Gets or sets the prodoctsId.
        /// </summary>
        [Display(Name = "محصولات")]
        [Required]
        public List<Guid> prodoctsId { get; set; }

        /// <summary>
        /// Gets or sets the packings.
        /// </summary>
        [Display(Name = "سبدها")]
        public List<Models.tbls.alltbl> packings { get; set; }

        /// <summary>
        /// Gets or sets the packingsId.
        /// </summary>
        [Display(Name = "سبدها")]
        [Required]
        public List<Guid> packingsId { get; set; }

        /// <summary>
        /// Gets or sets the _CountIn.
        /// </summary>
        [Display(Name = " مقدار ورود")]

        public long? _CountIn { get; set; }

        /// <summary>
        /// Gets or sets the _WeightIn.
        /// </summary>
        [Display(Name = " وزن ورود")]

        public decimal? _WeightIn { get; set; }

        /// <summary>
        /// Gets or sets the _CountOut.
        /// </summary>
        [Display(Name = " مقدار خروج")]

        public long? _CountOut { get; set; }

        /// <summary>
        /// Gets or sets the _WeightOut.
        /// </summary>
        [Display(Name = " وزن خروج")]

        public decimal? _WeightOut { get; set; }

        /// <summary>
        /// Gets the _WeightForOut.
        /// </summary>
        [Display(Name = "  % وزن قابل خروج")]

        public decimal? _WeightForOut
        {
            get
            {
                if (ContractType==null)
                {
                    return null;

                }
                if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {
                    var conf = ContractType.ConfigASardKhane();
                    try
                    {
                        return (conf.OutControlByPercent ? (_WeightIn * PercentForOut / 100) : _WeightIn).gadrmotlagh();
                    }
                    catch { return null; }
                }
                else
                   if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)

                {
                    try
                    {
                        return (WeightMaxOut).gadrmotlagh();
                    }
                    catch { return null; }
                }
                return null;

            }
        }

        /// <summary>
        /// Gets the _CountForOut.
        /// </summary>
        [Display(Name = "  % تعداد قابل خروج")]

        public long? _CountForOut
        {
            get
            {
                if (ContractType == null)
                {
                    return null;

                }
                if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {
                    var conf = ContractType.ConfigASardKhane();
                    return (conf.OutControlByPercent ? (((_CountIn * PercentForOut / 100))) :  _CountIn ).gadrmotlagh();

                }
                else
                   if (ContractType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)

                {
                    return (  CountMaxOut).gadrmotlagh();

                }
                return null;

            }
            
        }

        /// <summary>
        /// Gets the _WeightForInMande.
        /// </summary>
        [Display(Name = " مانده وزن ورودی")]

        public decimal? _WeightForInMande
        {
            get { return WeightMaxIn - _WeightIn; }
        }

        /// <summary>
        /// Gets the _CountForInMande.
        /// </summary>
        [Display(Name = " مانده تعداد ورودی")]

        public long? _CountForInMande
        {
            get { return CountMaxIn - _CountIn; }
        }

        /// <summary>
        /// Gets the _WeightForOutMande.
        /// </summary>
        [Display(Name = " مانده وزن خروج")]

        public decimal? _WeightForOutMande
        {
            get { return _WeightForOut + _WeightOut; }
        }

        /// <summary>
        /// Gets the _CountForOutMande.
        /// </summary>
        [Display(Name = " مانده تعداد خروج")]

        public decimal? _CountForOutMande
        {
            get { return _CountForOut + _CountOut; }
        }
    }
}
