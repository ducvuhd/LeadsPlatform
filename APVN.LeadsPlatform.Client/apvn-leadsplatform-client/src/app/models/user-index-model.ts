import { UserSearchResult } from './user-search-result';

export class UserIndexModel {
    constructor() {
        this.result= [];
    }
    filterKeyword: string;
    cityId: number;
    departmentId: number;
    userName: string;
    pageIndex: number;
    pageSize: number;
    result: UserSearchResult[];
    totalRecord: number;
}
