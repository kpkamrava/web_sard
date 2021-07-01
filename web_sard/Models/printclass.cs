namespace web_sard.Models
{
    using Microsoft.AspNetCore.Hosting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="printclass" />.
    /// </summary>
    public class printclass
    {
     
        public static Dictionary<string, string> getlistfile(string contoll, string action, IWebHostEnvironment env)
        {
            try
            {
                var z = env.WebRootPath + $"/Reports/{contoll.ToString()}/";
                var list = new Dictionary<string, string>();
                foreach (var item in System.IO.Directory.GetFiles(z, $"rpt_{action}_*"))
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

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        public Dictionary<string, string> files { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Gets or sets the contolltype.
        /// </summary>
        public string contolltype { get; set; }

        /// <summary>
        /// Gets or sets the actiontype.
        /// </summary>
        public string actiontype { get; set; }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public string kind { get; set; }
    }
}
