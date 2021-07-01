using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblQueu
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Mob { get; set; }
        public int? Codesend { get; set; }
        public DateTime? Datecodesend { get; set; }
        public string Codemeli { get; set; }
        public DateTime Date { get; set; }
        public int KindQueu { get; set; }
        public string CodeMahsuls { get; set; }
        public string Addras { get; set; }
        public long Weight { get; set; }
        public string Mahsuls { get; set; }
        public Guid? ContractId { get; set; }
        public string Name { get; set; }
        public string Txt { get; set; }
        public string TxtReq { get; set; }

        public virtual TblContract Contract { get; set; }
    }
}
