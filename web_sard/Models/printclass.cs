using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models
{
    public class printclass
    {
       
        public static Dictionary<string,string> getlistfile(string contoll,string action, IWebHostEnvironment env)
        {
            try
            {
               var z = env.WebRootPath + $"/Reports/{contoll.ToString()}/"; 
            var list = new Dictionary<string, string>();
            foreach (var item in System.IO.Directory.GetFiles(z, $"rpt_{action}_*.mrt"))
            {
                var s = (item.ToLower().Split($"/rpt_{action.ToLower()}_")[1]);
             list.Add(item, s.Split(".")[0]);
            }
           return list;
            }
            catch  
            {
                return new Dictionary<string, string>();
                
            }
          
        }

        public Dictionary<string, string> files { get; set; }
        public Guid id { get; set; }
        public string contolltype { get; set; }
        public string actiontype { get; set; }
        public string kind { get; set; }


    }
}
