//App常量
export class AppConsts {

    //远程服务基础Url
    static remoteServiceBaseUrl: string;
    //应用程序基础Url
    static appBaseUrl: string;

    //用户管理
    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    //本地化
    static readonly localization = {
        defaultLocalizationSourceName: 'Freedom'
    };

    //授权
    static readonly authorization = {
        encrptedAuthTokenName: 'enc_auth_token'
    };
}