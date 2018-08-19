using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftUniClone.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using SoftUniClone.App.Areas.Identity.Services;
using SoftUniClone.Models;
using SoftUniClone.Web.Common;
using AutoMapper;
using SoftUniClone.Services.Admin.Interfaces;
using SoftUniClone.Services.Admin;
using SoftUniClone.Services.Lecturer.Interfaces;
using SoftUniClone.Services.Lecturer;
using Microsoft.AspNetCore.Mvc.Razor;
using SoftUniClone.ServiceModels.Resources;
using SoftUniClone.Web.Areas.Identity.Services;
using SoftUniClone.Services.Student.Interfaces;
using SoftUniClone.Services.Student;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SoftUniClone.Web.Hubs;

namespace SoftUniClone.Web
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
            services.AddResponseCaching();
            services.AddResponseCompression();

            services.AddMemoryCache();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.AddSupportedCultures("en", "bg");
                options.AddSupportedUICultures("en", "bg");
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SoftUniCloneDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SoftUniClone"),
                          dbOptions => dbOptions.MigrationsAssembly("SoftUniClone.Data")));

           

            services.AddAuthentication()
            //    .AddFacebook(options =>
            //{
            //    options.AppId = "223779121657176";
            //    options.AppSecret = "5267b4f183fcd1e4f199360c5f0f0cb7";
            //}) 
            //// or
              .AddFacebook(options =>
              {
                  options.AppId = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppId").Value;
                  options.AppSecret = this.Configuration.GetSection("ExternalAuthentication:Facebook:AppSecret").Value;
              })

            .AddGoogle(options =>
            {
                options.ClientId = "118947079794-nqjluv5b5ispnkpjq35tmlgbgdscsg1v.apps.googleusercontent.com";
                options.ClientSecret = "uKcT8kRyK9B3aOhK3WOyStqZ";
            })
            .AddGitHub(options =>
            {
                options.ClientId = "a56a500d0fd9f31e6eb6";
                options.ClientSecret = "0c83f552c3a1d69603af26b8bdd1219f5fae1236";
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("supersecretsupersecretsupersecretsupersecret"))
                        //Encoding.UTF8.GetBytes(this.Configuration.GetSection(
                        //    "JwtSettings:SecretKey").Value))
                };
            });

            services.AddIdentity<User, IdentityRole>()
               .AddDefaultUI()             // gives Scaffolding for all pages!
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<SoftUniCloneDbContext>();
            //services.AddDefaultIdentity<User>() // IdentityUser - DOESNT WORK!!!
            //    .AddRoles<IdentityRole>() // in order to have RoleManager on roll 104!
            //    .AddEntityFrameworkStores<SoftUniCloneDbContext>()
            //    .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                options.Password = new PasswordOptions()
                {
                    RequiredLength = 4,
                    RequiredUniqueChars = 1,
                    RequireDigit = false,
                    RequireLowercase = true,
                    RequireUppercase = false,
                    RequireNonAlphanumeric = false
                };

               //  options.SignIn.RequireConfirmedEmail = true; // very usefull !!! but also:
            });



            ////services.AddSingleton<IEmailSender>(new SendGridEmailSender());
            ////// or
            //services.AddSingleton<IEmailSender, SendGridEmailSender>();
            //services.Configure<SendGridOptions>(this.Configuration.GetSection("EmailSettings"));

            services.AddAutoMapper(); // this is Singleton


            RegisterServiceLayer(services);

            services.AddSignalR();

            services
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                      // .AddDataAnnotationsLocalization() //- for separate resource file for each B. Model
                .AddDataAnnotationsLocalization(option =>
                {
                    option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ValidationResources));
                })
                   .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

       

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRequestLocalization(); // makes new middleware

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.SeedDatabase(); // from created class ApplicationBuilderAuthExtensions
            }

            app.UseSignalR(options =>
            {
               // options.MapHub<QuestionsHub>("/questions");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IAdminCoursesService, AdminCoursesService>(); // in order to inject this service to CoursesController
            services.AddScoped<IAdminCourseInstancesService, AdminCourseInstancesService>();
            services.AddScoped<IAdminLecturersServices, AdminLecturersServices>();

            services.AddScoped<ILecturerCourseInstancesService, LecturerCourseInstancesService>(); // in order to inject this service to CourseInstancesController
            services.AddScoped<IStudentCourseInstancesService, StudentCourseInstancesService>();
            services.AddScoped<ILecturerCoursesService, LecturerCoursesService>();
        }
    }
}
