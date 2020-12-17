using A35Mge.Database.Entities;
using AutoMapper;
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

        public ScheduleService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IMapper mapper
            )
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;
            this.mapper = mapper;
        }
        /// <summary>
        /// 开始执行任务
        /// </summary>
        /// <param name="Schedule"></param>
        /// <returns></returns>
        public async Task StartTask(JobScheduleDTO Schedule)
        {
            var t = Assembly.GetExecutingAssembly();
            var type = t.GetType($"{t.GetName().Name}.Job.{Schedule.AssemblyName}");
            Schedule.JobType = type;
            var Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            var jobData = new JobDataMap();
            jobData.Add("JSON", Schedule?.Params ?? string.Empty);
            var job = CreateJob(Schedule, jobData);
            ITrigger trigger;
            // 触发器时间
            if (Schedule.TriggerType == 0)
                trigger = CreateTrigger(Schedule);
            else if (Schedule.TriggerType == 2)
                trigger = CreateStartAtTrigger(Schedule);
            else
                trigger = CreateTrigger(Schedule);
            await Scheduler.ScheduleJob(job, trigger);
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
        private static IJobDetail CreateJob(JobScheduleDTO schedule, JobDataMap data = null)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(new JobKey(schedule.JobName))
                .WithDescription(jobType.Name)
                .SetJobData(data)
                .Build();
        }

        private static ITrigger CreateTrigger(JobScheduleDTO schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
        private static ITrigger CreateStartAtTrigger(JobScheduleDTO schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobName}.trigger")
                .StartAt(DateTimeOffset.Parse(schedule.StartNow.ToString()))
                .Build();
        }
    }
}
