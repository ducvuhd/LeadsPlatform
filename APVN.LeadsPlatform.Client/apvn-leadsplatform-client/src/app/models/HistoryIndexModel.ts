import { Pager } from './ResponseData';

export class HistoryIndexModel extends Pager {
    constructor() {
        super();
    }
    RelationId: number;
    Type: number;
}