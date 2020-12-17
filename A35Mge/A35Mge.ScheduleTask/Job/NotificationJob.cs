using A35Mge.Database;
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
    [DisallowConcurrentExecution]
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        private readonly IServiceProvider _provider;

        public NotificationJob(
            ILogger<NotificationJob> logger,
                 IServiceProvider provider
            )
        {
            _logger = logger;
            this._provider = provider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _provider.CreateScope())
            {
                await Task.Delay(100);
                var a35MgeDbContext = scope.ServiceProvider.GetService<A35MgeDbContext>();
                var Params = context.JobDetail.JobDataMap.GetInt("JSON");
                _logger.LogInformation($"{DateTime.Now} 接收参数:{Params}");
                _logger.LogInformation($"Hello world! num:{new Random(Guid.NewGuid().GetHashCode()).Next(1000)}");
                var data =await a35MgeDbContext.JobSchedule.FirstOrDefaultAsync(x => x.JobScheduleId == Params);
                data.JobStatu = Database.Entities.JobStatus.Complete;
                await a35MgeDbContext.SaveChangesAsync();
                _logger.LogInformation($"{DateTime.Now} 任务执行完成");
            }

        }
    }
}
