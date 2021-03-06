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
            // N???u roleName l?? delete v?? ng?????i b??? x??a l?? admin (isAdmin = true) th?? kh??ng cho x??a
            if (currentUser.userName == Constants.AdminUsername) return false;
            if (currentUser.isAdminGroup) {
                // Ch??? user admin m???i ???????c x??a t??i kho???n thu???c nh??m admin
                return groupRole.isAdmin;
            }
            // V???i user th?????ng th?? t??i kho???n admin ho???c t??i kho???n thu???c nh??m admin ???????c quy???n x??a
            if (groupRole.isAdmin || groupRole.isAdminGroup) return true;
            return false;
        }
        if (roleName === '/user/index') return groupRole.isAdmin || groupRole.isAdminGroup;
        if (roleName === '/user/edit') {
            // T??i kho???n qu???n tr??? c?? quy???n s???a m???i account
            if (groupRole.isAdmin) return true;
            // N???u l?? tr?????ng h???p th??m m???i
            if (currentUser === null) return groupRole.isAdminGroup;
            // T??i kho???n admin kh??ng ai ch???nh s???a ???????c tr??? admin
            if (currentUser.userName == Constants.AdminUsername) return false;
            // N???u t??i kho???n s???a thu???c nh??m qu???n tr??? th?? ch??? edit ???????c t??i kho???n ch??nh ch???
            if (currentUser.isAdminGroup) return currentUser.userId === loginUserInfo.userId;
            // T??i kho???n th?????ng th?? nh??m admin ho???c ch??nh ch??? s???a ???????c
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
        // x??? l?? l??u th??m checksumClient = username ????? ph??ng server l???i tr??? ra token c???a user kh??c
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
                    // x??? l?? l??u th??m checksumClient = username ????? ph??ng server l???i tr??? ra token c???a user kh??c
                    if (res !== null && res.code === ResponseCode.Success && res.token !== '') {
                        if (this.httpService.validChecksumClient(res.token)) {
                            window.localStorage.setItem(Constants.TokenKey, res.token);
                            console.log(res.token);
                            this.toastrService.success('????ng nh???p th??nh c??ng!');
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
