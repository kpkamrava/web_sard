using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_lib;
namespace web_sard.Controllers
{
    [LoginAuth ]
    public class ContractController : Controller
    {
        web_db.sardweb_Context db;
        public ContractController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        [LoginAuth(_UserRol._Rolls.Contract)]
        public IActionResult Index(Guid idtype)
        {

            ViewData["type"] = db.TblContractType.Find(idtype);
            var x = from n in db.TblContract
                    where n.FkContractType==idtype&& n.FkSalmali == User._getuserSalMaliDef()
                    orderby n.Date
                    select new Models.tbls.contract.contract(n, db,false,false,null);
            return View(x.ToList());
        }
        [LoginAuth(_UserRol._Rolls.Contract)]
        public IActionResult Create(Guid id, Guid idtype)
        {
            var sal = User._getuserSalMaliDef();
             var x = db.TblContract.Find(id);
            if (x != null)
            {
                idtype = (Guid)x.FkContractType;
                if (x.FkSalmali != sal)
                {
                    return RedirectToAction(nameof(Index), new { idtype = idtype });
                }
            }
            else
            {
                x = new web_db.TblContract();

                var s = db.TblSalMali.Find(sal);
                if (s != null)
                {
                    x.Azdate = s.SalAz.Date;
                    x.Tadate = s.SalTa.Date;
                }
                x.Code =   Convert.ToInt64((db.TblContract.Where(a => a.FkSalmali == sal).Max(a => (long?)a.Code) ?? 0) + 1);
            }
            ViewData["type"] = db.TblContractType.Find(idtype);
            x.FkContractType = idtype;

            Models.cl._ListCustomer = db.TblCustomer.Where(a=>a.FkSalmali==User._getuserSalMaliDef()).OrderBy(a=>a.Code).ToList();
            Models.cl._ListProduct = Models.cl._ListProduct.ToList();
            Models.cl._ListPacking = Models.cl._ListPacking.ToList(); 

            return View(new Models.tbls.contract.contract(x, db,true));
        }
        [LoginAuth(_UserRol._Rolls.Contract)]
        [HttpPost]
        public IActionResult Create(Guid id,Models.tbls.contract.contract model)
        {
            var sal = User._getuserSalMaliDef();
            var ContractType = db.TblContractType.Find(model.FkContractType);
             
            if (ContractType.IsProduct1Packing0)
            { 
               ModelState.Remove("CountMaxIn");
                 
               ModelState.Remove("CountMaxOut");
 
               ModelState.Remove("packingsId");
            }
            else
            { 
                    ModelState.Remove("WeightMaxIn");
                 
                    ModelState.Remove("WeightMaxOut");
 
                ModelState.Remove("prodoctsId"); 
            }


            if (ContractType.IsEntry == false)
            {
                ModelState.Remove("CountMaxIn");
                ModelState.Remove("WeightMaxIn");  
            }
            if (ContractType.IsExit == false)
            {
                ModelState.Remove("CountMaxOut");
                ModelState.Remove("WeightMaxOut");



            }
            else
            {

                if (ContractType.OutControlByPercent)
                {
                    ModelState.Remove("WeightMaxOut");

                }
                else
                {
                    ModelState.Remove("PercentForOut");
                    
                }
            }



            if (ModelState.IsValid)
            {
  
                var x = db.TblContract.Find(id);



                {
                    var x2 = db.TblContract.Where(a => a.Code == model.Code && a.FkSalmali == sal);
                    if (x2.Any()&&x2.Single().Id!=x.Id)
                    {
                        x.Code = Convert.ToInt64((db.TblContract.Where(a => a.FkSalmali == sal).Max(a => (long?)a.Code) ?? 0) + 1);

                    } 
                }
                 
                if (x == null)
                {
                    x = new web_db.TblContract {
                        Date = DateTime.Now.Date,
                      
                        Dateadd = DateTime.Now,
                        FkContractType = model.FkContractType,
                        FkSalmali = User._getuserSalMaliDef(),
                        FkUsAdd = User._getuserid().Value,
                        Id = Guid.NewGuid(),
                        
                        FkCustomer = model.FkCustomer, 
                    };
                    db.TblContract.Add(x); 
                }
                else
                {
                    x.FkUsEdit = User._getuserid();
                    x.Dateedit = DateTime.Now;
                }
                x.Code = model.Code;
                x.Azdate = model.Azdate.ToDate();
                x.Tadate = model.Tadate.ToDate();
                x.WeightMaxIn = model.WeightMaxIn;
                x.CountMaxIn = model.CountMaxIn;

                x.WeightMaxOut  = model.WeightMaxOut;
                x.CountMaxOut = model.CountMaxOut;
                x.PercentForOut = model.PercentForOut;

                x.Txt = model.Txt??"";

                x.PriceOfBoxIn = model.PriceOfBoxIn ;
                x.PriceOfBoxOut = model.PriceOfBoxOut  ;
                x.PriceOfKiloIn = model.PriceOfKiloIn  ;
                x.PriceOfKiloOut = model.PriceOfKiloOut ; 
                if (ContractType.IsProduct1Packing0)
                {
                    db.TblContractProduct.RemoveRange(db.TblContractProduct.Where(a=>a.FkContract==x.Id));
                    foreach (var item in model.prodoctsId)
                    {
                        db.TblContractProduct.Add(new web_db.TblContractProduct { FkContract = x.Id, FkProduct = item });
                    }
                }
                else
                {
                    db.TblContractPacking.RemoveRange(db.TblContractPacking.Where(a => a.FkContract == x.Id));
                    foreach (var item in model.packingsId)
                    {
                        db.TblContractPacking.Add(new web_db.TblContractPacking { FkContract = x.Id, FkPacking = item });
                    }

                } 
                
                db.SaveChanges();
                ViewBag.txt = "ثبت انجام شد";
                return RedirectToAction(nameof( Index),new { idtype=model.FkContractType });
            }


            ViewData["type"] = ContractType;
            ViewBag.error = "لطفا موارد را درست پر کنید";
            return View(model);
        }

        [LoginAuth(_UserRol._Rolls.Contract)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                db.TblContract.Remove(db.TblContract.Find(id));
                db.TblContractPacking.RemoveRange(db.TblContractPacking.Where(a => a.FkContract == id));
                db.TblContractProduct.RemoveRange(db.TblContractProduct.Where(a => a.FkContract == id));
                db.SaveChanges();
            }
            catch
            {


            }


            return Redirect(Request.UrlReferer());
        }




        public IActionResult ViewContractAjax(Guid id,Guid fkportageAdd)
        {
          
          var rows=new List< Models.tbls.contract.contract>();
          
            
            
            var x = db.TblContract.Find(id);
            var x2 = db.TblContract.Where(a => a.FkCustomer == id);
            if (x != null)
            {

                rows.Add(new Models.tbls.contract.contract(x, db, true, true));
            }
            else if (x2 != null)
            {

                foreach (var item in x2)
                {
                    rows.Add(new Models.tbls.contract.contract(item, db, true, true, fkportageAdd));
                }

            }
            else {
                var x3 = db.TblPortageRow.Where(a => a.FkPortage == id).GroupBy(a => a.FkContract).Select(a => a.Key).Distinct();
                foreach (var item in x3)
                {
                    rows.Add(new Models.tbls.contract.contract(db.TblContract.Find(item), db, true, true, fkportageAdd));  
                }


            }        
            
                    
            return View(rows);
        }

    }
}