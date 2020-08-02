using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.portage
{
    public class kindPortage
    {
        public enum kindPortageEnum
        {
            In = 1,
            OutBack = 5,

            Out = 11,
            InBack = 15,


        }
        public static List<kindPortage> listkindcontract = new List<kindPortage> {
          new kindPortage {code=1,txt="جهت تخلیه",isEntry=true,classcolor="primary" },
          new kindPortage {code=15,txt="برگشت از تخلیه",isEntry=true,classcolor="warning" },
          new kindPortage {code=11,txt="جهت بارگیری" ,isEntry=false,classcolor="success"},
          new kindPortage {code=5,txt="برگشت از بارگیری",isEntry=false,classcolor="secondary"  },
           
        };
        public int code { get; set; }
        public string txt { get; set; }
        public bool isEntry { get; set; }
        public string classcolor { get; set; }
    }
}
