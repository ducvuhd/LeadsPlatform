import { LeadsQuantityForDealerModel } from './LeadsQuantityForDealerModel';
import { Pager } from './ResponseData';

export class LeadsQuantityForDealerIndexModel extends Pager{
    constructor() {
        super();
        this.dealerEmail = '';
        this.listLeadsQuantityForDealer = [];
    }
    dealerEmail: string;
    startDate: Date;
    endDate: Date;
    listLeadsQuantityForDealer: LeadsQuantityForDealerModel[];
}