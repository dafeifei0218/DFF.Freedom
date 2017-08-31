namespace DFF.Freedom.Web.Models.Common.Modals
{
    /// <summary>
    /// 模态框头部 视图模型
    /// </summary>
    public class ModalHeaderViewModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">标题</param>
        public ModalHeaderViewModel(string title)
        {
            Title = title;
        }
    }
}
