using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_lib;
namespace web_sard.Areas.Note.Controllers
{
    [Area("Note")]
    [LoginAuth(_UserRol._Rolls.Note)]
    public class HomeController : Controller
    {
        web_db.sardweb_Context db;
        public HomeController(web_db.sardweb_Context db)
        {
            this.db = db;
        }
        public IActionResult Index(string date = "")
        {
            var d = date.ToDateNull() ?? DateTime.Now.Date;
            ViewBag.date = d;
            var rols = User._getRolsSTR();
            var x = from n in db.TblNoteRows.Include(a => a.TblNote)
                    where
                    n.Date >= d.AddDays(-1) &&
                    n.Date <= d.AddDays(7)
                    &&
                     (n.ForUserId == User._getuserid() ||

                    (rols.Contains(n.ForUserroll))/*|| rols.Contains(_UserRol._Rolls.NoteAdmin.ToString())*/)
                    select n;

            if (User._getRolAny(_UserRol._Rolls.NoteAdmin))
            {
                var xx = from n in db.TblNoteRows.Include(a => a.TblNote)
                         where
                         n.Date >= d.AddDays(-1) &&
                         n.Date <= d.AddDays(7)

                         select n;

                ViewBag.xx = xx.AsEnumerable();
            }


            return View(x.AsEnumerable());
        }
        public IActionResult List(bool All=false)
        {
            ViewBag.All = All;
            var d1 = Models.cl._ListSalmali.Single(a => a.Id == User._getuserSalMaliDef());
              
            if (User._getRolAny(_UserRol._Rolls.NoteAdmin)&&All)
            {
                var xx = from n in db.TblNoteRows.Include(a => a.TblNote)
                         where
                         n.Date >= d1.SalAz &&
                         n.Date <= d1.SalTa
                         orderby n.Date
                         select n;  
              
                return View(xx.AsEnumerable());


            } 

         
            var rols = User._getRolsSTR();

            var x = from n in db.TblNoteRows.Include(a => a.TblNote)
                    where
                    n.Date >= d1.SalAz &&
                    n.Date <= d1.SalTa
                    &&
                     (n.ForUserId == User._getuserid() ||

                    (rols.Contains(n.ForUserroll))/*|| rols.Contains(_UserRol._Rolls.NoteAdmin.ToString())*/)
                    orderby n.Date
                    select n;
            return View(x.AsEnumerable());

        }

