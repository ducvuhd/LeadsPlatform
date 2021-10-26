export class NoteModel {
    constructor() {
        this.id = 0;
    }
    id: number;
    relationId: number;
    type: number;
    currentRelationStatus: number;
    notes: string;
    createdDate: Date;
    createdBy: string;
    lastModifiedDate: Date;
    lastModifiedBy: Date;
    currentRelationStatusStr: string;
    createdDateName: string;
    lastModifiedDateName: string;
}