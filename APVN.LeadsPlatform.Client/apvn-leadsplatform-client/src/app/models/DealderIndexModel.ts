import { Pager } from './ResponseData';

export class DealerIndexModel extends Pager {
    constructor() {
        super();
        this.userNameFilter = '';
    }
    userNameFilter: string;
    listDealer = [];
}