namespace DFF.Freedom.Configuration.Ui
{
    /// <summary>
    /// UI主题信息
    /// </summary>
    public class UiThemeInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// css类
        /// </summary>
        public string CssClass { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="cssClass">css类</param>
        public UiThemeInfo(string name, string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}