using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.user
{
    public class login
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public bool remember { get; set; }
    }
}
