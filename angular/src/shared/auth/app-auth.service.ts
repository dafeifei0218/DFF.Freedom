import { Injectable } from '@angular/core';
import { AppConsts } from '@shared/AppConsts';

/**
 * 应用程序认证服务
 */
@Injectable()
export class AppAuthService {

    /**
     * 退出、注销
     * 
     * @param reload 是否重新登录
     */
    logout(reload?: boolean): void {
        abp.auth.clearToken();
        if (reload !== false) {
            location.href = AppConsts.appBaseUrl;
        }
    }
}