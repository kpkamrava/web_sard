namespace web_sard.Models.tbls.portage
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="kindPortage" />.
    /// </summary>
    public class kindPortage
    {

        public enum kindPortageEnum
        {

            [web_lib.classAttr.KPvalus(Description = "جهت تخلیه")]  In = 1,


            [web_lib.classAttr.KPvalus(Description = "برگشت از بارگیری")] OutBack = 5,


            [web_lib.classAttr.KPvalus(Description = "جهت بارگیری")] Out = 11,


            [web_lib.classAttr.KPvalus(Description = "برگشت از تخلیه")] InBack = 15,


        }


        public static List<kindPortage> listkindcontract = new List<kindPortage> {
          new kindPortage {code=1,txt="جهت تخلیه",isEntry=true,Manfi=false,classcolor="primary" },
          new kindPortage {code=15,txt="برگشت از تخلیه",isEntry=true,Manfi=true,classcolor="warning text-dark" },
          new kindPortage {code=11,txt="جهت بارگیری" ,isEntry=false,Manfi=true,classcolor="success"},
          new kindPortage {code=5,txt="برگشت از بارگیری",isEntry=false,Manfi=false,classcolor="secondary "  },

        };


        public int code { get; set; }


        public string txt { get; set; }


        public bool isEntry { get; set; }


        public bool Manfi { get; set; }


        public string classcolor { get; set; }
    }
}
