import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PermissionInit } from 'src/app/models/permission-init';
import { PermissionSave } from 'src/app/models/permission-save';
import { ResponseData } from 'src/app/models/ResponseData';
import { UserSearchResult } from 'src/app/models/user-search-result';
import { UserService } from 'src/app/services/user.service';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';

@Component({
  selector: 'app-user-permission',
  templateUrl: './user-permission.component.html',
  styleUrls: ['./user-permission.component.css'],
  providers: [UserService]
})
export class UserPermissionComponent implements OnInit {
  permissionInit: PermissionInit;
  permissionSave: PermissionSave;
  userName: string;
  lstPermission: UserSearchResult[];
  constructor(private activatedRoute: ActivatedRoute,
              private userService: UserService,
              private toastr: ToastrService,
              private router: Router) { 
                this.permissionSave = new PermissionSave();
                this.lstPermission = [];
              }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.userName = params['username'];
      this.getList();
    });
  }
  getList(): void {
    this.userService.getPermissionByUserName(this.userName).subscribe((res: ResponseData) => {
      if(res.code === ResponseCode.Success) {
        //this.toastr.success(res.message);
        this.lstPermission = res.data as UserSearchResult[];
      } else if(res.code === ResponseCode.Error) {
        this.toastr.error(res.message);
      } else {
        this.toastr.warning(res.message)  
      }
    });
  }
  create(): void {
    this.router.navigateByUrl('/user/add-permission?username=' +this.userName);
  }
  delete_permission(id: number): void{
    if(confirm('Bạn có muốn xóa quyền này ?')) {
      this.userService.deletePermission(id).subscribe((res: ResponseData)=> {
        if(res.code === ResponseCode.Success) {
          this.toastr.success(res.message);
          //this.router.navigateByUrl('/user/permission?username='+this.userName);
          this.getList();
        } else if(res.code === ResponseCode.Error) {
          this.toastr.error(res.message);
        } else {
          this.toastr.warning(res.message);
        }
      });
    }
  }
}
