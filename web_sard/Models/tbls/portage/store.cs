namespace web_sard.Models.tbls.portage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="store" />.
    /// </summary>
    public class store
    {
        /// <summary>
        /// The getByListByLocation.
        /// </summary>
        /// <param name="fklocation">The fklocation<see cref="Guid"/>.</param>
        /// <param name="sal">The sal<see cref="int"/>.</param>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        /// <returns>The <see cref="IEnumerable{store}"/>.</returns>
        public static IEnumerable<store> getByListByLocation(Guid fklocation, int sal, web_db.sardweb_Context db)
        {



            var x = from n in db.TblStoreLogs.Where(a => a.FkSalmali == sal && (a.FkLocation1 == fklocation || a.FkLocation2 == fklocation || a.FkLocation3 == fklocation)).AsEnumerable()
                    select n;

            return getByListlog(x, db);
        }

        /// <summary>
        /// The getByListlog.
        /// </summary>
        /// <param name="rows">The rows<see cref="IEnumerable{web_db.TblStoreLog}"/>.</param>
        /// <param name="db">The db<see cref="web_db.sardweb_Context"/>.</param>
        /// <returns>The <see cref="IEnumerable{store}"/>.</returns>
        public static IEnumerable<store> getByListlog(IEnumerable<web_db.TblStoreLog> rows, web_db.sardweb_Context db)
        {

            var x = from n in rows
                    let count = n.Count
                    where count != 0
                    let contract = db.TblContracts.Find(n.FkContract) ?? new web_db.TblContract()
                    let contracttype = db.TblContractTypes.Find(n.FkContractType)
                    let loc3 = Models.cl._ListLocation.SingleOrDefault(z => z.Id == n.FkLocation3)
                    let loc2 = Models.cl._ListLocation.SingleOrDefault(z => z.Id == n.FkLocation2)
                    let loc1 = Models.cl._ListLocation.SingleOrDefault(z => z.Id == n.FkLocation1)
                    select new Models.tbls.portage.store
                    {
                        id = n.Id,
                        fklocation1 = n.FkLocation1,
                        fklocation2 = n.FkLocation2,
                        fklocation3 = n.FkLocation3,
                        fkcontract = n.FkContract,
                        fkPacking = n.FkPacking,
                        fkProdoct = n.FkProduct,
                        count = count,
                        location = (loc3 ?? loc2 ?? loc1 ?? new web_db.TblLocation()).CodeFull,
                        contracttype = contracttype.Title,
                        customer = (db.TblCustomers.Find(contract.FkCustomer) ?? new web_db.TblCustomer()).Title,
                        contract = contract.Code,
                        Packing = (Models.cl._ListPacking.SingleOrDefault(z => z.Id == n.FkPacking) ?? new web_db.TblPacking()).Title,
                        Prodoct = (Models.cl._ListProduct.SingleOrDefault(z => z.Id == n.FkProduct) ?? new web_db.TblProduct()).Title,

                    };

            return x;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the customer.
        /// </summary>
        public string customer { get; set; }

        /// <summary>
        /// Gets or sets the contracttype.
        /// </summary>
        public string contracttype { get; set; }

        /// <summary>
        /// Gets or sets the contract.
        /// </summary>
        public double contract { get; set; }

        /// <summary>
        /// Gets or sets the Packing.
        /// </summary>
        public string Packing { get; set; }

        /// <summary>
        /// Gets or sets the Prodoct.
        /// </summary>
        public string Prodoct { get; set; }

        /// <summary>
        /// Gets or sets the fklocation1.
        /// </summary>
        public Guid? fklocation1 { get; set; }

        /// <summary>
        /// Gets or sets the fklocation2.
        /// </summary>
        public Guid? fklocation2 { get; set; }

        /// <summary>
        /// Gets or sets the fklocation3.
        /// </summary>
        public Guid? fklocation3 { get; set; }

        /// <summary>
        /// Gets or sets the fkcontract.
        /// </summary>
        public Guid? fkcontract { get; set; }

        /// <summary>
        /// Gets or sets the fkPacking.
        /// </summary>
        public Guid? fkPacking { get; set; }

        /// <summary>
        /// Gets or sets the fkProdoct.
        /// </summary>
        public Guid? fkProdoct { get; set; }

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public long count { get; set; }
    }
}
