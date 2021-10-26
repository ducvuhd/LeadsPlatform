export class ResponseData {
    code: number;
    message: string;
    data: any;
    token: string;
}
export class Pager {
    constructor(){
        this.pageIndex =1;
        this.pageSize = 25;
        this.totalRecord = 0;
        this.totalPage = 0;
    }
    public pageIndex: number;
    public pageSize: number;
    public totalRecord: number;
    public totalPage: number;
}

export class NotifyModel {
    public type: string;
    public msg: string;
    public timeout: number;
}

export class GroupRole {
    public isAdmin: boolean;
    public isAdminGroup: boolean;
    public roles: string[];
}

export enum SystemCode{
    Error = 0,
    /// <summary>
    /// Thành công
    /// </summary>
    Success = 1,
    /// <summary>
    /// Lỗi báo đối tượng được kiểm tra là đã tồn tại
    /// </summary>
    ErrorExist = 2,
    /// <summary>
    /// Không có dữ liệu
    /// </summary>
    DataNull = 3,
    /// <summary>
    /// Tham số không đúng
    /// </summary>
    ErrorParam = 4,
    /// <summary>
    /// Không có quyền thực thi
    /// </summary>
    NotPermitted = 5,
    /// <summary>
    /// Không hợp lệ
    /// </summary>
    NotValid = 6,
    /// <summary>
    /// Lỗi kết nối
    /// </summary>
    ErrorConnect = 7,
    /// <summary>
    /// Lỗi liên quan đến các vấn đề vượt quá giới hạn
    /// </summary>
    Overflow = 8
}
