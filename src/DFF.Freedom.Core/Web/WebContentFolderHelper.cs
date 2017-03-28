using System;
using System.IO;
using System.Linq;

namespace DFF.Freedom.Web
{
    /// <summary>
    /// This class is used to find root path of the web project in;
    /// unit tests (to find views) and entity framework core command line commands (to find conn string).
    /// 网站项目内容查找器。
    /// 该类用于查找web项目中的根路径；
    /// 单元测试（找视图）和实体框架的核心命令行命令（找连接字符串）。
    /// </summary>
    public static class WebContentDirectoryFinder
    {
        /// <summary>
        /// 计算内容根文件夹
        /// </summary>
        /// <returns></returns>
        public static string CalculateContentRootFolder()
        {
            var coreAssemblyDirectoryPath = Path.GetDirectoryName(typeof(FreedomCoreModule).Assembly.Location);
            if (coreAssemblyDirectoryPath == null)
            { //DFF.Freedom.Core程序集的目录路径为空
                throw new ApplicationException("Could not find location of DFF.Freedom.Core assembly!");
            }

            var directoryInfo = new DirectoryInfo(coreAssemblyDirectoryPath);
            while (!DirectoryContains(directoryInfo.FullName, "DFF.Freedom.sln"))
            {
                if (directoryInfo.Parent == null)
                {
                    throw new ApplicationException("Could not find content root folder!");
                }

                directoryInfo = directoryInfo.Parent;
            }

            return Path.Combine(directoryInfo.FullName, @"src\DFF.Freedom.Web");
        }

        /// <summary>
        /// 目录中是否包含文件名
        /// </summary>
        /// <param name="directory">目录</param>
        /// <param name="fileName">文件名</param>
        /// <returns>true：包含；false：不包含</returns>
        private static bool DirectoryContains(string directory, string fileName)
        {
            return Directory.GetFiles(directory).Any(filePath => string.Equals(Path.GetFileName(filePath), fileName));
        }
    }
}
