using System;
using System.Collections.Generic;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class Pager
    {
        public Pager()
        {
            this.PageIndex = 1;
            this.PageSize = 25;
            this.TotalRecord = 0;
        }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
        public int TotalPage
        {
            get
            {
                if (this.TotalRecord % this.PageSize != 0)
                {
                    return (this.TotalRecord / this.PageSize) + 1;
                }
                else
                {
                    return this.TotalRecord / this.PageSize;
                }
            }
        }
    }
}
