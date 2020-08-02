using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_lib;
namespace web_sard.Models
{
    public class cl
    {
        public class bascul {
            public bascul(decimal weight){

                if (WeightBacul != weight)
                { var w = ((WeightBacul??0) - weight);
                   
                    if (w.gadrmotlagh()>2)
                    {
                        lastWeightdate = DateTime.Now;

                    }
                 
                    WeightBacul = weight; 
                }
                lastdate = DateTime.Now;
            }
            private static DateTime lastWeightdate { get; set; }
            private static DateTime lastdate { get; set; }
            public static decimal? WeightBacul { get; set; }
            public static bool WeightBaculIsAllow { get {

                    if ((DateTime.Now- lastdate).TotalSeconds>5)
                    {
                         WeightBacul = null;
                        return false;
                    }
                    var t=(DateTime.Now - lastWeightdate).TotalSeconds > 3;
                    return t;
                } }
        }
    
        public static web_db.TblConfig _config { get; set; }
        public static IEnumerable<web_db.TblSalMali> _ListSalmali { get; set; }
        public static IEnumerable<web_db.TblContractType> _ListContractType { get; set; }
        public static IEnumerable<web_db.TblCustomer> _ListCustomer { get; set; }
        public static IEnumerable<web_db.TblProduct> _ListProduct { get; set; }
        public static IEnumerable<web_db.TblInjury> _ListInjury { get; set; }
        public static IEnumerable<web_db.TblPacking> _ListPacking { get; set; }
        public static IEnumerable<web_db.TblLocation> _ListLocation { get; set; }

    }
}
