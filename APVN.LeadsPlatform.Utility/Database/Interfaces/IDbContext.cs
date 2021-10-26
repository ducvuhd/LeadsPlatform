using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace APVN.LeadsPlatform.Utility.Database.Interfaces
{
    public interface IDbContext
    {
        void SetWriteDbContext(IDbConnection writeContext, IDbTransaction transaction);
    }
}
