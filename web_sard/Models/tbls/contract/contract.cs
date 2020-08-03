﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using web_lib;

namespace web_sard.Models.tbls.contract
{
    public class contract
    {


        public static bool IsPermitOK(web_db.TblPortage por, web_db.sardweb_Context db)
        {
            return false;
            //mandeHesabclass m = new mandeHesabclass(por.FkContract, db, false);

            //var contype = db.TblContractType.Find(por.FkContracttype);

            //if (contype.OutControlByContract==false) return true;
             
            //if (por.KindCode < 10)//in
            //{
            //    if (contype.IsProduct1Packing0)
            //    {
            //        if (m.MaxWeightInMande>por.WeightNet)
            //        {
            //            return true;
            //        }   

            //    }
            //    else
            //    {
            //        if (m.MaxCountOutMande>por.PackingCount)
            //        {
            //            return true;
            //        }
                    
            //    }

            //}
            //else//out
            //{ 
                
                
            //    if (contype.IsProduct1Packing0)
            //    {
            //        if (m.MaxWeightOutMande>por.WeightNet)
            //        {
            //            return true;
            //        } 
            //    }
            //    if (m.MaxCountOutMande > por.PackingCount)
            //    {
            //        return true;
            //    }
             
              
              
        //   }







            return false;

        }




        public contract() { }
        public contract(web_db.TblContract row,web_db.sardweb_Context db,bool isProductsPackings=false,bool mohasebeMande=false)
        {
            var cus = db.TblCustomer.Find(row.FkCustomer)??new web_db.TblCustomer();
            this.Azdate = row.Azdate.ToPersianDate();
            this.Code =Convert.ToDouble(row.Code);
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
            this.useradd =(db.TblUser.Find(row.FkUsAdd)??new web_db.TblUser()).Title ;
            this.useredit = (db.TblUser.Find(row.FkUsEdit) ?? new web_db.TblUser()).Title;
            this.Custumer =cus.Title +" ("+ cus.Code+")"; 
            prodocts = new List<Models.tbls.alltbl>();
            prodoctsId = new List<Guid>();
            packings = new List<Models.tbls.alltbl>();
            packingsId = new List<Guid>();
            this.ContractType  = new ContractType(db.TblContractType.Find(row.FkContractType));
            if (isProductsPackings)
                {
                 
                    var listp= db.TblContractProduct.Where(a=>a.FkContract==this.Id).Include(a=>a.FkProductNavigation);

                    foreach (var item in listp)
                    {
                        prodoctsId.Add(item.FkProduct);
                        prodocts.Add(new alltbl {code=item.FkProductNavigation.Code,key=item.FkProduct,title=item.FkProductNavigation.Title } );
                    }

                    var listpp = db.TblContractPacking.Where(a => a.FkContract == this.Id).Include(a => a.FkPackingNavigation);

                    foreach (var item in listpp)
                    {
                        packingsId.Add(item.FkPacking);
                        packings.Add(new alltbl { code = item.FkPackingNavigation.Code, key = item.FkPacking, title = item.FkPackingNavigation.Title });
                    }

                }

           this. mandeHesab = new List<mandeHesabclass>();
          
            if (mohasebeMande)
            {
                var x = db.TblPortageRow.Where(a => a.FkContract == row.Id).Include(a => a.FkPortageNavigation);

                mandeHesab.Add( new mandeHesabclass(x ,this));

              
                foreach (var item in x.GroupBy(a=>new { a.FkPacking, a.FkProduct }))
                {
                    var str = (cl._ListProduct.SingleOrDefault(a => a.Id == item.Key.FkProduct) ?? new web_db.TblProduct()).Title
                                 + " " + 
                              (cl._ListPacking.SingleOrDefault(a => a.Id == item.Key.FkPacking) ?? new web_db.TblPacking()).Title;

                    mandeHesab.Add(new mandeHesabclass(item.Select(a=>a), this) {txt= str });

                }
              


            }
        }

        public class mandeHesabclass

        {
            public string txt { get; set; }
            public mandeHesabclass() { }
            public mandeHesabclass(IEnumerable<web_db.TblPortageRow> x, contract contr)
            {

                { var xin = x.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.In);
                    var xinback = x.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.InBack);
                    var xout = x.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.Out);
                    var xoutback = x.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.OutBack);

