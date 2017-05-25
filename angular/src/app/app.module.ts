import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule } from '@angular/http';

import { ModalModule } from 'ng2-bootstrap';

//应用程序路由模块
import { AppRoutingModule } from './app-routing.module';
//应用程序组件
import { AppComponent } from './app.component';

import { AbpModule, ABP_HTTP_PROVIDER } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

//Api基础Url
import { API_BASE_URL } from '@shared/service-proxies/service-proxies';
//应用程序常亮
import { AppConsts } from '@shared/AppConsts';

//首页 组件
import { HomeComponent } from '@app/home/home.component';
//关于 组件
import { AboutComponent } from '@app/about/about.component';
//用户 组件
import { UsersComponent } from '@app/users/users.component';
//创建用户模态窗口 组件
import { CreateUserModalComponent } from '@app/users/create-user-modal.component';
//租户 组件
import { TenantsComponent } from '@app/tenants/tenants.component';
//创建租户模态窗口 组件
import { CreateTenantModalComponent } from '@app/tenants/create-tenant-modal.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        AboutComponent,
        UsersComponent,
        CreateUserModalComponent,
        TenantsComponent,
        CreateTenantModalComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpModule,
        JsonpModule,
        ModalModule.forRoot(),
        AbpModule,
        AppRoutingModule,
        ServiceProxyModule,
        SharedModule
    ],
    providers: [

    ]
})
export class AppModule { }