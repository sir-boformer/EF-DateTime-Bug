using System;
using System.Linq;
using EFDebug.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFDebug
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
            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AppDbContext>(options =>
            {
                // Configure the context to use Microsoft SQL Server.
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AppDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();

            AppDbInitializer.Initialize(context);
        }
    }

    public class AppDbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Memberships.Any())
            {
                context.Memberships.Add(new Membership
                {
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.MaxValue,
                    UserId = "UserMinMax"
                });

                context.Memberships.Add(new Membership
                {
                    StartDate = DateTime.UtcNow.Date.AddDays(-10),
                    EndDate = DateTime.MaxValue,
                    UserId = "UserMax"
                });

                context.Memberships.Add(new Membership
                {
                    StartDate = DateTime.MinValue,
                    EndDate = DateTime.UtcNow.Date.AddDays(10),
                    UserId = "UserMin"
                });

                context.Memberships.Add(new Membership
                {
                    StartDate = DateTime.UtcNow.Date.AddDays(-10),
                    EndDate = DateTime.UtcNow.Date.AddDays(10),
                    UserId = "User"
                });

                context.SaveChanges();
            }
        }
    }
}