                    this.Count = new mohasebe<long?>
                    {
                        InMaxContract = contr.CountMaxIn ?? 0,
                        InSum = xin.Sum(a => a.Count) - xinback.Sum(a => a.Count),
                        OutMaxContract = contr.CountMaxOut,
                        OutSum = xout.Sum(a => a.Count) - xoutback.Sum(a => a.Count)


                    };
                    this.Weight = new mohasebe<Decimal?>
                    {
                        InMaxContract = contr.WeightMaxIn,
                        InSum = xin.Sum(a => a.Count * a.FkPortageNavigation.PackingOfWeight) - xinback.Sum(a => a.Count * a.FkPortageNavigation.PackingOfWeight),
                        OutMaxContract = contr.WeightMaxOut,
                        OutSum = xout.Sum(a => a.Count * a.FkPortageNavigation.PackingOfWeight) - xoutback.Sum(a => a.Count * a.FkPortageNavigation.PackingOfWeight),


                    };

                    if (contr.ContractType.OutControlByPercent)
                    {
                        this.Count.OutMaxContract = this.Count.InSum * contr.PercentForOut / 100;
                        this.Weight.OutMaxContract = this.Weight.InSum * contr.PercentForOut / 100;

                    }



                    this.mandelocations = new Dictionary<string, long>();
                }
                {
                    foreach (var item in x.GroupBy(a => a.CodeLocation))
                    {
                        var xin = item.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.In).Sum(a => a.Count);
                        var xinback = item.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.InBack).Sum(a => a.Count);
                        var xout = item.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.Out).Sum(a => a.Count);
                        var xoutback = item.Where(a => a.FkPortageNavigation.KindCode == (int)portage.kindPortage.kindPortageEnum.OutBack).Sum(a => a.Count);


                        mandelocations.Add(item.Key, (xin- xinback)-(xout- xoutback));
                    } 
                }

            }
            public class mohasebe<T> //where T //:Nullable<struct>
            {
                public T InSum { get; set; }
                public T InMaxContract { get; set; }
                public T InMandeContract { get { return (((dynamic)InMaxContract) - ((dynamic)InSum)); } }
                public T OutSum { get; set; }
                public T OutMaxContract { get; set; }
                public T OutMandeContract { get { return (((dynamic)OutMaxContract) - ((dynamic)OutSum)); } }

            }

            public mohasebe<Decimal?> Weight { get; set; }
            public mohasebe<long?> Count { get; set; }
            public Dictionary<string, long> mandelocations { get; set; }


        }


        public List<mandeHesabclass> mandeHesab { get; set; }
        public Guid Id { get; set; }
        [Display(Name ="کد")]
        [Required]
        public double Code { get; set; }
        [Display(Name = "طرف قرارداد")]
        public Guid FkCustomer { get; set; } 
        [Display(Name = "طرف قرارداد")]
        public string  Custumer { get; set; }
        [Display(Name = "نوع قرارداد")]
         public Guid FkContractType { get; set; }
   
        [Display(Name = "نوع قرارداد")]
        public ContractType ContractType  { get; set; }

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
        public string  Azdate { get; set; }
        [Display(Name = "زمان تحویل تا")]
        [Required]
        public string  Tadate { get; set; }
        [Display(Name = "توضیحات")]
        public string Txt { get; set; }


        [Display(Name = "نرخ هر کیلو ورود")] 
        public decimal PriceOfKiloIn { get; set; }
        [Display(Name = "نرخ هر جعبه ورود")] 
        public decimal PriceOfBoxIn { get; set; }
      
        [Display(Name = "نرخ هر کیلو خروج")]
        public decimal PriceOfKiloOut { get; set; }
        [Display(Name = "نرخ هر جعبه خروج")]
        public decimal PriceOfBoxOut { get; set; }


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

        [Display(Name = "بسته بندیها")]
        public List< Models.tbls.alltbl> packings { get; set; }
        [Display(Name = "بسته بندیها")]
        [Required]
        public List<Guid> packingsId { get; set; }
    }
}
