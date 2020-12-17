using A35Mge.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A35Mge.ScheduleTask
{
    public class CustomeSchedule : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly JobScheduleDTO jobScheduleDTO;
        private readonly ILogger<CustomeSchedule> logger;
        private readonly IServiceProvider _provider;

        public IScheduler Scheduler { get; set; }
        public CustomeSchedule(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            JobScheduleDTO jobScheduleDTO,
            ILogger<CustomeSchedule> logger,
            IServiceProvider provider
            )
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
            this.jobScheduleDTO = jobScheduleDTO;
            this.logger = logger;
            this._provider = provider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($"全局调度任务启动!");
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            var job = CreateJob(jobScheduleDTO);
            var trigger = CreateTrigger(jobScheduleDTO);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
            using (var scope = _provider.CreateScope())
            {
                await Task.Delay(100);
                var a35MgeDbContext = scope.ServiceProvider.GetService<A35MgeDbContext>();
                var TaskList = await a35MgeDbContext.JobSchedule.Where(x => x.JobStatu == Database.Entities.JobStatus.Running).ToListAsync();
                TaskList.ForEach(x => x.JobStatu = Database.Entities.JobStatus.Init);
                await a35MgeDbContext.SaveChangesAsync();
                logger.LogInformation($"全局调度任务结束!");
            }
        }
        private ITrigger CreateTrigger(JobScheduleDTO jobMetadata)
        {
            return TriggerBuilder.Create()
                                 .WithIdentity(jobMetadata.JobName.ToString())
                                 .WithCronSchedule(jobMetadata.CronExpression)
                                 .WithDescription($"{jobMetadata.JobName}")
                                 .Build();
        }
        private IJobDetail CreateJob(JobScheduleDTO jobMetadata)
        {
            return JobBuilder
            .Create(jobMetadata.JobType)
            .WithIdentity(jobMetadata.JobName.ToString())
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
    }
}
