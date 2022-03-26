using Diff_API_Task.BAL;
using Diff_API_Task.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diff_API_Task
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
            services.AddTransient<IDiffTaskDBContext, DiffTaskDBContext>();
            services.AddTransient<IRepository, Repository>();
            services.AddTransient<IDiffTaskProvider, DiffTaskProvider>();

            //Databse connection
            var connectionString = Configuration[$"connectionString:DiffTaskDB"];
            services.AddDbContextPool<DiffTaskDBContext>(options => options.UseSqlServer(connectionString, sqlServerOptions => { sqlServerOptions.CommandTimeout(120); }));

            services.AddMvc();
            services.AddMvcCore();
            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Diff API Task", Version = "v1"});
            });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diff_API_Task v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
