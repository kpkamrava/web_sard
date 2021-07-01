namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using web_lib;
    using web_sard.Models;
 
    [Route("[controller]")]
    [ApiController]
     
    public class AjaxController : Controller 
    {

        internal web_db.sardweb_Context db;


        public AjaxController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        [HttpGet("GetLastBascul")]
        public JsonResult GetLastBascul()
        {
            return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                WeightBacul = Models.cl.bascul.WeightBacul,
                WeightZerooBacul = Models.cl.bascul._WeightZeroBacul,
                WeightBaculIsAllow = Models.cl.bascul.WeightBaculIsAllow
            }));
        }


        [HttpGet("SetZeroBascul")]
        [LoginAuth(_UserRol._Rolls.Bascul)]
        public JsonResult SetZeroBascul()
        {

            cl.bascul.setZeroBacul();
            return new JsonResult(Newtonsoft.Json.JsonConvert.SerializeObject(new { WeightBacul = Models.cl.bascul.WeightBacul, WeightBaculIsAllow = Models.cl.bascul.WeightBaculIsAllow }));
        }


        [HttpGet("SetBasculWeight/{id}")]

        public ActionResult<string> SetBasculWeight(decimal id,string username,string password )
        {  
            new Models.cl.bascul(id);

            var row=db.TblUsers.SingleOrDefault(a => a.Username == username.Trim() && a.Password == password);
            if (row!=null)
            {
                if (row.Roles.Contains(_UserRol._Rolls.Bascul.ToString()))
                {
                  
                    return Ok();
                }
            }
          
            
                return Unauthorized();
            
         
        }
        [HttpGet("getcontracts")]

        public ActionResult<string> getcontracts(string id)
        {
            var row = db.TblContracts.Include(a=>a.FkCustomerNavigation).Where(a => id.Contains( a.FkCustomer.ToString()));
          

            return Ok(row.Select(a=>new {a.Id,a.Code,a.FkCustomerNavigation.Title }).ToJson());


        }
        [HttpGet("Docs/{id}")]
        public IActionResult Docs(Guid id,string title="")
        {
            ViewData["Title"] ="مستندات "+ title;
            ViewData["idp"] = id;

            var x = db.TblPortageDocuments.Where(a=>a.FkPortage==id||a.FkP==id).OrderBy(a=>a.Date).ToList();
           
            return View(x);
        }



          [HttpPost("DocUpload")]
        public IActionResult DocUpload(IList<IFormFile> files, Guid idp)
        {
            foreach (IFormFile source in files)
            {
                string Extension = Path.GetExtension(source.FileName).ToLowerInvariant();
                 

                var x = new web_db.TblPortageDocument
                {

                    Id = Guid.NewGuid(),
                    FkP = idp,
                    Kind = source.FileName,
                    Date = DateTime.Now,

                };
                db.TblPortageDocuments.Add(x);


                x.Date = DateTime.Now;


                using (var target = new MemoryStream())
                {
                    source.CopyTo(target);
                    x.Image = target.ToArray();
                }
                x.Format = Extension;
                string[] imgExtensions = { ".jpg", ".jpeg" };
                if (imgExtensions.Contains(Extension))
                {
                    x.Image = x.Image.imgTosmall(web_lib.FilesHelper.imageSize.s800);

                }
                db.TblPortageDocuments.Add(x);

            }
            db.SaveChanges();
            return Content("OK");
        }


        [HttpGet("DocRemove/{id}")]
        public IActionResult DocRemove( Guid id)
        {

            try
            {
   db.TblPortageDocuments.Remove(db.TblPortageDocuments.Find(id));

             
             db.SaveChanges();
            }
            catch  
            {

                
            }
          
            return Content("OK");
        }








        [HttpGet("Doc/{id}")]
        public ActionResult<string> Doc(Guid id)
        {

            var row = db.TblPortageDocuments.Find(id);
            if (row == null)
            {
                return NotFound();
            }
            string[] imgExtensions = { "", ".jpg", ".jpeg" };
            if (imgExtensions.Contains(row.Format ?? ""))
            {
                return File(row.Image, "image/jpeg");
            }
            else {
             
                var contentType = "APPLICATION/octet-stream";
                 var fileName = row.Kind;
                 return File(row.Image,contentType, fileName);

            }
        }


        [HttpGet("searchcar/{id}")]
        public ActionResult<string> searchcar(string id)
        {

            var row = db.TblPortages.OrderByDescending(a => a.Date1).FirstOrDefault(a => a.CarShMashin.Replace(" ", "").Equals(id.Replace(" ", "")));
            if (row == null)
            {
                return null;
            }

            return new { row.FkCar, row.CarMashin, row.CarRanande, row.CarTell }.ToJson();
        }




       
     }
}
