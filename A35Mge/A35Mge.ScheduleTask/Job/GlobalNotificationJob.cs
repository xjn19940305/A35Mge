using A35Mge.Database;
using A35Mge.Database.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.ScheduleTask.Job
{
    /// <summary>
    /// 全局调度任务器 抓取JobSchedule中的数据
    /// </summary>
    [DisallowConcurrentExecution]
    public class GlobalNotificationJob : IJob
    {
        private readonly ILogger<GlobalNotificationJob> logger;
        private readonly IServiceProvider _provider;
        private readonly ScheduleService scheduleService;
        private readonly IMapper mapper;

        public GlobalNotificationJob(
            ILogger<GlobalNotificationJob> logger,
            IServiceProvider provider,
            ScheduleService scheduleService,
            IMapper mapper
            )
        {
            this.logger = logger;
            this._provider = provider;
            this.scheduleService = scheduleService;
            this.mapper = mapper;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                await Task.Delay(100);
                var a35MgeDbContext = scope.ServiceProvider.GetService<A35MgeDbContext>();
                var TaskList = await a35MgeDbContext.JobSchedule.ToListAsync();
                logger.LogInformation($"{DateTime.UtcNow} 当前初始化队列任务:{TaskList.Count(x => x.JobStatu == Database.Entities.JobStatus.Init)}");
                logger.LogInformation($"{DateTime.UtcNow} 当前正在执行队列任务:{TaskList.Count(x => x.JobStatu == Database.Entities.JobStatus.Running)}");
                logger.LogInformation($"{DateTime.UtcNow} 当前已完成队列任务:{TaskList.Count(x => x.JobStatu == Database.Entities.JobStatus.Complete)}");
                var InitList = TaskList.Where(x => x.JobStatu == Database.Entities.JobStatus.Init).ToList();
                foreach(var x in InitList)
                {
                    var model = mapper.Map<JobSchedule, JobScheduleDTO>(x);
                    // 启动任务
                    await scheduleService.StartTask(model);
                    x.JobStatu = Database.Entities.JobStatus.Running;
                    await a35MgeDbContext.SaveChangesAsync();
                }

            }

        }
    }
}
