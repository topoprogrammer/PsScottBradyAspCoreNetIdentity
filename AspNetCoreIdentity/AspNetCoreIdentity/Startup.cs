using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;


namespace AspNetCoreIdentity
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddIdentityCore<CustomUser>(options => { });
            //services.AddScoped<IUserStore<CustomUser>, CustomUserStore>();

            //services.AddIdentityCore<IdentityUser>(options => { });
            //services.AddScoped<IUserStore<IdentityUser>, CustomIdentityStore>();

            //Default unextended identity user
            //const string connectionString =
            //    @"Data Source=.\sqlexpress;Initial Catalog=AspIdentityCoreDbDefault;Integrated Security=True";
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //services.AddDbContext<IdentityDbContext>(opt => opt.UseSqlServer(connectionString,
            //    sql => sql.MigrationsAssembly(migrationAssembly)));
            //services.AddIdentityCore<IdentityUser>(options => { });
            //services.AddScoped<IUserStore<IdentityUser>,
            //    UserOnlyStore<IdentityUser, IdentityDbContext>>();


            //Extended identity user 
            //const string connectionString =
            //  @"Data Source=.\sqlexpress;Initial Catalog=AspIdentityCoreDbCustom;Integrated Security=True";
            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //services.AddDbContext<CustomDbContext>(opt => opt.UseSqlServer(connectionString,
            //    sql => sql.MigrationsAssembly(migrationAssembly)));       
            ////Just Core funcionality without userRoles
            ////***************************************************
            //services.AddIdentityCore<CustomUser>(options => { });
            ////***************************************************
            //services.AddScoped<IUserStore<CustomUser>,
            //    UserOnlyStore<CustomUser, CustomDbContext>>();
            //services.AddAuthentication("cookies")
            //    .AddCookie("cookies", options => options.LoginPath = "/Home/Login");


            const string connectionString =
             @"Data Source=.\sqlexpress;Initial Catalog=AspIdentityCoreDbCustom;Integrated Security=True";
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<CustomDbContext>(opt => opt.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(migrationAssembly)));
            //Full funcionality of identity
            //***************************************************
            services.AddIdentity<CustomUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconf";

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 4;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            })
            .AddEntityFrameworkStores<CustomDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<EmailConfirmationTokenProvider<CustomUser>>("emailconf")
            .AddPasswordValidator<DoesNotContainPasswordValidator<CustomUser>>();

            services.AddScoped<IUserClaimsPrincipalFactory<CustomUser>, CustomUserPrincipalFactory>();

            services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(3));

            services.Configure<EmailConfirmationTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromDays(2));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/Login");
            //***************************************************

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
