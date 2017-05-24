import { IsTenantAvailableOutputState } from '@shared/service-proxies/service-proxies';

/*
* 应用程序租户可用状态
*/
export class AppTenantAvailabilityState {
    //可用
    static Available: number = IsTenantAvailableOutputState._1;
    //未激活
    static InActive: number = IsTenantAvailableOutputState._2;
    //未找到
    static NotFound: number = IsTenantAvailableOutputState._3;
}