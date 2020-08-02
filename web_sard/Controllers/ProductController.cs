using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_lib;

namespace web_sard.Controllers
{
    [LoginAuth(_UserRol._Rolls.EtelahatePaie)]
    public class ProductController : Controller
    {
        web_db.sardweb_Context db;
        public ProductController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var x = from n in db.TblProduct
                    orderby n.Code
                    select  new Models.tbls.product.Product(n) { title=n.Title } ; 
            return View(x);
        }




        public IActionResult Create(Guid id)
        {
            var model = new Models.tbls.product.Product();
            var x = db.TblProduct.Find(id);
            if (x==null)
            {
                model.code =( db.TblProduct.Max(a => (int?)a.Code) ?? 0 )+ 1;
            }
            else
            {
                model = new Models.tbls.product.Product(x);
                
            }
            
            return View(model); 
        
        
        }
        [HttpPost]
        public IActionResult Create( Guid id,Models.tbls.product.Product model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.txt = "ثبت انجام نشد - فیلد ها را درست پر کنید";
                return View(model);
            }
            try
            {
                var x = db.TblProduct.Find(id);
                if (x == null)
                {
                    x = new web_db.TblProduct { Id = Guid.NewGuid() };

                       db.TblProduct.Add(x);
                }
                x.Code = model.code;
                x.IsActive = model.isActive;
                x.Title = model.title;
              
                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.error = "ثبت انجام نشد";
            }
            return View(model);
        }




    }
}