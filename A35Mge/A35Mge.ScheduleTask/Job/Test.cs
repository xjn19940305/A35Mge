using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A35Mge.ScheduleTask.Job
{
    public class Test : IJob
    {
        public Test(ILogger<Test> logger)
        {
            Logger = logger;
        }

        public ILogger<Test> Logger { get; }

        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"{DateTime.Now} 我是测试1");
            return Task.CompletedTask;
        }
    }
    public class Test2 : IJob
    {
        public Test2(ILogger<Test2> logger)
        {
            Logger = logger;
        }

        public ILogger<Test2> Logger { get; }
        public Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"{DateTime.Now} 我是测试2");
            return Task.CompletedTask;
        }
    }
}
