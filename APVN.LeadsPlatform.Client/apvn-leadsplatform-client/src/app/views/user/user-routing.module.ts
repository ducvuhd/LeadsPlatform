import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './user-list.component';
import { UserEditComponent } from './user-edit.component';
import { UserPermissionComponent } from './user-permission/user-permission.component';
import { UserPermissionCreateComponent } from './user-permission-create/user-permission-create.component';
import { UserChangePasswordComponent } from './user-change-password/user-change-password.component';

const routes: Routes = [
    {
        path: '',
        component: UserListComponent,
        data: {
            title: 'Danh sách người dùng'
        }
    },
    {
        path: 'list',
        component: UserListComponent,
        data: {
            title: 'Danh sách người dùng'
        },
    },
    {
        path: 'permission',
        component: UserPermissionComponent,
        data: {
            title: 'Quyền của người dùng'
        },
    },
    {
        path: 'add-permission',
        component: UserPermissionCreateComponent,
        data: {
            title: 'Tạo mới quyền'
        }
    },
    {
        path: 'change-password',
        component: UserChangePasswordComponent,
        data: {
            title: 'Thay đổi mật khẩu'
        }
    },
    {
        path: 'edit/:userId',
        component: UserEditComponent,
        data: {
            title: 'Tạo mới, sửa tài khoản'
        }
    },
    
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
