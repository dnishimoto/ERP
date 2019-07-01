using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.CommentDomain;
using lssWebApi2.EntityFramework;
using lssWebApi2.Interfaces;
using lssWebApi2.SalesOrderDomain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace lssWebApi2
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

            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
         
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                //.SetBasePath(basePath) 
                .AddJsonFile("appsettings.json")
                 .Build();
           
            var connectionString = configuration.GetConnectionString("DbCoreConnectionString2");

            services.AddDbContext<ListensoftwaredbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));

            services.AddTransient<ISalesOrderRepository, SalesOrderRepository>();
            services.AddTransient<ISalesOrderDetailRepository, SalesOrderDetailRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseMvc();
        }
        public class MySection
        {
            public string SharedValue { get; set; }
            public string AppSpecificValue { get; set; }

        }
    }
}

