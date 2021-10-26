export class AssigLeadToDealerModel{
    constructor(){
        this.leadsId = 0;
        this.dealerId = 0;
        this.leadsActionId = null;
        this.dealerName = '';
        this.lstDealerId = [];
        this.lstDealerName = [];
    }
    leadsId: number;
    dealerId: number;
    leadsActionId: number;
    dealerName: string;
    dealerNote: string;
    leadActionPaymentStatus: number;
    lstDealerId: number[];
    lstDealerName: string[];
}