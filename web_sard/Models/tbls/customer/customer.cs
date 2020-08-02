using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Models.tbls.customer
{
    public class customer
    {
        public customer() { }
        public static web_sard.Models.tbls.customer.customer get(web_db.TblCustomer row, web_db.sardweb_Context db) {

            var r = new web_sard.Models.tbls.customer.customer
            {
                Addras = row.Addras,
                Code = row.Code,
                FkSalmali = row.FkSalmali,
                Id = row.Id,
                IdOtherSystem = row.IdOtherSystem,
                IsEnable = row.IsEnable,
                Mob = row.Mob,
                NationalCode = row.NationalCode,
                Title = row.Title
            };
            r.listContract = new List<contract.contract>();

            return r;

        }

        // Properties
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string NationalCode { get; set; }
        public string Title { get; set; }
        public string Mob { get; set; }
        public string Addras { get; set; }
        public bool IsEnable { get; set; }
        public string IdOtherSystem { get; set; }
        public int FkSalmali { get; set; }
        public List<contract.contract> listContract { get; set; }

    }
}
