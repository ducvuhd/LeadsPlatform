export class UtilityHelper {

  public static convertDate(dateValue: string, fmt: string): string {
    const arrPart = dateValue.split('-');
    switch (fmt) {
      case 'MM/dd/yyyy':
        return arrPart[1] + '/' + arrPart[2] + '/' + arrPart[0];
      default:
        return arrPart[2] + '/' + arrPart[1] + '/' + arrPart[0];
    }
  }
  public static dateLargerOrEqual(firstTime: string, secondTime: string, separator: string = '/', format: string = 'yyyy-MM-dd'): boolean {
    const arrFirstTime = firstTime.split(separator);
    const arrSecondTime = secondTime.split(separator);
    switch (format) {
      case 'yyyy-MM-dd':
        // So sánh năm
        if (parseInt(arrFirstTime[0] + arrFirstTime[1] + arrFirstTime[2], 10) < parseInt(arrSecondTime[0] + arrSecondTime[1] + arrSecondTime[2], 10)) {
          return false;
        }
        // // So sánh tháng
        // if ( parseInt(arrFirstTime[1], 10) < parseInt(arrSecondTime[1], 10) ) {
        //     return false;
        // }
        // // So sánh ngày
        // if ( parseInt(arrFirstTime[2], 10) < parseInt(arrSecondTime[2], 10) ) {
        //     return false;
        // }
        return true;
      case 'dd/MM/yyyy':
      default:
        // So sánh năm
        if (parseInt(arrFirstTime[2] + arrFirstTime[1] + arrFirstTime[0], 10) < parseInt(arrSecondTime[2] + arrSecondTime[1] + arrSecondTime[0], 10)) {
          return false;
        }
        // // So sánh tháng
        // if ( parseInt(arrFirstTime[0], 10) < parseInt(arrSecondTime[0], 10) ) {
        //     return false;
        // }
        // // So sánh ngày
        // if ( parseInt(arrFirstTime[1], 10) < parseInt(arrSecondTime[1], 10) ) {
        //     return false;
        // }
        return true;
    }
  }

  public static toPascal(o) {
    var newO, origKey, newKey, value
    if (o instanceof Array) {
      return o.map(function (value) {
        if (typeof value === "object") {
          value = UtilityHelper.toPascal(value)
        }
        return value
      })
    } else {
      newO = {}
      for (origKey in o) {
        if (o.hasOwnProperty(origKey)) {
          newKey = (origKey.charAt(0).toUpperCase() + origKey.slice(1) || origKey).toString()
          value = o[origKey]
          if (value instanceof Array || (value !== null && value.constructor === Object)) {
            value = UtilityHelper.toPascal(value)
          }
          newO[newKey] = value
        }
      }
    }
    return newO
  }
  public static s2ab(s) {
    const buf = new ArrayBuffer(s.length);
    const view = new Uint8Array(buf);
    for (let i = 0; i !== s.length; ++i) {
      // tslint:disable-next-line:no-bitwise
      view[i] = s.charCodeAt(i) & 0xFF;
    }
    return buf;
  }
}
