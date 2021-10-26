import { CityInfo } from './city-info';
import { DepartmentInfo } from './department-info';

export class PermissionInit {
    constructor() {
        this.lstCity = [];
        this.lstDepartment = [];
    }
    userName: string;
    cityId: number;
    lstCity: CityInfo[];
    lstDepartment: DepartmentInfo[];
}
