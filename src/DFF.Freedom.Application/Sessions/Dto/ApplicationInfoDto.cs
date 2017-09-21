using System;
using System.Collections.Generic;

namespace DFF.Freedom.Sessions.Dto
{
    /// <summary>
    /// 应用程序 数据传输对象
    /// </summary>
    public class ApplicationInfoDto
    {
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// 特征
        /// </summary>
        public Dictionary<string, bool> Features { get; set; }
    }
}
