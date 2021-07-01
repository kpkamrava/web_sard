namespace web_sard.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;
    using web_lib;
    using web_sard.Models;

    /// <summary>
    /// Defines the <see cref="CustomerController" />.
    /// </summary>
    [LoginAuth(_UserRol._Rolls.AddEditCustomer)]
    public class CustomerController : Controller
    {

        internal web_db.sardweb_Context db;

        public CustomerController(web_db.sardweb_Context db)
        {
            this.db = db;
        }


        public IActionResult Index(string txt, string error)
        {


            var x = from n in db.TblCustomers
                    orderby n.Code
                    where n.FkSalmali == User._getuserSalMaliDef()
                    select new Models.tbls.customer.customer(n, db, true);
            ViewBag.txt = txt;
            ViewBag.error = error;
            return View(x.ToList());
        }


        public IActionResult Create(Guid id)
        {
            //var Mali_KindOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_KindOT) ?? new web_db.TblConf()).Value;

            //if (!Mali_KindOT.IsEmpty())
            //{
            //    return RedirectToAction("index");

            //}

            var z = db.TblCustomers.Find(id)??new web_db.TblCustomer();
           
            var x = new Models.tbls.customer.customer(z, db);
            if (x.Id == Guid.Empty)
            {
                x.Code = (db.TblCustomers.Where(a => a.FkSalmali == User._getuserSalMaliDef()).Max(a => (int?)a.Code) ?? 0) + 1;
            }

            return View(x);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Models.tbls.customer.customer model)
        {
            //var Mali_KindOT = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Mali_KindOT) ?? new web_db.TblConf()).Value;

            //if (!Mali_KindOT.IsEmpty())
            //{
            //    return RedirectToAction("index");

            //}
            if (ModelState.IsValid)
            {
                var x = db.TblCustomers.Find(model.Id);
                if (x == null)
                {
                    x = new web_db.TblCustomer
                    {
                        Id = Guid.NewGuid(),
                        FkSalmali = User._getuserSalMaliDef(),
                        Code = (db.TblCustomers.Where(a => a.FkSalmali == User._getuserSalMaliDef()).Max(a => (int?)a.Code) ?? 0) + 1

                    };
                    db.TblCustomers.Add(x);
                }
                x.Addras = model.Addras??"";
                x.IdOtherSystem = model.IdOtherSystem ?? "";
                x.IsEnable = model.IsEnable;
                x.Mob = model.Mob ?? "";
                x.NationalCode = model.NationalCode ?? "";
                x.Title = model.Title;
                db.SaveChanges();
                return RedirectToAction("Index", new { txt = "ثبت موفق" });

            }
            return View(model);
        }


        public IActionResult delete(Guid id)
        {
            var x = db.TblCustomers.Find(id);
            x.IsEnable = false;
            db.SaveChanges();
            try
            {
                db.TblCustomers.Remove(x);
                db.SaveChanges();
            }
            catch
            {

            }

            return RedirectToAction("Index", new { txt = "انجام شد" });
        }

        [LoginAuth(_UserRol._Rolls.MainSuperVisor)]
        public IActionResult deleteFull(Guid id)
        {


            using (var transaction = new TransactionScope())
            {
                var x = db.TblCustomers.Find(id);

                foreach (var item in db.TblPortages.Where(a => a.FkCustomer == id))
                {
                    cl.RemoveFullPortage(item.Id, db);
                }
                foreach (var item in db.TblContracts.Where(a => a.FkCustomer == id))
                {
                    cl.RemoveFullContract(item.Id, db);

                }
                db.TblCustomers.Remove(x);
                db.SaveChanges();
                transaction.Complete();

            }



            return RedirectToAction("Index", new { txt = "انجام شد" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerPaivastAsync(string CellPhone)
        {
            if ((await cl.AddPayvastCustomerAsync(db, CellPhone, User._getuserSalMaliDef())).HasValue)
            {
                return RedirectToAction("Index", new { txt = "ثبت شد" });

            }
            else
            {
                return RedirectToAction("Index", new { error = "یافت نشد" });
            };


        }

        public IActionResult setgroup(Guid id)
        {
            var x = new Models.tbls.customer.customer(db.TblCustomers.Find(id), db);
            return View(x);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult setgroup(Guid id, Guid[] FkGroups)
        {


            var row = db.TblCustomers.Find(id);
            if (row == null)
            {
                return RedirectToAction("Index", new { error = "ثبت نشد" });

            }

            var x = db.TblCustomerGroups.Where(a => a.FkCustumer == id);

            var listfordel = x.Select(a => a.FkGroup).ToList();
            foreach (var item in FkGroups)
            {
                if (listfordel.Remove(item) == false)
                {
                    db.TblCustomerGroups.Add(new web_db.TblCustomerGroup { FkGroup = item, FkCustumer = id });



                }
            }
            foreach (var item in listfordel)
            {
                db.TblCustomerGroups.Remove(x.Single(a => a.FkGroup == item));

            }
            db.SaveChanges();

            return RedirectToAction("Index", new { txt = "ثبت شد" });
        }
    }
}
