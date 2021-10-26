import { Component, Input, TemplateRef } from '@angular/core';
//import { ngxLoadingAnimationTypes } from 'ngx-loading';
import { UserLoginModel } from '../../models/AdminUserModel';
import { AuthService } from '../../services/auth.service';
import { Title } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { Constants } from '../../utils/constants';
const PrimaryWhite = '#ffffff';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-dashboard',
    templateUrl: 'login.component.html'
})
export class LoginComponent {
    @Input() _userLoginModel: UserLoginModel;
    public loadingTemplate: TemplateRef<any>;
    public primaryColour = PrimaryWhite;

    constructor(private authService: AuthService, private titleService: Title,  private router: Router) {
        this.titleService.setTitle(`Login - ${environment.APP_TITLE}`);
        this._userLoginModel = new UserLoginModel();
    }

    public login() {
        this.authService.doLogin(this._userLoginModel).subscribe();
    }
}
