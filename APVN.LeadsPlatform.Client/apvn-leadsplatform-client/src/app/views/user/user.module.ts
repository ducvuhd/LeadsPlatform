import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';

// Routing
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserRoutingModule } from './user-routing.module';
import { UserEditComponent } from './user-edit.component';
import { UserListComponent } from './user-list.component';
import { CustomModalModule } from '../modal/custom-modal.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { UserPermissionComponent } from './user-permission/user-permission.component';
import { UserPermissionCreateComponent } from './user-permission-create/user-permission-create.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserChangePasswordComponent } from './user-change-password/user-change-password.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
    imports: [
        CommonModule,
        UserRoutingModule,
        HttpClientModule,
        PaginationModule.forRoot(),
        FormsModule,
        CustomModalModule,
        ReactiveFormsModule,
        NgSelectModule,
        BsDropdownModule
    ],
    declarations: [
        UserListComponent,
        UserEditComponent,
        UserPermissionComponent,
        UserPermissionCreateComponent,
        UserChangePasswordComponent,
    ]
})
export class UserModule { }
