using BusinessLayer.Mappers;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Repositories;
using DataLayerDBContext_DBContext;
using Models_DBModels;

namespace UserServiceApi
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


            services.AddCors((options) =>
            {
                options.AddPolicy(name: "NotFightClubLocal", builder =>
                {
                    builder.WithOrigins(
                           "https://localhost:5000",
                           "https://localhost:5002",
                           "https://localhost:5004",
                           "https://localhost:5006",
                           "https://localhost:5008",
                           "https://localhost:5010",


                           "http://localhost4200:",
                           "http://localhost:5001",
                           "http://localhost:5003",
                           "http://localhost:5005",
                           "http://localhost:5007",
                           "http://localhost:5009",
                           "http://localhost:5011"
                        )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddDbContext<NotFightClubUserContext>();
            services.AddSingleton<IRepository<ViewUser, string>, UserInfoRepository>();
            services.AddSingleton<IMapper<UserInfo, ViewUser>, UserMapper>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserServiceApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserServiceApi v1"));
            }

            app.UseCors("NotFightClubLocal");

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
