using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IServiceCache;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController
    {
        private readonly IMakeBL _makeBL;
        private readonly IMakeServiceCache _makeServiceCacheBL;
        private readonly IModelServiceCache _modelServiceCacheBL;

        public CommonController(IMakeBL makeBL, IMakeServiceCache makeServiceCacheBL, IModelServiceCache modelServiceCacheBL)
        {
            _makeBL = makeBL;
            _makeServiceCacheBL = makeServiceCacheBL;
            _modelServiceCacheBL = modelServiceCacheBL;
        }

        /**
         * FORM FILTER SEARCHING
         * 
         * */
        [HttpPost]
        [Route("handler-make-change")]
        public JsonResult HandlerMakeChange(int makeId)
        {
            // Model list
            List<APVN.LeadsPlatform.Entity.Models> modelList = _modelServiceCacheBL.ListAll().Where(x => x.MakeID == makeId)?.ToList();

            var result = new
            {
                modelList = modelList
            };

            return new JsonResult(
                new Response(
                    SystemCode.Success,
                    "",
                    result
                )
            );
        }
    }
}
