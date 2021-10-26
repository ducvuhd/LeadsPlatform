
export class HelperFormat {

    constructor() { }

    public static checkformatphone(inputVal: string): boolean {
        var regexMobile1 = /^0[1-9][0-9]{8,9}$/;
        var regexMobile2 = /^84[0-9]{8,9}$/;
        if (!regexMobile1.test(inputVal) && !regexMobile2.test(inputVal)) {
            return true;
        } else {
            return false;
        }
    }
    public static formatNumber(obj: number, value = -1) {
        if (obj === undefined || obj === null || obj.toString() === '0' || obj.toString() === 'NaN') {
            return value != undefined && value >= 0 ? value.toString() : '';
        }

        return obj.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }
    public static formatNumber2(obj: number, value = -1) {
        if (obj === undefined || obj === null || obj.toString() === 'NaN') {
            return value != undefined && value >= 0 ? value.toString() : '';
        }

        return obj.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }
    public static getNumberFromString(obj: string, defaultReturn = 0) {
        if (obj === undefined || obj === '') {
            return defaultReturn;
        }

        obj = obj.toString().replace(/[^0-9]+/gi, '');
        return parseInt(obj.toString(), 10);
    }
    public static checkformatdate(value) {
        if (/^(0[1-9]|1\d|2\d|3[01])\/(0[1-9]|1[0-2])\/(19|20)\d{2}$/.test(value)) {
            return true;
        }
        else {
            return false;
        }
    }
    public static checkformatdatehmm(value) {
        if (/^([1-9]|([012][0-9])|(3[01]))\/([0]{0,1}[1-9]|1[012])\/\d\d\d\d( )+[012]{0,1}[0-9]:[0-6][0-9]$/.test(value)) {
            return true;
        }
        else {
            return false;
        }
    }
    public static Validate(x: string) {
        if (x == null || x == undefined || x.trim() == '')
            return true;
        else
            return false;
    }
    public static formatDateToString(date) {
        var year = date.getFullYear().toString();
        var month = (date.getMonth() + 101).toString().substring(1);
        var day = (date.getDate() + 100).toString().substring(1);
        return year + "-" + month + "-" + day;
    }

}