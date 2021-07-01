namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="GroupController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class GroupController : Controller
    {
 
        internal web_db.sardweb_Context db;
 
        internal IConfiguration _mySettings;

 
        public GroupController(web_db.sardweb_Context db, IConfiguration _mySettings)
        {
            this.db = db;
            this._mySettings = _mySettings;
        }

    
        public IActionResult Index(string txt = "")
        {
            ViewBag.txt = txt;
            var list = db.TblGroups.OrderBy(a => a.Title);
            return View(list.ToList());
        }

      
        public IActionResult Create(Guid id)
        {

            var row = db.TblGroups.Find(id);
            if (row == null)
            {
                row = new web_db.TblGroup { IsActive = true };
            }
            ViewBag.nums = string.Join(System.Environment.NewLine, db.TblCustomerGroups.Include(a => a.FkCustumerNavigation).Where(a => a.FkGroup == id).Select(a => a.FkCustumerNavigation.Mob));
            return View(row);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(web_db.TblGroup model)
        {
            try
            {
                var x = db.TblGroups.Find(model.Id);
                if (x == null)
                {
                    x = new web_db.TblGroup { Id = Guid.NewGuid() };

                    db.TblGroups.Add(x);
                }

                x.IsActive = model.IsActive;
                x.Title = model.Title;
                x.Fklocation = model.Fklocation;
                x.Class = model.Class;

                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                Models.cl._ListGroup = db.TblGroups.OrderBy(a => a.Title).ToList();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.error = "ثبت انجام نشد";
            }
            return View(model);
        }

        /// <summary>
        /// The SendSMSAsync.
        /// </summary>
        /// <param name="Id">The Id<see cref="Guid"/>.</param>
        /// <param name="nums">The nums<see cref="string"/>.</param>
        /// <param name="msg">The msg<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendSMSAsync(Guid Id, string nums, string msg)
        {
            try
            {
                var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
                var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
                var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
                var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


                web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };

                foreach (var item in nums.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (item.Trim().IsMobile())
                    {
                        await s.sendsmsAsync(item.Trim(), msg);

                    }
                }


                return RedirectToAction("index", new { txt = "پیامها ارسال گردید" });
            }
            catch(Exception e)
            {
                return RedirectToAction("index", new { txt = "خطا در ارسال پیامها. "+e.Message });
            }
        }
    }
}
