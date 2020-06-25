using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ninject.Activation;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using WebServet.Models;
using Microsoft.OpenApi.Models;
using System.IO;

namespace WebServet
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
            //�ٴ�����Լ�д�ķ���

            // services.AddDbContext<UserContext>(opt =>
            // opt.UseMySQL(Configuration.GetConnectionString("conn")));

            // ��ȡappsettings.json������Ϣ
            //var config = new ConfigurationBuilder()
            //              .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            //            .AddJsonFile("appsettings.json")
            //          .Build();
            //var alloweHosts = config["AllowedHosts"]; // ��ȡ����������������

            //���ÿ�����
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("cors", builder =>
            //    {
            //        builder.WithOrigins(alloweHosts) //����ָ����������
            //        .AllowAnyMethod()
            //        .AllowAnyHeader()
            //        .AllowCredentials();//ָ������cookie
            //    });
            //});

            services.AddCors(option => option.AddPolicy("cors", policy => policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(new[] { "http://localhost:8080" })));

            services.AddDbContext<UserContext>();

            //string[] assemblyArray = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "localLib.dll");

            // ���Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Demo", Version = "v1" });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("cors");

            // ���Swagger�й��м��
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Demo v1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
