using System;
using System.Collections.Generic;
using System.Text;

namespace WindowServer.TopShelf
{
    public class JobConfig
    {
        /// <summary>
        /// crontab 表达式
        /// </summary>
        public string Crontab { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
