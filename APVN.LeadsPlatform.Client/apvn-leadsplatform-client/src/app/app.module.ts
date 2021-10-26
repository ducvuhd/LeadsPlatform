import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { LocationStrategy, /*HashLocationStrategy, */PathLocationStrategy } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { HttpErrorHandler } from './http-error-handler.service';
import { MessageService } from './message.service';
import { FormsModule } from '@angular/forms';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from "ngx-spinner";
//import { NotifierModule, NotifierOptions } from 'angular-notifier';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};

import { AppComponent } from './app.component';

// Import containers
import { DefaultLayoutComponent } from './containers';

import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginComponent } from './views/login/login.component';
//import { RegisterComponent } from './views/register/register.component';

const APP_CONTAINERS = [
    DefaultLayoutComponent
];

import {
    AppAsideModule,
    AppBreadcrumbModule,
    AppHeaderModule,
    AppFooterModule,
    AppSidebarModule,
} from '@coreui/angular';

// Import routing module
import { AppRoutingModule } from './app.routing';

// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
//import { ChartsModule } from 'ng2-charts';
import { NgxPaginationModule } from 'ngx-pagination';
import { UserModule } from './views/user/user.module';
import { LeadsmanagermentModule } from './views/leadsmanagerment/leadsmanagerment.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { OtpComponent } from './views/otp/otp.component';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        AppAsideModule,
        AppBreadcrumbModule.forRoot(),
        AppFooterModule,
        AppHeaderModule,
        AppSidebarModule,
        PerfectScrollbarModule,
        BsDropdownModule.forRoot(),
        TabsModule.forRoot(),
        //ChartsModule,
        HttpClientModule,
        FormsModule,
        ToastrModule.forRoot({
            timeOut: 2000,
            positionClass: 'toast-top-right',
            preventDuplicates: true,
        }),
        NgxSpinnerModule,
        //NotifierModule.withConfig(customNotifierOptions),
        //NgxLoadingModule.forRoot({})
        HttpClientModule,
        NgxPaginationModule,
        UserModule,
        LeadsmanagermentModule,
    ],
    declarations: [
        AppComponent,
        ...APP_CONTAINERS,
        P404Component,
        P500Component,
        LoginComponent,
        //RegisterComponent
        OtpComponent,
    ],
    providers: [
        HttpErrorHandler,
        MessageService,
        {
            provide: LocationStrategy,
            //useClass: HashLocationStrategy
            useClass: PathLocationStrategy
        }
    ],
    bootstrap: [AppComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    
})
export class AppModule { }
