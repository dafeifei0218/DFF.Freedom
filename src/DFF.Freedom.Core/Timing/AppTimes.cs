using System;
using Abp.Dependency;

namespace DFF.Freedom.Timing
{
    /// <summary>
    /// 应用程序时间
    /// </summary>
    public class AppTimes : ISingletonDependency
    {
        /// <summary>
        /// Gets the startup time of the application.
        /// 获取或设置应用程序启动的时间
        /// </summary>
        public DateTime StartupTime { get; set; }
    }
}
