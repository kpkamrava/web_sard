using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mob { get; set; }
        public string Title { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDel { get; set; }
        public int Salmalidef { get; set; }
    }
}
