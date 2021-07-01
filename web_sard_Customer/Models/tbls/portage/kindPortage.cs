using System.Collections.Generic;

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
          new kindPortage {code=1,txt="جهت تخلیه",isEntry=true,Manfi=false,classcolor="primary" },
          new kindPortage {code=15,txt="برگشت از تخلیه",isEntry=true,Manfi=true,classcolor="warning text-dark" },
          new kindPortage {code=11,txt="جهت بارگیری" ,isEntry=false,Manfi=true,classcolor="success"},
          new kindPortage {code=5,txt="برگشت از بارگیری",isEntry=false,Manfi=false,classcolor="secondary text-dark"  },

        };
        public int code { get; set; }
        public string txt { get; set; }
        public bool isEntry { get; set; }
        public bool Manfi { get; set; }
        public string classcolor { get; set; }
    }
}
