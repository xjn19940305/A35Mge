using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace A35Mge.Enum
{
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
        [Description("等待执行中")]
        Wait = 1,
        [Description("正在执行中")]
        Running = 2,
        [Description("已完成")]
        Complete = 3,
        [Description("已停止")]
        Stopped = 4,
        [Description("已取消")]
        Cancel = 5

    }
}
