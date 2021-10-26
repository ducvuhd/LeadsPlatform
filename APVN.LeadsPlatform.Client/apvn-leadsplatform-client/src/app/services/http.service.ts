import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import {
    HttpClient,
    HttpErrorResponse,
    HttpHeaders,
    HttpParams
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { JwtHelper } from '../utils/helpers/jwt.helper';
import { Constants } from '../utils/constants';
import { KeyValueObject } from '../utils/KeyValueObject';

@Injectable({
    providedIn: 'root'
})
export class HttpService {
    constructor(
        private http: HttpClient,
        private toastrService: ToastrService,
        private router: Router,
        private spinner: NgxSpinnerService
    ) { }

    public validChecksumClient(token: string) {
        const checksumClient = window.localStorage.getItem(Constants.ChecksumClient);
        if (token && token.length > 0) {
            const decode = JwtHelper.decodeToken(token);
            if (decode) {
                const uniqueName = decode.unique_name.toLowerCase();
                if (uniqueName && uniqueName.length > 0 && checksumClient && uniqueName === checksumClient.toLowerCase()) {
                    return true;
                }
            }
        }
        return false;
    }

    public doPost(route, model = null, params = null, isShowLoading = true): Observable<any> {
        if (isShowLoading) {
            this.spinner.show();
        }

        const authorizationKey = Constants.TokenKey;
        const token = window.localStorage.getItem(authorizationKey);
        if (token === null || token.length <= 0 || JwtHelper.isExpired(token)) {
            if (isShowLoading) {
                this.spinner.hide();
            }
            this.doRedirectLogin();
            return new Observable<any>();
        }
        // if (params !== null) {
        //     params = this.buildQueryStrings(params);
        // }
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Authorization: token
            }),
            params: params
        };
        // const httpRequest = model !== null
        //     ? this.http.post<any>(route, JSON.stringify(model), httpOptions)
        //     : this.http.post<any>(route, null, httpOptions);
        const httpRequest = this.http.post<any>(route, model, httpOptions);
        return httpRequest.pipe(
            map(res => {
                if (isShowLoading) {
                    this.spinner.hide();
                }
                if (res != null && res.RefreshToken && res.RefreshToken === true) {
                    // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
                    if (this.validChecksumClient(res.Token)) {
                        window.localStorage.setItem(Constants.TokenKey, res.Token);
                        this.callDeleteLockRefreshToken(res.Token);
                        res.Token = '';
                    } else {
                        this.toastrService.error('Checksum client invalid. Please login again!');
                        setTimeout(() => {
                            window.localStorage.removeItem(Constants.TokenKey);
                        }, 2000);
                    }
                }
                window.localStorage.removeItem(Constants.RetryCount401);
                return res;
            }),
            catchError((error) => this.handleError(error))
        );
    }

    public doPostFile(route, fileToUpload: File, lstKeyValue=[] as KeyValueObject[]): Observable<any> {
        const authorizationKey = Constants.TokenKey;
        const token = window.localStorage.getItem(authorizationKey);
        const formData: FormData = new FormData();
        formData.append(fileToUpload.name, fileToUpload);
        if (lstKeyValue.length > 0) {
            for (const item of lstKeyValue) {
                formData.append(item.Key, item.Value);
            }
        }
        //formData.append(parameterName, fileToUpload);
        const httpOptions = {
            headers: new HttpHeaders({
                Authorization: token
            })
        };
        return this.http
            .post<any>(route, formData, httpOptions)
            .pipe(
                map(res => {
                    if (res != null && res.RefreshToken && res.RefreshToken === true) {
                        // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
                        if (this.validChecksumClient(res.Token)) {
                            window.localStorage.setItem(Constants.TokenKey, res.Token);
                            this.callDeleteLockRefreshToken(res.Token);
                            res.Token = '';
                        } else {
                            this.toastrService.error('Checksum client invalid. Please login again!');
                            setTimeout(() => {
                                window.localStorage.removeItem(Constants.TokenKey);
                            }, 2000);
                        }
                    }
                    window.localStorage.removeItem(Constants.RetryCount401);
                    return res;
                }),
                catchError((error) => this.handleError(error))
            );
    }

    public doPostFileMulti(route, fileToUpload: FileList): Observable<any> {
        const authorizationKey = Constants.TokenKey;
        const token = window.localStorage.getItem(authorizationKey);
        const formData: FormData = new FormData();
        for (let i = 0; i < fileToUpload.length; i++) {
            formData.append(fileToUpload[i].name, fileToUpload[i]);
        }
        const httpOptions = {
            headers: new HttpHeaders({
                Authorization: token
            })
        };
        return this.http
            .post<any>(route, formData, httpOptions)
            .pipe(
                map(res => {
                    if (res != null && res.RefreshToken && res.RefreshToken === true) {
                        // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
                        if (this.validChecksumClient(res.Token)) {
                            window.localStorage.setItem(Constants.TokenKey, res.Token);
                            this.callDeleteLockRefreshToken(res.Token);
                            res.Token = '';
                        } else {
                            this.toastrService.error('Checksum client invalid. Please login again!');
                            setTimeout(() => {
                                window.localStorage.removeItem(Constants.TokenKey);
                            }, 2000);
                        }
                    }
                    window.localStorage.removeItem(Constants.RetryCount401);
                    return res;
                }),
                catchError((error) => this.handleError(error))
            );
    }
    public doPostNotNeedValid(route, model = null, params = null, isShowLoading = true): Observable<any> {
        if (isShowLoading) {
            this.spinner.show();
        }

        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            }),
            params: params
        };
      
        const httpRequest = this.http.post<any>(route, model, httpOptions);
        return httpRequest.pipe(
            map(res => {
                if (isShowLoading) {
                    this.spinner.hide();
                }
                return res;
            }),
            catchError((error) => this.handleError(error))
        );
    }
    public doGet(route, params = null, isShowLoading = true): Observable<any> {
        if (isShowLoading) {
            this.spinner.show();
        }
        const authorizationKey = Constants.TokenKey;
        const token = window.localStorage.getItem(authorizationKey);
        if (token === null || token.length <= 0 || JwtHelper.isExpired(token)) {
            if (isShowLoading) {
                this.spinner.hide();
            }
            this.doRedirectLogin();
            return new Observable<any>();
        }
        /*if (params !== null) {
            params = this.buildQueryStrings(params);
        }*/
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json charset=utf-8' ,
                Authorization: token
            }),
            params: params
        };

        return this.http.get<any>(route, httpOptions).pipe(
            map(res => {
                if (isShowLoading) {
                    this.spinner.hide();
                }
                if (res != null && res.RefreshToken && res.RefreshToken === true) {
                    // xử lý lưu thêm checksumClient = username để phòng server lỗi trả ra token của user khác
                    if (this.validChecksumClient(res.Token)) {
                        window.localStorage.setItem(Constants.TokenKey, res.Token);
                        this.callDeleteLockRefreshToken(res.Token);
                        res.Token = '';
                    } else {
                        this.toastrService.error('Checksum client invalid. Please login again!');
                        setTimeout(() => {
                            window.localStorage.removeItem(Constants.TokenKey);
                        }, 2000);
                    }
                }
                window.localStorage.removeItem(Constants.RetryCount401);
                return res;
            }),
            catchError((error) => this.handleError(error))
        );
    }

    public handleError(error: HttpErrorResponse) {
        this.spinner.hide();
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        } else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            const err =
                `Backend returned code ${error.status}, ` + `body was: ${error.message}`;
            console.error(err);
        }
        if (error.status === 401) { this.doRedirectLogin(); }
        if (error.status === 405) { this.doRedirect405(); }
        // return an observable with a user-facing error message
        return throwError('Something bad happened; please try again later.');
    }

    public doRedirectLogin(isLogout = false) {
        window.localStorage.removeItem(Constants.TokenKey);
        const isNotSSO = window.localStorage.getItem(Constants.IsNotSSO);
        if (environment.loginSSO === true && !isLogout && (isNotSSO === null || isNotSSO === '0')) {
            window.localStorage.setItem(Constants.ReturnUrl, this.router.url);
            window.location.href = environment.loginUrl + '?returnUrl=' + environment.CLIENT_URL;
            return;
        }

        this.router.navigate(['/login'], {
            queryParams: { returnUrl: this.router.url }
        });
    }

    public doRedirect405() {
        this.router.navigate(['/405']).then(e => {
            if (e) {
                console.log('Navigation is successful!');
            } else {
                console.log('Navigation has failed!');
            }
        });
    }

    private buildQueryParams(source: Object): HttpParams {
        let target: HttpParams = new HttpParams();
        Object.keys(source).forEach((key: string) => {
            const value: string | number | boolean | Date = source[key];
            if ((typeof value !== 'undefined') && (value !== null)) {
                target = target.append(key, value.toString());
            }
        });
        return target;
    }

    private buildQueryStrings(source: Object) {
        let params = new HttpParams();
        //let target = '';
        //let isFirst = true;
        Object.keys(source).forEach((key: string) => {
            const value: string | number | boolean | Date = source[key];
            params.append(key, value.toString());
            // if ((typeof value !== 'undefined') && (value !== null)) {
            //     target += (isFirst ? '?' : '&') + key + '=' + value.toString();
            // }
            // isFirst = false;
        });
        //return target;
        return params;
    }

    private callDeleteLockRefreshToken(token: string) {
        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
        const model = new ChecksumKeyModel();
        model.Token = token;
        const route = environment.API_URL + '/user/deletelockrefreshtoken';
        console.log(route);
        this.http.post<any>(route, JSON.stringify(model), httpOptions).pipe(
            map(res => {
                //         console.log(res);
            }),
            catchError((error) => this.handleError(error))
        ).subscribe();
    }
}

export class ChecksumKeyModel {
    public Token: string;
}
