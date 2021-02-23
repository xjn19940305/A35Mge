using A35Mge.Model;
using A35Mge.Model.Config;
using A35Mge.Redis;
using A35Mge.ScheduleTask;
using A35Mge.ScheduleTask.Job;
using A35Mge.Service.Implement;
using A35Mge.Service.Interface;
using A35Mge.Service.Permission;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using UEditor.Core;

namespace A35Mge.Dependency
{
    public static class AllExtension
    {
        public static IServiceCollection AddA35Service(this IServiceCollection services, IConfiguration cofiguration)
        {
            services.TryAddScoped<ILanguageService, LanguageService>();
            services.TryAddScoped<IMenuService, MenuService>();
            services.TryAddScoped<IRoleService, RoleService>();
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<RedisCache>();
            var ue = cofiguration.GetSection("UEditor").Value;
            services.AddUEditorService(ue);
            return services;
        }
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration cofiguration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.Audience = cofiguration["JwtConfig:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // The signing key must match!
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(cofiguration["JwtConfig:SecurityKey"])),

                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = cofiguration["JwtConfig:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = cofiguration["JwtConfig:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // If you want to allow a certain amount of clock drift, set that here
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.Configure<JwtConfig>(cofiguration.GetSection("JwtConfig"));
            //services.AddOptions<JwtConfig>();
            services.TryAddScoped<JwtService>();
            return services;
        }
        public static IServiceCollection AddQuartzService(this IServiceCollection services)
        {
            //添加Quartz服务
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //测试JOB

            //通用注册服务
            services.AddSingleton<ScheduleService>();
            //注册全局调度任务
            services.AddSingleton<GlobalNotificationJob>();
            services.AddSingleton<DemoJob>();
            services.AddSingleton(new JobScheduleDTO { JobName = "全局调度器", JobType = typeof(GlobalNotificationJob), CronExpression = "0/59 0/3 * * * ? *" });
            //全局调度任务注册为宿主服务随程序启动关闭
            services.AddHostedService<CustomeSchedule>();
            return services;
        }
    }
}
