import { Pager } from './ResponseData';

export class NoteIndexModel extends Pager {
    constructor() {
        super();
    }
    RelationId: number;
    Type: number;
}