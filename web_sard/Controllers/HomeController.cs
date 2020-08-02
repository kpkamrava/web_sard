using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using web_lib;
using web_sard.Models;

namespace web_sard.Controllers
{
    [LoginAuth]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly web_db.sardweb_Context db;
        public HomeController(ILogger<HomeController> logger,web_db.sardweb_Context db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
