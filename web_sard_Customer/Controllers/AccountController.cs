using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using web_lib;

namespace web_sard_Customer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _settings;

        web_db.sardweb_Context db;
        public AccountController(web_db.sardweb_Context db, IConfiguration settings)
        { this.db = db; this._settings = settings; }

        public IActionResult index()
        {
            Models.cl._conf = db.TblConf.OrderBy(a => a.Key).ToList();

            if (Models.cl._conf.Any(a=>a.Key==web_db.TblConf.KeyEnum.US_SefareshMoshtarianEnable&&a.Value==true.ToString())==false)
            {
               return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Login(string error)
        {
            ViewBag.p = 1;
            ViewBag.error = error;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, int p)
        {

            ViewBag.username = username;
            ViewBag.p = 1;


            if (username.IsEmpty() || username.IsCodemeli() == false)
            {
                ViewBag.error = "کد ملی اشتباه است";

                return View();

            }
           // var def = db.TblConfigs.First();
            var x = db.TblCustomers.OrderByDescending(a=>a.FkSalmali).FirstOrDefault(a => /*a.FkSalmali == def.SalMaliMain*/ a.NationalCode == username);
            if (x == null)
            {

                ViewBag.error = "کد ملی شما در سیستم ثبت نیست";
                return View();

            }


            if (p == 1)
            {


                if (x.Datesendpassword.HasValue == false)
                {

                    await sendsmsAsync(x);
                    db.SaveChanges();
                    ViewBag.p = 2;
                    ViewBag.txt = "رمز عبور برای شما ارسال گردید";

                }
                else
                {
                    //ViewBag.txt = "رمز عبور قبلا برای شما ارسال گردیده";
                    ViewBag.p = 2;
                    if (x.Datesendpassword.HasValue == false || ((DateTime.Now - x.Datesendpassword.Value) > TimeSpan.FromMinutes(5)))
                    {

                        await sendsmsAsync(x);
                        db.SaveChanges();
                        ViewBag.txt = "رمز عبور برای شما ارسال گردید";


                    }
                    else
                    {
                        ViewBag.error = "رمز عبور قبلا برای شما ارسال گردیده";


                    }
                }

            }



            else if (p == 2)
            {

                if (x.Password == password)
                {
                    x.Dateloginlast = DateTime.Now;
                    db.SaveChanges();

                    var claims = new List<Claim>
            {
                 new Claim( ClaimTypes.NameIdentifier,x.Id.ToString()),
                  new Claim( ClaimTypes.Name,x.Id.ToString()),

            };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    await sendsmsloginAsync(x);
                    return Redirect("/");
                }
                else
                {
                    ViewBag.p = 2;
                    ViewBag.txt = "رمز عبوراشتباه است";

                }


            }

            else if (p == 3)
            {
                ViewBag.p = 2;
                if (x.Datesendpassword.HasValue == false || ((DateTime.Now - x.Datesendpassword.Value) > TimeSpan.FromMinutes(5)))
                {

                    await sendsmsAsync(x);
                    db.SaveChanges();
                    ViewBag.txt = "رمز عبور برای شما ارسال گردید";

                }
                else
                {
                    ViewBag.error = "رمز عبور قبلا برای شما ارسال گردیده";

                }
            }


            return View();



        }





        public async Task<IActionResult> SignOut()
        {


            await HttpContext.SignOutAsync();

            return Redirect("/");





        }

        async Task<string> sendsmsAsync(web_db.TblCustomer c)
        {
            c.Password = new Random().Next(11001, 99999).ToString();
            c.Datesendpassword = DateTime.Now;

            var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
            var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
            var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
            var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


            web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };
            return await s.sendsmsAsync(c.Mob, @"سردخانه میوه طلایی
کد ورود به سایت: " + c.Password);
        }
        async Task<string> sendsmsloginAsync(web_db.TblCustomer c)
        {

            var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
            var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
            var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
            var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


            web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };
            return await s.sendsmsAsync(c.Mob, @"سردخانه میوه طلایی
ورود موفق
" + DateTime.Now.ToPersianDateTime());
        }
    }
}
