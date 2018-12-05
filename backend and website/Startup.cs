using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Lunchers.Data;
using Lunchers.Data.Repositories;
using Lunchers.Models.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Lunchers.Models.IRepositories;

namespace Lunchers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //windows of mac invullen afhankelijk van je os -> Deployment voor azure -> server voor brent
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("windows")));




            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddScoped<DummyDataInitializer>();
            services.AddScoped<IGebruikerRepository, GebruikerRepository>();
            services.AddScoped<ILunchRespository, LunchRespository>();
            services.AddScoped<IHandelaarRepository, HandelaarRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IAfbeeldingRepository, AfbeeldingRepository>();
            services.AddScoped<IReservatieRepository, ReservatieRepository>();
            services.AddScoped<IKlantRepository, KlantRepository>();
            services.AddScoped<IFavorietRepository, FavorietRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DummyDataInitializer dummyDataInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            //dummyDataInitializer.InitializeData();
        }
    }
}
