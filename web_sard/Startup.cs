namespace web_sard
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;
    using System.Linq;
    using System.Threading;
    using web_lib;

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.ConfigureApplicationCookie(options => { options.LoginPath = "Account/login"; });

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddAuthentication().AddCookie();
            services.AddControllersWithViews();


            services.AddDbContext<web_db.sardweb_Context>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("kpDatabase"));
            });


            services.AddSession(options =>
            {
                options.Cookie.Name = "Sh.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.IsEssential = true;
            });
            // Resolve the services from the service provider

            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHl2AD0gPVknKsaW0un+3PuM6TTcPMUAWEURKXNso0e5OFPaZYasFtsxNoDemsFOXbvf7SIcnyAkFX/4u37NTfx7g+0IqLXw6QIPolr1PvCSZz8Z5wjBNakeCVozGGOiuCOQDy60XNqfbgrOjxgQ5y/u54K4g7R/xuWmpdx5OMAbUbcy3WbhPCbJJYTI5Hg8C/gsbHSnC2EeOCuyA9ImrNyjsUHkLEh9y4WoRw7lRIc1x+dli8jSJxt9C+NYVUIqK7MEeCmmVyFEGN8mNnqZp4vTe98kxAr4dWSmhcQahHGuFBhKQLlVOdlJ/OT+WPX1zS2UmnkTrxun+FWpCC5bLDlwhlslxtyaN9pV3sRLO6KXM88ZkefRrH21DdR+4j79HA7VLTAsebI79t9nMgmXJ5hB1JKcJMUAgWpxT7C7JUGcWCPIG10NuCd9XQ7H4ykQ4Ve6J2LuNo9SbvP6jPwdfQJB6fJBnKg4mtNuLMlQ4pnXDc+wJmqgw25NfHpFmrZYACZOtLEJoPtMWxxwDzZEYYfT";

        b1:
            try
            {  
                using (var db = services.BuildServiceProvider().GetService<web_db.sardweb_Context>())
                {
                    db.Database.Migrate(); 

                    Models.cl._listSmsForSend = new System.Collections.Generic.List<Models.cl.SmsForSend>();
                 
                    Models.cl._conf = db.TblConf.OrderBy(a=>a.Key).ToList();
                    Models.cl._ListSalmali = db.TblSalMalis.OrderBy(a => a.Id).ToList();
                    Models.cl.__ListContractType = db.TblContractTypes.OrderBy(a => a.Code).ToList();
                    Models.cl._ListInjury = db.TblInjuries.OrderBy(a => a.Ord).ToList();
                    Models.cl._ListPacking = db.TblPackings.OrderBy(a => a.Code).ToList();
                    Models.cl._ListProduct = db.TblProducts.OrderBy(a => a.Code).ToList();
                    Models.cl._ListLocation = db.TblLocations.OrderBy(a => a.Code).ToList();
                    Models.cl._ListCar = db.TblCars.OrderBy(a => a.Title).ToList();
                    Models.cl._ListGroup = db.TblGroups.OrderBy(a => a.Title).ToList();
                    Models.cl._ListRoll = _UserRol._Rolls.AddEditCustomer.GetAllItems();


                }
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);
                goto b1;

            }


            services.AddHostedService<Models.sms.GracePeriodManagerService>(ab => new Models.sms.GracePeriodManagerService(Configuration));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "Areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
