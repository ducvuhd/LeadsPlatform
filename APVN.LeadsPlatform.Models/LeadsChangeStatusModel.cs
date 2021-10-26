using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsChangeStatusModel
    {
        public int LeadsId { get; set; }
        public int Status { get; set; }
        //Tạm thời chưa có quản lý cuộc gọi sẽ không dùng cái này
        public int CallQuantity { get; set; }
        public string ChangeStatusBy { get; set; }
        public NoteModel noteModel { get; set; }
    }
}
