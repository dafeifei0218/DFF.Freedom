namespace DFF.Freedom.Configuration.Ui
{
    /// <summary>
    /// 
    /// </summary>
    public class UiThemeInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CssClass { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cssClass"></param>
        public UiThemeInfo(string name, string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}