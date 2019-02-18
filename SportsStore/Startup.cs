using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //every time instance of IProductRepository is needed, new object of FakeProductRepository will be created each time
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStore:ConnectionString"])
            );
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: null,
                        template: "{categoryName}/Page{productPage:int}",
                        defaults: new
                        {
                            controller = "Product",
                            action = "List",
                            productPage=1
                        }
                        );

                    routes.MapRoute(
                       name: null,
                       template: "Page{productPage:int}",
                       defaults: new
                       {
                           controller = "Product",
                           action = "List",
                           productPage = 1
                       }
                   );

                    routes.MapRoute(
                        name: null,
                        template: "{categoryID:int}",
                        defaults: new
                        {
                            controller = "Product",
                            action = "List",
                            productPage = 1
                        }
                    );

                    routes.MapRoute(
                     name: null,
                     template: "",
                     defaults: new
                     {
                         controller = "Product",
                         action = "List",
                         productPage = 1
                     });



                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Product}/{action=list}/{id?}"
                        );
                });


        }
    }
}
