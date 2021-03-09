using A35Mge.Api.AtMap;
using A35Mge.Database;
using A35Mge.Dependency;
using A35Mge.ScheduleTask;
using A35Mge.ScheduleTask.Job;
using A35Mge.Service.GlobalException;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace A35Mge.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static readonly ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder =>
                {
#if DEBUG
                    builder.AddConsole();
#endif
                });
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(o =>
                {
                    o.Filters.Add(typeof(CustomeExceptionFilter));
                })
                .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                        //options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
                        //options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                        //options.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTime;
                    });
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});
            var config = Configuration.GetSection("Connection");

            services.AddDbContext<A35MgeDbContext>(
                options => options.UseMySql(config?.Value ?? string.Empty, mysql =>
            {
                var builder = mysql
                 .MigrationsAssembly(System.Reflection.Assembly.Load("A35Mge.MySqlDatabase").FullName)
                 .EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
            }).UseLoggerFactory(MyLoggerFactory));
            services
                .AddAutoMapper(
                Assembly.Load("A35Mge.Api"),
                Assembly.Load("A35Mge.Service"))
                .AddA35Service(Configuration)
                .AddJwtConfig(Configuration)
                .AddQuartzService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("manager", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Manager Web Api",
                    Description = "Manager Web Api",
                    TermsOfService = new Uri("https://example.com/terms")
                });
                c.SwaggerDoc("front", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Front Web Api",
                    Description = "Front Web Api",
                    TermsOfService = new Uri("https://example.com/terms")
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "\u5728\u4e0b\u6846\u4e2d\u8f93\u5165\u8bf7\u6c42\u5934\u4e2d\u9700\u8981\u6dfb\u52a0\u004a\u0077\u0074\u6388\u6743\u0054\u006f\u006b\u0065\u006e\uff1a\u0042\u0065\u0061\u0072\u0065\u0072\u0020\u0054\u006f\u006b\u0065\u006e",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            var cors = (string[])Configuration.GetSection("Cors:Url").Get(typeof(string[]));
            //允许一个或多个来源可以跨域
            services.AddCors(options =>
            {
                options.AddPolicy("cors",

                builder => builder.AllowAnyOrigin()

                .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")

                );
                options.AddPolicy("CustomCorsPolicy", policy =>
                {
                    // 设定允许跨域的来源，有多个可以用','隔开
                    policy.WithOrigins(cors)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
                options.AddPolicy("AllAllow", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "upload");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path)
                {

                },
                RequestPath = "/upload",
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age36000");
                }

            });
            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(path)
                {

                },
                RequestPath = "/upload"
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CustomCorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger(c =>
        {
            c.RouteTemplate = "swagger/{documentName}/swagger.json";
            //c.SerializeAsV2 = true;
        });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/manager/swagger.json", "Manager");
                c.SwaggerEndpoint("/swagger/front/swagger.json", "Front");
                //c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpa(spa =>
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8000");
                }
            });
        }
    }
}
