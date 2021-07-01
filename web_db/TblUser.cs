using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace web_db
{
    public partial class TblUser
    {
        public TblUser()
        {
            //TblTempRows = new HashSet<TblTempRow>();
            //TblTemps = new HashSet<TblTemp>();
            TblUserSals = new HashSet<TblUserSal>();
        }
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mob { get; set; }
        public string Title { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsDel { get; set; }
        public int Salmalidef { get; set; }

        //public virtual ICollection<TblTempRow> TblTempRows { get; set; }



         // public ICollection<TblTemp> TblTempAdds { get; set; }

         //public ICollection<TblTemp> TblTempTaiids { get; set; }




        public virtual ICollection<TblUserSal> TblUserSals { get; set; }
    }
}
