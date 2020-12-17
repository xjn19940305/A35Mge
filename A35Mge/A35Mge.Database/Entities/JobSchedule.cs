﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;

namespace A35Mge.Database.Entities
{
    public class JobSchedule : EntityBase
    {
        public JobSchedule()
        {
        }
        public int JobScheduleId { get; set; }
        public string JobName { get; set; }
        /// <summary>
        /// 对应的程序集名字
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }
        /// <summary>
        /// 根据cron或者轮询或指定时间执行
        /// 0:cron触发器 1:Simple
        /// </summary>
        public TriggerType TriggerType { get; set; }

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
        /// Job状态
        /// </summary>
        public JobStatus JobStatu { get; set; } = JobStatus.Init;
    }
    public enum TriggerType
    {
        [Description("Cron触发器")]
        Cron = 0,
        [Description("Simple触发器")]
        Simple = 1
    }
    public enum SimpleTriggerType
    {
        [Description("指定时间开始触发,不重复")]
        StartNow = 0,
        [Description("立即触发,每多少分钟执行一次")]
        StartRepeat = 1
    }
    /// <summary>
    /// Job运行状态
    /// </summary>
    public enum JobStatus : int
    {
        [Description("初始化")]
        Init = 0,
        [Description("运行中")]
        Running = 2,
        [Description("已完成")]
        Complete = 3,
        [Description("已停止")]
        Stopped = 4,

    }
}