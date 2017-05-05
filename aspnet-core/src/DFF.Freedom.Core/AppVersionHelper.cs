using System;
using System.IO;

namespace DFF.Freedom
{
    /// <summary>
    /// Central point for application version.
    /// Abp版本帮助类。
    /// 应用程序版本。
    /// </summary>
    public class AppVersionHelper
    {
        /// <summary>
        /// Gets current version of the application.
        /// It's also shown in the web page.
        /// 获取应用程序的当前版本。
        /// 它也显示在网页中。
        /// </summary>
        public const string Version = "1.0.0.0";

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// 获取应用程序的发布（最后生成）日期。
        /// 它显示在网页中。
        /// </summary>
        public static DateTime ReleaseDate
        {
            get { return new FileInfo(typeof(AppVersionHelper).Assembly.Location).LastWriteTime; }
        }
    }
}
