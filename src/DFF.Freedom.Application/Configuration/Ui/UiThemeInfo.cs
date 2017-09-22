namespace DFF.Freedom.Configuration.Ui
{
    /// <summary>
    /// UI������Ϣ
    /// </summary>
    public class UiThemeInfo
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// css��
        /// </summary>
        public string CssClass { get; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="cssClass">css��</param>
        public UiThemeInfo(string name, string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }
    }
}