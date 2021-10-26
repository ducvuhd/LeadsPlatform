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
    public class MakeServiceCache : CacheService, IMakeServiceCache
    {
        private static readonly object _objLocked = new object();
        IHttpContextAccessor _httpContextAccessor;
        private ICache _cacheService;
        public IMakeBL _makeBL;

        public MakeServiceCache(ICache cache, IHttpContextAccessor httpContextAccessor, IMakeBL makeBL) : base(cache)
        {
            _cacheService = cache;
            _httpContextAccessor = httpContextAccessor;
            _makeBL = makeBL;
        }

        public List<Makes> ListAll()
        {
            string keyCached = FormatKey(CacheConstants.LeadMakeCache);
            return Execute(() => _makeBL.ListAll(), keyCached, CacheConstants.ExpireLongTime);
        }
    }
}
