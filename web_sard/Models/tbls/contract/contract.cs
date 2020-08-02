using Microsoft.EntityFrameworkCore;
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

            mandeHesabclass m = new mandeHesabclass(por.FkContract, db, false);

            var contype = db.TblContractType.Find(por.FkContracttype);

            if (contype.OutControlByContract==false) return true;
             
            if (por.KindCode < 10)//in
            {
                if (contype.IsProduct1Packing0)
                {
                    if (m.MaxWeightInMande>por.WeightNet)
                    {
                        return true;
                    }   

                }
                else
                {
                    if (m.MaxCountOutMande>por.PackingCount)
                    {
                        return true;
                    }
                    
                }

            }
            else//out
            { 
                
                
                if (contype.IsProduct1Packing0)
                {
                    if (m.MaxWeightOutMande>por.WeightNet)
                    {
                        return true;
                    } 
                }
                if (m.MaxCountOutMande > por.PackingCount)
                {
                    return true;
                }
             
              
              
           }







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
            this.ContractTypeTbl = new ContractType(db.TblContractType.Find(row.FkContractType));
             this.ContractType = ContractTypeTbl.Title;
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

            mandeHesab = new mandeHesabclass();

            if (mohasebeMande)
            {

                mandeHesab = new mandeHesabclass(row.Id, db);
            }
        }

        public  class mandeHesabclass 
         
        {
            public mandeHesabclass() { }
            public mandeHesabclass(Guid FkContract, web_db.sardweb_Context db,bool portagesok=true) {

                var con = db.TblContract.Find(FkContract);
                var contype = db.TblContractType.Find(con.FkContractType);
                var ppp = db.TblPortage.Where(a => a.FkContract == FkContract && a.IsDel == false && a.IsEnd);

                SumCountIN = (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.In).Sum(a => a.PackingCount) ?? 0)
                      - (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.InBack).Sum(a => a.PackingCount) ?? 0);


                SumCountOut = (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.Out).Sum(a => a.PackingCount) ?? 0)
                - (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.OutBack).Sum(a => a.PackingCount) ?? 0);

                SumWeightIN = (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.In).Sum(a => a.WeightNet) ?? 0)
                - (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.InBack).Sum(a => a.WeightNet) ?? 0);

                SumWeightOut = (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.Out).Sum(a => a.WeightNet) ?? 0)
                - (ppp.Where(a => a.KindCode == (int)Models.tbls.portage.kindPortage.kindPortageEnum.OutBack).Sum(a => a.WeightNet) ?? 0);
                 


                MaxWeightOutContract = con.WeightMaxOut ?? 0;
                MaxCountOutContract = con.CountMaxOut ?? 0;

                MaxWeightInContract = con.WeightMaxIn ?? 0;
                MaxCountInContract = con.CountMaxIn ?? 0;


                if (contype.OutControlByPercent)
                {
                    MaxWeightOutContract = SumWeightIN * (con.PercentForOut ?? 0) / 100;
                    MaxCountOutContract = SumCountIN * (con.PercentForOut ?? 0) / 100;

                }


            }
            public long SumCountIN { get; set; }
            public long SumCountOut { get; set; }
            public decimal SumWeightIN { get; set; }
            public decimal SumWeightOut { get; set; }
             
            public decimal MaxWeightInContract { get; set; }
            public long MaxCountInContract { get; set; }
             
            public decimal MaxWeightOutContract { get; set; }
            public long MaxCountOutContract { get; set; }
             
            public decimal MaxWeightInMande { get { return MaxWeightInContract- SumWeightIN; } }
            public long MaxCountInMande { get { return MaxCountInContract - SumCountIN; } }
             
            public decimal MaxWeightOutMande { get { return MaxWeightOutContract - SumWeightOut; } }
            public long MaxCountOutMande { get { return MaxCountOutContract - SumCountOut; } }


        }

        public mandeHesabclass mandeHesab { get; set; }
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
        public string ContractType { get; set; }
        [Display(Name = "نوع قرارداد")]
        public ContractType ContractTypeTbl { get; set; }

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
