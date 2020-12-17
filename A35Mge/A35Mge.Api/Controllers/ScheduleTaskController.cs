using A35Mge.Database;
using A35Mge.Database.Entities;
using A35Mge.ScheduleTask;
using A35Mge.ScheduleTask.Job;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace A35Mge.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleTaskController : ControllerBase
    {
        private readonly ScheduleService scheduleService;
        private readonly ILogger<ScheduleTaskController> logger;
        private readonly A35MgeDbContext a35MgeDbContext;
        private readonly IMapper mapper;
        public ScheduleTaskController(
            ScheduleService scheduleService,
            ILogger<ScheduleTaskController> logger,
            A35MgeDbContext a35MgeDbContext,
            IMapper mapper
            )
        {
            this.scheduleService = scheduleService;
            this.logger = logger;
            this.a35MgeDbContext = a35MgeDbContext;
            this.mapper = mapper;
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //CancellationToken cancellationToken = cancellationTokenSource.Token;
            //job = new JobSchedule("测试", "0/5 * * * * ?", cancellationToken);
        }
        /// <summary>
        /// 指定启动任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> RunTask(int Id)
        {
            var Task = await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Id);
            var job = mapper.Map<JobSchedule, JobScheduleDTO>(Task);
            logger.LogWarning($"{DateTime.Now}开始执行任务");
            await scheduleService.StartTask(job);
            return Ok("开始执行任务");
        }
        /// <summary>
        /// 停止调度器
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> StopTask(int Id)
        {
            var Task = await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Id);
            var job = mapper.Map<JobSchedule, JobScheduleDTO>(Task);
            await scheduleService.StopTask(job);
            logger.LogWarning($"{DateTime.Now}停止执行任务");
            return Ok("停止执行任务");
        }
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PauseTask(int Id)
        {
            var Task = await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Id);
            var job = mapper.Map<JobSchedule, JobScheduleDTO>(Task);
            await scheduleService.PauseTask(job);
            logger.LogWarning($"{DateTime.Now}暂停执行任务");
            return Ok("暂停执行任务");
        }
        /// <summary>
        /// 恢复任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ResumeTask(int Id)
        {
            var Task = await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Id);
            var job = mapper.Map<JobSchedule, JobScheduleDTO>(Task);
            await scheduleService.ResumeJob(job);
            logger.LogWarning($"{DateTime.Now}恢复任务");
            return Ok("恢复任务");
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteTask(int Id)
        {
            var Task = await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Id);
            var job = mapper.Map<JobSchedule, JobScheduleDTO>(Task);
            await scheduleService.DeleteJob(job);
            logger.LogWarning($"{DateTime.Now}删除任务 jobName:{job.JobName}");
            return Ok("删除任务");
        }
    }
}
