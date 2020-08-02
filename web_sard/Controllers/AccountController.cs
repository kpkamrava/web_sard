using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using web_lib;

namespace web_sard.Controllers
{
    public class AccountController : Controller
    {
        private readonly web_db.sardweb_Context db;
        public AccountController(web_db.sardweb_Context db)
        {
            this.db = db; 
        }
        async Task _loginAsync(web_db.TblUser x)
        {
            var claims = new List<Claim>
            {
                new Claim( ClaimTypes.NameIdentifier,x.Id.ToString()),
                new Claim( ClaimTypes.Name,x.Username??""),
                new Claim( ClaimTypes.Role,x.Roles??""),
                new Claim( ClaimTypes.GivenName,x.Title??""),
                new Claim( ClaimTypes.Dsa,x.Salmalidef.ToString()),

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
        public async Task<IActionResult> Login(Models.tbls.user.login model)
        {

            var x = db.TblUser.SingleOrDefault(a=>a.Username.ToLower()==model.username.Trim().ToLower()&&a.Password==model.password);
            if (x==null)
            {
                ViewBag.error = "نام کاربری یا رمز عبور اشتباه است";
                return View(model);
            }
            if (x.IsDel==true||x.IsActive==false)
            {
                ViewBag.error = "حساب کاربری غیر فعال شده است";
                return View(model);
            }

            await _loginAsync(x);

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
            var x = db.TblUser.Select(a=>new Models.tbls .user.user(a));
            return View(x);

        }
        [LoginAuth(_UserRol._Rolls.AddEditUser)]
        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.user.user { Roles = new string[0]  };
            var x = db.TblUser.Find(id);
            if (x!=null)
            {
                model = new Models.tbls.user.user(x);
                
            }
            return View(model);

        }


        [LoginAuth(_UserRol._Rolls.AddEditUser)]
        [HttpPost]
        public IActionResult Create(Guid id, Models.tbls.user.user model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.error = "ثبت انجام نشد";
                return View(model);
            }
            var x = db.TblUser.Find(id);
            if (x == null)
            {
                x = new web_db.TblUser { Id = Guid.NewGuid(), IsDel = false };
                db.TblUser.Add(x);
            }
            try
            {
                x.Mob = model.Mob;
                x.Password = model.Password;
                x.Roles = string.Join(',', model.Roles);
                x.Title = model.Title;
                x.Username = model.Username;
                x.IsActive = model.isActive;
                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                return RedirectToAction(nameof(Index));
            }
            catch { }
            return View(model);

        }



        public async Task<IActionResult> DefYearAsync(int id)
        {
            var x = db.TblUser.Find(User._getuserid());
            x.Salmalidef = id;
            db.SaveChanges();
            await _loginAsync(x);
            return Redirect("/");
        }
    }
}