import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';

// Routing
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CustomModalModule } from '../modal/custom-modal.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { LeadsmanagermentRoutingModule } from './leadsmanagerment-routing.module';
import { LeadsmanagermentListComponent } from './leadsmanagerment-list/leadsmanagerment-list.component';
import { LeadsmanagermentAddLeadComponent } from './leadsmanagerment-add-lead/leadsmanagerment-add-lead.component'
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule, MatDatetimePickerInputEvent } from '@angular-material-components/datetime-picker';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSelectModule } from '@angular/material/select';
import { MAT_FORM_FIELD_DEFAULT_OPTIONS } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxPaginationModule } from 'ngx-pagination';
import { LeadsmanagermentDetailComponent } from './leadsmanagerment-detail/leadsmanagerment-detail.component';
import { CampaignComponent } from './campaign/campaign.component'; // <-- import the module
import { ModalModule } from 'ngx-bootstrap/modal';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { CurrencyMaskInputMode, NgxCurrencyModule } from 'ngx-currency';
import { DealergetleadComponent } from './dealergetlead/dealergetlead.component';
export const customCurrencyMaskConfig = {
    align: "left",
    allowNegative: true,
    allowZero: true,
    decimal: ".",
    precision: 0,
    prefix: "",
    suffix: "VNĐ",
    thousands: ",",
    nullable: true,
    min: null,
    max: null,
    inputMode: CurrencyMaskInputMode.FINANCIAL
};
@NgModule({
    declarations: [
        LeadsmanagermentListComponent,
        LeadsmanagermentDetailComponent,
        LeadsmanagermentAddLeadComponent,
        CampaignComponent,
        DealergetleadComponent,
    ],
    entryComponents: [LeadsmanagermentListComponent, LeadsmanagermentDetailComponent, CampaignComponent],
    bootstrap: [LeadsmanagermentListComponent, LeadsmanagermentDetailComponent, CampaignComponent],
    providers: [
        { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
    ],
    imports: [
        CommonModule,
        LeadsmanagermentRoutingModule,
        BsDropdownModule,
        HttpClientModule,
        MatSelectModule,
        MatDialogModule,
        PaginationModule.forRoot(),
        FormsModule,
        CustomModalModule,
        ReactiveFormsModule,
        NgSelectModule,
        MatDatepickerModule,
        MatInputModule,
        MatNativeDateModule,
        OwlDateTimeModule,
        OwlNativeDateTimeModule,
        NgxPaginationModule,
        ModalModule.forRoot(),
        NgxCurrencyModule.forRoot(customCurrencyMaskConfig),
        CollapseModule.forRoot(),
    ]
})
export class LeadsmanagermentModule {

}
