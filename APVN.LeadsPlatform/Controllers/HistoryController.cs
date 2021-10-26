using APVN.LeadsPlatform.API.Controllers;
using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomizeAttribute]
    public class HistoryController : Controller
    {
        private readonly IHistoryBL _historyBL;

        public HistoryController(IHistoryBL historyBL)
        {
            _historyBL = historyBL;
        }
        [HttpPost]
        [Route("getlist")]
        public IActionResult GetList(HistoryIndexModel indexModel)
        {
            indexModel = _historyBL.GetListByRelationIdAndType(indexModel);
            return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Success, "", indexModel)));
        }
    }
}
