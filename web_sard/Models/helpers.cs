using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web_lib;

namespace web_sard.Models
{
    public static class helpers
    {
     

        public static IHtmlContent _Sign(this IHtmlHelper helper,string url,string txt,string name)
        {
            string[] str = new[] { url, txt, name };
            StringBuilder builder = new StringBuilder();
           
          
            return helper.Partial("li/paintsign", str);
        }




        public static IHtmlContent _Conf(this IHtmlHelper helper, web_db.TblConf.KeyEnum key,string type)
        {
              
            return helper.Partial("li/conf", new KeyValuePair<web_db.TblConf.KeyEnum,string>(key,type ));
        }
    }
}
