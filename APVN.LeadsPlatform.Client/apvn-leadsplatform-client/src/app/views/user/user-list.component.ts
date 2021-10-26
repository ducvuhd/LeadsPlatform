import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AdminUserModel } from '../../models/AdminUserModel';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CustomModalComponent } from '../modal/custom-modal.component';
import { AuthService } from '../../services/auth.service';
import { GroupRole, ResponseData } from '../../models/ResponseData';
import { UserIndexModel } from 'src/app/models/user-index-model';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';

@Component({
    selector: 'app-user-list',
    templateUrl: 'user-list.component.html',
    providers: [UserService],
})

export class UserListComponent implements OnInit {
    @ViewChild('modalDialog') modalDialog: CustomModalComponent;
    userList: AdminUserModel[];
    groupRole: GroupRole;
    loginUser: AdminUserModel;
    filter: UserIndexModel;

    constructor(
        private userService: UserService,
        private toastr: ToastrService,
        private router: Router) {
        this.filter = new UserIndexModel();
    }

    ngOnInit(): void {
        this.getList(1);
    }

    getList(pageIndex: number): void {
        if (this.filter.filterKeyword == null) this.filter.filterKeyword = '';
        if (this.filter.userName == null) this.filter.userName = '';
        this.filter.pageIndex = pageIndex;
        this.userService.search(this.filter).subscribe((res: UserIndexModel) => {
            if (res != null) {
                this.filter = res;
            }
        });
    }

    public getUrlOTPPrivateKey(userId: number) {
        this.userService.getUrlOTP(userId).subscribe((res: any) => {
            if (res.Code === ResponseCode.Success) {
                const url = 'otp/otpprivatekey/' + res.Data.UrlEndCode;
                window.open(url);
            } else {
                this.toastr.warning(res.Message);
            }
        });
    }
    // Delete button event
    delete(userId) {
        this.modalDialog.showConfirm('Xác nhận', 'Bạn có chắc chắn muốn xóa người dùng này', 1, userId);
    }
}
