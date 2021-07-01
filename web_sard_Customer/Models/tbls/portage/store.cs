using System;
using System.Collections.Generic;
using System.Linq;

namespace web_sard.Models.tbls.portage
{
    public class store
    {
        public store() { }
        public store(web_db.TblStoreLog n, web_db.sardweb_Context db)
        {
            var contract = db.TblContracts.Find(n.FkContract) ?? new web_db.TblContract();
            var contracttype = db.TblContractTypes.Find(n.FkContractType);
            var loc3 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation3);
            var loc2 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation2);
            var loc1 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation1);


            this.id = n.Id;
            this.fklocation1 = n.FkLocation1;
            this.fklocation2 = n.FkLocation2;
            this.fklocation3 = n.FkLocation3;
            this.fkcontract = n.FkContract;
            this.fkPacking = n.FkPacking;
            this.fkProdoct = n.FkProduct;
            this.count = n.Count;
            this.Weight = n.Weight;

            this.countin = n.CountIn;
            this.Weightin = n.WeightIn;


            this.countout = n.CountOut;
            this.Weightout = n.WeightOut;

            this.location = (loc3 ?? loc2 ?? loc1 ?? new web_db.TblLocation()).CodeFull;
            this.contracttype = contracttype.Title;
            this.customer = (db.TblCustomers.Find(contract.FkCustomer) ?? new web_db.TblCustomer()).Title;
            this.contract = contract.Code;
            this.Packing = (db.TblPackings.SingleOrDefault(z => z.Id == n.FkPacking) ?? new web_db.TblPacking()).Title;
            this.Prodoct = (db.TblProducts.SingleOrDefault(z => z.Id == n.FkProduct) ?? new web_db.TblProduct()).Title;



        }
        public static IEnumerable<store> getByListByLocation(Guid fklocation, int sal, web_db.sardweb_Context db)
        {



            var x = from n in db.TblStoreLogs.Where(a => a.FkSalmali == sal && (a.FkLocation1 == fklocation || a.FkLocation2 == fklocation || a.FkLocation3 == fklocation)).AsEnumerable()
                    select n;

            return getByListlog(x, db);


        }
        public static IEnumerable<store> getByListlog(IEnumerable<web_db.TblStoreLog> rows, web_db.sardweb_Context db)
        {

            var x = from n in rows
                    let count = n.Count
                    where count != 0
                    let contract = db.TblContracts.Find(n.FkContract) ?? new web_db.TblContract()
                    let contracttype = db.TblContractTypes.Find(n.FkContractType)
                    let loc3 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation3)
                    let loc2 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation2)
                    let loc1 = db.TblLocations.SingleOrDefault(z => z.Id == n.FkLocation1)
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
                        Weight = n.Weight,
                        location = (loc3 ?? loc2 ?? loc1 ?? new web_db.TblLocation()).CodeFull,
                        contracttype = contracttype.Title,
                        customer = (db.TblCustomers.Find(contract.FkCustomer) ?? new web_db.TblCustomer()).Title,
                        contract = contract.Code,
                        Packing = (db.TblPackings.SingleOrDefault(z => z.Id == n.FkPacking) ?? new web_db.TblPacking()).Title,
                        Prodoct = (db.TblProducts.SingleOrDefault(z => z.Id == n.FkProduct) ?? new web_db.TblProduct()).Title,

                    };

            return x;


        }
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public string location { get; set; }
        public string customer { get; set; }
        public string contracttype { get; set; }
        public double contract { get; set; }
        public string Packing { get; set; }
        public string Prodoct { get; set; }

        public Guid? fklocation1 { get; set; }
        public Guid? fklocation2 { get; set; }
        public Guid? fklocation3 { get; set; }
        public Guid? fkcontract { get; set; }
        public Guid? fkPacking { get; set; }
        public Guid? fkProdoct { get; set; }
        public long count { get; set; }
        public decimal? Weight { get; set; }


        public long? countin { get; set; }
        public decimal? Weightin { get; set; }


        public long? countout { get; set; }
        public decimal? Weightout { get; set; }
        public string txt { get; set; }


    }
}
