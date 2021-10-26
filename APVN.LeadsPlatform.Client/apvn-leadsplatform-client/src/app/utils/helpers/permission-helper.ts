import { INavData } from '@coreui/angular';
import { Constants } from '../constants';
import { SystemRole } from '../enums/system-role.enum';
import { JwtHelper } from './jwt.helper';

export class PermissionHelper {
    public static hasRole(...arrRole: number[]): boolean {
        const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
        const roles = currUser.role;
        if (typeof roles === 'undefined' || roles == null) {
            return false;
        }

        let hasRole = false;
        var arrRoleItem = roles.split(',');
        //dùng some check khi có role thì break luôn
        arrRoleItem.some(roleItem => {
            if (+roleItem == SystemRole.Admin || arrRole.includes(+roleItem)) {
                hasRole = true;
                return true;
            }
        });
        return hasRole;
    }
    public static hasRoleInList(arrRole: number[]): boolean {
        const currUser = JwtHelper.decodeToken(localStorage.getItem(Constants.TokenKey));
        const roles = currUser.role;
        if (typeof roles === 'undefined' || roles == null) {
            return false;
        }

        let hasRole = false;
        var arrRoleItem = roles.split(',');
        //dùng some check khi có role thì break luôn
        arrRoleItem.some(roleItem => {
            if (+roleItem == SystemRole.Admin || arrRole.includes(+roleItem)) {
                hasRole = true;
                return true;
            }
        });
        return hasRole;
    }
    public static getMenuItems(menuItems: INavData[]): INavData[] {
        let arr = [];

        menuItems.forEach(els => {
            if (els.attributes != null && els.attributes.role != null) {
                if (els.children != null && els.children.length > 0) {
                    let childArr = [];
                    els.children.forEach(childEls => {
                        if (childEls.attributes != null && childEls.attributes.role != null) {
                            if (this.hasRoleInList(childEls.attributes.role)) {
                                childArr.push(childEls);
                            }
                            // if(this.hasRole(childEls.attributes.role)) {
                            //     childArr.push(childEls);
                            // }         
                        } else {
                            childArr.push(childEls);
                        }
                    });
                    els.children = childArr;
                }
                if (els.attributes.role[0] == "All" || this.hasRoleInList(els.attributes.role)) {
                    arr.push(els);
                }
            } else {
                arr.push(els);
            }
        });
        return arr;
    }
  
    public static IsSale(){
        if (this.hasRole(SystemRole.Sales)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsLead(){
        if (this.hasRole(SystemRole.Lead)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsAdmin(){
        if (this.hasRole(SystemRole.Admin)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsCoordinate(){
        if (this.hasRole(SystemRole.Coordinate)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsTeamLeader(){
        if (this.hasRole(SystemRole.TeamLeader)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsMKT(){
        if (this.hasRole(SystemRole.MKT)) {
            return true;
        }else{
            return false;
        }
    }
    public static IsBD(){
        if (this.hasRole(SystemRole.BD)) {
            return true;
        }else{
            return false;
        }
    }
}
