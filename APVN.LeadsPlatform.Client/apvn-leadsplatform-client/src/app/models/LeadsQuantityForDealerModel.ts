export class LeadsQuantityForDealerModel{
    constructor(){
        this.dealerId = 0;
        this.dealerName = '';
        this.dealerEmail = '';
        this.startDate = null;
        this.endDate = null;
        this.assignQuantity = 0;
        this.assignQuantityActive = 0;
    }
    id   : number;                   
    dealerId : number;
    dealerName : string;
    dealerEmail: string;
    startDate: Date;
    endDate: Date;
    assignQuantity: number;
    assignQuantityActive: number;
    isActive: boolean;
    startDateStr: string;
    endDateStr: string;

}