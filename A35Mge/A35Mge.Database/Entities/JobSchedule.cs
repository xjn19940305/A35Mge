using A35Mge.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace A35Mge.Database.Entities
{
    public class JobSchedule : EntityBase
    {
        public int Id { get; set; }
        public string JobId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 对应的程序集名字
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// 发生错误时的报错
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 0:秒级别轮询 1:分钟级别轮询 2:小时级别轮询
        /// </summary>
        public int? LoopType { get; set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }
        /// <summary>
        /// 定时执行的时间
        /// </summary>
        public DateTime? StartNow { get; set; }
        /// <summary>
        /// 根据cron或者轮询或指定时间执行
        /// 0:cron触发器 1:Simple
        /// </summary>
        public TriggerType TriggerType { get; set; }
        /// <summary>
        /// Job状态
        /// </summary>
        public JobStatus JobStatu { get; set; } = JobStatus.Init;
    }

}
