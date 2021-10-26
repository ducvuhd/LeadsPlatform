import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ChangePassword } from 'src/app/models/change-password';
import { ResponseData } from 'src/app/models/ResponseData';
import { UserService } from 'src/app/services/user.service';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';

@Component({
  selector: 'app-user-change-password',
  templateUrl: './user-change-password.component.html',
  styleUrls: ['./user-change-password.component.css'],
  providers: [UserService]
})
export class UserChangePasswordComponent implements OnInit {
  changePass: ChangePassword;
  constructor(private router: Router,
              private userService: UserService,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.changePass = new ChangePassword();
  }
  changePassWord(): void {
    if(this.changePass.newPassword !== '' && this.changePass.newPassword !== this.changePass.confirmNewPassword) {
      this.toastr.warning('Thông tin mật khẩu mới và thông tin xác nhận mật khẩu không khớp nhau!');
    } else {
      this.userService.resetPassword(this.changePass).subscribe( (response: ResponseData) => {
        if (response.code === ResponseCode.Success) {
          this.toastr.success(response.message);
          this.router.navigateByUrl('/');
        } else if (response.code === ResponseCode.Warning) {
          this.toastr.warning(response.message);
        } else {
          this.toastr.error(response.message);
        }
      });
    }
  }
}
