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
    public class CityServiceCache : CacheService, ICityServiceCache
    {
        private static readonly object _objLocked = new object();
        IHttpContextAccessor _httpContextAccessor;
        private ICache _cacheService;
        public ICityBL _cityBL;

        public CityServiceCache(ICache cache, IHttpContextAccessor httpContextAccessor, ICityBL cityBL) : base(cache)
        {
            _cacheService = cache;
            _httpContextAccessor = httpContextAccessor;
            _cityBL = cityBL;
        }

        public List<City> ListAll()
        {
            string keyCached = FormatKey(CacheConstants.LeadCityCache);
            return Execute(() => _cityBL.ListAll(), keyCached, CacheConstants.ExpireLongTime);
        }
    }
}
