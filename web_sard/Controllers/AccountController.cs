namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using web_lib;
    using web_sard.Models;

    /// <summary>
    /// Defines the <see cref="AccountController" />.
    /// </summary>
    public class AccountController : Controller
    {

        private readonly web_db.sardweb_Context db;

        IConfiguration _mySettings;
        public AccountController(web_db.sardweb_Context db, IConfiguration _mySettings)
        {
            this.db = db;
            this._mySettings = _mySettings;
        }


        internal async Task _loginAsync(web_db.TblUser x, web_db.sardweb_Context db)
        {

            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier,x.Id.ToString()),
                new Claim( ClaimTypes.Name,x.Username??""),
                new Claim( ClaimTypes.Role,x.Roles??""),
                new Claim( ClaimTypes.GivenName,x.Title??""),
                new Claim("Years",db.TblUserSals.Where(a=>a.FkUser==x.Id).Select(a=>a.FkSal).ToArray().ToJson()),
                new Claim( ClaimTypes.Dsa,x.Salmalidef.ToString()),
                new Claim("Permis",Newtonsoft.Json.JsonConvert.SerializeObject(db.TblUserPermis.Where(a=>a.FkUser==x.Id)).ToString()),
            };

            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Models.tbls.user.login model, CancellationToken stoppingToken = default)
        {

            var x = db.TblUsers.SingleOrDefault(a => a.Username.ToLower() == model.username.Trim().ToLower() && a.Password == model.password);
            if (x == null)
            {
                ViewBag.error = "نام کاربری یا رمز عبور اشتباه است";
                return View(model);
            }
            if (x.IsDel == true || x.IsActive == false)
            {
                ViewBag.error = "حساب کاربری غیر فعال شده است";
                return View(model);
            }

            await _loginAsync(x, db);


            cl._listSmsForSend.Add(new cl.SmsForSend {number=x.Mob,txt="ورود موفق"+System.Environment.NewLine+DateTime.Now.ToPersianDateTime() });

            return Redirect("/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        [LoginAuth(_UserRol._Rolls.AddEditUser)]
        public IActionResult ListUser()
        {
            var x = db.TblUsers.Select(a => new Models.tbls.user.user(a, db));
            return View(x);
        }


        [LoginAuth(_UserRol._Rolls.AddEditUser)]
        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.user.user { Roles = new string[0], isActive = true, Id = Guid.NewGuid() };
            var x = db.TblUsers.Find(id);
            if (x != null)
            {
                model = new Models.tbls.user.user(x, db);

            }
            return View(model);
        }


        [LoginAuth(_UserRol._Rolls.AddEditUser)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.user.user model)
        {   
            var x = db.TblUsers.Find(model.Id);
            if (x!=null)
            {
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.error = "ثبت انجام نشد";
                return View(model);
            }
              if (x == null)
            {
                x = new web_db.TblUser { Id = model.Id, IsDel = false, Salmalidef = User._getuserSalMaliDef() };
                db.TblUsers.Add(x);
            }
            try
            {
                x.Mob = model.Mob;
                if (model.Password.IsEmpty() == false)
                {
                    x.Password = model.Password;

                }
                x.Roles = string.Join(',', model.Roles);
                x.Title = model.Title;
                x.Username = model.Username;
                x.IsActive = model.isActive;

                foreach (var item in model.Permis)
                {
                    var z = db.TblUserPermis.SingleOrDefault(a => a.FkUser == x.Id && a.FkPortageType == item.FkPortageType);
                    if (z == null)
                    {
                        z = new web_db.TblUserPermi { FkPortageType = item.FkPortageType, FkUser = x.Id };
                        db.TblUserPermis.Add(z);
                    }
                    z.IsIn = item.IsIn;
                    z.IsInBack = item.IsInBack;
                    z.IsOut = item.IsOut;
                    z.IsOutBack = item.IsOutBack;
                    z.IsReport = item.IsReport;
                    z.IsType = z.IsIn || z.IsInBack || z.IsOut || z.IsOutBack;

                }

                try
                {
                    db.SaveChanges(); ViewBag.txt = "ثبت انجام شد";
                    return RedirectToAction(nameof(ListUser));
                }
                catch (Exception e)
                {

                    ViewBag.error = e.Message;
                }



            }
            catch { }
            return View(model);
        }

        [LoginAuth()]
        public IActionResult RePassword()
        {

            return View();
        }


        [LoginAuth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RePassword(string password, string newPassword, string reNewPassword)
        {

            if (newPassword != reNewPassword)
            {
                ViewBag.error = "تکرار رمز عبور اشتباه است";
                return View();
            }

            var x = db.TblUsers.Find(User._getuserid());
            if (x.Password != password)
            {
                ViewBag.error = "رمز عبور اشتباه است";
                return View();


            }


            x.Password = newPassword;



            db.SaveChanges();
            ViewBag.txt = "ثبت انجام شد";
            return View();



        }

        public async Task<IActionResult> DefYearAsync(int id)
        {
            var x = db.TblUsers.Find(User._getuserid());
            x.Salmalidef = id;
            db.SaveChanges();
            await _loginAsync(x, db);
            return Redirect("/");
        }
    }
}
