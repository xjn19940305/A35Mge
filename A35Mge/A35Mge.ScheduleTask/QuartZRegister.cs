﻿using A35Mge.ScheduleTask.Job;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.ScheduleTask
{
    public static class QuartZRegister
    {
        public static IServiceCollection AddQuartzService(this IServiceCollection services)
         {
            //添加Quartz服务
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //测试JOB
            services.AddSingleton<NotificationJob>();
            services.AddSingleton<Test>();
            services.AddSingleton<Test2>();
            //通用注册服务
            services.AddSingleton<ScheduleService>();
            //注册全局调度任务
            services.AddSingleton<GlobalNotificationJob>();
            services.AddSingleton(new JobScheduleDTO { JobName = "全局调度器", JobType = typeof(GlobalNotificationJob), CronExpression = "0/59 0/3 * * * ? *" });
            //全局调度任务注册为宿主服务随程序启动关闭
            services.AddHostedService<CustomeSchedule>();
            return services;
        }
    }
}
