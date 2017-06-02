import { Component, ViewChild, Injector, Output, EventEmitter, ElementRef } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';
import { UserServiceProxy, UpdateUserInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/app-component-base';
import { AppConsts } from '@shared/AppConsts';

import * as _ from "lodash";

@Component({
    selector: 'updateUserModal',
    templateUrl: './update-user-modal.component.html'
})
export class UpdateUserModalComponent extends AppComponentBase {

    @ViewChild('updateUserModal') modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    //是否激活，默认 false未激活
    active: boolean = false;
    //是否保存，默认 false未保存
    saving: boolean = false;
    
    //user: User = null;
    //用户，更新用户输入模型
    user: UpdateUserInput = null;


    //构造函数
    constructor(
        injector: Injector,
        private _userService: UserServiceProxy
    ) {
        super(injector);
    }

    //显示 模态窗口
    show(): void {
        this.active = true;
        this.modal.show();
        this.user = new UpdateUserInput({ isActive: false });
    }

    //保存
    save(): void {

        this.saving = true;
        this._userService.updateUser(this.user)
            .finally(() => { this.saving = false; })
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    //关闭
    close(): void {
        this.active = false;
        this.modal.hide();
    }
}