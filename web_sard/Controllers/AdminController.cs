using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web_lib;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.SuperVisor)]
    public class AdminController : Controller
    {
        internal web_db.sardweb_Context db;
        IHostingEnvironment env;
        public AdminController(web_db.sardweb_Context db, IHostingEnvironment env) { this.db = db; this.env = env; }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Backup()
        {
            var path = $"{env.WebRootPath }\\Backup.bak";
            db.backupDb(path);


            return File(System.IO.File.OpenRead(path), "APPLICATION/octet-stream", $"backup-{DateTime.Now.ToString()}.bak", true);

        }



        public IActionResult contracttype(Guid id)
        {
            var x = db.TblContractTypes.Find(id);
            ViewBag.contype = x;
            if (x.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
            {

                return View("contracttypeASardKhane", x.ConfigASardKhane());
            }
            if (x.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)
            {
                return View("contracttypeASabad", x.ConfigASabad());
            }
            if (x.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ABaskul)
            {
                return View("contracttypeABaskul", x.ConfigABaskul());
            }
            return null;

        }


        [HttpPost]
        public IActionResult contracttypeASardKhane(Guid id, string title, web_db.TblContractType.ASardKhane model)
        {
            var z = db.TblContractTypes.Find(id);
            z.Title = title;
            z.Config(model);

            db.SaveChanges();
            Models.cl.__ListContractType = db.TblContractTypes.ToList();
            return RedirectToAction("contracttype", new { id = id });
        }
        [HttpPost]
        public IActionResult contracttypeASabad(Guid id, string title, web_db.TblContractType.ASabad model)
        {
            var z = db.TblContractTypes.Find(id);
            z.Title = title;
            z.Config(model);

            db.SaveChanges();
            Models.cl.__ListContractType = db.TblContractTypes.ToList();
            return RedirectToAction("contracttype", new { id = id });
        }
        public IActionResult contracttypeAbaskul(Guid id, string title, web_db.TblContractType.ABaskul model)
        {
            var z = db.TblContractTypes.Find(id);
            z.Title = title;
            z.Config(model);

            db.SaveChanges();
            Models.cl.__ListContractType = db.TblContractTypes.ToList();
            return RedirectToAction("contracttype", new { id = id });
        }





        [HttpPost]
        public IActionResult Conf(web_db.TblConf.KeyEnum Key, string value, int? ord = null, string k1 = "", string k2 = "", string k3 = "")
        {
            var x = db.TblConf.SingleOrDefault(a => a.Key == Key && a.ord == ord);
            if (x == null)
            {
                x = new web_db.TblConf
                {
                    Id = Guid.NewGuid(),
                    Key = Key,
                };
                db.Add(x);
            }
            x.k1 = k1;
            x.k2 = k2;
            x.k3 = k3;
            x.KeyStr = Key.ToKPvalusAttr().Description;
            x.ord = ord;
            x.Value = value;
            db.SaveChanges();

            Models.cl._conf = db.TblConf.OrderBy(a => a.Key).ToList();
            return Redirect(Request.UrlReferer());
        }

    }
}
