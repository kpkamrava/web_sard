using ApiPaivast;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using web_lib;

namespace web_sard.Areas.Acc.Controllers
{

    [Area("Acc")]
    [LoginAuth(_UserRol._Rolls.Mali)]
    public class FactureController : Controller
    {
       

        internal web_db.sardweb_Context db;
         
        public FactureController(web_db.sardweb_Context db)
        {
              
            this.db = db;
        }
  
        public IActionResult Index(bool SanadShode,Guid fkContractType,web_sard. Models.tbls.portage.kindPortage.kindPortageEnum? kindPortage=null)
        {
            ViewBag.SanadShode = SanadShode;
            ViewBag.fkContractType = fkContractType;
            ViewBag.kindPortage = kindPortage; 

            var contracttype = db.TblContractTypes.Find(fkContractType);
            if (contracttype.OtSanadPortageKind==web_db.TblContractType.SanadPortageEnum.ByPortage)
            {
                var x = from n in db.TblPortages.Include(a=>a.FkCustomerNavigation)
                        where n.FkSalmali == User._getuserSalMaliDef()&&n.FkContracttype== fkContractType&&
                        n.IsDel==false&&n.IsEnd&&
                        (kindPortage==null?true:(n.KindCode==(int?)kindPortage))&&
                        string.IsNullOrWhiteSpace(n.OtcodeResid) == !SanadShode
                        orderby n.Date2
                        select n;

                 return View("IndexP",x.ToList());
            } 
            else
              if (contracttype.OtSanadPortageKind == web_db.TblContractType.SanadPortageEnum.ByCustomer|| contracttype.OtSanadPortageKind == web_db.TblContractType.SanadPortageEnum.ByCustomerAndKindPortage)
            {
                var x = from n in db.TblCustomers.Include(a => a.TblPortages) 
                        let por = n.TblPortages.Any(a => a.FkContracttype == fkContractType &&
                       
                        (kindPortage.HasValue?(
                        a.KindCode == (int?)kindPortage):true)
                        &&
                        a.IsDel==false&&
                        a.IsEnd
                        && (string.IsNullOrWhiteSpace(a.OtcodeResid)== !SanadShode))


                        where n.FkSalmali == User._getuserSalMaliDef() &&
                        n.IsEnable&& por
                        orderby n.Code
                        select n;

                return View("IndexA", x.ToList());
            } 
            else
            {
                return this.Unauthorized();
            }
         
        }
        
        public IActionResult Acc(Guid id, string txt = "", string error = "")
        {
            ViewBag.txt = txt;
            ViewBag.error = error;
            ViewBag.id = id;
            Models.cl.refPortageAccMoney(db, id);
            var x = db.TblPortageMoneys.Where(a => a.FkPortage == id).OrderBy(a => a.Date);
            ViewBag.portage = db.TblPortages.Find(id);

            return View(x.ToList());
        }
         
        public IActionResult AccCustomer(Guid id, Guid idcontracttype, int kind, string txt = "", string error = "")
        {
            ViewBag.txt = txt;
            ViewBag.error = error;
            ViewBag.id = id;
            ViewBag.idcontracttype = idcontracttype;
            ViewBag.kind = kind;
            using (var transaction = new TransactionScope())
            {
                foreach (var item in db.TblPortages.Where(a =>
                a.KindCode == kind &&
                a.FkCustomer == id &&
                a.FkContracttype == idcontracttype &&
                a.IsEnd && a.IsDel == false &&
                a.FkSalmali == User._getuserSalMaliDef()))
                {
                    Models.cl.refPortageAccMoney(db, item.Id);

                }
                transaction.Complete();
            }

            var x = db.TblPortageMoneys.Include(a => a.FkPortageNavigation).Where(a => a.FkPortageNavigation.KindCode == kind && a.FkCustomer == id && a.FkContractType == idcontracttype).OrderBy(a => a.Date);

            return View(x.ToList());
        }

