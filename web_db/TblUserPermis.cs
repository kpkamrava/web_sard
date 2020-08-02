using System;
using System.Collections.Generic;

namespace web_db
{
    public partial class TblUserPermis
    {
        public Guid FkPortageType { get; set; }
        public Guid FkUser { get; set; }
        public bool IsIn { get; set; }
        public bool IsOut { get; set; }
        public bool IsInBack { get; set; }
        public bool IsOutBack { get; set; }
        public bool IsReport { get; set; }
        public bool IsType { get; set; }
    }
}
