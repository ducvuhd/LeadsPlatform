using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Enums
{
    public enum SystemCode
    {
        /// <summary>
        /// Lỗi
        /// </summary>
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
}
