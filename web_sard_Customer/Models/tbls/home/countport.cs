using System;
using System.Collections.Generic;

namespace web_sard.Models.tbls.home
{
    public class countport
    {
        public DateTime date { get; set; }
        public string contracttype { get; set; }
        public string KindTitle { get; set; }
        public int Kindcode { get; set; }
        public int count { get; set; }
        public int contracttypecode { get; set; }
        public decimal WeightNet { get; set; }
        public decimal PackingCount { get; set; }
    }

    public class countEmroz
    {

        public int carDakhel1 { get; set; }
        public int carDakhel10 { get; set; }

    }

    public class mainclass
    {

        public ICollection<countport> countports { get; set; }


    }
}
