using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IServiceCache;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using APVN.LeadsPlatform.Utility.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomizeAttribute]
    public class LeadsManagermentController : BaseController
    {
        private readonly ILeadsBL _leadsBL;
        private readonly IMakeBL _makeBL;
        private readonly IMakeServiceCache _makeServiceCacheBL;
        private readonly IModelServiceCache _modelServiceCacheBL;
        private readonly ICityServiceCache _cityServiceCacheBL;
        private readonly IUserService _userService;
        private readonly IUserBL _userBL;
        private readonly ICampaignBL _campaignBL;
        private readonly IBankServiceCache _bankServiceCacheBL;
        private readonly IDealerLeadBL _dealerLeadBL;
        private readonly INoteBL _noteBL;
        private readonly ILeadsQuantityForDealerBL _leadsQuantityForDealerBL;

        public LeadsManagermentController(ILeadsBL leadsBL, IMakeBL makeBL, IMakeServiceCache makeServiceCacheBL, IModelServiceCache modelServiceCacheBL, ICityServiceCache cityServiceCacheBL, IUserService userService, IUserBL userBL, ICampaignBL campaignBL, IBankServiceCache bankServiceCacheBL, IDealerLeadBL dealerLeadBL, INoteBL noteBL, ILeadsQuantityForDealerBL leadsQuantityForDealerBL)
        {
            _leadsBL = leadsBL;
            _makeBL = makeBL;
            _makeServiceCacheBL = makeServiceCacheBL;
            _modelServiceCacheBL = modelServiceCacheBL;
            _cityServiceCacheBL = cityServiceCacheBL;
            _userService = userService;
            _userBL = userBL;
            _campaignBL = campaignBL;
            _bankServiceCacheBL = bankServiceCacheBL;
            _dealerLeadBL = dealerLeadBL;
            _noteBL = noteBL;
            _leadsQuantityForDealerBL = leadsQuantityForDealerBL;
        }

        [HttpPost]
        [Route("init-detail")]
        public JsonResult InitDetail()
        {
            var lstActionStatus = Utils.GetAllEnumValueAndDescription<LeadActionStatus>();
            return new JsonResult(new Response(SystemCode.Success, "", lstActionStatus));
        }

        [HttpPost]
        [Route("save")]
        public IActionResult Save([FromBody] LeadsModel model)
        {
            var res = new Response();
            var lstDuplicate = model.ListAction.GroupBy(x => new { x.MakeId, x.ModelId }).Where(g => g.Count() > 1).ToList();
            if (lstDuplicate != null && lstDuplicate.Count > 0)
            {
                return new JsonResult(new Response(SystemCode.ErrorExist, "Thông tin xe không được trùng hãng và dòng", null));
            }

            var fullNameSearch = Utils.UnicodeToKoDau(model.FullName.Trim()).ToLower();
            model.FullTextSearch = $"{fullNameSearch} {model.Mobile.Trim()} {(model.Email ?? "").Trim().ToLower()}";

            if (model.Id == 0)
            {
                var phoneExist = _leadsBL.CheckExistMobile(model.Mobile.Trim());
                if (phoneExist)
                {
                    return new JsonResult(new Response(SystemCode.ErrorExist, "SĐT đã tồn tại", null));
                }
                model.Status = (int)LeadStatus.New;
                model.IsActive = true;
                model.CreatedDate = model.CreatedDate != null ? model.CreatedDate : DateTime.Now;
                model.CreatedBy = CurrUser.UserName;
                model.LastModifiedDate = DateTime.Now;
                model.LastModifiedBy = CurrUser.UserName;
                if (model.AssignToId > 0)
                {
                    model.StartCareDate = DateTime.Now;
                }
                res = _leadsBL.Insert(model);
            }
            else
            {
                var response = _leadsBL.GetLeadsModelById(model.Id);
                if (response.Code == SystemCode.Success)
                {
                    var leadModel = (LeadsModel)response.Data;
                    res = _leadsBL.Update(model, leadModel);
                }
                else
                {
                    res = new Response(SystemCode.Error, "Có lỗi xảy ra: Không tìm thấy Lead", null);
                }
            }
            return new JsonResult(res);
        }

        /**
         * FORM FILTER SEARCHING
         * 
         * */
        [HttpPost]
        [Route("get-form-filter")]
        public async Task<JsonResult> GetFormFilter()
        {
            // Dealer all
            var dealerList = new List<Users>();

            // Make list
            List<Makes> makeList = _makeServiceCacheBL.ListAll();

            // Model list
            List<APVN.LeadsPlatform.Entity.Models> modelList = _modelServiceCacheBL.ListAll();

            // City list
            List<APVN.LeadsPlatform.Entity.City> cityList = _cityServiceCacheBL.ListAll();

            // Saller
            List<Users> userList = (List<Users>)await _userBL.GetGetUserActiveAsync();

            // Lead status
            var leadStatusList = Utils.GetAllEnumValueAndDescription<LeadActionStatus>();

            // Leads IsActive
            var LeadsIsActiveList = LeadsHelper.LeadsIsActiveList();

            // Campaign list
            var campaignList = await _campaignBL.ListAllAsync();

            // Nguồn list
            var lstSource = Utils.GetAllEnumValueAndDescription<LeadActionSource>();

            // Box lead
            var boxLeadsList = Utils.GetAllEnumValueAndDescription<LeadActionType>();

            // Bank list
            var bankList = _bankServiceCacheBL.ListAll();

            // Payment Status
            var paymentStatusList = Utils.GetAllEnumValueAndDescription<LeadActionPaymentStatus>();

            // Payment Status
            var secondHandList = Utils.GetAllEnumValueAndDescription<Secondhand>();

            var result = new
            {
                dealerList = dealerList,
                makeList = makeList,
                modelList = modelList,
                cityList = cityList,
                bankList = bankList,
                userList = userList,
                leadStatusList = leadStatusList,
                campaignList = campaignList,
                sourceLeadsList = lstSource,
                paymentStatusList = paymentStatusList,
                boxLeadsList = boxLeadsList,
                secondHandList = secondHandList,
                leadsIsActiveList = LeadsIsActiveList
            };

            return new JsonResult(
                new Response(
                    SystemCode.Success,
                    "",
                    result
                )
            );
        }

        /**
         * FORM MAIN LIST LEADS
         * 
         * */
        [HttpPost]
        [Route("get-main-list")]
        public async Task<JsonResult> GetMainList(LeadsFilterModel filterModel)
        {
            int totalRow = 0;
            // filterModel.Id = filterModel.Id == null ? 0 : filterModel.Id;
            // filterModel.Status = filterModel.Status == null ? -1 : filterModel.Status;
            filterModel.KeyWord = filterModel.KeyWord == null ? "" : Utils.UnicodeToKoDau(filterModel.KeyWord.Trim()).ToLower();
            filterModel.DealerIds = filterModel.DealerIds == null ? "" : filterModel.DealerIds.Trim();

            // Saller
            List<Users> userList = (List<Users>)await _userBL.GetGetUserActiveAsync();

            // Campaign list
            var campaignList = await _campaignBL.ListAllAsync();
            ////nếu là CS thì chỉ đc nhìn thấy ds Lead của minh
            //if (CurrUser.HasPermission(SystemRole.Sales) && CurrUser.Roles.Count == 1)
            //{
            //    filterModel.AssignToId = CurrUser.UserId;
            //}

            var result = _leadsBL.GetList(filterModel, out totalRow)?.Select(x => new LeadsListModel(
                x,
                _makeServiceCacheBL.ListAll(),
                _modelServiceCacheBL.ListAll(),
                campaignList,
                userList))?.ToList();

            var Pager = new Pager();
            Pager.PageIndex = filterModel.PageIndex;
            Pager.PageSize = filterModel.PageSize;
            Pager.TotalRecord = totalRow;

            var data = new
            {
                List = result,
                Page = Pager
            };

            return new JsonResult(
                new Response(
                    SystemCode.Success,
                    "",
                    data
                )
            );
        }

        [HttpPost]
        [Route("get-detail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var data = await _leadsBL.GetDetail(id);
            return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Success, "", data), Formatting.Indented));
        }
        [Route("get-form-create")]
        public async Task<JsonResult> GetFormCreate()
        {
            var lstPaymentStatus = Utils.GetAllEnumValueAndDescription<LeadActionPaymentStatus>();
            var lstSource = Utils.GetAllEnumValueAndDescription<LeadActionSource>();
            var lstCampaign = _campaignBL.ListAllAsync();
            var lstSecondhand = Utils.GetAllEnumValueAndDescription<Secondhand>();
            var lstLeadActionType = Utils.GetAllEnumValueAndDescription<LeadActionType>();
            var lstActionStatus = Utils.GetAllEnumValueAndDescription<LeadActionStatus>();

            // Make list
            List<Makes> makeList = _makeServiceCacheBL.ListAll();

            // Model list
            List<APVN.LeadsPlatform.Entity.Models> modelList = _modelServiceCacheBL.ListAll();

            // City list
            List<APVN.LeadsPlatform.Entity.City> cityList = _cityServiceCacheBL.ListAll();

            // Saller
            List<Users> userList = (List<Users>)await _userBL.GetGetUserActiveAsync();
            // Bank list
            var bankList = _bankServiceCacheBL.ListAll();

            var result = new
            {
                lstmake = makeList,
                lstmodel = modelList,
                cityList = cityList,
                lstPaymentStatus = lstPaymentStatus,
                lstSource = lstSource,
                lstCampaign = lstCampaign,
                lstSecondhand = lstSecondhand,
                lstLeadActionType = lstLeadActionType,
                lstUser = userList,
                secondHandList = Const.SecoundHandList(),
                bankList = bankList,
                lstActionStatus = lstActionStatus
            };

            return new JsonResult(new Response(SystemCode.Success, "", result));
        }

        [HttpGet]
        [Route("getbyid/{leadId}")]
        public IActionResult GetById(int leadId)
        {
            var res = _leadsBL.GetLeadsModelById(leadId);
            return Ok(JsonConvert.SerializeObject(res, Formatting.Indented));
        }
        [HttpGet]
        [Route("changestatus")]
        public JsonResult ChangeStatus(LeadsChangeStatusModel statusModel)
        {
            var res = new Response();
            var leadsModel = _leadsBL.GetById(statusModel.LeadsId);
            if (leadsModel != null && (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Lead) || leadsModel.AssignToId == CurrUser.UserId) && leadsModel.Status != statusModel.Status)
            {
                statusModel.ChangeStatusBy = CurrUser.UserName;
                res = _leadsBL.UpdateStatus(leadsModel, statusModel);
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }
        //Nhận Lead
        [HttpPost]
        [Route("getleadforsale/{leadsId}")]
        public JsonResult GetLeadForSale(int leadsId)
        {
            var res = new Response();
            if (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Sales))
            {
                res = _leadsBL.TakeLead(leadsId);
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }
        //Gán Lead
        [HttpPost]
        [Route("assignleadtosale")]
        public JsonResult AssignLeadToSale(int leadsId, int assignToId)
        {
            var res = new Response();
            if (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Lead))
            {
                res = _leadsBL.AssigToLead(leadsId, assignToId);
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }
        //Gán Lead cho Dealer
        [HttpPost]
        [Route("assigleadtodealer")]
        public JsonResult AssigLeadToDealer(AssigLeadToDealerModel assigLeadToDealer)
        {
            var res = new Response();
            if (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Lead, SystemRole.Coordinate, SystemRole.BD))
            {
                res = _leadsBL.AssigLeadToDealer(assigLeadToDealer);
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }

        //mở Lead
        [HttpPost]
        [Route("changeisactivelead")]
        public JsonResult ChangeIsActiveLead(int leadId, bool isActive)
        {
            var res = new Response();
            var leadModel = (LeadsModel)_leadsBL.GetLeadsModelById(leadId).Data;
            if (leadModel != null && (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Lead) || leadModel.AssignToId == CurrUser.UserId))
            {
                res = _leadsBL.ChangeIsActive(leadId, isActive);
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }
        //khóa Lead
        [HttpPost]
        [Route("block-lead")]
        public JsonResult BlockLead(NoteModel noteModel)
        {
            var res = new Response();
            var leadModel = (LeadsModel)_leadsBL.GetLeadsModelById(noteModel.RelationId).Data;
            if (leadModel != null && (CurrUser.HasPermission(SystemRole.Admin, SystemRole.Lead) || leadModel.AssignToId == CurrUser.UserId))
            {
                res = _leadsBL.ChangeIsActive(noteModel.RelationId, false);
                if (res.Code == SystemCode.Success)
                {
                    noteModel.CreatedDate = DateTime.Now;
                    noteModel.CreatedBy = CurrUser.UserName;
                    noteModel.LastModifiedDate = DateTime.Now;
                    noteModel.LastModifiedBy = CurrUser.UserName;
                    noteModel.CurrentInActive = true;
                    _noteBL.Insert(noteModel);
                }
            }
            else
            {
                res = new Response(SystemCode.NotPermitted, "Bạn không có quyền thao tác", null);
            }
            return new JsonResult(res);
        }
        [Route("downloadTemplate")]
        [HttpPost]
        public JsonResult DownloadTemplate()
        {
            byte[] result = null;
            var response = new Response();
            try
            {
                var sheetLead = new string[]
                {
                    "Họ tên",
                    "SĐT",
                    "Email",
                    "Tỉnh/TP",
                    "Hãng xe",
                    "Dòng xe",
                    "Tình trạng xe",
                    "Ngày đăng ký",
                    "Box thu lead",
                    "Nguồn",
                    "Ghi chú"
                };

                var sheetCity = new string[]
                {
                    "ID",
                    "CityName"
                };
                var sheetDemo = new string[]
                {
                    "Id",
                    "Name"
                };
                using (var package = new ExcelPackage())
                {
                    // add a new worksheet to the empty workbook
                    var columSTT = 1;

                    var worksheet = package.Workbook.Worksheets.Add("Template"); //Worksheet name
                    worksheet.DefaultColWidth = 20;
                    worksheet.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 8])
                    {
                        cells.Style.Font.Bold = true;
                    }

                    //First add the headers
                    for (var i = 0; i < sheetLead.Count(); i++)
                    {
                        worksheet.Cells[1, i + 1].Value = sheetLead[i];
                    }

                    using (var range = worksheet.Cells["A1:K1"])
                    {
                        // Set PatternType
                        range.Style.Fill.PatternType = ExcelFillStyle.DarkGray;
                        // Set Màu cho Background
                        range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        // Set Border
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        // Set màu ch Border
                        range.Style.Border.Bottom.Color.SetColor(Color.Blue);

                    }

                    #region sheet thành phố
                    var excelCity = package.Workbook.Worksheets.Add("Tỉnh/TP"); //Worksheet name
                    excelCity.DefaultColWidth = 25;
                    excelCity.Cells.Style.WrapText = true;
                    excelCity.Column(columSTT).Width = 10;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    //First add the headers
                    for (var i = 0; i < sheetCity.Count(); i++)
                    {
                        excelCity.Cells[1, i + 1].Value = sheetCity[i];
                    }
                    var lstCity = _cityServiceCacheBL.ListAll();
                    int row = 2;
                    foreach (var item in lstCity)
                    {
                        int cell = 1;
                        excelCity.Cells[row, cell++].Value = item.CityID;
                        excelCity.Cells[row, cell++].Value = $"{item.CityName}_{item.CityID}";
                        row++;
                    }
                    #endregion
                    #region sheet hãng
                    var excelMake = package.Workbook.Worksheets.Add("Hãng"); //Worksheet name
                    excelMake.DefaultColWidth = 25;
                    excelMake.Column(columSTT).Width = 10;
                    excelMake.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    for (var i = 0; i < sheetDemo.Count(); i++)
                    {
                        excelMake.Cells[1, i + 1].Value = sheetDemo[i];
                    }
                    row = 2;
                    var lstMake = _makeServiceCacheBL.ListAll().OrderByDescending(x => x.MakeName);
                    foreach (var item in lstMake)
                    {
                        int cell = 1;
                        excelMake.Cells[row, cell++].Value = item.MakeID;
                        excelMake.Cells[row, cell++].Value = $"{item.MakeName}_{item.MakeID}";
                        row++;
                    }
                    #endregion
                    #region sheet dòng
                    var excelModel = package.Workbook.Worksheets.Add("Dòng"); //Worksheet name
                    excelModel.DefaultColWidth = 25;
                    excelModel.Column(columSTT).Width = 10;
                    excelModel.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    for (var i = 0; i < sheetDemo.Count(); i++)
                    {
                        excelModel.Cells[1, i + 1].Value = sheetDemo[i];
                    }
                    row = 2;
                    var lstModel = _modelServiceCacheBL.ListAll().OrderByDescending(x => x.ModelName);
                    foreach (var item in lstModel)
                    {
                        int cell = 1;
                        excelModel.Cells[row, cell++].Value = item.ModelID;
                        excelModel.Cells[row, cell++].Value = $"{item.ModelName}_{item.ModelID}";
                        row++;
                    }
                    #endregion
                    #region sheet tình trạng
                    var excelSeconhand = package.Workbook.Worksheets.Add("Tình trạng"); //Worksheet name
                    excelSeconhand.DefaultColWidth = 25;
                    excelSeconhand.Column(columSTT).Width = 10;
                    excelSeconhand.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    for (var i = 0; i < sheetDemo.Count(); i++)
                    {
                        excelSeconhand.Cells[1, i + 1].Value = sheetDemo[i];
                    }
                    row = 2;
                    var lstSecondhand = Utils.GetAllEnumValueAndDescription<Secondhand>();
                    foreach (var item in lstSecondhand)
                    {
                        int cell = 1;
                        excelSeconhand.Cells[row, cell++].Value = item.Key;
                        excelSeconhand.Cells[row, cell++].Value = $"{item.Value}_{item.Key}";
                        row++;
                    }
                    #endregion
                    #region sheet box thu lead
                    var excelLeadAction = package.Workbook.Worksheets.Add("Box thu lead"); //Worksheet name
                    excelLeadAction.DefaultColWidth = 25;
                    excelLeadAction.Column(columSTT).Width = 10;
                    excelLeadAction.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    for (var i = 0; i < sheetDemo.Count(); i++)
                    {
                        excelLeadAction.Cells[1, i + 1].Value = sheetDemo[i];
                    }
                    row = 2;
                    var lstLeadActionType = Utils.GetAllEnumValueAndDescription<LeadActionType>();
                    foreach (var item in lstLeadActionType)
                    {
                        int cell = 1;
                        excelLeadAction.Cells[row, cell++].Value = item.Key;
                        excelLeadAction.Cells[row, cell++].Value = $"{item.Value}_{item.Key}";
                        row++;
                    }
                    #endregion
                    #region sheet nguồn 
                    var excelLeadSource = package.Workbook.Worksheets.Add("Nguồn"); //Worksheet name
                    excelLeadSource.DefaultColWidth = 25;
                    excelLeadSource.Column(columSTT).Width = 10;
                    excelLeadSource.Cells.Style.WrapText = true;

                    using (var cells = worksheet.Cells[1, 1, 1, 2])
                    {
                        cells.Style.Font.Bold = true;
                    }
                    for (var i = 0; i < sheetDemo.Count(); i++)
                    {
                        excelLeadSource.Cells[1, i + 1].Value = sheetDemo[i];
                    }
                    row = 2;
                    var lstLeadActionSource = Utils.GetAllEnumValueAndDescription<LeadActionSource>();
                    foreach (var item in lstLeadActionSource)
                    {
                        int cell = 1;
                        excelLeadSource.Cells[row, cell++].Value = item.Key;
                        excelLeadSource.Cells[row, cell++].Value = $"{item.Value}_{item.Key}";
                        row++;
                    }
                    #endregion
                    result = package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                response.Code = SystemCode.Error;
                response.Message = "Error: " + ex.Message;
                return new JsonResult(response);
            }
            response = new Response(SystemCode.Success, "Download template successfully !", result);
            return new JsonResult(response);
        }
        [Route("importleads")]
        [HttpPost]
        public JsonResult ImportLeads()
        {
            List<LeadsModel> lstLeads = new List<LeadsModel>();
            var file = Request.Form.Files[0];
            var campaignId = Request.Form["CampaignId"][0].ToInt(0);
            bool isValid = true;
            string invalidFullName = "";
            string invalidMobile = "";
            string sameMobile = "";
            string sameMobileExcel = "";
            string invalidCity = "";
            string invalidMake = "";
            string invalidModel = "";
            string invalidSecondhand = "";
            string invalidCreatedDate = "";
            string invalidLeadActionType = "";
            if (!string.IsNullOrEmpty(file.Name) && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                        if (worksheet != null && worksheet.Dimension.Rows > 0)
                        {
                            int rows = worksheet.Dimension.Rows;
                            for (int i = 2; i <= rows; i++)
                            {
                                int column = 1;
                                LeadsModel model = new LeadsModel();
                                LeadsActionModel leadsAction = new LeadsActionModel();
                                LeadsActionExtendsModel actionExtends = new LeadsActionExtendsModel();

                                var valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null) { model.FullName = valueColumn?.ToString().Trim(); }
                                else { isValid = false; invalidFullName += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    model.Mobile = valueColumn?.ToString().Trim();
                                    bool isExitMobile = _leadsBL.CheckExistMobile(model.Mobile);
                                    if (isExitMobile)
                                    {
                                        isValid = false; sameMobile += i.ToString() + ", ";
                                    }
                                    var lstMobileInExcel = lstLeads.Select(x => x.Mobile).ToList();
                                    if (lstMobileInExcel.Contains(model.Mobile))
                                    {
                                        isValid = false; sameMobileExcel += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidMobile += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null) { model.Email = valueColumn?.ToString().Trim(); }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    model.CityId = valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);
                                    if (model.CityId == 0)
                                    {
                                        isValid = false; invalidCity += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidCity += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    leadsAction.MakeId = valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);
                                    if (leadsAction.MakeId == 0)
                                    {
                                        isValid = false; invalidMake += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidMake += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    leadsAction.ModelId = valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);
                                    if (leadsAction.ModelId == 0)
                                    {
                                        isValid = false; invalidModel += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidModel += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    actionExtends.SecondHand = valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);
                                    if (actionExtends.SecondHand == 0)
                                    {
                                        isValid = false; invalidSecondhand += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidSecondhand += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    model.CreatedDate = valueColumn.ToDateTime(DateTime.MinValue) > DateTime.MinValue ? valueColumn.ToDateTime(DateTime.MinValue) : valueColumn.ToString().Split('_')[1].ToDateTime(DateTime.MinValue);
                                    if (model.CreatedDate == DateTime.MinValue)
                                    {
                                        isValid = false; invalidCreatedDate += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidCreatedDate += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                if (valueColumn != null)
                                {
                                    leadsAction.Type = valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);
                                    if (leadsAction.Type == 0)
                                    {
                                        isValid = false; invalidLeadActionType += i.ToString() + ", ";
                                    }
                                }
                                else { isValid = false; invalidLeadActionType += i.ToString() + ", "; }

                                valueColumn = worksheet.Cells[i, column++].Value;
                                leadsAction.Source = valueColumn != null && valueColumn.ToInt(0) > 0 ? valueColumn.ToInt(0) : (valueColumn != null && valueColumn.ToString().Split('_').Length > 1 ? valueColumn.ToString().Split('_')[1].ToInt(0) : 0);

                                valueColumn = worksheet.Cells[i, column++].Value;
                                model.Note = valueColumn?.ToString();

                                if (isValid)
                                {
                                    actionExtends.CreatedBy = CurrUser.UserName;
                                    actionExtends.LastModifiedBy = CurrUser.UserName;
                                    actionExtends.CreatedDate = DateTime.Now;
                                    actionExtends.LastModifiedDate = DateTime.Now;
                                    leadsAction.ListLeadsActionExtends.Add(actionExtends);

                                    leadsAction.CreatedBy = CurrUser.UserName;
                                    leadsAction.LastModifiedBy = CurrUser.UserName;
                                    leadsAction.CreatedDate = DateTime.Now;
                                    leadsAction.LastModifiedDate = DateTime.Now;
                                    leadsAction.CampaignId = campaignId;
                                    model.ListAction.Add(leadsAction);
                                    lstLeads.Add(model);
                                }
                            }
                        }
                    }
                    if (!isValid)
                    {
                        var message = "";
                        if (!string.IsNullOrEmpty(invalidFullName)) message += $"<span>Họ tên: rows: {invalidFullName.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidMobile)) message += $"<span>SĐT sai: rows: {invalidMobile.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(sameMobile)) message += $"<span>SĐT tồn tại trong DB: rows: {sameMobile.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(sameMobileExcel)) message += $"<span>SĐT trong excel trùng nhau: rows: {sameMobileExcel.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidCity)) message += $"<span>Tỉnh/TP: rows: {invalidCity.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidMake)) message += $"<span>Hãng xe: rows: {invalidMake.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidModel)) message += $"<span>Dòng xe: rows: {invalidModel.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidSecondhand)) message += $"<span>Tình trạng: rows: {invalidSecondhand.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidCreatedDate)) message += $"<span>Ngày đăng ký: rows: {invalidCreatedDate.Trim().Trim(',')}</span><br>";
                        if (!string.IsNullOrEmpty(invalidLeadActionType)) message += $"<span>Box thu lead: {invalidLeadActionType.Trim().Trim(',')}</span><br>";
                        return new JsonResult(new Response(SystemCode.Error, message, null));
                    }

                    var res = _leadsBL.ImportLeadFromExcel(lstLeads);
                    return new JsonResult(res);
                }
            }
            return new JsonResult(new Response(SystemCode.Error, "File không tồn tại", null));
        }
        [Route("get-all-campaign")]
        [HttpGet]

        public async Task<JsonResult> GetAllCampaign()
        {
            var lstCampaign = await _campaignBL.ListAllAsync();
            var res = new Response(SystemCode.Success, "", lstCampaign);
            return new JsonResult(res);
        }

        [Route("save-campaign")]
        [HttpPost]
        public JsonResult SaveCampaign(Campaign campaign)
        {
            if (campaign.Id == 0)
            {
                campaign.CreatedBy = CurrUser.UserName;
                campaign.CreatedDate = DateTime.Now;
                campaign.LastModifiedBy = CurrUser.UserName;
                campaign.ModifiedDate = DateTime.Now;
                var res = _campaignBL.Insert(campaign);
                return new JsonResult(res);
            }
            else
            {
                var res = _campaignBL.Update(campaign);
                return new JsonResult(res);
            }
        }
        [Route("get-dealer-from-oto")]
        [HttpPost]
        public JsonResult GetDealerFromOto(DealerIndexModel indexModel)
        {
            var res = _dealerLeadBL.GetUserProfileByNamePaging(indexModel);
            return new JsonResult(res);
        }
        [Route("get-leadaction-by-leadsId/{leadsId}")]
        [HttpPost]
        public JsonResult GetLeadActionByLeadsId(int leadsId)
        {
            var lead = _leadsBL.GetById(leadsId);
            if (lead != null)
            {
                lead.ListAction.ForEach(x =>
                {
                    x.CityName = string.Join(",", _cityServiceCacheBL.ListAll().FindAll(item => x.ListLeadsActionExtends.Select(y => y.CityId).ToList().Contains(item.CityID)).Select(x => x.CityName).ToList());
                    x.MakeName = _makeServiceCacheBL.ListAll().Find(item => item.MakeID == x.MakeId)?.MakeName;
                    x.ModelName = _modelServiceCacheBL.ListAll().Find(item => item.ModelID == x.ModelId)?.ModelName;
                });
                return new JsonResult(new Response(SystemCode.Success, "", lead.ListAction));
            }
            return new JsonResult(new Response(SystemCode.DataNull, "", null));
        }
        [Route("update-leadactionstatus")]
        [HttpPost]
        public JsonResult UpdateLeadActionStatus(int status, int leadActionId)
        {
            var res = _leadsBL.UpdateLeadActionStatus(status, leadActionId);
            return new JsonResult(res);
        }
        [HttpPost]
        [Route("leadquantity-getlistpaging")]
        public JsonResult LeadsQuantityForDealerGetListPaging(LeadsQuantityForDealerIndexModel indexModel)
        {
            var res = _leadsQuantityForDealerBL.GetListPaging(indexModel);
            return new JsonResult(res);
        }
        [HttpPost]
        [Route("leadquantity-insert")]
        public JsonResult LeadsQuantityForDealerInsert(LeadsQuantityForDealer entity)
        {
            var quantity = _leadsQuantityForDealerBL.GetByDealerId(entity.DealerId);
            if (quantity != null && quantity.IsActive)
            {
                return new JsonResult(new Response(SystemCode.Error, "Dealer đã được set lịch lấy khách hàng", null));
            }
            entity.CreatedBy = CurrUser.UserName;
            entity.CreatedDate = DateTime.Now;
            entity.LastModifedDate = DateTime.Now;
            entity.LastModifiedBy = CurrUser.UserName;
            entity.IsActive = true;
            entity.AssignQuantityActive = entity.AssignQuantity;
            entity.EndDate = entity.EndDate.AddDays(1).AddSeconds(-1);
            var res = _leadsQuantityForDealerBL.Insert(entity);
            return new JsonResult(res);
        }
        [HttpPost]
        [Route("leadquantity-cancel-schedule")]
        public JsonResult LeadsQuantityCancelSetSchedule(int id)
        {
            var res = _leadsQuantityForDealerBL.CancelSetSchedule(id);
            return new JsonResult(res);
        }
    }
}
