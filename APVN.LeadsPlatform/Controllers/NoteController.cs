using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APVN.LeadsPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomizeAttribute]
    public class NoteController : BaseController
    {
        private readonly INoteBL _noteBL;
        public NoteController(INoteBL noteBL)
        {
            _noteBL = noteBL;
        }

        // GET: api/<NoteController>
        [HttpPost]
        [Route("getlist")]
        public IActionResult GetList(NoteIndexModel indexModel)
        {
            indexModel = _noteBL.GetListByRelationIdAndType(indexModel);
            return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Success, "", indexModel)));
        }
        [HttpPost]
        [Route("save")]
        public IActionResult Save(NoteModel model)
        {
            var res = new Response();
            model.LastModifiedDate = DateTime.Now;
            model.LastModifiedBy = CurrUser.UserName;
            if (model.Id == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrUser.UserName;
                res = _noteBL.Insert(model);
            }
            else
            {
                var note = _noteBL.GetByIdAndType(model.Id, model.Type);
                if (note != null && note.Id > 0)
                {
                    res = _noteBL.Update(model);
                }
            }
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        [HttpPost]
        [Route("getbyid")]
        public JsonResult GetById(int noteId, int type)
        {
            try
            {
                var note = _noteBL.GetByIdAndType(noteId, type);
                return new JsonResult(new Response(SystemCode.Success, "", note));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response(SystemCode.Error, ex.Message, null));
            }
        }
    }
}
