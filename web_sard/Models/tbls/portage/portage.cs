namespace web_sard.Models.tbls.portage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using web_lib;

   
    public class portage
    { 
        public portage()
        {
            ListRows = new List<PortageRow>();
            Sadamat = new List<alltbl>();
        }

     
        public portage(web_db.TblPortage row, web_db.sardweb_Context db, bool rowChilds = false, bool isprint = false, bool documents = false)
        { 
            this.Code = row.Code;
            this.Date1 = row.Date1;

            this.Date1date = row.Date1.ToPersianDate();
            this.Date1time = new TimeSpan(row.Date1.Hour, row.Date1.Minute, 0);
            this.Date2 = row.Date2;
            if (Date2.HasValue)
            {
                this.Date2date = row.Date2.ToPersianDate();
                this.Date2time = new TimeSpan(row.Date2.Value.Hour, row.Date2.Value.Minute, 0);

            }
            this.FkSalmali = row.FkSalmali;
            this.Id = row.Id;
            this.KindCode = row.KindCode;
            this.KindTitle = row.KindTitle;
            this.PackingCount = row.PackingCount;
            this.PackingOfWeight = row.PackingOfWeight;
            this.Txt = row.Txt;
            this.Weight1 = row.Weight1;
            this.Weight2 = row.Weight2;
            this.WeightNet = row.WeightNet;

            this.CarRanande = row.CarRanande;

            this.CarShMashin = row.CarShMashin;

            this.CarTell = row.CarTell;

            this.CarMashin = row.CarMashin;

            this.Weight1IsBascul = row.Weight1IsBascul;
            this.Weight2IsBascul = row.Weight2IsBascul;



            this.IsDel = row.IsDel;
            this.IsEnd = row.IsEnd;

            {

                this.FkCustomer = row.FkCustomer;
                var cus = row.FkCustomerNavigation ?? db.TblCustomers.Find(FkCustomer);
                this.Customer = new customer.customer();
                this.Customer.Title = cus.Title;
                this.Customerstr = cus.Title;
                this.Customer.Id = cus.Id;
                if (rowChilds)
                {
                    this.Customer = new Models.tbls.customer.customer(cus, db);

                }
            }

            this.Dateadd1 = row.Dateadd1.ToPersianDateTime();
            this.Dateedit1 = row.Dateedit1.ToPersianDateTime();

            this.UsAdd1 = (db.TblUsers.Find(row.FkUsAdd1) ?? new web_db.TblUser()).Title;
            this.UsEdit1 = (db.TblUsers.Find(row.FkUsEdit1) ?? new web_db.TblUser()).Title;

            this.Dateadd2 = row.Dateadd2.ToPersianDateTime();
            this.Dateedit2 = row.Dateedit2.ToPersianDateTime();
            this.UsAdd2 = (db.TblUsers.Find(row.FkUsAdd2) ?? new web_db.TblUser()).Title;
            this.UsEdit2 = (db.TblUsers.Find(row.FkUsEdit2) ?? new web_db.TblUser()).Title;
            this.FkContractType = row.FkContracttype;
            this.ContractType =  db.TblContractTypes.Single(a => a.Id == this.FkContractType);
            this.FkCar = row.FkCar;

            this.SmsOk = row.SmsOk;
            this.OtcodeResid = row.OtcodeResid;
            this.sumMoney = row.SumMoney;
            this.SmsVaziat = row.SmsVaziat;
            this.UsPermit = (db.TblUsers.Find(row.FkUsPermit) ?? new web_db.TblUser()).Title;
            this.IsPermitOk = row.IsPermitOk;
            this.TxtPermit = row.TxtPermit ?? "";
            ListRows = new List<PortageRow>() { new PortageRow() };
            Sadamat = new List<alltbl>() { new alltbl() };
            SadamatStr = "";
            if (rowChilds)
            {
                ListRows = new List<PortageRow>();
                var z = db.TblPortageRows.Where(a => a.FkPortage == this.Id).OrderBy(a => a.Date).Select(a => new PortageRow(a, db));
                ListRows = z.ToList();
                var zs = z.Select(a => a.ListInjurysTbl);
                if (zs.Any())
                {
                    Sadamat = new List<alltbl>();
                }

                if (isprint)
                {
                    var t = new List<alltbl>();
                    foreach (var item in zs)
                    {
                        t.AddRange(item);
                    }
                    Sadamat = t.GroupBy(a => a.key).Select(a => a.First()).ToList();
                    SadamatStr = string.Join(" , ", Sadamat.Select(a => a.title));
                }
            }

            Documents = new List<PortageDocument>();
            if (documents)
            {

                Documents = db.TblPortageDocuments.Where(a => a.FkPortage == row.Id).OrderByDescending(a => a.Date).Select(a => new PortageDocument(a)).ToList();


                DocumentSign = Documents.SingleOrDefault(a => a.Kind == "Sign");
            }
        }

      
        public List<Models.tbls.portage.PortageRow> ListRows { get; set; }

       
        public List<Models.tbls.alltbl> Sadamat { get; set; }

 
        public String SadamatStr { get; set; }

    
        public ICollection<Models.tbls.portage.PortageDocument> Documents { get; set; }

    
        public Models.tbls.portage.PortageDocument DocumentSign { get; set; }
 
        public Guid Id { get; set; }

 
        public int FkSalmali { get; set; }

   
        [Display(Name = "کد")]

        public long Code { get; set; }

     
        [Display(Name = "نام راننده")]
        [Required]
        public string CarRanande { get; set; }

       
        [Display(Name = "شماره ماشین")]
        [Required]
        public string CarShMashin { get; set; }

      
        [Display(Name = "تماس راننده")]
        public string CarTell { get; set; }

        
        [Display(Name = "نوع ماشین")]

        public string CarMashin { get; set; }

      
        [Display(Name = "زمان باسکول اول")]
        public DateTime Date1 { get; set; }

      
        [Display(Name = "زمان باسکول اول")]
        [Required]
        public string Date1date { get; set; }

        
        [Display(Name = "زمان باسکول اول")]
        [Required]
        public TimeSpan Date1time { get; set; }

  
        [Display(Name = "زمان باسکول دوم")]
        public DateTime? Date2 { get; set; }

   
        [Display(Name = "زمان باسکول دوم")]
        [Required]
        public string Date2date { get; set; }

        
        [Display(Name = "زمان باسکول دوم")]
        [Required]
        public TimeSpan? Date2time { get; set; }

     
        [Display(Name = "وزن باسکول اول")]
        [Required]
        public decimal? Weight1 { get; set; }

       
        [Display(Name = "وزن باسکول دوم")]
        [Required]
        public decimal? Weight2 { get; set; }

 
        [Display(Name = "وزن خالص")]
        public decimal? WeightNet { get; set; }

 
        [Display(Name = "نوع")]
        public int KindCode { get; set; }

 
        [Display(Name = "نوع")]
        public string KindTitle { get; set; }

 
        [Display(Name = "تعداد جعبه")]
        public long? PackingCount { get; set; }

 
        [Display(Name = "میانگین وزن")]
        public double? PackingOfWeight { get; set; }
 
        [Display(Name = "توضیحات")]
        public string Txt { get; set; }

   
        [Display(Name = "توضیحات مجوز")]
        public string TxtPermit { get; set; }

  
        [Display(Name = "کاربر ثبت مجوز")]
        public string UsPermit { get; set; }
 
        [Display(Name = "ثبت کننده")]
        public string UsAdd1 { get; set; }

      
        [Display(Name = "ویرایش کننده")]
        public string UsEdit1 { get; set; }

    
        [Display(Name = "زمان ثبت")]
        public string Dateadd1 { get; set; }

 
        [Display(Name = "زمان ویرایش")]
        public string Dateedit1 { get; set; }

     
        [Display(Name = "ثبت کننده")]
        public string UsAdd2 { get; set; }

 
        [Display(Name = "ویرایش کننده")]
        public string UsEdit2 { get; set; }

      
        [Display(Name = "زمان ثبت")]
        public string Dateadd2 { get; set; }

     
        [Display(Name = "زمان ویرایش")]
        public string Dateedit2 { get; set; }
 
        public bool IsDel { get; set; }

      
        public bool? IsPermitOk { get; set; }

      
        public bool IsEnd { get; set; }

       
        public bool? SmsOk { get; set; }

    
        public string SmsVaziat { get; set; }

     
        public string OtcodeResid { get; set; }

 
        public decimal? sumMoney { get; set; }

         
        public Guid FkContractType { get; set; }

        
        public web_db.TblContractType ContractType { get; set; }

       
        [Display(Name = "طرف قرارداد"), Required]
        public Guid FkCustomer { get; set; }

        
        [Display(Name = "طرف قرارداد")]
        public customer.customer Customer { get; set; }

      
        [Display(Name = "طرف قرارداد")]
        public string Customerstr { get; set; }
 
        [Display(Name = "نوع ماشین"), Required]

        public Guid? FkCar { get; set; }

        
        public bool? Weight1IsBascul { get; set; }

       
        public bool? Weight2IsBascul { get; set; }

      
        public Guid __defcontractFK { get; set; }
    }
}
