import { HttpService } from './http.service';
import { Observable } from 'rxjs';
import { ResponseData } from '../models/ResponseData';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserIndexModel } from '../models/user-index-model';
import { PermissionInit } from '../models/permission-init';
import { DepartmentInfo } from '../models/department-info';
import { PermissionSave } from '../models/permission-save';
import { ChangePassword } from '../models/change-password';
import { UrlOtp } from '../models/UrlOtp';

@Injectable({
    providedIn:"root"
})
export class UserService {
    constructor(private httpService: HttpService) {
    }

    getToEdit(userId: number) {
        return this.httpService.doGet(environment.API_URL + '/user/get-to-edit', { 'userId': userId });
    }

    getList(params): Observable<ResponseData> {
        return this.httpService.doGet(environment.API_URL + '/user/index', params);
    }

    update(userModel: any) {
        return this.httpService.doPost(environment.API_URL + '/user/save', userModel);
    }

    delete(userId: any) {
        return this.httpService.doPost(environment.API_URL + '/user/delete', { 'userId': userId });
    }

    getGroupRoles() {
        return this.httpService.doGet(environment.API_URL + '/user/get-group-roles', null, false);
    }

    search(model: UserIndexModel): Observable<UserIndexModel> {
        return this.httpService.doPost(environment.API_URL + '/user/get-list', model);
    }
    initPermission(userName: string): Observable<PermissionInit> {
        return this.httpService.doGet(environment.API_URL + '/user/init-permission?userName=' + userName);
    }
    deletePermission(id: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/user/delete-permission/' + id);
    }
    getDepartmentByCityId(cityId: number): Observable<DepartmentInfo> {
        return this.httpService.doGet(environment.API_URL + '/user/get-department/' + cityId);
    }
    save(model: PermissionSave): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/user/save', model);
    }
    getPermissionByUserName(userName: string): Observable<ResponseData> {
        return this.httpService.doGet(environment.API_URL + '/user/get-list-permission?userName=' + userName);
    }
    resetPassword(model: ChangePassword): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/user/change-password', model);
    }
    getUrlOTP(userId: number): Observable<ResponseData> {
        return this.httpService.doPost(environment.API_URL + '/user/geturl/' + userId);
    }
    getOTP(urlOtp: UrlOtp): Observable<ResponseData> {
        return this.httpService.doPostNotNeedValid(environment.API_URL + '/user/getotpprivatekey', urlOtp);
    }
}
