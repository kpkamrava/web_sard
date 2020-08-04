using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.portage
{
    public class store
    {
        public string location { get; set; }
        public string customer { get; set; }
        public string contracttype { get; set; }
        public double contract { get; set; }
        public string Packing { get; set; }
        public string Prodoct { get; set; }

        public Guid? fklocation1 { get; set; }
        public Guid? fklocation2 { get; set; }
        public Guid? fklocation3 { get; set; }
        public Guid fkcontract { get; set; }
        public Guid? fkPacking { get; set; }
        public Guid? fkProdoct { get; set; }
        public long count { get; set; }



    }
}
