﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Transactions.Query;
using Transactions.Manipulation;
using Transactions.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Transactions
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
            services.AddScoped<IQueryService, QueryService>(ctor => new QueryService(Configuration["ConnectionString"]));
            services.AddScoped<IManipService, ManipService>();

            services.AddDbContext<DBTransContext>(optionsAction: opt =>
                opt.UseSqlServer(Configuration["ConnectionString"],
                optionsAction => optionsAction.MigrationsAssembly(typeof(DBTransContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DBTransContext>();
                context.Database.Migrate();
            }
            app.UseMvc();
        }
    }
}
