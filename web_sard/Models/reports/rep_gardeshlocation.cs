namespace web_sard.Models.reports
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="rep_gardeshlocation" />.
    /// </summary>
    public class rep_gardeshlocation
    {
    
        public class Model
        {
         
            [Display(Name = " موقعیت")]
            [Required]
            public Guid[] locations { get; set; }

         
            [Display(Name = "محصولات")]
            public Guid[] prodocts { get; set; }

           
            public string[] prodoctsSTR
            {
                get { return prodocts != null ? prodocts.Select(a => Models.cl._ListProduct.Single(s => s.Id == a).Title).ToArray() : new string[0]; ; }
            }

          
            [Display(Name = " سبد ")]
            public Guid[] pakings { get; set; }

           
            [Display(Name = " مشتری ")]
            public Guid[] custumer { get; set; }
          
            [Display(Name = " قرارداد")]
            public Guid[] contract { get; set; }


            public string[] pakingsSTR
            {
                get { return pakings != null ? pakings.Select(a => Models.cl._ListPacking.Single(s => s.Id == a).Title).ToArray() : new string[0]; }
            }

            
            [Display(Name = " نوع عملیات ")]
            [Required]
            public int[] kindPortage { get; set; }

           
            [Display(Name = "از تاریخ")]
            [Required]
            public string d1 { get; set; }

           
            [Display(Name = "تا تاریخ")]
            [Required]
            public string d2 { get; set; }


            [Display(Name = " بصورت مجموع ماشین")]

            public bool isMajmuh{ get; set; }


            [Display(Name = " بصورت تفکیک محصول")]

            public bool isTafkik { get; set; }



        }



        public class Rows
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Rows"/> class.
            /// </summary>
            public Rows()
            {
            }

                public Rows(web_db.TblPortageRow row, web_db.sardweb_Context db)
            {

                var por = db.TblPortages.Find(row.FkPortage);
                var con = db.TblContracts.Find(row.FkContract);
                var contype = db.TblContractTypes.Find(por.FkContracttype);
                var cus = db.TblCustomers.Find(con.FkCustomer);

                Contract = cus.Title + $" ({cus.Code})";
                Contractcode = con.Code;
                ContractType = contype.Title;
                ContractTypecode = contype.Code;
                Count = row.Count;
                date = por.Date1;
                datestr = por.Date1.ToPersianDate();
                IdPort = row.FkPortage;
                IdRow = row.Id;
                Location = row.CodeLocation;
                Packing = row.FkPacking.HasValue ? (cl._ListPacking.Single(a => a.Id == row.FkPacking).Title) : "";
                Product = row.FkProduct.HasValue ? (cl._ListProduct.Single(a => a.Id == row.FkProduct).Title) : "";
                UserAdd = db.TblUsers.Find(row.FkUser).Title;
                Weight = row.WeightOne ?? 0;
                Portagekindcode = por.KindCode;
                Portagekindstr = por.KindTitle;
                residcode = por.Code;
                Car = por.CarMashin +" "+por.CarShMashin+" ("+por.CarRanande+por.CarTell+")";

            }

            /// <summary>
            /// Gets or sets the IdRow.
            /// </summary>
            public Guid IdRow { get; set; }

            /// <summary>
            /// Gets or sets the IdPort.
            /// </summary>
            public Guid? IdPort { get; set; }

            /// <summary>
            /// Gets or sets the datestr.
            /// </summary>
            public string datestr { get; set; }

            /// <summary>
            /// Gets or sets the date.
            /// </summary>
            public DateTime date { get; set; }

            /// <summary>
            /// Gets or sets the Count.
            /// </summary>
            public long Count { get; set; }

            /// <summary>
            /// Gets or sets the Weight.
            /// </summary>
            public double Weight { get; set; }

            /// <summary>
            /// Gets or sets the Packing.
            /// </summary>
            public string Packing { get; set; }

            /// <summary>
            /// Gets or sets the Product.
            /// </summary>
            public string Product { get; set; }

            /// <summary>
            /// Gets or sets the Contract.
            /// </summary>
            public string Contract { get; set; }

            /// <summary>
            /// Gets or sets the Location.
            /// </summary>
            public string Location { get; set; }

            /// <summary>
            /// Gets or sets the Contractcode.
            /// </summary>
            public double Contractcode { get; set; }

            /// <summary>
            /// Gets or sets the ContractType.
            /// </summary>
            public string ContractType { get; set; }

            /// <summary>
            /// Gets or sets the ContractTypecode.
            /// </summary>
            public int ContractTypecode { get; set; }

            /// <summary>
            /// Gets or sets the Portagekindcode.
            /// </summary>
            public int Portagekindcode { get; set; }

          
            public string Portagekindstr { get; set; }
            public long residcode { get; set; }
            public string Car { get; set; }


            public string UserAdd { get; set; }
        }
    }
 
    public class rep_JameGarardad
    {
        
        public class Model
        {
            /// <summary>
            /// Gets or sets the Customers.
            /// </summary>
            [Display(Name = "مشتریها")]
            public Guid[] Customers { get; set; }

            /// <summary>
            /// Gets or sets the kindContracts.
            /// </summary>
            [Display(Name = "نوع قراداد")]
            [Required]
            public Guid[] kindContracts { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether GardeshYes.
            /// </summary>
            [Display(Name = "دارای گردش")]

            public bool GardeshYes { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether GardeshNo.
            /// </summary>
            [Display(Name = "فاقد گردش")]

            public bool GardeshNo { get; set; }

            /// <summary>
            /// Gets the kindContractSTR.
            /// </summary>
            public string[] kindContractSTR
            {
                get { return kindContracts != null ? (Models.cl._ListProduct.Where(s => kindContracts.Contains(s.Id)).Select(a => a.Title)).ToArray() : new string[0]; }
            }
        }

        /// <summary>
        /// Defines the <see cref="Report" />.
        /// </summary>
        public class Report
        {
            /// <summary>
            /// Gets or sets the model.
            /// </summary>
            public Model model { get; set; }

            /// <summary>
            /// Gets or sets the customers.
            /// </summary>
            public IEnumerable<customer> customers { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="customer" />.
        /// </summary>
        public class customer
        {
            /// <summary>
            /// Gets or sets the id.
            /// </summary>
            public Guid id { get; set; }

            /// <summary>
            /// Gets or sets the code.
            /// </summary>
            public int code { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string name { get; set; }
        }
    }
 
    public class rep_JameGarardadProductPackings
    {
        
        public class Model
        {
            /// <summary>
            /// Gets or sets the kindContract.
            /// </summary>
            [Display(Name = "نوع قراداد")]
            [Required]
            public Guid? kindContract { get; set; }

            /// <summary>
            /// Gets or sets the prodocts.
            /// </summary>
            [Display(Name = "محصولات")]
            public Guid[] prodocts { get; set; }

            /// <summary>
            /// Gets the prodoctsSTR.
            /// </summary>
            public string[] prodoctsSTR
            {
                get { return prodocts != null ? prodocts.Select(a => Models.cl._ListProduct.Single(s => s.Id == a).Title).ToArray() : new string[0]; ; }
            }

            /// <summary>
            /// Gets or sets the pakings.
            /// </summary>
            [Display(Name = " سبد ")]
            public Guid[] pakings { get; set; }

            /// <summary>
            /// Gets the pakingsSTR.
            /// </summary>
            public string[] pakingsSTR
            {
                get { return pakings != null ? pakings.Select(a => Models.cl._ListPacking.Single(s => s.Id == a).Title).ToArray() : new string[0]; }
            }

            /// <summary>
            /// Gets the kindContractSTR.
            /// </summary>
            public string kindContractSTR
            {
                get { return kindContract != null ? ((Models.cl._ListProduct.SingleOrDefault(s => kindContract == s.Id) ?? new web_db.TblProduct()).Title) : ""; }
            }
        }

        /// <summary>
        /// Defines the <see cref="Report" />.
        /// </summary>
        public class Report
        {
            /// <summary>
            /// Gets or sets the model.
            /// </summary>
            public Model model { get; set; }

            /// <summary>
            /// Gets or sets the rows.
            /// </summary>
            public IEnumerable<row> rows { get; set; }
        }

        /// <summary>
        /// Defines the <see cref="row" />.
        /// </summary>
        public class row
        {
            /// <summary>
            /// Gets or sets the Customer.
            /// </summary>
            public string? Customer { get; set; }

            /// <summary>
            /// Gets or sets the Packing.
            /// </summary>
            public string? Packing { get; set; }

            /// <summary>
            /// Gets or sets the Product.
            /// </summary>
            public string? Product { get; set; }

            /// <summary>
            /// Gets or sets the Contract.
            /// </summary>
            public double? Contract { get; set; }

            //  public string  ContractType { get; set; }
            /// <summary>
            /// Gets or sets the Location1.
            /// </summary>
            public string? Location1 { get; set; }

            /// <summary>
            /// Gets or sets the Location2.
            /// </summary>
            public string? Location2 { get; set; }

            /// <summary>
            /// Gets or sets the Location3.
            /// </summary>
            public string? Location3 { get; set; }

            /// <summary>
            /// Gets or sets the log.
            /// </summary>
            public web_db.TblStoreLog log { get; set; }
        }
    }
}
