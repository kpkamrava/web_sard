using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard.Areas.Acc.Controllers
{
    [Area("Acc")]
    [LoginAuth(_UserRol._Rolls.MainSuperVisor)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
