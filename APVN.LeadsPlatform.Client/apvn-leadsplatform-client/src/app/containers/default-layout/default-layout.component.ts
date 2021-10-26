import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { PermissionHelper } from 'src/app/utils/helpers/permission-helper';
import { AuthService } from '../../services/auth.service';
import { HttpService } from '../../services/http.service';
import { Constants } from '../../utils/constants';
import { navItems } from '../../_nav';

@Component({
    selector: 'app-dashboard',
    templateUrl: './default-layout.component.html',
    styleUrls: ['./default-layout.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class DefaultLayoutComponent implements OnInit {
    public sidebarMinimized = false;
    public navItems = navItems;
    public currUserName = null;

    constructor(private httpService: HttpService, private authService: AuthService, private router: Router) {
    }

    ngOnInit(): void {
        if (!AuthService.IsAuthenticated) {
            this.httpService.doRedirectLogin();
            return;
        } else {
            this.currUserName = window.localStorage.getItem(Constants.ChecksumClient);
            this.navItems = PermissionHelper.getMenuItems(this.navItems);
        }
    }

    public doLogout() {
        this.authService.doLogout().subscribe();
    }

    toggleMinimize(e) {
        this.sidebarMinimized = e;
    }
}
