using System; 
using System.Linq; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using web_lib;

namespace web_sard.Controllers
{
   [LoginAuth(_UserRol._Rolls.Bascul)]
    public class PortageController : Controller
    {
        web_db.sardweb_Context db;
        IWebHostEnvironment env;
        public PortageController(web_db.sardweb_Context db, IWebHostEnvironment env )
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index(Guid idtype, int? kindPortage = null, Guid? printid = null)
        {

            var x = from n in db.TblPortage

                    where n.FkSalmali == User._getuserSalMaliDef() && n.FkContracttype == idtype &&
                    (
                    kindPortage > 0 ? ((kindPortage == n.KindCode) && n.IsEnd && n.IsDel == false) :
                     (
                       kindPortage == -1 ? n.IsDel == false && n.IsEnd :
                       kindPortage == -2 ? n.IsDel :
                       (n.IsEnd == false && n.IsDel == false)
                     )
                    )
                    orderby n.Code descending
                    select new Models.tbls.portage.portage(n, db, false, false,false);
            ViewData["type"] = db.TblContractType.Find(idtype);
            ViewData["kindPortage"] = kindPortage;
            if (printid != null)
            {
                ViewData["printid"] = printid.Value;

            }
            return View(x.ToList());
        }

        public IActionResult CreateIN(Guid id, Guid idtype,int kindPortage )
        {
             
            var tim = DateTime.Now;
            Models.tbls.portage.portage model = new Models.tbls.portage.portage
            {
                Date1 = tim,
                Date1date = tim.ToPersianDate(),
                Date1time = new TimeSpan(tim.Hour, tim.Minute, 0),
            
            };
             
            var row = db.TblPortage.Find(id);
            if (row!=null)
            {
                if (row.IsEnd)
                {
                    return Redirect( Request.UrlReferer());
                }
                model = new Models.tbls.portage.portage(row,db);
                idtype =model._Contract.FkContractType;
                kindPortage =model.KindCode;
            }

            if (model.Code==0)
            {
                model.Code = (db.TblPortage.Where(a=> a.FkSalmali == User._getuserSalMaliDef() && a.FkContracttype==idtype&&a.KindCode==kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;
            }
            
            ViewData["type"] = db.TblContractType.Find(idtype);
            ViewData["kindPortage"] = kindPortage; 
            ViewData["listcontract"] = db.TblContract.Where(a=>a.FkSalmali==User._getuserSalMaliDef()&&a.FkContractType==idtype).Select(a=>new   Models.tbls.contract.contract(a,db,false,false)).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateIN(Guid idtype, int kindPortage, Models.tbls.portage.portage model)
        {
            ModelState.Remove("Weight2");
            ModelState.Remove("Date2date");
            ModelState.Remove("Date2time");

            var typecontract = db.TblContractType.Find(idtype);
            if (typecontract.IsProduct1Packing0==false)
            {
                ModelState.Remove("Weight1");
            }
            if (ModelState.IsValid)
            {
                var x = db.TblPortage.Find(model.Id);
                if (x == null)
                { 
                  
                   model.Code = (db.TblPortage.Where(a => a.FkSalmali == User._getuserSalMaliDef()&& a.FkContracttype == idtype && a.KindCode == kindPortage).Max(a => (long?)a.Code) ?? 0) + 1;
                     


                    x = new web_db.TblPortage
                    {
                        FkSalmali = User._getuserSalMaliDef(),
                        Code = model.Code,
                        Dateadd1 = DateTime.Now,
                        FkContracttype = idtype,
                        FkUsAdd1 = User._getuserid().Value,
                        Id = Guid.NewGuid(),
                        KindCode = kindPortage,
                        KindTitle = Models.tbls.portage.kindPortage.listkindcontract.Find(a => a.code == kindPortage).txt,
                         
                    };
                    db.TblPortage.Add(x);
                }
                else
                {
                    x.Dateedit1 = DateTime.Now;
                    x.FkUsEdit1 = User._getuserid().Value;

                }
                x.CarMashin = model.CarMashin;
                x.CarRanande = model.CarRanande;
                x.CarShMashin = model.CarShMashin;
                x.CarTell = model.CarTell;
                x.FkContract = model.FkContract;
                x.Fkcustomer = db.TblContract.Find(model.FkContract).FkCustomer;
                x.Weight1 = model.Weight1;
                x.Weight1IsBascul = model.Weight1.HasValue;
                x.Txt = model.Txt;
                x.Date1 = model.Date1date.ToDate().AddHours(model.Date1time.Hours).AddMinutes(model.Date1time.Minutes);
                x.IsDel = false;
                x.IsPermitOk = null;
                x.FkUsPermit = null;
                x.TxtPermit = "";
                db.SaveChanges();
                return RedirectToActionPermanent("index",new { idtype=x.FkContracttype });
            }
       

            ViewData["type"] = db.TblContractType.Find(idtype);
            ViewData["kindPortage"] = kindPortage;
            ViewData["listcontract"] = db.TblContract.Where(a => a.FkSalmali == User._getuserSalMaliDef() && a.FkContractType == idtype).Select(a => new Models.tbls.contract.contract(a, db, false,false)).ToList();
            return View(model);
        }


        public IActionResult CreateOut(Guid id )
        { 

            var x = db.TblPortage.Find(id);
            if (x.Date2.HasValue == false)
            {
                x.Date2 = DateTime.Now;
            }
           
            var model= new Models.tbls.portage.portage(x,db,true,false,true);
        
             return View(model);
        }

        [HttpPost]
        public IActionResult CreateOut(Models.tbls.portage.portage model,string print)
        {   var x = db.TblPortage.Find(model.Id);

            model._Contract = new Models.tbls.contract.contract(db.TblContract.Find(x.FkContract), db);
            ModelState.Remove("Date1date");
            ModelState.Remove("Date1time");
            if (model._Contract.ContractTypeTbl.IsProduct1Packing0 == false)
            {
                ModelState.Remove("Weight1");
                ModelState.Remove("Weight2");
            }
            if (ModelState.IsValid)
            {
                x.CarMashin = model.CarMashin;
                x.CarRanande = model.CarRanande;
                x.CarShMashin = model.CarShMashin;
                x.CarTell = model.CarTell;
                x.Weight2 = model.Weight2;
                x.WeightNet = x.Weight2 - x.Weight1;
                x.WeightNet =  x.WeightNet<0?(-x.WeightNet): x.WeightNet;
                x.Weight2IsBascul = model.Weight2.HasValue;
                x.Txt = model.Txt;
                x.Date2 = model.Date2date.ToDate().AddHours(model.Date2time.Value.Hours).AddMinutes(model.Date2time.Value.Minutes);
                 x.IsDel = false;
                if (x.Dateadd2.HasValue == false)
                {
                    x.Dateadd2 = DateTime.Now;
                    x.FkUsAdd2 = User._getuserid();
                }
                else {
                    x.Dateedit2 = DateTime.Now;
                    x.FkUsEdit2 = User._getuserid();
                }

                var rows = db.TblPortageRow.Where(a => a.FkPortage == x.Id);
                x.PackingCount=rows.Sum(a=>a.Count);
                if (x.PackingCount<1)
                {
                    x.PackingCount = 1;
                }
            
                
                x.PackingOfWeight=x.WeightNet/x.PackingCount;
              
                x.IsPermitOk = null;
                x.FkUsPermit = null;
                x.TxtPermit = "";
                 x.IsEnd = false;
            
                db.SaveChanges();
                
                x.IsPermitOk = Models.tbls.contract.contract.IsPermitOK(x,db);

               if (x.IsPermitOk==true)
                {
                    x.IsEnd = true;
                    x.TxtPermit = "";
                    x.FkUsPermit = null;

                }
                else
                {
                    x.IsEnd = false; 
                }


                db.SaveChanges();

                if (x.IsPermitOk == false)
                {
                    return RedirectToAction("CreateOut",  x.Id  );

                }
                 


                if (string.IsNullOrEmpty(print))
                {
                    return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage=x.KindCode });

                }
                return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode ,printid= x.Id });  

            }
            ViewData["type"] = db.TblContractType.Find(x.FkContracttype);
            ViewData["kindPortage"] = x.KindCode;
             return View(model);
        }

