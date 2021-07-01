using System;
using System.Collections.Generic;

#nullable disable

namespace web_db
{
    public partial class TblPacking
    {
        public TblPacking()
        {
            TblContractPackings = new HashSet<TblContractPacking>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal WightFull { get; set; }
        public decimal WightEmpty { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public string OtcodeKala { get; set; }
        public int? OtcodeVahedShomaresh { get; set; }
        public string OtcodeKalaAcc { get; set; }
        public bool? IsNotAc { get; set; }
        public float? WightScale { get; set; }
      
      
        public string ForContractTypeJson { get; set; }

        public List<TblContractType.KindCotractTypeEnum> ForContractType()
        {
            List<TblContractType.KindCotractTypeEnum> v;
            try
            {
                v = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TblContractType.KindCotractTypeEnum>>(ForContractTypeJson);

                return v;
            }
            catch (Exception)
            {

                v = new List<TblContractType.KindCotractTypeEnum>();

            }
            return v;

        }
        public void ForContractType(List<TblContractType.KindCotractTypeEnum> listtype)
        {

            ForContractTypeJson = Newtonsoft.Json.JsonConvert.SerializeObject(listtype);

        }


        public virtual ICollection<TblContractPacking> TblContractPackings { get; set; }
    }
}
