using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_lib;
namespace web_sard_Customer.Controllers
{
    public class QueuController : Controller
    {
        web_db.sardweb_Context db;
        IConfiguration settings;
        public QueuController(web_db.sardweb_Context db, IConfiguration settings)
        { 
            this.db = db; 
            this.settings = settings;
        }

        public IActionResult Index()
        {
         
            ViewBag.p = 0;
           
            return View(); 
          
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int p, web_db._queu.TblQueu model, List<Guid> Product, List<Guid> Packing)
        {

            if (model.mob.IsMobile() == false)
            {
                ViewBag.error = "شماره موبایل اشتباه است";
                p = 0;
            }




            if (p == 0)
            {

            }
            else
            if (p == 1)
            {
                var row = db.TblQueus.FirstOrDefault(a => a.mob == model.mob&&a.KindQueu==web_db._queu.TblQueu.QueuEnum.Empty);

                if (row == null)
                {
                    row = new web_db._queu.TblQueu
                    {
                        Id = Guid.NewGuid(),
                        mob = model.mob,
                        date = DateTime.Now,
                       
                        Code = (db.TblQueus.Max(a => (long?)a.Code) ?? 0) + 1,
                        KindQueu = web_db._queu.TblQueu.QueuEnum.Empty,
                        Addras = "",
                        codemeli = "",
                        CodeMahsuls = "",
                        Weight = 0,
                        Name="",
                        Txt="",
                       
                    };
                    db.TblQueus.Add(row);
                }
                if (row.datecodesend.HasValue == false || (DateTime.Now - row.datecodesend.Value > TimeSpan.FromMinutes(15)))
                {
                    await sendsmsAsync(row);
                }
                db.SaveChanges();
                model = row;

            }
            else
            {

                var row = db.TblQueus.SingleOrDefault(a => a.Id == model.Id);
             
                var z = db.TblProducts.Where(a => Product.Contains(a.Id)).Select(a => a.Title).ToList();
                z.AddRange(db.TblPackings.Where(a => Packing.Contains(a.Id)).Select(a => a.Title).ToList());

                Product.AddRange(Packing);
                row.CodeMahsuls = Product.ToJson();


                if (row.codesend != model.codesend)
                {
                    ViewBag.error = "کد ارسالی به موبایل اشتباه است ";
                    p = 1;
                }
                else
                {
                    row.Addras = model.Addras;

                  
                    row.Mahsuls = string.Join(',', z);
                    row.Weight = model.Weight;
                    row.codemeli = model.codemeli;
                    row.KindQueu = web_db._queu.TblQueu.QueuEnum.Save; 

                    row.Name = model.Name;
                    row.Txt = model.Txt;
                   
                    db.SaveChanges();

                    await sendsmsloginAsync(row);
                }

                model = row;

            }



            ViewBag.p = p;
            model.codesend = null;
            if (p == 0)
            {
                return View(model);
            }
            else
           if (p == 1)
            {
                ViewBag.listPacking = db.TblPackings.Where(a => a.ForContractType().Contains(web_db.TblContractType.KindCotractTypeEnum.ASardKhane) && a.IsActive == true).OrderBy(a => a.Code).ToList();
                ViewBag.listProduct = db.TblProducts.Where(a => a.IsActive).OrderBy(a => a.Ord).ToList();
                return View("indexEdit", model);
            }
            else
            {


                return RedirectToAction("success", new { model.Id });

            }

        }


        public IActionResult Success(Guid Id)
        {
            var row = db.TblQueus.SingleOrDefault(a => a.Id == Id);

           

            return View(row);

        }



        private  async Task<string> sendsmsAsync(web_db._queu.TblQueu c)
        {
            c.codesend = new Random().Next(11001, 99999);
            c.datecodesend = DateTime.Now;

            var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
            var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
            var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
            var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


            web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };
            return await s.sendsmsAsync(c.mob, settings.GetValue<string>("txtPortagesms")+ @"
کد ثبت درخواست: " + c.codesend);
        }
        private async Task<string> sendsmsloginAsync(web_db._queu.TblQueu c)
        {

            var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
            var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
            var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
            var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


            web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };
            return await s.sendsmsAsync(c.mob, settings.GetValue<string>("txtPortagesms") +@$"
 شماره پیگیری درخواست شما : {c.Code}
زمان ثبت :{DateTime.Now.ToPersianDateTime()}
"  );
        }



    }
}
