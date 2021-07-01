using System.ComponentModel.DataAnnotations;

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
