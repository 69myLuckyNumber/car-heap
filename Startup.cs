using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using car_heap.Core.Abstract;
using car_heap.Core.Models;
using car_heap.Infrastructure.ConfigPocos;
using car_heap.Persistence;
using car_heap.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace car_heap
{
    public class Startup
    {
        private const string SecretKey = "q4krLli6cLP38toSX8py2KChZY7R0Q0X";
        private readonly SymmetricSecurityKey signingKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // app dependencies
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<IMakeRepository, MakeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            var jwtAppSettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(opts => 
            {
                opts.Issuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                opts.Audience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)];
                opts.SigningCredentials = new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256); 
            });

            // app identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults : new { controller = "Home", action = "Index" });
            });

            await SeedDataAsync(app);

        }

        private async Task SeedDataAsync(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData.SeedAsync(serviceScope.ServiceProvider.GetRequiredService<AppDbContext>());
            }
        }
    }
}