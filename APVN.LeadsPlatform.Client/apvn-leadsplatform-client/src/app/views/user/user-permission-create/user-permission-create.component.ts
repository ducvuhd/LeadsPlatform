import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PermissionInit } from 'src/app/models/permission-init';
import { PermissionSave } from 'src/app/models/permission-save';
import { ResponseData } from 'src/app/models/ResponseData';
import { UserService } from 'src/app/services/user.service';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';

@Component({
  selector: 'app-user-permission-create',
  templateUrl: './user-permission-create.component.html',
  styleUrls: ['./user-permission-create.component.css'],
  providers: [UserService]
})
export class UserPermissionCreateComponent implements OnInit {
  permissionInit: PermissionInit;
  permissionSave: PermissionSave;
  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private userService: UserService,
              private toastr: ToastrService) {
                this.permissionSave = new PermissionSave();
              }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      const userName = params['username'];
      this.userService.initPermission(userName).subscribe((initObj: PermissionInit) => {
        if(initObj != null) {
          this.permissionInit = initObj;
          if(this.permissionInit.lstCity.length > 0) {
            this.permissionSave.cityId = this.permissionInit.lstCity[0].id;
          }
          this.permissionSave.userName = initObj.userName;
        }
      });
    });
  }
  save(): void {
    this.permissionSave.cityId = parseInt(this.permissionSave.cityId.toString(), 10);
    this.permissionSave.departmentId = parseInt(this.permissionSave.departmentId.toString(), 10);
    this.permissionSave.roleId = parseInt(this.permissionSave.roleId.toString(), 10);

    this.userService.save(this.permissionSave).subscribe((res: ResponseData)=> {
      if(res.code === ResponseCode.Success) {
        this.toastr.success(res.message);
        this.router.navigateByUrl('/user/permission?username='+this.permissionSave.userName);
      } else if(res.code === ResponseCode.Error) {
        this.toastr.error(res.message);
      } else {
        this.toastr.warning(res.message);
      }
    });
  }
  changeCity(newCityId: number): void {
    this.userService.getDepartmentByCityId(newCityId).subscribe((data: any) => {
      this.permissionInit.lstDepartment = data.lstDepartment;
    })
  }

}
