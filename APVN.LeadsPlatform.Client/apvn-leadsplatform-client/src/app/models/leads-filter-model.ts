import { LeadsManagermentResult } from './leadsmanagerment-search-result';
import { DatePipe } from '@angular/common';

export class LeadsFilterModel {
    KeyWord: string;
    DealerName: string;
    CreatedDate: Array<Date>;
    CreatedDateArr: Array<string>;
    StartCareDate: Array<Date>;
    StartCareDateArr: Array<string>;
    MakeId: number;
    ModelId: number;
    SecondHand: number;
    CityId: number;
    AssignToId: number;
    IsActive: number;
    CampaignId: number;
    SourceLead: number;
    Status: number;
    Type: number;
    BankId: number;
    PaymentStatus: number;

    pageIndex: number;
    CreateFromDate: DatePipe;
    pageSize: number = 10;
    // result: LeadsManagermentResult[];
    totalRecord: number;
}