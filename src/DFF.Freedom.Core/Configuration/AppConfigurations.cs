using System.Collections.Concurrent;
using Abp.Extensions;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;

namespace DFF.Freedom.Configuration
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public static class AppConfigurations
    {
        //配置缓存字典
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> ConfigurationCache;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AppConfigurations()
        {
            ConfigurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        /// <summary>
        /// 获取配置根的对象
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="environmentName">环境的名称</param>
        /// <param name="addUserSecrets">是否添加用户秘密的配置源</param>
        /// <returns></returns>
        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return ConfigurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets)
            );
        }

        /// <summary>
        /// 建造配置根的对象
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="environmentName">环境的名称</param>
        /// <param name="addUserSecrets">是否添加用户秘密的配置源</param>
        /// <returns></returns>
        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();

            if (addUserSecrets)
            {
                builder.AddUserSecrets(typeof(AppConfigurations).GetAssembly());
            }

            return builder.Build();
        }
    }
}
