using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblSalMali
    {
        public int Id { get; set; }
        public string Sal { get; set; }
        public bool IsOpen { get; set; }
        public DateTime SalAz { get; set; }
        public DateTime SalTa { get; set; }
    }
}
