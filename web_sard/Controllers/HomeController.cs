namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using web_sard.Models;

    /// <summary>
    /// Defines the <see cref="HomeController" />.
    /// </summary>
    [LoginAuth]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;


        private readonly web_db.sardweb_Context db;

        public HomeController(ILogger<HomeController> logger, web_db.sardweb_Context db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {

            var row = new Models.tbls.home.mainclass();

            var ussal = User._getuserSalMaliDef();


            row.countports = (from n in db.TblPortageRows.Include(a => a.FkPortageNavigation)
                              let pack = db.TblPackings.SingleOrDefault(a => a.Id == n.FkPacking)
                              let prod = db.TblProducts.SingleOrDefault(a => a.Id == n.FkProduct)
                              where n.FkPortage.HasValue && n.FkPortageNavigation.FkSalmali == ussal &&
                              (pack == null ? true : (pack.IsNotAc != true)) &&
                              (prod == null ? true : (prod.IsNotAc != true))

                              let port = n.FkPortageNavigation
                              where port.IsEnd && (port.IsDel == false)

                              group n by new { n.FkPortageNavigation.Date2.Value.Date, port.KindCode, port.KindTitle, n.FkLocation1, n.FkPortage } into m

                              select new Models.tbls.home.countport
                              {
                                  date = m.Key.Date,
                                  location = m.Key.FkLocation1.GetValueOrDefault(),
                                  KindTitle = m.Key.KindTitle,
                                  Kindcode = m.Key.KindCode,
                                  WeightNet = (decimal)((m.Sum(a => a.Count * a.WeightOne) ?? 0)),

                                  PackingCount = m.Sum(a => a.Count)
                              }).ToList();

            ViewBag.row2 = row;

            //ViewBag.fromyear = cl._ListSalmali.Single(a => a.Id == User._getuserSalMaliDef());
            return View(row);
        }




        public IActionResult IndexMain()
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
