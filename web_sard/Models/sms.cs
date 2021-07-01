namespace web_sard.Models
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using web_lib;

    /// <summary>
    /// Defines the <see cref="sms" />.
    /// </summary>
    public class sms
    {
       static bool sendnote = false;
        public class GracePeriodManagerService : BackgroundService
        {

            private readonly IConfiguration _settings;


            public GracePeriodManagerService(IConfiguration settings)
            {

                this._settings = settings;
            }


            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {

                await DoWork(stoppingToken);
            }


            private async Task DoWork(CancellationToken stoppingToken)
            {

                var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
                if (sms)
                {
 var _db = new web_db.sardweb_Context();
                _db.Database.SetConnectionString(_settings.GetConnectionString("kpDatabase"));

                try
                {
                    await sendsmsAllAsync(_db, _settings, stoppingToken);
                      
                    var d = DateTime.Now.TimeOfDay ;
                    if (sendnote==false &&d>=new TimeSpan(8,0,0)&&d<= new TimeSpan(8, 5, 0))
                    {
                        sendnote = true;
                        await sendsmsNoteAsync(_db, _settings, stoppingToken);
                    }
                    if (d >  new TimeSpan(8, 10, 0))
                    {
                        sendnote = false;
                    }





                }
                catch(Exception e)
                {


                }

                }
               

                await Task.Delay(10000, stoppingToken);
                await DoWork(stoppingToken);
            }


            public override async Task StopAsync(CancellationToken stoppingToken)
            {


                await Task.CompletedTask;
            }
        }

        public static async Task sendsmsAllAsync(web_db.sardweb_Context db, IConfiguration _mySettings, CancellationToken stoppingToken = default)
        {
            {
                var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
                var us = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
                var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
                var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;
                var site = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.US_UrlSite) ?? new web_db.TblConf()).Value;


                web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = us };

                var x = db.TblPortages.Where(a => a.IsEnd && a.IsDel == false && a.SmsOk == null).Include(a => a.FkCustomerNavigation).Include(a => a.FkContracttypeNavigation)
                .Include(a => a.TblPortageRows).ThenInclude(a => a.FkContractNavigation);
                await db.SaveChangesAsync(stoppingToken);
                foreach (var itemm in x)
                {
                    if (itemm.FkContracttypeNavigation.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                    {
                        var txt = new StringBuilder();
                        txt.AppendLine(_mySettings.GetValue<string>("txtPortagesms"));

                        txt.AppendLine($"شماره برگه : " + itemm.Code.ToString());

                        foreach (var item in itemm.TblPortageRows.GroupBy(a => a.FkContractNavigation.Code).Select(a => new { a.Key, count = a.Sum(s => s.Count) }))
                        {
                            txt.AppendLine($"کد مشتری :" + item.Key);
                            txt.AppendLine($"تعداد :" + item.count.gadrmotlagh());
                        }

                        if (itemm.FkContracttypeNavigation.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASardKhane)
                        {
                            txt.AppendLine($"خالص :" + itemm.WeightNet.gadrmotlagh().ToKilo());
                        }


                        txt.AppendLine($"زمان :" + itemm.Date2.ToPersianDateTime());
                        txt.AppendLine($"راننده :" + itemm.CarRanande);
                        txt.AppendLine("");
                        txt.AppendLine("سامانه مشتری : ");


                        txt.AppendLine(site);

                        try
                        {
                            var v = await s.sendsmsAsync(itemm.FkCustomerNavigation.Mob, txt.ToString(), stoppingToken);
                            itemm.SmsVaziat = v;
                            itemm.SmsOk = v.Contains("OK");
                        }
                        catch
                        {

                            itemm.SmsVaziat = "خطا";

                        }
                    }
                    else
                        if (itemm.FkContracttypeNavigation.KindCotractType == web_db.TblContractType.KindCotractTypeEnum.ASabad)

                    {
                        var txt = new StringBuilder();
                        txt.AppendLine(_mySettings.GetValue<string>("txtPortagesms"));

                        txt.AppendLine($"شماره برگه : " + itemm.Code.ToString());

                        foreach (var item in itemm.TblPortageRows.GroupBy(a => a.FkContractNavigation.Code).Select(a => new { a.Key, count = a.Sum(s => s.Count) }))
                        {
                            txt.AppendLine($"کد مشتری :" + item.Key);
                            txt.AppendLine($"تعداد :" + item.count.gadrmotlagh());
                        } 

                        txt.AppendLine($"زمان :" + itemm.Date2.ToPersianDateTime());
                        txt.AppendLine($"راننده :" + itemm.CarRanande);
                        txt.AppendLine("");
                        txt.AppendLine("سامانه مشتری : ");


                        txt.AppendLine(site);

                        try
                        {
                            var v = await s.sendsmsAsync(itemm.FkCustomerNavigation.Mob, txt.ToString(), stoppingToken);
                            itemm.SmsVaziat = v;
                            itemm.SmsOk = v.Contains("OK");
                        }
                        catch
                        {

                            itemm.SmsVaziat = "خطا";

                        }
                    }


                }
                await db.SaveChangesAsync(stoppingToken);

                foreach (var item in db.TblContracts.Include(a => a.FkCustomerNavigation).Where(a => a.SendSms == true))
                {

                    var txt = new StringBuilder();
                    txt.AppendLine(_mySettings.GetValue<string>("txtPortagesms"));

                    txt.AppendLine($"قرارداد شماره : " + item.Code.ToString());
                    txt.AppendLine($"در مورخه : " + (item.Dateedit.HasValue ? item.Dateedit.Value : item.Dateadd).ToPersianDateTime());
                    if (item.Dateedit.HasValue)
                    {
                        txt.AppendLine($"اصلاح گردید");

                    }
                    else
                    {
                        txt.AppendLine($"بنام شما ثبت گردید");

                    }
                    try
                    {
                        var v = await s.sendsmsAsync(item.FkCustomerNavigation.Mob, txt.ToString(), stoppingToken);
                        item.SendSms = false;
                    }
                    catch
                    {



                    }

                }
                await db.SaveChangesAsync(stoppingToken);


                foreach (var item in cl._listSmsForSend)
                {
                    var txt = new StringBuilder();
                    txt.AppendLine(_mySettings.GetValue<string>("txtPortagesms"));

                    txt.AppendLine(item.txt); 
                    var v = await s.sendsmsAsync(item.number, txt.ToString(), stoppingToken);
                    cl._listSmsForSend.Remove(item);

                }


            };
        }

        public static async Task sendsmsNoteAsync(web_db.sardweb_Context db, IConfiguration _mySettings, CancellationToken stoppingToken = default)
        {
            {
                var sms = Models.cl._conf.Any(a => a.Key == web_db.TblConf.KeyEnum.Main_ActiceSms && a.Value == true.ToString());
                var user = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_UserSms) ?? new web_db.TblConf()).Value;
                var pass = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_PassSms) ?? new web_db.TblConf()).Value;
                var num = (Models.cl._conf.SingleOrDefault(a => a.Key == web_db.TblConf.KeyEnum.Main_NumSms) ?? new web_db.TblConf()).Value;


                web_lib.sms s = new web_lib.sms { numfrom = num, password = pass, username = user };


                var x =  db.TblNoteRows.Where(a =>a.SendSms && a.Date==DateTime.Now.Date ).Include(a => a.TblNote).AsEnumerable().GroupBy(a=>a.TblNote) ;
                var us =  db.TblUsers.Where(a => a.IsActive && a.IsDel == false ).AsEnumerable();
                foreach (var mes in x)
                {
                     foreach (var item in us.Where(a => mes.Any(s => s.ForUserId.GetValueOrDefault() == a.Id ||  (string.IsNullOrWhiteSpace(s.ForUserroll)==false? a.Roles.Contains(s.ForUserroll??""):false))))
                    {


                       var txt = new StringBuilder();
                        txt.AppendLine(_mySettings.GetValue<string>("txtPortagesms"));
                        txt.AppendLine($"یاداوری" );

                        txt.AppendLine($"در مورخه : " + (DateTime.Now.Date).ToPersianDate());
                      
                        txt.AppendLine(mes.Key.Caption); 
                       
                        try
                        {
                            var v = await s.sendsmsAsync(item.Mob, txt.ToString(), stoppingToken);
                          
                        }
                        catch
                        {



                        }




                    }
              



                }
                  
             

              


            };
        }





    }
}