        public IActionResult Add(Guid? id, string date = "")
        {
            var row = db.TblNotes.Include(a=>a.TblNoteDates).Include(a=>a.TblNoteRolls).SingleOrDefault(a=>a.Id==id);
            if (row==null)
            {
                row = new web_db._note.TblNote() { 
                FkuserAdd=User._getuserid().Value,
                DateAdd=DateTime.Now,
                };
                row.TblNoteDates.Add(new web_db._note.TblNoteDate {
                FromDate=date.ToDate(),
                ToDate=date.ToDate()
                });
            }

            if (row.TblNoteDates.Count<2)
            {
                row.TblNoteDates.Add(new web_db._note.TblNoteDate
                {
                    
                });
            }

            ViewBag.users = db.TblUsers.Where(a => a.IsActive).ToList();
           
            return View(row);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(web_db._note.TblNote model,_UserRol._Rolls[] roll,Guid[] us,string delete,string date="")
        {
            var row = db.TblNotes.Find(model.Id);
            if (delete.IsEmpty()==false)
            {

                if (row!=null)
                {
                    if (User._getRolAny(_UserRol._Rolls.TempAdmin))
                    {
                        db.TblNoteRows.RemoveRange(db.TblNoteRows.Where(a => a.FkTblNote == row.Id));
                        db.TblNoteDates.RemoveRange(db.TblNoteDates.Where(a => a.FkTblNote == row.Id));
                        db.TblNoteRolls.RemoveRange(db.TblNoteRolls.Where(a => a.FkTblNote == row.Id));
                        db.TblNotes.Remove(row);
                        db.SaveChanges();
                    }

                } 
                return Redirect(Request.UrlReferer());

            }
            if (row == null)
            {
                row = new web_db._note.TblNote
                {
                    Id=Guid.NewGuid(),
                    DateAdd=DateTime.Now,
                    FkuserAdd=User._getuserid().Value, 
                };
                db.TblNotes.Add(row);
                 
            }

            if ((row.FkuserAdd != User._getuserid()) &&  User._getRolAny(_UserRol._Rolls.TempAdmin)==false )
            {
                return Redirect(Request.UrlReferer());

            }

            row.Txt = model.Txt.Trim();
            row.Caption = model.Caption.Trim();

            db.TblNoteRows.RemoveRange(db.TblNoteRows.Where(a=>a.FkTblNote==row.Id));
            db.TblNoteDates.RemoveRange(db.TblNoteDates.Where(a => a.FkTblNote == row.Id));
            db.TblNoteRolls.RemoveRange(db.TblNoteRolls.Where(a => a.FkTblNote == row.Id));
          

            List<web_db._note.TblNoteRoll> rols = new List<web_db._note.TblNoteRoll>();
            if (User._getRolAny(_UserRol._Rolls.NoteAdmin))
            { 
                foreach (var item in roll)
                {
                    var z = new web_db._note.TblNoteRoll
                    {
                        ForUserroll = item.ToString(),
                        Id = Guid.NewGuid(),
                        FkTblNote = row.Id
                    };
                    db.TblNoteRolls.Add(z);
                    rols.Add(z);

                }
                foreach (var item in us)
                {
                    var z = new web_db._note.TblNoteRoll
                    {
                        ForUserId = item,
                        Id = Guid.NewGuid(),
                        FkTblNote = row.Id
                    };
                    db.TblNoteRolls.Add(z);
                    rols.Add(z);
                } 

            }
            if (User._getRolAny(_UserRol._Rolls.NoteAdmin)==false||((roll.Any()==false)&&(us.Any()==false)))
            {
               var z= new web_db._note.TblNoteRoll
                {
                    ForUserId = User._getuserid(),
                    Id = Guid.NewGuid(),
                   FkTblNote = row.Id
               };
                db.TblNoteRolls.Add(z); rols.Add(z);
            }

           
            foreach (var itemd in model.TblNoteDates)
            {
                if (itemd._ToDate.IsEmpty()||itemd._FromDate.IsEmpty())
                {
                    break;
                }
                itemd.FromDate= itemd._FromDate.ToDate();
                itemd.ToDate= itemd._ToDate.ToDate();
                db.TblNoteDates.Add(new web_db._note.TblNoteDate
                {
                    Id = Guid.NewGuid(),
                    FromDate = itemd.FromDate,
                    SendSms = itemd.SendSms,
                    KindBesorat = itemd.KindBesorat,
                    ToDate = itemd.ToDate,
                    FkTblNote = row.Id

                });
                 


                List<DateTime> dates = new List<DateTime>();
                var d = itemd.FromDate.Value;
                while (itemd.ToDate>= d)
                {
                    dates.Add(d);

                    switch (itemd.KindBesorat)
                    {
                        case web_db._note.TblNoteDate.Kind.Harroz:
                            d = d.AddDays(1);
                            break;
                        case web_db._note.TblNoteDate.Kind.HarHafte:
                            d = d.AddDays(7);
                            break;
                        case web_db._note.TblNoteDate.Kind.Har2Hafte:
                            d = d.AddDays(7);
                            break;
                        case web_db._note.TblNoteDate.Kind.Har4Week:
                            d = d.AddDays(7 * 4);
                            break;
                        case web_db._note.TblNoteDate.Kind.HarMah:
                            d = d.ToPersianAddMonths(1);
                            break;
                        default:
                            break;
                    }

                } 

                foreach (var item in dates)
                { 
                    foreach (var itemr in rols)
                    {
                        db.TblNoteRows.Add(new web_db._note.TblNoteRows
                        {
                            Date=item,
                            ForUserId=itemr.ForUserId,
                            ForUserroll=itemr.ForUserroll,
                            Id=Guid.NewGuid(), 
                            FkTblNote=row.Id,
                            SendSms= itemd.SendSms,
                        });
                    }
                }   
            }
            db.SaveChanges();


            return Redirect(Request.UrlReferer());
        }


        public IActionResult CountNote()
        {

            var d = DateTime.Now.Date;
            var rols = User._getRolsSTR();
            var x = (from n in db.TblNoteRows.Include(a => a.TblNote)
                    where
                    n.Date == d
                    
                    &&
                     (n.ForUserId == User._getuserid() ||

                    (rols.Contains(n.ForUserroll)) )
                    select n).Select(a=>a.FkTblNote).Distinct();

           

            return Content(x.Count().ToString());
        }
        public IActionResult Notes()
        {

            var d = DateTime.Now.Date;
            var rols = User._getRolsSTR();
            var x = (from n in db.TblNotes.Include(a => a.TblNoteRows).Include(a=>a.UserAdd)
                    where
                    n.TblNoteRows.Any(a=>
                     a.Date == d
                    &&
                     (a.ForUserId == User._getuserid() || 
                     rols.Contains(a.ForUserroll) ) 
                    )
                   
                    select n);



            return View(x.AsEnumerable());
        }

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             