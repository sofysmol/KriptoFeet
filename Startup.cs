using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using KriptoFeet.DB;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using KriptoFeet.Comments.DB;
using KriptoFeet.Categories.DB;
using KriptoFeet.News.DB;
using KriptoFeet.Users.DB;
using KriptoFeet.News;
using KriptoFeet.Comments;
using KriptoFeet.Users;
using KriptoFeet.Users.Models;
using KriptoFeet.Utils;
using Microsoft.AspNetCore.Identity;

namespace KriptoFeet
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
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessMySqlProvider");

            services.AddDbContext<DomainModelMySqlContext>(options =>
                options.UseMySql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("KriptoFeet")
                )
            );

            services.AddIdentity<Account, IdentityRole>()
        .AddEntityFrameworkStores<DomainModelMySqlContext>()
        .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
        // Password settings
        options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

        // Lockout settings
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

        // User settings
        options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
        // Cookie settings
        options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Home/SignIn"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
        options.LogoutPath = "/Home/LogOff"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
        options.AccessDeniedPath = "/Home/Error"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
        options.SlidingExpiration = true;
            });
            services.AddScoped<ICommentsProvider, CommentsProvider>();
            services.AddScoped<ICategoriesProvider, CategoriesProvider>();
            services.AddScoped<INewsProvider, NewsProvider>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IContentManagerRequestProvider, ContentManagerRequestsProvider>();
            services.AddSingleton<ILongRandomGenerator, LongRandomGenerator>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
           bool createRoles = Configuration.GetValue<bool>("CreateRoles");
            if(createRoles)
                CreateRoles(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<Account>>();
            string[] roleNames = { "Admin", "ContentManager", "User" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //creating a super user who could maintain the web app
            var poweruser = new Account
            {
                UserName = Configuration.GetSection("UserSettings")["Nickname"],
                Email = Configuration.GetSection("UserSettings")["Email"]
            };
            string UserPassword = Configuration.GetSection("UserSettings")["Password"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["Email"]);
            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
