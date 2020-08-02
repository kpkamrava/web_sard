using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web_sard.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AjaxController : ControllerBase
    {
        web_db.sardweb_Context db;
        public AjaxController(web_db.sardweb_Context db)
        { this.db = db; }
        [HttpGet("GetLastBascul")]
        public JsonResult  GetLastBascul()
        {

            return new JsonResult( Newtonsoft.Json.JsonConvert.SerializeObject(new { WeightBacul= Models.cl.bascul.WeightBacul, WeightBaculIsAllow= Models.cl.bascul.WeightBaculIsAllow }));
        }

      

        [HttpGet("SetBasculWeight/{id}")]
        public ActionResult<string> SetBasculWeight(decimal id)
        {
            new Models.cl.bascul(id);


            return Ok();
        }


        [HttpGet("Doc/{id}")]
        public ActionResult<string> Doc(Guid id)
        {

            var row = db.TblPortageDocument.Find(id);
            if (row==null)
            {
                return NotFound();
            }
            return File(row.Image, "image/jpeg");


        }


    }
}