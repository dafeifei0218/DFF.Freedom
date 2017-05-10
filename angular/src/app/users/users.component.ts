import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { UserServiceProxy, UserListDto } from '@shared/service-proxies/service-proxies';

import { CreateUserModalComponent } from './create-user-modal.component';
import { UpdateUserModalComponent } from './update-user-modal.component';

@Component({
    templateUrl: './users.component.html',
    animations: [appModuleAnimation()]
})
//用户组件
export class UsersComponent extends AppComponentBase implements OnInit {

    @ViewChild('createUserModal') createUserModal: CreateUserModalComponent;
    users: UserListDto[] = [];

    constructor(
        injector: Injector,
        private _userService: UserServiceProxy
    ) {
        super(injector);
    }

    //初始化
    ngOnInit() {
        this.getUsers();
    }

    //获取用户集合
    getUsers(): void {
        this._userService.getUsers()
            .subscribe((result) => {
                this.users = result.items;
            });
    }

    //打开 创建用户 模态窗口
    createUser(): void {
        this.createUserModal.show();
    }

    //打开 修改用户 模态窗口
    updateUser(): void {
        this.updateUserModal.show();
    }

    //删除用户
    deleteUser(): void {

    }
}