        [HttpPost]
        public IActionResult Permit(Guid id, string txt)
        {

            var x = db.TblPortage.Find(id);
            x.TxtPermit = txt;
            x.FkUsPermit = User._getuserid();
            x.IsEnd = true;
            x.IsDel = false;
            if (User._getRolAny(_UserRol._Rolls.PermitPortage))
            { 
                db.SaveChanges();
            } 
            return RedirectToAction("index", new { idtype = x.FkContracttype, kindPortage = x.KindCode });
             

        }

        public IActionResult Delete(Guid id)
        {

            var x = db.TblPortage.Find(id);

            if (x.IsEnd)
            {
                x.IsEnd = false;
            }
            else 
            {
                x.IsDel = true;

            }
            db.SaveChanges();

            return Redirect(Request.UrlReferer());
        }



        public IActionResult Print(  Guid id, string kind)
        {
            var t1 = db.TblPortage.Find(id);

        

            var actiontype = ((Models.tbls.portage.kindPortage.kindPortageEnum)t1.KindCode).ToString();

            var t = Models.printclass.getlistfile("Portage", actiontype, env);
            if (kind.IsEmpty())
            {
                  if (t.Any()==false)
                {
                    return Content("فرمت چاپ وجود ندارد");

                }
                kind = t.FirstOrDefault().Value;
                return RedirectToAction("Print", new Models.printclass {   id = id, kind = kind, actiontype = actiontype });
            }
            return View(new Models.printclass { files = t, id = id,   actiontype = actiontype, kind = kind });


        }

        public IActionResult GetReport( string actiontype, Guid id, string kind)
        {
            string contolltype = "Portage";
            var x = new Models.tbls.portage.portage(db.TblPortage.Find(id), db, true,true);
            StiReport report = new StiReport();
            var z = env.WebRootPath + $"/Reports/{contolltype}/rpt_{actiontype}_{kind}.mrt";
            report.Load(StiNetCoreHelper.MapPath(this, z));
            report.Dictionary.Databases.Clear();

           
                report.RegData("data.root", x);
           
            return StiNetCoreViewer.GetReportResult(this, report);
        }
        public IActionResult ViewerEvent() 
        {

            return StiNetCoreViewer.ViewerEventResult(this);

        }






    }
}