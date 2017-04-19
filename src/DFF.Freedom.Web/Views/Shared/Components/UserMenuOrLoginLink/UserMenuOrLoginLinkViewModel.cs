using DFF.Freedom.Sessions.Dto;

namespace DFF.Freedom.Web.Views.Shared.Components.UserMenuOrLoginLink
{
    /// <summary>
    /// �û��˵����¼���� ��ͼģ��
    /// </summary>
    public class UserMenuOrLoginLinkViewModel
    {
        /// <summary>
        /// ��¼��Ϣ
        /// </summary>
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }
        /// <summary>
        /// �Ƿ����ö��⻧
        /// </summary>
        public bool IsMultiTenancyEnabled { get; set; }

        /// <summary>
        /// ��ȡ��ʾ��¼��
        /// </summary>
        /// <returns></returns>
        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }
    }
}