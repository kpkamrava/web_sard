using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Areas.Acc.Controllers
{
    public class HomeController : Controller
    {
        [Area("Acc")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
