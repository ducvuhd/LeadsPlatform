using APVN.LeadsPlatform.BusinessLayer.Cache;
using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IServiceCache;
using APVN.LeadsPlatform.Caching;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.ServiceCache
{
    public class BankServiceCache : CacheService, IBankServiceCache
    {
        private static readonly object _objLocked = new object();
        IHttpContextAccessor _httpContextAccessor;
        private ICache _cacheService;
        public IBankBL _bankBL;

        public BankServiceCache(ICache cache, IHttpContextAccessor httpContextAccessor, IBankBL bankBL) : base(cache)
        {
            _cacheService = cache;
            _httpContextAccessor = httpContextAccessor;
            _bankBL = bankBL;
        }

        public List<Bank> ListAll()
        {
            string keyCached = FormatKey(CacheConstants.LeadMakeCache);
            return Execute(() => _bankBL.ListAll(), keyCached, CacheConstants.ExpireLongTime);
        }
    }
}
