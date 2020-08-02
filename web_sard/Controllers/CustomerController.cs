using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPaivast;
using Microsoft.AspNetCore.Mvc;
using web_lib;
namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.AddEditCustomer)]
    public class CustomerController : Controller
    {

        web_db.sardweb_Context db;
        public CustomerController(web_db.sardweb_Context db) { this.db = db; }
        public IActionResult Index()
        {
            var x = from n in db.TblCustomer
                    select n;
            return View(x.ToList());
        }
        public async Task<IActionResult> CustomerPaivastAsync(string CellPhone)
        {
           ApiPaivast.AccountingServiceSoapClient vv = new AccountingServiceSoapClient(AccountingServiceSoapClient.EndpointConfiguration.AccountingServiceSoap,Models.cl._config.ApiUrlPaivest);
          var c=await  vv.GetCustomerIdByCellPhoneAsync(new GetCustomerIdByCellPhoneRequest {CellPhone=CellPhone});
          var idc= c.GetCustomerIdByCellPhoneResult;

          var cid = await vv.GetCustomerInfoAsync(new GetCustomerInfoRequest(idc,Models.cl._config.ApiUser,Models.cl._config.ApiPass));

        

            return RedirectToAction("cid",new {txt="ثبت موفق" });
        }
       
    }
}