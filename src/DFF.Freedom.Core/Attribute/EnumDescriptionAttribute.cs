using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DFF.Freedom.Core.Attribute
{
    /// <summary>
    /// 枚举项附加信息特性（Attribute）类。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumDescriptionAttribute : System.Attribute
    {
        #region 公共属性

        /// <summary>
        /// 获取附加信息关键字。
        /// </summary>
        /// <value><see cref="String"/></value>
        public string Key { get; private set; }

        /// <summary>
        /// 获取附加信息。
        /// </summary>
        /// <value><see cref="String"/></value>
        public string Description { get; private set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 新建默认枚举项附加信息特性。
        /// 即 Key 值为“Default”的附加信息。
        /// </summary>
        /// <param name="description">枚举项附加信息字符串。</param>
        public EnumDescriptionAttribute(string description)
            : this(String.Empty, description)
        {
        }

        /// <summary>
        /// 新建枚举项多样描述特性（Attribute）
        /// </summary>
        /// <param name="key">描述关键字。</param>
        /// <param name="description">枚举项描述信息。</param>
        /// <exception cref="ParaNullException">当参数 <paramref name="key"/> 接收空引用(<c>null</c>)时发生异常。</exception>
        /// <exception cref="StringParaEmptyOrNullException">
        /// 当参数 <paramref name="description"/> 接收空字符串（<see cref="String.Empty"/>）或空引用（<c>null</c>）时发生异常。
        /// </exception>
        public EnumDescriptionAttribute(string key, string description)
        {
            #region 参数验证

            if (key == null)
                //throw ParaUtility.CanNotNullObject("key");
                throw new ArgumentNullException("key");
            if (String.IsNullOrEmpty(description))
                //throw ParaUtility.CanNotEmptyOrNullString("description");
                throw new ArgumentNullException("description");

            #endregion

            this.Key = key;
            this.Description = description;
        }

        #endregion
    }
}
