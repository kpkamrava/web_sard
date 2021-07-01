using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace web_sard_Customer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        //    services.ConfigureApplicationCookie(options => { options.LoginPath = "Account/index"; });

            services.AddAuthentication(sharedOptions =>
            {
                
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });
            services.AddAuthentication().AddCookie(a=> {
                a.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = (context) =>
                    {
                        context.HttpContext.Response.Redirect("Account/index");
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddControllersWithViews();


            services.AddDbContext<web_db.sardweb_Context>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("kpDatabase")));


            services.AddSession(options =>
            {
                options.Cookie.Name = "Sh.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.IsEssential = true;
            });




            using (var db = services.BuildServiceProvider().GetService<web_db.sardweb_Context>())
            {
                db.Database.Migrate();

            
                Models.cl._conf = db.TblConf.OrderBy(a=>a.Key).ToList();
                //   Models.cl._listSmsForSend = new System.Collections.Generic.List<Models.cl.SmsForSend>();
                //Models.cl._config = db.TblConfigs.First(); Models.cl._ListSalmali = db.TblSalMalis.OrderBy(a => a.Id).ToList();
                //Models.cl.__ListContractType = db.TblContractTypes.OrderBy(a => a.Code).ToList();
                //Models.cl._ListInjury = db.TblInjuries.OrderBy(a => a.Ord).ToList();
                //Models.cl._ListPacking = db.TblPackings.OrderBy(a => a.Code).ToList();
                //Models.cl._ListProduct = db.TblProducts.OrderBy(a => a.Code).ToList();
                //Models.cl._ListLocation = db.TblLocations.OrderBy(a => a.Code).ToList();
                //Models.cl._ListCar = db.TblCars.OrderBy(a => a.Title).ToList();
                //Models.cl._ListGroup = db.TblGroups.OrderBy(a => a.Title).ToList();
                //Models.cl._ListRoll = _UserRol._Rolls.AddEditCustomer.GetAllItems();


            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
