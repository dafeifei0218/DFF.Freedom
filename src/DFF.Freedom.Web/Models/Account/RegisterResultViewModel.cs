namespace DFF.Freedom.Web.Models.Account
{
    /// <summary>
    /// 注册结果 视图模型
    /// </summary>
    public class RegisterResultViewModel
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string TenancyName { get; set; }
        
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
        
        /// <summary>
        /// 名称和真是名称
        /// </summary>
        public string NameAndSurname { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool IsActive { get; set; }
    }
}