        public async Task<IActionResult> CreateFacktorCustomerAsync(Guid[] ids)
        {


            if (ids == null || ids.Any() == false)
            {
                return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی ارسال نشد"));

            } 

            var rowps = db.TblPortageMoneys.Include(a => a.FkPortageNavigation).Where(a => ids.Contains(a.Id));
            if (rowps.Any() == false)
            {
                return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی یافت نشد"));

            }

            var Mali_KindOT = (web_sard.Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_KindOT) ?? new web_db.TblConf()).Value;
            var Mali_UrlOT = (web_sard.Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_UrlOT) ?? new web_db.TblConf()).Value;
            var Mali_UserOT = (web_sard.Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_UserOT) ?? new web_db.TblConf()).Value;
            var Mali_PassOT = (web_sard.Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_PassOT) ?? new web_db.TblConf()).Value;




            var fir = rowps.First();
            var rowc = db.TblCustomers.Find(fir.FkCustomer);
            var rowtype = db.TblContractTypes.Find(fir.FkContractType);
            AccountingServiceSoapClient vv = new AccountingServiceSoapClient(AccountingServiceSoapClient.EndpointConfiguration.AccountingServiceSoap, Mali_UrlOT);

            var cid = await vv.GetCustomerInfoAsync(Convert.ToInt32(rowc.IdOtherSystem), Mali_UserOT,
                Mali_PassOT);
            if (cid == null)
            {

                return Redirect(Request.UrlReferer().queryChange("error", "مشتری یافت نشد"));
            }


            if (rowtype.KindCotractType==web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {

                var conf = rowtype.ConfigASardKhane();

                if (rowtype.OtSanadByGroup == true)
                {

                    var poKindCode = rowps.AsEnumerable().GroupBy(a => new { a.FkCar, a.FkContract, a.FkContractType, a.FkCustomer, a.Fklocation, a.FkPacking, a.FkProduct, a.Kind, a.Txt, a.PriceOneWeight });


             

                {

                        var faktortype = -1;
                        var OtcodHesabFactor = "";

                        {
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                faktortype = conf.OttypeFaktor.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactor;
                            }
                            else
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                faktortype = conf.OttypeFaktorInBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorInBack;
                            }
                            else
                                 if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                faktortype = conf.OttypeFaktorOut.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOut;
                            }
                            else
                         if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                faktortype = conf.OttypeFaktorOutBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOutBack;
                            }
                        }
                        if (faktortype == -1)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی ارسال نشد"));
                        }


                        var datefaktor = DateTime.Now;
                        switch (rowtype.OtSanadPortageKind)
                        {
                            case web_db.TblContractType.SanadPortageEnum.None:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByPortage:
                                datefaktor = fir.Date;
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomer:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomerAndKindPortage:
                                break;
                            default:
                                break;
                        }

                        var c = await vv.CreateFactorAsync(

                            date: datefaktor,
                            customerId: cid.Id,
                            account: OtcodHesabFactor,// "کد حساب ثابت",//////////////////////////
                            floatingAccount1: cid.FloatingAccount1,
                            floatingAccount2: cid.FloatingAccount2,
                            floatingAccount3: cid.FloatingAccount3,
                            description: "ثبت فاکتور عملیات " + rowtype.Title,
                            customString1: "",
                            SC: conf.OtcodeMarkaz.GetValueOrDefault(),//کد مرکز,
                            BrokerId: 0,//کدبازاریاب
                            Discount: 0,//جمع تخفیف ردیفها
                            userName: Mali_UserOT,
                            password: Mali_PassOT,
                            factorType: faktortype,/////////////////////////////
                            referenceNo: fir.FkPortageNavigation.Code.ToString()
                            , payRcvDate: datefaktor
                         );

                        var period = await vv.GetFiscalPeriodIdAsync(fir.Date);

                        foreach (var item in poKindCode)
                        {




                            {
                                var locationcode = (web_sard.Models.cl.GuidToLocation(item.Key.Fklocation).FirstOrDefault() ?? new web_db.TblLocation());

                                var codekala = "";
                                var codekalaAcc = "";
                                var codekalaShomaresh = 0;
                                if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {
                                    codekala = conf.OtbasculkalaCode;
                                }
                                else if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString())
                                {
                                    codekala = conf.OtcodHesabKargar;
                                }
                                else if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.product.ToString())
                                {
                                    var p = db.TblProducts.Find(item.Key.FkProduct);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }
                                else if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.packing.ToString())
                                {
                                    var p = db.TblPackings.Find(item.Key.FkPacking);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }


                                if (codekala.IsEmpty())
                                {
                                    return Redirect(Request.UrlReferer().queryChange("error", "کد کالا " + item.Key.Txt + " یافت نشد"));

                                }

                                if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString() || item.Key.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {



                                    await vv.AddCostAsync(

                                        factorId: c,
                                        EKTypeCode: int.Parse(codekala ?? "1"),//کد کالا /////////////////////////
                                        amount: item.Sum(a => a.PriceSum.gadrmotlagh()),
                                        isIncremental: true,

                                        description: item.Key.Txt//شرح
                                                                 //ACC = ""//کد حساب
                                     );
                                }
                                else
                                {
                                    if (codekalaShomaresh == 0)
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "کد واحد شمارش  " + item.Key.Txt + " یافت نشد"));

                                    }
                                    if (codekalaAcc.IsEmpty())
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "  " + item.Key.Txt + " یافت نشد"));

                                    }
                                    await vv.AddArticleByCodeAsync(

                                        factorId: c,
                                        merchandiseCode: codekala,//کد کالا /////////////////////////
                                        amount: (int)item.Sum(a => a.Weight.gadrmotlagh()),
                                        unitId: codekalaShomaresh,//کد واحد شمارش
                                        unitPrice: item.Key.PriceOneWeight.gadrmotlagh(),//قیمت هر واحد
                                        auxAmount: 0,//مقدار واحد فرعی
                                        vTax: 0,
                                        discount: 0,
                                        stockRoomId: locationcode.OtcodeAnbar.GetValueOrDefault(),//انبار
                                        description: item.Key.Txt,//شرح
                                                                  //ACC = ""//کد حساب
                                        ACC: codekalaAcc

                                    );
                                }


                            }


                        }
                        //   2183847000 1101 

                        try
                        {
                            var cc = await vv.CommitFactorAsync(c, period.GetFiscalPeriodIdResult.GetValueOrDefault());

                            foreach (var item in poKindCode)
                            {
                                foreach (var item2 in item)
                                {
                                    item2.FkPortageNavigation.OtcodeResid = cc.Number.ToString();

                                }

                            }
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", e.Message));

                        }


                    }

                }
                else
                {


                    var poKindCode = rowps;
                    {

                        var faktortype = -1;

                        var OtcodHesabFactor = "";

                        {
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                faktortype = conf.OttypeFaktor.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactor;
                            }
                            else
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                faktortype = conf.OttypeFaktorInBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorInBack;
                            }
                            else
                                 if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                faktortype = conf.OttypeFaktorOut.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOut;
                            }
                            else
                         if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                faktortype = conf.OttypeFaktorOutBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOutBack;
                            }
                        }
                        if (faktortype == -1)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی ارسال نشد"));
                        }

                        var datefaktor = DateTime.Now;
                        switch (rowtype.OtSanadPortageKind)
                        {
                            case web_db.TblContractType.SanadPortageEnum.None:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByPortage:
                                datefaktor = fir.Date;
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomer:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomerAndKindPortage:
                                break;
                            default:
                                break;
                        }
                        var c = await vv.CreateFactorAsync(

                            date: datefaktor,
                            customerId: cid.Id,
                            account: OtcodHesabFactor,// "کد حساب ثابت",//////////////////////////
                            floatingAccount1: cid.FloatingAccount1,
                            floatingAccount2: cid.FloatingAccount2,
                            floatingAccount3: cid.FloatingAccount3,
                            description: "ثبت فاکتور عملیات " + rowtype.Title,
                            customString1: "",
                            SC: conf.OtcodeMarkaz.GetValueOrDefault(),//کد مرکز,
                            BrokerId: 0,//کدبازاریاب
                            Discount: 0,//جمع تخفیف ردیفها
                            userName: Mali_UserOT,
                            password: Mali_PassOT,
                            factorType: faktortype,/////////////////////////////
                            referenceNo: fir.FkPortageNavigation.Code.ToString()
                            , payRcvDate: datefaktor
                         ); ;

                        var period = await vv.GetFiscalPeriodIdAsync(fir.Date);

                        foreach (var item in poKindCode)
                        {

                            {
                                var locationcode = (web_sard.Models.cl.GuidToLocation(item.Fklocation).FirstOrDefault() ?? new web_db.TblLocation());

                                var codekala = "";
                                var codekalaAcc = "";
                                var codekalaShomaresh = 0;
                                if (item.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {
                                    codekala = conf.OtbasculkalaCode;
                                }
                                else if (item.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString())
                                {
                                    codekala = conf.OtcodHesabKargar;
                                }
                                else if (item.Kind == Models.cl.refPortageAccMoneyEnum.product.ToString())
                                {
                                    var p = db.TblProducts.Find(item.FkProduct);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }
                                else if (item.Kind == Models.cl.refPortageAccMoneyEnum.packing.ToString())
                                {
                                    var p = db.TblPackings.Find(item.FkPacking);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }


                                if (codekala.IsEmpty())
                                {
                                    return Redirect(Request.UrlReferer().queryChange("error", "کد کالا " + item.Txt + " یافت نشد"));

                                }

                                if (item.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString() || item.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {



                                    await vv.AddCostAsync(

                                        factorId: c,
                                        EKTypeCode: int.Parse(codekala ?? "1"),//کد کالا /////////////////////////
                                        amount: item.PriceSum.gadrmotlagh(),
                                        isIncremental: true,

                                        description: item.Txt//شرح
                                                             //ACC = ""//کد حساب
                                     );
                                }
                                else
                                {
                                    if (codekalaShomaresh == 0)
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "کد واحد شمارش  " + item.Txt + " یافت نشد"));

                                    }
                                    if (codekalaAcc.IsEmpty())
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "  " + item.Txt + " یافت نشد"));

                                    }
                                    await vv.AddArticleByCodeAsync(

                                        factorId: c,
                                        merchandiseCode: codekala,//کد کالا /////////////////////////
                                        amount: (int)item.Weight.gadrmotlagh(),
                                        unitId: codekalaShomaresh,//کد واحد شمارش
                                        unitPrice: item.PriceOneWeight.gadrmotlagh(),//قیمت هر واحد
                                        auxAmount: 0,//مقدار واحد فرعی
                                        vTax: 0,
                                        discount: 0,
                                        stockRoomId: locationcode.OtcodeAnbar.GetValueOrDefault(),//انبار
                                        description: item.Txt,//شرح
                                                              //ACC = ""//کد حساب
                                        ACC: codekalaAcc

                                    );
                                }


                            }


                        }
                        //   2183847000 1101 

                        try
                        {
                            var cc = await vv.CommitFactorAsync(c, period.GetFiscalPeriodIdResult.GetValueOrDefault());

                            foreach (var item in poKindCode)
                            {

                                item.FkPortageNavigation.OtcodeResid = cc.Number.ToString();


                            }
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", e.Message));

                        }

                    }

                }

            }
            else
              if (rowtype.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {

                var conf = rowtype.ConfigASabad();

                if (rowtype.OtSanadByGroup == true)
                {

                    var poKindCode = rowps.AsEnumerable().GroupBy(a => new { a.FkCar, a.FkContract, a.FkContractType, a.FkCustomer, a.Fklocation, a.FkPacking, a.FkProduct, a.Kind, a.Txt, a.PriceOneWeight });




                    {

                        var faktortype = -1;
                        var OtcodHesabFactor = "";

                        {
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                faktortype = conf.OttypeFaktor.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactor;
                            }
                            else
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                faktortype = conf.OttypeFaktorInBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorInBack;
                            }
                            else
                                 if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                faktortype = conf.OttypeFaktorOut.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOut;
                            }
                            else
                         if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                faktortype = conf.OttypeFaktorOutBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOutBack;
                            }
                        }
                        if (faktortype == -1)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی ارسال نشد"));
                        }


                        var datefaktor = DateTime.Now;
                        switch (rowtype.OtSanadPortageKind)
                        {
                            case web_db.TblContractType.SanadPortageEnum.None:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByPortage:
                                datefaktor = fir.Date;
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomer:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomerAndKindPortage:
                                break;
                            default:
                                break;
                        }

                        var c = await vv.CreateFactorAsync(

                            date: datefaktor,
                            customerId: cid.Id,
                            account: OtcodHesabFactor,// "کد حساب ثابت",//////////////////////////
                            floatingAccount1: cid.FloatingAccount1,
                            floatingAccount2: cid.FloatingAccount2,
                            floatingAccount3: cid.FloatingAccount3,
                            description: "ثبت فاکتور عملیات " + rowtype.Title,
                            customString1: "",
                            SC: conf.OtcodeMarkaz.GetValueOrDefault(),//کد مرکز,
                            BrokerId: 0,//کدبازاریاب
                            Discount: 0,//جمع تخفیف ردیفها
                            userName: Mali_UserOT,
                            password: Mali_PassOT,
                            factorType: faktortype,/////////////////////////////
                            referenceNo: fir.FkPortageNavigation.Code.ToString()
                            , payRcvDate: datefaktor
                         );

                        var period = await vv.GetFiscalPeriodIdAsync(fir.Date);

                        foreach (var item in poKindCode)
                        {




                            {
                                var locationcode = (web_sard.Models.cl.GuidToLocation(item.Key.Fklocation).FirstOrDefault() ?? new web_db.TblLocation());

                                var codekala = "";
                                var codekalaAcc = "";
                                var codekalaShomaresh = 0;
                                if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString())
                                {
                                    codekala = conf.OtcodHesabKargar;
                                }
                                else if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.product.ToString())
                                {
                                    var p = db.TblProducts.Find(item.Key.FkProduct);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }
                                else if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.packing.ToString())
                                {
                                    var p = db.TblPackings.Find(item.Key.FkPacking);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }


                                if (codekala.IsEmpty())
                                {
                                    return Redirect(Request.UrlReferer().queryChange("error", "کد کالا " + item.Key.Txt + " یافت نشد"));

                                }

                                if (item.Key.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString() || item.Key.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {



                                    await vv.AddCostAsync(

                                        factorId: c,
                                        EKTypeCode: int.Parse(codekala ?? "1"),//کد کالا /////////////////////////
                                        amount: item.Sum(a => a.PriceSum.gadrmotlagh()),
                                        isIncremental: true,

                                        description: item.Key.Txt//شرح
                                                                 //ACC = ""//کد حساب
                                     );
                                }
                                else
                                {
                                    if (codekalaShomaresh == 0)
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "کد واحد شمارش  " + item.Key.Txt + " یافت نشد"));

                                    }
                                    if (codekalaAcc.IsEmpty())
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "  " + item.Key.Txt + " یافت نشد"));

                                    }
                                    await vv.AddArticleByCodeAsync(

                                        factorId: c,
                                        merchandiseCode: codekala,//کد کالا /////////////////////////
                                        amount: (int)item.Sum(a => a.Weight.gadrmotlagh()),
                                        unitId: codekalaShomaresh,//کد واحد شمارش
                                        unitPrice: item.Key.PriceOneWeight.gadrmotlagh(),//قیمت هر واحد
                                        auxAmount: 0,//مقدار واحد فرعی
                                        vTax: 0,
                                        discount: 0,
                                        stockRoomId: locationcode.OtcodeAnbar.GetValueOrDefault(),//انبار
                                        description: item.Key.Txt,//شرح
                                                                  //ACC = ""//کد حساب
                                        ACC: codekalaAcc

                                    );
                                }


                            }


                        }
                        //   2183847000 1101 

                        try
                        {
                            var cc = await vv.CommitFactorAsync(c, period.GetFiscalPeriodIdResult.GetValueOrDefault());

                            foreach (var item in poKindCode)
                            {
                                foreach (var item2 in item)
                                {
                                    item2.FkPortageNavigation.OtcodeResid = cc.Number.ToString();

                                }

                            }
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", e.Message));

                        }


                    }

                }
                else
                {


                    var poKindCode = rowps;
                    {

                        var faktortype = -1;

                        var OtcodHesabFactor = "";

                        {
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.In)
                            {
                                faktortype = conf.OttypeFaktor.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactor;
                            }
                            else
                            if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.InBack)
                            {
                                faktortype = conf.OttypeFaktorInBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorInBack;
                            }
                            else
                                 if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.Out)
                            {
                                faktortype = conf.OttypeFaktorOut.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOut;
                            }
                            else
                         if (fir.FkPortageNavigation.KindCode == (int)web_sard.Models.tbls.portage.kindPortage.kindPortageEnum.OutBack)
                            {
                                faktortype = conf.OttypeFaktorOutBack.GetValueOrDefault(1);
                                OtcodHesabFactor = conf.OtcodHesabFactorOutBack;
                            }
                        }
                        if (faktortype == -1)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", "هیچ موردی ارسال نشد"));
                        }

                        var datefaktor = DateTime.Now;
                        switch (rowtype.OtSanadPortageKind)
                        {
                            case web_db.TblContractType.SanadPortageEnum.None:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByPortage:
                                datefaktor = fir.Date;
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomer:
                                break;
                            case web_db.TblContractType.SanadPortageEnum.ByCustomerAndKindPortage:
                                break;
                            default:
                                break;
                        }
                        var c = await vv.CreateFactorAsync(

                            date: datefaktor,
                            customerId: cid.Id,
                            account: OtcodHesabFactor,// "کد حساب ثابت",//////////////////////////
                            floatingAccount1: cid.FloatingAccount1,
                            floatingAccount2: cid.FloatingAccount2,
                            floatingAccount3: cid.FloatingAccount3,
                            description: "ثبت فاکتور عملیات " + rowtype.Title,
                            customString1: "",
                            SC: conf.OtcodeMarkaz.GetValueOrDefault(),//کد مرکز,
                            BrokerId: 0,//کدبازاریاب
                            Discount: 0,//جمع تخفیف ردیفها
                            userName: Mali_UserOT,
                            password: Mali_PassOT,
                            factorType: faktortype,/////////////////////////////
                            referenceNo: fir.FkPortageNavigation.Code.ToString()
                            , payRcvDate: datefaktor
                         ); ;

                        var period = await vv.GetFiscalPeriodIdAsync(fir.Date);

                        foreach (var item in poKindCode)
                        {

                            {
                                var locationcode = (web_sard.Models.cl.GuidToLocation(item.Fklocation).FirstOrDefault() ?? new web_db.TblLocation());

                                var codekala = "";
                                var codekalaAcc = "";
                                var codekalaShomaresh = 0;
                               if (item.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString())
                                {
                                    codekala = conf.OtcodHesabKargar;
                                }
                                else if (item.Kind == Models.cl.refPortageAccMoneyEnum.product.ToString())
                                {
                                    var p = db.TblProducts.Find(item.FkProduct);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }
                                else if (item.Kind == Models.cl.refPortageAccMoneyEnum.packing.ToString())
                                {
                                    var p = db.TblPackings.Find(item.FkPacking);
                                    codekala = p.OtcodeKala;
                                    codekalaAcc = p.OtcodeKalaAcc;
                                    codekalaShomaresh = p.OtcodeVahedShomaresh.GetValueOrDefault();
                                }


                                if (codekala.IsEmpty())
                                {
                                    return Redirect(Request.UrlReferer().queryChange("error", "کد کالا " + item.Txt + " یافت نشد"));

                                }

                                if (item.Kind == Models.cl.refPortageAccMoneyEnum.kargar.ToString() || item.Kind == Models.cl.refPortageAccMoneyEnum.bascul.ToString())
                                {



                                    await vv.AddCostAsync(

                                        factorId: c,
                                        EKTypeCode: int.Parse(codekala ?? "1"),//کد کالا /////////////////////////
                                        amount: item.PriceSum.gadrmotlagh(),
                                        isIncremental: true,

                                        description: item.Txt//شرح
                                                             //ACC = ""//کد حساب
                                     );
                                }
                                else
                                {
                                    if (codekalaShomaresh == 0)
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "کد واحد شمارش  " + item.Txt + " یافت نشد"));

                                    }
                                    if (codekalaAcc.IsEmpty())
                                    {
                                        return Redirect(Request.UrlReferer().queryChange("error", "  " + item.Txt + " یافت نشد"));

                                    }
                                    await vv.AddArticleByCodeAsync(

                                        factorId: c,
                                        merchandiseCode: codekala,//کد کالا /////////////////////////
                                        amount: (int)item.Weight.gadrmotlagh(),
                                        unitId: codekalaShomaresh,//کد واحد شمارش
                                        unitPrice: item.PriceOneWeight.gadrmotlagh(),//قیمت هر واحد
                                        auxAmount: 0,//مقدار واحد فرعی
                                        vTax: 0,
                                        discount: 0,
                                        stockRoomId: locationcode.OtcodeAnbar.GetValueOrDefault(),//انبار
                                        description: item.Txt,//شرح
                                                              //ACC = ""//کد حساب
                                        ACC: codekalaAcc

                                    );
                                }


                            }


                        }
                        //   2183847000 1101 

                        try
                        {
                            var cc = await vv.CommitFactorAsync(c, period.GetFiscalPeriodIdResult.GetValueOrDefault());

                            foreach (var item in poKindCode)
                            {

                                item.FkPortageNavigation.OtcodeResid = cc.Number.ToString();


                            }
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            return Redirect(Request.UrlReferer().queryChange("error", e.Message));

                        }

                    }

                }

            }









            return Redirect(Request.UrlReferer().queryChange("txt", "ثبت فاکتور   " + " انجام شد."));
        }

    }
}
