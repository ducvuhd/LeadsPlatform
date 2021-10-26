import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { AdminUserModel } from '../../models/AdminUserModel';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { MatchValidator } from '../../utils/helpers/match.validator';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../services/auth.service';
import { ResponseData, SystemCode } from '../../models/ResponseData';
import { ResponseCode } from 'src/app/utils/enums/responseCode.enum';
import { RoleModel } from 'src/app/models/RoleModel';
import { KeyValueObject } from 'src/app/utils/KeyValueObject';

@Component({
    selector: 'app-user-edit',
    templateUrl: 'user-edit.component.html',
    providers: [UserService],

})

export class UserEditComponent implements OnInit {
    userModel: AdminUserModel;
    registerForm: FormGroup;
    submitted = false;
    isAdminRole: boolean;
    editMode: string;
    edit: boolean = false;
    selectedGroupRoles: [];
    lstRoleUser: any[];
    groupRoles: RoleModel[] = [];
    lstRoleId: number[] = [];
    //groupRoles: AdminGroupModel[];

    constructor(
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private formBuilder: FormBuilder,
        private userService: UserService,
        private toastr: ToastrService
    ) {
        //this.userModel = new AdminUserModel();
    }

    ngOnInit(): void {
        this.isAdminRole = AuthService.IsAdminRole;
        // const userId = parseInt(this.activatedRoute.snapshot.queryParams['userId']);
        let userId = 0;
        this.activatedRoute.params.subscribe(params => {
            userId = +params['userId'];
        });
        //var userModel = null;

        if (userId > 0) {
            this.registerForm = this.formBuilder.group({
                userId: [0],
                userName: ['', Validators.required],
                displayName: '',
                email: '',
                mobile: '',
                status: [0, Validators.required],
                role: ''
            });
            this.editMode = "Update";
            this.edit = true;
            this.userService.getToEdit(userId).subscribe(response => {
                if (response.code == SystemCode.Success) {
                    this.loadForm(response.data.user);
                    this.lstRoleUser = response.data.lstRole;
                    this.userService.getGroupRoles().subscribe((res: ResponseData) => {
                        if (res.code == ResponseCode.Success) {
                            res.data.forEach(x => {
                                let role = new RoleModel();
                                role.RoleId = x.Key;
                                role.RoleName = x.Value;
                                role.IsSelected = this.lstRoleUser.filter(y => y.roleId == role.RoleId).length > 0 ? true : false;
                                this.groupRoles.push(role);
                                this.lstRoleId.push(role.RoleId);
                            });
                        }
                    });
                } else {
                    this.toastr.error("UserId không hợp lệ");
                    this.router.navigate(['/user/list']);
                }
            }, error => { this.toastr.error(error); this.router.navigate(['/user/list']); });
        }
        else {
            this.registerForm = this.formBuilder.group({
                userId: [0],
                userName: ['', Validators.required],
                password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(24)]],
                confirmPassword: ['', Validators.required],
                displayName: '',
                email: '',
                mobile: '',
                status: [0, Validators.required],
                role: ''
            }, {
                validator: MatchValidator('password', 'confirmPassword')
            });
            this.editMode = "Add";
            this.edit = false;
            this.userService.getGroupRoles().subscribe(response => {
                const res = response as ResponseData;
                if (res && res.code == ResponseCode.Success) {
                    res.data.forEach(x => {
                        let role = new RoleModel();
                        role.RoleId = x.Key;
                        role.RoleName = x.Value;
                        role.IsSelected = false;
                        this.groupRoles.push(role);
                    });
                } else {
                    this.toastr.error("UserId không hợp lệ");
                    this.router.navigate(['/user/list']);
                }
            }, error => { this.toastr.error(error); this.router.navigate(['/user/list']); });
        }
    }
    loadForm(userModel: AdminUserModel) {
        this.registerForm.patchValue({
            userId: userModel.userId,
            userName: userModel.userName,
            //password: userModel.password,
            //confirmPassword: userModel.confirmPassword,
            displayName: userModel.displayName,
            email: userModel.email,
            mobile: userModel.mobile,
            status: userModel.status
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }

    onSubmit() {
        this.submitted = true;
        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        this.lstRoleId = [];
        for (const item of this.groupRoles) {
            if (item.IsSelected) {
                this.lstRoleId.push(item.RoleId);
            }
        }
        if (this.lstRoleId.length == 0) {
            return;
        }
        this.registerForm.patchValue({
            role: this.lstRoleId.join()
        });
        this.userService.update(this.registerForm.getRawValue()).subscribe(response => {
            if (response.code == ResponseCode.Success) {
                if (this.registerForm.value.userId === 0) {
                    this.toastr.success("Bạn đã thêm tài khoản quản trị thành công.");
                }
                else {
                    this.toastr.success("Bạn đã sửa tài khoản quản trị thành công.");
                }
            } else {
                this.toastr.error(response);
            }
        }, error => { this.toastr.error(error); });
    }

    onReset() {
        this.submitted = false;
        this.registerForm.reset();
    }
}
