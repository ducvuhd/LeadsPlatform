import { LeadsActionExtends } from './LeadsActionExtends';

export class LeadAction {
    constructor() {
        this.ListType = [];
        this.ListLeadsActionExtends = [];
        this.ListLeadsActionExtendsStr = "";
        this.Index = 0;
        this.Id =0;
    }
    Index: number;
    Id: Number;
    LeadsId: Number;
    MakeId: Number;
    ModelId: Number;
    Type: Number;
    Source: Number;

    CreatedDate: Date;
    CreatedBy: string;
    LastModifiedDate: Date;
    LastModifiedBy: string;
    CampaignId: number;
    PaymentStatus: number;
    ListType: number[];
    ListSaleSetType: number[];
    SaleSetType: number;
    ListLeadsActionExtendsStr: string;
    ListLeadsActionExtends: LeadsActionExtends[];
}
