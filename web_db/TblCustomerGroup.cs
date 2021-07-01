using System;

#nullable disable

namespace web_db
{
    public partial class TblCustomerGroup
    {
        public Guid FkCustumer { get; set; }
        public Guid FkGroup { get; set; }

        public virtual TblCustomer FkCustumerNavigation { get; set; }
        public virtual TblGroup FkGroupNavigation { get; set; }
    }
}
