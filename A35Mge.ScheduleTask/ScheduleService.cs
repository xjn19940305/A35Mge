using A35Mge.Database.Entities;
using A35Mge.Enum;
using A35Mge.Model;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.ScheduleTask
{
    public class ScheduleService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly IMapper mapper;
        private readonly ILogger<ScheduleService> logger;

        public ScheduleService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IMapper mapper,
            ILogger<ScheduleService> logger
            )
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
            this.mapper = mapper;
            this.logger = logger;
        }
        /// <summary>
        /// 开始执行任务
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task StartTask(JobScheduleDTO Schedule)
        {
            var t = Assembly.GetExecutingAssembly();
            Schedule.JobType = t.GetType($"{t.GetName().Name}.Jobs.{Schedule.AssemblyName}");
            var Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            var jobData = new JobDataMap { new KeyValuePair<string, object>("JSON", Schedule?.Params ?? string.Empty) };
            await Scheduler.ScheduleJob(CreateJob(Schedule, jobData), CreateTrigger(Schedule));
            await Scheduler.Start();
        }
        /// <summary>
        /// 停止任务
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task StopTask(JobScheduleDTO Schedule)
        {
            var Scheduler = await schedulerFactory.GetScheduler();
            await Scheduler?.Shutdown();
            Schedule.JobStatu = JobStatus.Stopped;
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task PauseTask(JobScheduleDTO Schedule)
        {
            var Scheduler = await schedulerFactory.GetScheduler();
            await Scheduler.PauseJob(new JobKey(Schedule.JobName));
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task ResumeJob(JobScheduleDTO Schedule)
        {
            var Scheduler = await schedulerFactory.GetScheduler();
            await Scheduler.ResumeJob(new JobKey(Schedule.JobName));
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task DeleteJob(JobScheduleDTO Schedule)
        {
            var Scheduler = await schedulerFactory.GetScheduler();
            await Scheduler.DeleteJob(new JobKey(Schedule.JobName));
        }
        private IJobDetail CreateJob(JobScheduleDTO schedule, JobDataMap data = null)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(new JobKey(schedule.JobName))
                .WithDescription(jobType.Name)
                .SetJobData(data)
                .Build();
        }

        private ITrigger CreateTrigger(JobScheduleDTO schedule)
        {
            var trig = TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobName}.trigger");
            logger.LogInformation($"本地时间:{DateTime.Now}");
            logger.LogInformation($"本地UTC时间:{DateTime.UtcNow}");
            logger.LogInformation($"任务执行时间:{schedule.StartNow.ToString()}");
            //判断是用 cron触发器还是simple
            //目前simple只支持立即执行以后还可以扩展
            if (schedule.TriggerType == TriggerType.Cron)
                trig.WithCronSchedule(schedule.CronExpression);
            else if (schedule.TriggerType == TriggerType.Simple)
                trig.StartAt(new DateTimeOffset(schedule.StartNow.Value, TimeSpan.Zero));

            return trig.Build();
        }
    }
}
