﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.contract
{
    public class ContractType
    {
        public ContractType()
        {
        }
            public ContractType(web_db.TblContractType r) 
        {
            this.Code = r.Code;
      
            this.IsEntry = r.IsEntry;
            this.IsExit = r.IsExit;
            this.IsMali = r.IsMali;
            this.IsProduct1Packing0 = r.IsProduct1Packing0;
            this.Needbascule = r.Needbascule;
            this.Title = r.Title;
            this.Id = r.Id;
            this.OutControlByContract = r.OutControlByContract;
            this.OutControlByLocation = r.OutControlByLocation;
            this.OutControlByPercent = r.OutControlByPercent; 
        }
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }
        public bool IsProduct1Packing0 { get; set; }
        public bool Needbascule { get; set; }
        public bool IsMali { get; set; }
        public bool OutControlByContract { get; set; }
        public bool OutControlByLocation { get; set; }
        public bool OutControlByPercent { get; set; }

    }
}
