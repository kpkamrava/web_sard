namespace web_sard.Models
{
    using ApiPaivast;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using web_lib;
    using web_sard.Models.tbls.portage;


    public class cl
    {

        public class bascul
        {

            public bascul(decimal weight)
            {
                var Main_DegateBaskul = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_DegateBaskul) ?? new web_db.TblConf()).Value??"10";

                if (_WeightBacul != weight)
                {
                    var w = ((_WeightBacul ?? 0) - weight);

                    if (w.gadrmotlagh() > Convert.ToInt32( Main_DegateBaskul))
                    {
                        lastWeightdate = DateTime.Now;

                    }

                    _WeightBacul = weight;
                }
                lastdate = DateTime.Now;
            }

     
            public static void setZeroBacul()
            {
                _WeightZeroBacul = _WeightBacul;
            }

         
            private static DateTime lastWeightdate { get; set; }

          
            private static DateTime lastdate { get; set; }

           
            private static decimal? _WeightBacul { get; set; }

          
            public static decimal? _WeightZeroBacul { get; set; }

            /// <summary>
            /// Gets the WeightBacul.
            /// </summary>
            public static decimal? WeightBacul
            {
                get { return _WeightBacul - (_WeightZeroBacul ?? 0); }
            }

            /// <summary>
            /// Gets a value indicating whether WeightBaculIsAllow.
            /// </summary>
            public static bool WeightBaculIsAllow
            {
                get
                {

                    if ((DateTime.Now - lastdate).TotalSeconds > 5)
                    {
                        _WeightBacul = null;
                        return false;
                    }
                    var t = (DateTime.Now - lastWeightdate).TotalSeconds > 3;
                    return t;
                }
            }
        }

        public static bool RemoveFullPortage(Guid id, web_db.sardweb_Context db)
        {
            var portage = db.TblPortages.Find(id);


            var contypeclone = db.TblContractTypes.Single(a => a.Id == portage.FkContracttype);


            #region rem PortageRows


            foreach (var item in db.TblPortageRows.Where(a => a.FkPortage == portage.Id))
            {
                db.TblPortageRowInjuries.RemoveRange(db.TblPortageRowInjuries.Where(a => a.FkPortageRow == item.Id));
                db.TblPortageRows.Remove(item);

            }


            #endregion


            #region rem images

            db.TblPortageDocuments.RemoveRange(db.TblPortageDocuments.Where(a => a.FkPortage == portage.Id));
            db.TblPortages.Remove(portage);
            #endregion
            db.SaveChanges();

            if (contypeclone.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                var x = db.TblPortageRows.Where(a => a.FkPortage == portage.Id);
                foreach (var item in x.Select(a => a.FkContract).Distinct())
                {
                    Models.cl.refTblStoreLogcontract(item.Value, db);
                }
            }

            else if (contypeclone.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

            {
                Models.cl.refTblStoreLogcontractType(contypeclone.Id, portage.FkSalmali, db);

            }


            return true;
        }
        public static bool RemoveFullContract(Guid id, web_db.sardweb_Context db)
        {
            try
            {
                db.TblContractPackings.RemoveRange(db.TblContractPackings.Where(a => a.FkContract == id));
                db.TblContractProducts.RemoveRange(db.TblContractProducts.Where(a => a.FkContract == id));
                db.TblContracts.Remove(db.TblContracts.Find(id));
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }


        }

        public static async Task<Guid?> AddPayvastCustomerAsync(web_db.sardweb_Context db, string CellPhone, int sal)
        {

            var Mali_KindOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_KindOT) ?? new web_db.TblConf()).Value;
            var Mali_UrlOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_UrlOT) ?? new web_db.TblConf()).Value;
            var Mali_UserOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_UserOT) ?? new web_db.TblConf()).Value;
            var Mali_PassOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_PassOT) ?? new web_db.TblConf()).Value;


            ApiPaivast.AccountingServiceSoapClient vv = new AccountingServiceSoapClient(AccountingServiceSoapClient.EndpointConfiguration.AccountingServiceSoap, Mali_UrlOT);
            var c = await vv.GetCustomerIdByCellPhoneAsync(CellPhone: CellPhone);
            var idc = c;

            var cid = await vv.GetCustomerInfoAsync(idc, Mali_UserOT, Mali_PassOT);
            if (cid != null)
            {
                string txt = "ثبت موفق";
                var row = db.TblCustomers.FirstOrDefault(a => a.FkSalmali == sal && a.IdOtherSystem == cid.Id.ToString());
                if (row == null)
                {
                    row = new web_db.TblCustomer
                    {
                        Id = Guid.NewGuid(),
                        FkSalmali = sal,
                        IdOtherSystem = cid.Id.ToString(),
                        IsEnable = true,
                        Code = (db.TblCustomers.Where(a => a.FkSalmali == sal).Max(a => (int?)a.Code) ?? 0) + 1,

                    };
                    db.TblCustomers.Add(row);
                    txt = "بروزرسانی موفق";
                }
                row.IsEnable = true;
                row.Addras = "";
                row.Mob = CellPhone;
                row.NationalCode = cid.NationalCode;
                row.Title = cid.Name.ToPersianChars();
                db.SaveChanges();
                return row.Id;

            }
            return null;

        }




        public static byte[] captureDorbinBaskul() {

         {
                var navhdorbin = ((Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NavhDorbinBaskul) ?? new web_db.TblConf()).Value ?? "");
                if (navhdorbin.IsEmpty() == false)
                {
                    var urldorbin = ((Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_URLDorbinBaskul) ?? new web_db.TblConf()).Value ?? "");
                    var userdorbin = ((Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserDorbinBaskul) ?? new web_db.TblConf()).Value ?? "");
                    var passdorbin = ((Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassDorbinBaskul) ?? new web_db.TblConf()).Value ?? "");
                    if (!urldorbin.IsEmpty() && !userdorbin.IsEmpty() && !passdorbin.IsEmpty())
                    {
                        var z = web_lib.stat.CaptureCamera(stat.CaptureCameraKind.HikvisionISAPI, urldorbin, userdorbin, passdorbin);
                        if (z != null)
                        {
                            return z.ToArray().imgTosmall();
                         
                        }





                    }
                }
            }
            return null;

        }
        public static void refTblStoreLogcontract(Guid fkcontract, web_db.sardweb_Context db)
        {
            var rowOK = new List<Guid>();
            //Add
            {
                var contr = db.TblContracts.Find(fkcontract);
                var contrType = db.TblContractTypes.Find(contr.FkContractType);
                var x = db.TblPortageRows.Include(a => a.FkPortageNavigation).Where(a => a.FkContract == fkcontract && (a.FkPortage.HasValue ? (a.FkPortageNavigation.IsEnd && a.FkPortageNavigation.IsDel == false) : true)).AsEnumerable();


                contr.SumInCount = x.Where(a => a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.In || a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.InBack).Sum(a => a.Count);
                contr.SumInWeight = x.Where(a => a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.In || a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.InBack).Sum(a => (decimal?)(a.Count * a.WeightOne));

                contr.SumOutCount = x.Where(a => a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.Out || a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.OutBack).Sum(a => a.Count);
                contr.SumOutWeight = x.Where(a => a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.Out || a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.OutBack).Sum(a => (decimal?)(a.Count * a.WeightOne));

                if (contrType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {
                    var xx = x.GroupBy(a => new { a.FkContract, a.FkLocation1, a.FkLocation2, a.FkLocation3, a.FkPacking, a.FkProduct }).ToList();

                    foreach (var item in xx)
                    {
                        var iall = db.TblStoreLogs.Where(a =>
                                   a.FkContract == item.Key.FkContract &&
                                   a.FkLocation1 == item.Key.FkLocation1 &&
                                   a.FkLocation2 == item.Key.FkLocation2 &&
                                   a.FkLocation3 == item.Key.FkLocation3 &&
                                   a.FkPacking == item.Key.FkPacking &&
                                   a.FkProduct == item.Key.FkProduct
                               );
                        if (iall.Count() > 1)
                        {
                            db.TblStoreLogs.RemoveRange(iall);
                            db.SaveChanges();
                        }
                        var i = db.TblStoreLogs.SingleOrDefault(a =>
                                 a.FkContract == item.Key.FkContract &&
                                 a.FkLocation1 == item.Key.FkLocation1 &&
                                 a.FkLocation2 == item.Key.FkLocation2 &&
                                 a.FkLocation3 == item.Key.FkLocation3 &&
                                 a.FkPacking == item.Key.FkPacking &&
                                 a.FkProduct == item.Key.FkProduct
                               );
                        if (i == null)
                        {
                            i = new web_db.TblStoreLog
                            {
                                Id = Guid.NewGuid(),
                                FkContract = fkcontract,
                                FkContractType = contrType.Id,
                                FkCustomer = contr.FkCustomer,
                                FkLocation1 = item.Key.FkLocation1,
                                FkLocation2 = item.Key.FkLocation2,
                                FkLocation3 = item.Key.FkLocation3,
                                FkPacking = item.Key.FkPacking,
                                FkProduct = item.Key.FkProduct,
                                FkSalmali = contr.FkSalmali,

                            };
                            db.TblStoreLogs.Add(i);
                        }
                        i.Count = item.Sum(a => a.Count);
                        i.Weight = item.Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.CountIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count);
                        i.CountOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count);
                        i.CountMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count);
                        i.WeightIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.WeightOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.WeightMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count * (decimal?)a.WeightOne);
                        rowOK.Add(i.Id);

                    }
                }
                else
                  if (contrType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

                {
                    var xx = x.GroupBy(a => new { a.FkLocation1, a.FkLocation2, a.FkLocation3, a.FkPacking, a.FkProduct });
                    foreach (var item in xx)
                    {
                        var i = db.TblStoreLogs.SingleOrDefault(a =>
                             //   a.FkContract == item.Key.FkContract &&
                             a.FkLocation1 == item.Key.FkLocation1 &&
                             a.FkLocation2 == item.Key.FkLocation2 &&
                             a.FkLocation3 == item.Key.FkLocation3 &&
                             a.FkPacking == item.Key.FkPacking &&
                             a.FkProduct == item.Key.FkProduct
                                );
                        if (i == null)
                        {
                            i = new web_db.TblStoreLog
                            {
                                Id = Guid.NewGuid(),
                                //  FkContract = fkcontract,
                                FkContractType = contrType.Id,
                                //   FkCustomer = contr.FkCustomer,
                                FkLocation1 = item.Key.FkLocation1,
                                FkLocation2 = item.Key.FkLocation2,
                                FkLocation3 = item.Key.FkLocation3,
                                FkPacking = item.Key.FkPacking,
                                FkProduct = item.Key.FkProduct,
                                FkSalmali = contr.FkSalmali,
                            };
                            db.TblStoreLogs.Add(i);
                        }

                        i.Count = item.Sum(a => a.Count);
                        i.CountIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count);
                        i.CountOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count);
                        i.CountMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count);
                        rowOK.Add(i.Id);
                    }
                }
                db.TblStoreLogs.RemoveRange(db.TblStoreLogs.Where(a => a.FkContract == fkcontract && (rowOK.Contains(a.Id) == false)));
            }
        }
        public static void refTblStoreLogcontractType(Guid fkcontracttype, int fkyear, web_db.sardweb_Context db)
        {


            var rowOK = new List<Guid>();
            var contrType = db.TblContractTypes.Find(fkcontracttype);

           
            {

               
                var x = (from n in db.TblContracts.Include(a => a.TblPortageRows).ThenInclude(a => a.FkPortageNavigation)
                         where

                         n.FkSalmali == fkyear &&

                         n.FkContractType == fkcontracttype
                         select n);

                foreach (var it in x)
                {

                    var inn = it.TblPortageRows.Where(a => a.FkPortageNavigation.IsEnd && ((a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.In) || (a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.InBack)));
                    var outt = it.TblPortageRows.Where(a => a.FkPortageNavigation.IsEnd && ((a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.Out) || (a.FkPortageNavigation.KindCode == (int)kindPortage.kindPortageEnum.OutBack)));
                    var item = new
                    {
                        c = it,
                        SumInCount = inn.Sum(a => a.Count),
                        SumInWeight = inn.Sum(a => (decimal?)(a.Count * a.WeightOne)),
                        SumOutCount = outt.Sum(a => a.Count),
                        SumOutWeight = outt.Sum(a => (decimal?)(a.Count * a.WeightOne))
                    };

                    it.SumInCount = item.SumInCount;
                    it.SumInWeight = item.SumInWeight;
                    it.SumOutCount = item.SumOutCount;
                    it.SumOutWeight = item.SumOutWeight;

                }
            }


            {
                var x = db.TblPortageRows.Include(a => a.FkPortageNavigation).Where(a => a.FkContractType == fkcontracttype && (a.FkPortage.HasValue ? a.FkPortageNavigation.IsEnd : true)).AsEnumerable();





                // fkyear
                if (contrType.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                {

                    var xx = x.GroupBy(a => new { a.FkContract, a.FkLocation1, a.FkLocation2, a.FkLocation3, a.FkPacking, a.FkProduct });

                    foreach (var item in xx)
                    {

                        var i = db.TblStoreLogs.SingleOrDefault(a =>
                             a.FkContract == item.Key.FkContract &&
                             a.FkLocation1 == item.Key.FkLocation1 &&
                             a.FkLocation2 == item.Key.FkLocation2 &&
                             a.FkLocation3 == item.Key.FkLocation3 &&
                             a.FkPacking == item.Key.FkPacking &&
                             a.FkProduct == item.Key.FkProduct
                            );
                        if (i == null)
                        {
                            i = new web_db.TblStoreLog
                            {
                                Id = Guid.NewGuid(),

                                FkContractType = fkcontracttype,
                                FkLocation1 = item.Key.FkLocation1,
                                FkLocation2 = item.Key.FkLocation2,
                                FkLocation3 = item.Key.FkLocation3,
                                FkPacking = item.Key.FkPacking,
                                FkProduct = item.Key.FkProduct,
                                FkSalmali = fkyear,
                            };
                            db.TblStoreLogs.Add(i);
                        }

                        i.Count = item.Sum(a => a.Count);
                        i.Weight = item.Where(a => a.FkPortage.HasValue).Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.CountIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count);
                        i.CountOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count);
                        i.CountMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count);

                        i.WeightIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.WeightOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count * (decimal?)a.WeightOne);
                        i.WeightMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count * (decimal?)a.WeightOne);

                        rowOK.Add(i.Id);

                    }
                }
                else 
                if (contrType.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

                {
                    var xx = x.GroupBy(a => new { a.FkLocation1, a.FkLocation2, a.FkLocation3, a.FkPacking, a.FkProduct });

                    foreach (var item in xx)
                    {

                        var i = db.TblStoreLogs.SingleOrDefault(a =>
                             //   a.FkContract == item.Key.FkContract &&
                             a.FkLocation1 == item.Key.FkLocation1 &&
                             a.FkLocation2 == item.Key.FkLocation2 &&
                             a.FkLocation3 == item.Key.FkLocation3 &&
                             a.FkPacking == item.Key.FkPacking &&
                             a.FkProduct == item.Key.FkProduct
                             );
                        if (i == null)
                        {
                            i = new web_db.TblStoreLog
                            {
                                Id = Guid.NewGuid(),
                          
                                FkContractType = contrType.Id,
                           
                                FkLocation1 = item.Key.FkLocation1,
                                FkLocation2 = item.Key.FkLocation2,
                                FkLocation3 = item.Key.FkLocation3,
                                FkPacking = item.Key.FkPacking,
                                FkProduct = item.Key.FkProduct,
                                FkSalmali = fkyear,
                            };
                            db.TblStoreLogs.Add(i);
                        }

                        i.Count = item.Sum(a => a.Count);
                        i.CountIn = item.Where(a => a.FkPortage.HasValue && a.Count > 0).Sum(a => a.Count);
                        i.CountOut = item.Where(a => a.FkPortage.HasValue && a.Count < 0).Sum(a => a.Count);
                        i.CountMovement = item.Where(a => a.FkPortage.HasValue == false).Sum(a => a.Count);

                        rowOK.Add(i.Id);
                    }



                }
                db.TblStoreLogs.RemoveRange(db.TblStoreLogs.Where(a => a.FkContractType == fkcontracttype && (rowOK.Contains(a.Id) == false)));
            }
        }

        public static void GetMojavez(Guid custumer, Guid contracttype, web_db.sardweb_Context db, bool inn, out long? countFor, out decimal? weightFor)
        {
            var x = db.TblContracts.Where(a => a.FkCustomer == custumer && a.FkContractType == contracttype);
            var contype = db.TblContractTypes.Find(contracttype);

            if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {
                if (inn)
                {

                    countFor = null;
                    weightFor = (x.Sum(a => a.WeightMaxIn) ?? 0).gadrmotlagh();
                    weightFor -= (x.Sum(a => a.SumInWeight) ?? 0).gadrmotlagh();



                }
                else
                {
                    var v1 = (x.Sum(a => (a.PercentForOut.HasValue ? (a.SumInCount * a.PercentForOut / 100) : a.CountMaxOut)) ?? 0).gadrmotlagh();
                    var v2 = (x.Sum(a => a.SumOutCount) ?? 0).gadrmotlagh();
                    countFor = v1;
                    countFor -= v2;

                    weightFor = (x.Sum(a => (a.PercentForOut.HasValue ? (a.SumInWeight * a.PercentForOut / 100) : a.WeightMaxOut)) ?? 0).gadrmotlagh();
                    weightFor -= (x.Sum(a => a.SumOutWeight) ?? 0).gadrmotlagh();


                }
                return;
            }
            else if (contype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

            {

                var conf = contype.ConfigASabad();
                
                if (inn)
                {

                    countFor = (x.Sum(a => a.CountMaxIn) ?? 0).gadrmotlagh();
                    countFor -= (x.Sum(a => a.SumInCount) ?? 0).gadrmotlagh();
                    weightFor = null;



                }
                else
                {
                    var v1 =  x.Sum(a =>   a.CountMaxOut ?? 0).gadrmotlagh();
                    var v2 = (x.Sum(a => a.SumOutCount) ?? 0).gadrmotlagh();
                    countFor = v1;
                    countFor -= v2;


                    weightFor = null;


                }
                return;
            }

            countFor = null;

            weightFor = null;
        }

     
        public static web_db.TblLocation[] GuidToLocation(Guid? id)
        {
            var ll = _ListLocation.SingleOrDefault(a => a.Id == id);

            if (ll == null)
            {
                return new web_db.TblLocation[] { null, null, null };
            }
            web_db.TblLocation l1 = null
                , l2 = null
                , l3 = null
                ;
            if (ll.Kind == 1)
            {
                l1 = ll;
                l2 = null;
                l3 = null;

            }
            if (ll.Kind == 2)
            {
                l2 = ll;
                l1 = _ListLocation.SingleOrDefault(a => a.Id == l2.FkP);
                l3 = null;
            }
            if (ll.Kind == 3)
            {
                l3 = ll;
                l2 = _ListLocation.SingleOrDefault(a => a.Id == l3.FkP);
                l1 = _ListLocation.SingleOrDefault(a => a.Id == l2.FkP);

            }
            return new web_db.TblLocation[] {
            l1,
                   l2,
                      l3,
                 };
        }

        public static Guid?[] GuidToLocationId(Guid? id)
        {
            var l = GuidToLocation(id);
            return new Guid?[] {
                l[0]==null?null:(Guid?)l[0].Id,
                   l[1]==null?null:(Guid?)l[1].Id,
                      l[2]==null?null:(Guid?)l[2].Id,
                 };
        }

        public static int?[] GuidToLocationCode(Guid? id)
        {
            var l = GuidToLocation(id);
            return new int?[] {
                               l[0]==null?null:(int?)l[0].Code,
                               l[1]==null?null:(int?)l[1].Code,
                               l[2]==null?null:(int?)l[2].Code,
                              };
        }
        public static float getWeightScale(Guid?  idproduct,Guid? idpacking)
        {
            float r = 1;
            var pp=  _ListProduct.SingleOrDefault(a => a.Id == idproduct);
            if (pp!=null)
            {
                r = r * (pp.WightScale ?? 1);
            }
            var pp2 = _ListPacking.SingleOrDefault(a => a.Id == idpacking);
            if (pp2 != null)
            {
                r = r * (pp2.WightScale ?? 1);
            } 

            return r;
        }

        public static IEnumerable<web_db.TblConf> _conf { get; set; }
        //  static internal List<web_db.TblUser> _listloginSms { get; set; }
        public class SmsForSend {
            public string number { get; set; }
            public string txt { get; set; }  
        }
        static internal List<SmsForSend> _listSmsForSend { get; set; }
        public static IEnumerable<web_db.TblSalMali> _ListSalmali { get; set; }

        private static IEnumerable<web_db.TblContractType> ___ListContractType { get; set; }
        public static IEnumerable<web_db.TblContractType> __ListContractType { set { ___ListContractType = value; } }
        public static IEnumerable<web_db.TblContractType> _ListContractType(int sal) { return ___ListContractType.Where(a => a.FkSalmali == sal); }

        public static IEnumerable<web_db.TblProduct> _ListProduct { get; set; }

        public static IEnumerable<web_db.TblInjury> _ListInjury { get; set; }
        public static IEnumerable<_UserRol._Rolls> _ListRoll { get; set; }
     
        public static IEnumerable<web_db.TblPacking> _ListPacking { get; set; }

        public static IEnumerable<web_db.TblLocation> _ListLocation { get; set; }

        public static IEnumerable<web_db.TblCar> _ListCar { get; set; }

        public static IEnumerable<web_db.TblGroup> _ListGroup { get; set; }
    }
}
