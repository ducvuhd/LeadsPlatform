import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, Input, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { UserLoginModel, AdminUserModel } from '../models/AdminUserModel';
import { ResponseData, GroupRole } from '../models/ResponseData';
import { Constants } from '../utils/constants';
import { HttpService } from './http.service';
import { ToastrService } from 'ngx-toastr';
import { JwtHelper } from '../utils/helpers/jwt.helper';
import { group } from '@angular/animations';
import { ResponseCode } from '../utils/enums/responseCode.enum';

@Injectable({
    providedIn: 'root'
})

export class AuthService {
    constructor(
        private http: HttpClient,
        private httpService: HttpService,
        private toastrService: ToastrService,
        private spinner: NgxSpinnerService,
        private activatedRoute: ActivatedRoute,
        private router: Router
    ) { }

    public static get IsAuthenticated(): boolean {
        const token = window.localStorage.getItem(Constants.TokenKey);
        if (token && token.length > 0 && !JwtHelper.isExpired(token)) {
            return true;
        }

        return false;
    }

    public static getGroupRole(): GroupRole {
        const jsonRoles = window.localStorage.getItem(Constants.RolesKey);
        if (jsonRoles) {
            try {
                const roles = JSON.parse(jsonRoles) as GroupRole;
                return roles;
            } catch (ex) {
                console.log(ex);
            }
        }

        return null;
    }

    // Check role for normal module
    public checkRole(roleName: string, groupRole: GroupRole): boolean {
        if (groupRole.isAdmin || groupRole.isAdminGroup) return true;

        return groupRole.roles.indexOf(roleName) !== -1;
    }

    // Check role and logined in user is owner of object data
    public checkOwnerRole(roleName: string, groupRole: GroupRole, owner: string, loginUserInfo: AdminUserModel): boolean {
        if (groupRole.isAdmin || groupRole.isAdminGroup) return true;

        return owner === loginUserInfo.userName && groupRole.roles.indexOf(roleName) !== -1;
    }

    // Check role for user module
    public checkRoleInUserModule(roleName: string, currentUser: AdminUserModel, groupRole: GroupRole, loginUserInfo: AdminUserModel): boolean {
        if (roleName === '/user/delete') {
            // Nếu roleName là delete và người bị xóa là admin (isAdmin = true) thì không cho xóa
            if (currentUser.userName == Constants.AdminUsername) return false;
            if (currentUser.isAdminGroup) {
                // Chỉ user admin mới được xóa tài khoản thuộc nhóm admin
                return groupRole.isAdmin;
            }
            // Với user thường thì tài khoản admin hoặc tài khoản thuộc nhóm admin được quyền xóa
            if (groupRole.isAdmin || groupRole.isAdminGroup) return true;
            return false;
        }
        if (roleName === '/user/index') return groupRole.isAdmin || groupRole.isAdminGroup;
        if (roleName === '/user/edit') {
            // Tài khoản quản trị có quyền sửa mọi account
            if (groupRole.isAdmin) return true;
            // Nếu là trường hợp thêm mới
            if (currentUser === null) return groupRole.isAdminGroup;
            // Tài khoản admin không ai chỉnh sửa được trừ admin
            if (currentUser.userName == Constants.AdminUsername) return false;
            // Nếu tài khoản sửa thuộc nhóm quản trị thì chỉ edit được tài khoản chính chủ
            if (currentUser.isAdminGroup) return currentUser.userId === loginUserInfo.userId;
            // Tài khoản thường thì nhóm admin hoặc chính chủ sửa được
            if (groupRole.isAdminGroup || currentUser.userId === loginUserInfo.userId) return true;

            return false;
        }

        return false;
    }

    public static get IsAdminRole(): boolean {
        const token = window.localStorage.getItem(Constants.TokenKey);
        try {
            const decodeToken = JwtHelper.decodeToken(token);
            return decodeToken.unique_name === Constants.AdminUsername;
        } catch (ex) {
            console.log(ex);
        }
        return false;
    }

    public static getUserInfo(): AdminUserModel {
        const token = window.localStorage.getItem(Constants.TokenKey);
        try {
            const decodeToken = JwtHelper.decodeToken(token);
            const adminUserModel = new AdminUserModel();
            adminUserModel.userId = decodeToken.certserialnumber;
            adminUserModel.userName = decodeToken.unique_name;
            adminUserModel.isAdminGroup = decodeToken.isadmingroup;
            return adminUserModel;
        } catch (ex) {
            console.log(ex);
        }
        return null;
    }

    public doLogin(userLoginModel: UserLoginModel): Observable<ResponseData>  {
        this.spinner.show();
        // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
        const checksumClient = userLoginModel.username;
        if (checksumClient != null && checksumClient.length > 0) {
            window.localStorage.setItem(Constants.ChecksumClient, checksumClient);
        }
        const httpOptions = {
            withCredentials: true,
            headers: new HttpHeaders({
                'Content-Type': 'application/json charset=utf-8'
            })
        };
        const route = environment.API_URL + '/account/login';
        return this.http.post<any>(route, userLoginModel, httpOptions)
            .pipe( 
                map((res: ResponseData) => {
                    this.spinner.hide();
                    // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
                    if (res !== null && res.code === ResponseCode.Success && res.token !== '') {
                        if (this.httpService.validChecksumClient(res.token)) {
                            window.localStorage.setItem(Constants.TokenKey, res.token);
                            console.log(res.token);
                            this.toastrService.success('Đăng nhập thành công!');
                            let returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'];
                            if (returnUrl === undefined) {
                                returnUrl = '/';
                            }
                            console.log(returnUrl);
                            this.router.navigateByUrl(returnUrl);
                        } else {
                            this.toastrService.error('Checksum client invalid. Please login again!');
                        }
                    } else {
                        if (res !== null && res.message) {
                            this.toastrService.error(res.message);
                        }
                    }
                    return res;
                }),
                catchError((error) => this.httpService.handleError(error))
            );
    }

    public doLogout(): Observable<ResponseData> {
        const route = environment.API_URL + '/account/do-logout';
        return this.httpService.doPost(route).pipe(
            map(res => {
                if (res != null && res.code === ResponseCode.Success) {
                    window.localStorage.removeItem(Constants.TokenKey);
                    window.localStorage.removeItem(Constants.RolesKey);
                    window.localStorage.removeItem(Constants.ChecksumClient);
                    this.httpService.doRedirectLogin(true);
                }
                return res;
            }),
            catchError(() => this.httpService.handleError)
        );
    }
}
