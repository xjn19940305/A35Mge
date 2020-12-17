using A35Mge.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace A35Mge.ScheduleTask
{
    /// <summary>
    /// Job调度中间对象
    /// </summary>
    public class JobScheduleDTO
    {
        public JobScheduleDTO()
        {
        }
        /// <summary>
        /// Job类型
        /// </summary>
        public Type JobType { get; set; }

        public CancellationToken CancelToken { get; set; }
        public int JobScheduleId { get; set; }
        /// <summary>
        /// 对应的程序集名字
        /// </summary>
        public string AssemblyName { get; set; }
        public string JobName { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// 根据cron或者轮询或指定时间执行
        /// 0 cron 1轮询 2定时执行
        /// </summary>
        public int TriggerType { get; set; }
        /// <summary>
        /// 0:秒级别轮询 1:分钟级别轮询 2:小时级别轮询
        /// </summary>
        public int LoopType { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }
        /// <summary>
        /// 定时执行的时间
        /// </summary>
        public DateTime StartNow { get; set; }
        /// <summary>
        /// Job状态
        /// </summary>
        public JobStatus JobStatu { get; set; } = JobStatus.Init;
    }
}
