using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.BusinessLayer.IServiceCache;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Models.Condition;
using APVN.LeadsPlatform.Models.IgnoreEqualModels;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database;
using APVN.LeadsPlatform.Utility.Database.Interfaces;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class LeadsBL : ILeadsBL
    {
        private readonly ILeadsDAL _leadsDAL;
        private readonly IRepository _repository;
        private readonly IMakeServiceCache _makeServiceCacheBL;
        private readonly IModelServiceCache _modelServiceCacheBL;
        private readonly ICityServiceCache _cityServiceCacheBL;
        private readonly IUserService _userService;
        private readonly IBankServiceCache _bankServiceCacheBL;
        private readonly ICustomAuthenticationAppService _appService;
        private readonly INoteBL _noteBL;
        private readonly IDealerLeadBL _dealerLeadBL;
        private readonly IHistoryBL _historyBL;
        private readonly ICampaignBL _campaignBL;

        public LeadsBL(ILeadsDAL leadsDAL, IRepository repository, IMakeServiceCache makeServiceCacheBL, IModelServiceCache modelServiceCacheBL, ICityServiceCache cityServiceCacheBL, IUserService userService, IBankServiceCache bankServiceCacheBL, ICustomAuthenticationAppService appService, INoteBL noteBL, IDealerLeadBL dealerLeadBL, IHistoryBL historyBL, ICampaignBL campaignBL)
        {
            _leadsDAL = leadsDAL;
            _repository = repository;
            _makeServiceCacheBL = makeServiceCacheBL;
            _modelServiceCacheBL = modelServiceCacheBL;
            _cityServiceCacheBL = cityServiceCacheBL;
            _userService = userService;
            _bankServiceCacheBL = bankServiceCacheBL;
            _appService = appService;
            _noteBL = noteBL;
            _campaignBL = campaignBL;
            _dealerLeadBL = dealerLeadBL;
            _historyBL = historyBL;
        }

        public Response Insert(LeadsModel model)
        {
            using (var uow = new UnitOfWork())
            {
                uow.Begin();
                try
                {
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    var id = _repository.CommandGetId<Leads, int>(model, "Leads_Insert", "Id");
                    History history = new History();
                    if (id > 0)
                    {
                        if (model.ListAction.Count > 0)
                        {
                            model.ListAction.ForEach(x =>
                                {
                                    x.LeadsId = id;
                                    x.CreatedBy = model.CreatedBy;
                                    x.CreatedDate = model.CreatedDate;
                                    x.LastModifiedBy = model.CreatedBy;
                                    x.LastModifiedDate = model.CreatedDate;
                                    x.Status = (int)LeadActionStatus.New;
                                }
                            );
                            var lstLeadsActionsEntity = new List<LeadsAction>(model.ListAction);
                            //var dataTable = SqlHelper.GetDataTable(lstLeadsActionsEntity, "Id");
                            //string typeName = "LeadActionType";
                            //_repository.CommandDataSet("LeadsAction_InsertDataSet", dataTable, typeName);
                            var lstActionExtend = new List<LeadsActionExtends>();
                            foreach (var item in model.ListAction)
                            {
                                int leadActionId = _repository.CommandGetId<LeadsAction, int>(item, "LeadsAction_Insert", "Id");

                                history.RelationId = id;
                                history.FieldName = $"Tạo mới box Thông  tin xe. Mã TT: {leadActionId}";
                                history.NewValue = "";
                                history.OldValue = "";
                                history.CreatedBy = model.CreatedBy;
                                history.CreatedDate = DateTime.Now;
                                _repository.Command(history, "History_Insert", "Id");

                                if (!string.IsNullOrEmpty(item.ListLeadsActionExtendsStr))
                                {
                                    item.ListLeadsActionExtends = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadsActionExtendsModel>>(item.ListLeadsActionExtendsStr);
                                }
                                foreach (var y in item.ListLeadsActionExtends)
                                {
                                    y.LeadsId = id;
                                    y.LeadsActionId = leadActionId;
                                    y.CreatedBy = model.CreatedBy;
                                    y.CreatedDate = DateTime.Now;
                                    y.LastModifiedBy = model.CreatedBy;
                                    y.LastModifiedDate = DateTime.Now;
                                    lstActionExtend.Add(y);
                                }
                            }
                            if (lstActionExtend.Count > 0)
                            {
                                var dataTable = SqlHelper.GetDataTable(lstActionExtend, "Id");
                                var typeName = "LeadsActionExtendsType_Insert";
                                _repository.CommandDataSet("LeadsActionExtends_InsertDataSet", dataTable, typeName);
                            }
                        }

                        history.RelationId = id;
                        history.Type = (int)HistoryType.Leads;
                        history.FieldName = "Tạo mới";
                        history.NewValue = "";
                        history.OldValue = "";
                        history.CreatedBy = model.CreatedBy;
                        history.CreatedDate = DateTime.Now;
                        _repository.Command(history, "History_Insert", "Id");

                        if (!string.IsNullOrEmpty(model.Note))
                        {
                            var note = new Note();
                            note.RelationId = id;
                            note.Type = (int)NoteType.Lead;
                            note.CurrentRelationStatus = model.Status;
                            note.Notes = model.Note;
                            note.CreatedDate = DateTime.Now;
                            note.CreatedBy = model.CreatedBy;
                            note.LastModifiedDate = DateTime.Now;
                            note.LastModifiedBy = model.CreatedBy;
                            _repository.Command(note, "Note_Insert", "Id");
                        }
                        uow.Commit();
                        return new Response(SystemCode.Success, "Tạo lead thành công", null);
                    }
                    else
                    {
                        uow.Rollback();
                        return new Response(SystemCode.Error, $"Có lỗi xảy ra! (id = 0)", null);
                    }
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public Response Update(LeadsModel newModel, LeadsModel oldModel)
        {
            // Make list
            var makeList = _makeServiceCacheBL.ListAll();
            // Model list
            var modelList = _modelServiceCacheBL.ListAll();
            // City list
            var cityList = _cityServiceCacheBL.ListAll();
            // User list
            var lstUsers = _userService.GetGetUserActive().ToList();
            //current user 
            var current = _appService.CurrUser();
            //Campaign
            var task = _campaignBL.ListAllAsync();
            Task.WaitAll(task);
            var campaigns = (List<Campaign>)task.Result;
            // Bank list
            var bankList = _bankServiceCacheBL.ListAll();
            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());

                    newModel.CityName = cityList.Find(x => x.CityID == newModel.CityId)?.CityName;
                    oldModel.CityName = cityList.Find(x => x.CityID == oldModel.CityId)?.CityName;

                    newModel.AssignToName = lstUsers.Find(x => x.UserId == newModel.AssignToId)?.UserName;
                    oldModel.AssignToName = lstUsers.Find(x => x.UserId == oldModel.AssignToId)?.UserName;

                    //Change StartCareDate
                    newModel.StartCareDate = newModel.AssignToId == oldModel.AssignToId ? oldModel.StartCareDate : DateTime.Now;

                    var lstHistory = new List<HistoryModel>();
                    var ignoreModel = new LeadUpdateEqualgnore();
                    bool isEqualObject = Utils.EqualObject(oldModel, newModel, ignoreModel, out lstHistory);
                    //so sánh thay đổi lead và add vào history
                    //if (!isEqualObject && lstHistory.Count > 0)
                    //{
                    newModel.LastModifiedDate = DateTime.Now;
                    newModel.LastModifiedBy = current.UserName;
                    _repository.Command<Leads>(newModel, "Leads_Update");
                    if (lstHistory.Count > 0)
                    {
                        lstHistory.ForEach(x =>
                        {
                            x.RelationId = newModel.Id;
                            x.Type = (int)HistoryType.Leads;
                            x.CreatedBy = newModel.CreatedBy;
                            x.CreatedDate = DateTime.Now;
                        });
                        var lstHistoryEntity = new List<History>(lstHistory);
                        var dataTableHistory = SqlHelper.GetDataTable(lstHistoryEntity, "Id");
                        var typeName = "HistoryType_Insert";
                        _repository.CommandDataSet("History_InsertDataSet", dataTableHistory, typeName);
                    }
                    //}
                    //so sánh thay đổi của list action(thêm + sửa)
                    #region Insert, update LeadsAction, LeadsActionExtends
                    var lstActionInsert = newModel.ListAction.Where(x => x.Id == 0).ToList();
                    var lstActionUpdate = newModel.ListAction.Where(x => x.Id > 0).ToList();

                    lstActionInsert.ForEach(x =>
                    {
                        x.LeadsId = oldModel.Id;
                        x.CreatedBy = newModel.CreatedBy;
                        x.CreatedDate = DateTime.Now;
                        x.LastModifiedBy = newModel.CreatedBy;
                        x.LastModifiedDate = DateTime.Now;
                    });
                    lstActionUpdate.Where(x => x.Id > 0).ToList().ForEach(x =>
                    {
                        x.LeadsId = oldModel.Id;
                        x.LastModifiedBy = newModel.CreatedBy;
                        x.LastModifiedDate = DateTime.Now;
                    });
                    var lstActionExtendInsert = new List<LeadsActionExtends>();
                    var lstActionExtendUpdate = new List<LeadsActionExtends>();
                    if (lstActionInsert.Count > 0)
                    {
                        //var lstActionEntity = new List<LeadsAction>(lstActionInsert);
                        //var dataTableActionInsert = SqlHelper.GetDataTable(lstActionEntity, "Id");
                        //_repository.CommandDataSet("LeadsAction_InsertDataSet", dataTableActionInsert, "LeadActionType");
                        foreach (var actionInsert in lstActionInsert)
                        {
                            int leadActionId = _repository.CommandGetId<LeadsAction, int>(actionInsert, "LeadsAction_Insert", "Id");

                            History history = new History();
                            history.RelationId = oldModel.Id;
                            history.FieldName = $"Tạo mới box Thông  tin xe. Mã TT: {leadActionId}";
                            history.NewValue = "";
                            history.OldValue = "";
                            history.CreatedBy = current.UserName;
                            history.CreatedDate = DateTime.Now;
                            _repository.Command(history, "History_Insert", "Id");

                            if (!string.IsNullOrEmpty(actionInsert.ListLeadsActionExtendsStr))
                            {
                                actionInsert.ListLeadsActionExtends = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadsActionExtendsModel>>(actionInsert.ListLeadsActionExtendsStr);
                            }
                            foreach (var y in actionInsert.ListLeadsActionExtends)
                            {
                                y.LeadsId = actionInsert.LeadsId;
                                y.LeadsActionId = leadActionId;
                                y.CreatedBy = actionInsert.CreatedBy;
                                y.CreatedDate = DateTime.Now;
                                y.LastModifiedBy = actionInsert.CreatedBy;
                                y.LastModifiedDate = DateTime.Now;
                                lstActionExtendInsert.Add(y);
                            }
                        }
                    }
                    if (lstActionUpdate.Count > 0)
                    {
                        var lstActionEntity = new List<LeadsAction>(lstActionUpdate);
                        var dataTableActionUpdate = SqlHelper.GetDataTable(lstActionEntity);
                        _repository.CommandDataSet("LeadsAction_UpdateDataSet", dataTableActionUpdate, "LeadActionType_Update");
                        foreach (var actionUpdate in lstActionUpdate)
                        {
                            if (!string.IsNullOrEmpty(actionUpdate.ListLeadsActionExtendsStr))
                            {
                                actionUpdate.ListLeadsActionExtends = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LeadsActionExtendsModel>>(actionUpdate.ListLeadsActionExtendsStr);
                            }
                            var lstActionExtendsInsert = actionUpdate.ListLeadsActionExtends.Where(x => x.Id == 0).ToList();
                            foreach (var y in lstActionExtendsInsert)
                            {
                                y.LeadsId = actionUpdate.LeadsId;
                                y.LeadsActionId = actionUpdate.Id;
                                y.CreatedBy = actionUpdate.CreatedBy;
                                y.CreatedDate = DateTime.Now;
                                y.LastModifiedBy = actionUpdate.CreatedBy;
                                y.LastModifiedDate = DateTime.Now;
                                lstActionExtendInsert.Add(y);
                            }
                            var lstActionExtendsUpdate = actionUpdate.ListLeadsActionExtends.Where(x => x.Id > 0).ToList();
                            foreach (var y in lstActionExtendsUpdate)
                            {
                                y.LeadsId = actionUpdate.LeadsId;
                                y.LeadsActionId = actionUpdate.Id;
                                y.LastModifiedBy = actionUpdate.LastModifiedBy;
                                y.LastModifiedDate = DateTime.Now;
                                lstActionExtendUpdate.Add(y);
                            }
                        }
                        if (lstActionExtendUpdate.Count > 0)
                        {
                            var dataTable = SqlHelper.GetDataTable(lstActionExtendUpdate);
                            var typeName = "LeadsActionExtendsType_Update";
                            _repository.CommandDataSet("LeadsActionExtends_UpdateDataSet", dataTable, typeName);
                        }
                    }
                    if (lstActionExtendInsert.Count > 0)
                    {
                        var dataTable = SqlHelper.GetDataTable(lstActionExtendInsert, "Id");
                        var typeName = "LeadsActionExtendsType_Insert";
                        _repository.CommandDataSet("LeadsActionExtends_InsertDataSet", dataTable, typeName);
                    }
                    #endregion
                    //update history khi thay đổi leadaction
                    #region Insert history thay đổi của LeadsAction
                    var lstActionHistory = new List<History>();
                    foreach (var item in lstActionUpdate)
                    {
                        string oldValue = "";
                        string newValue = "";
                        var itemOld = oldModel.ListAction.Find(x => x.Id == item.Id);
                        if (itemOld != null)
                        {
                            if (itemOld.MakeId != item.MakeId)
                            {
                                oldValue += $", Hãng xe: {makeList.Find(x => x.MakeID == itemOld.MakeId)?.MakeName}";
                                newValue += $", Hãng xe: {makeList.Find(x => x.MakeID == item.MakeId)?.MakeName}";
                            }
                            if (itemOld.ModelId != item.ModelId)
                            {
                                oldValue += $", Dòng xe: {modelList.Find(x => x.ModelID == itemOld.ModelId)?.ModelName}";
                                newValue += $", Dòng xe: {modelList.Find(x => x.ModelID == item.ModelId)?.ModelName}";
                            }
                            if (itemOld.SaleSetType != item.SaleSetType)
                            {
                                oldValue += $", Nhu cầu khác: {itemOld.SaleSetTypeName}";
                                newValue += $", Nhu cầu khác: {item.SaleSetTypeName}";
                            }
                            if (itemOld.CampaignId != item.CampaignId)
                            {
                                oldValue += $", Campaign: {campaigns.Find(x => x.Id == itemOld.CampaignId)?.Name}";
                                newValue += $", Campaign: {campaigns.Find(x => x.Id == item.CampaignId)?.Name}";
                            }
                            if (itemOld.PaymentStatus != item.PaymentStatus)
                            {
                                oldValue += $", TT Thanh toán: {itemOld.PaymentStatusName}";
                                newValue += $", TT Thanh toán: {item.SaleSetTypeName}";
                            }
                            string newSecondHand = "", oldSecondHand = "", newCity = "", oldCity = "", newType = "", oldType = "";
                            string newBank = "", oldBank = "", newLoanMoney = "", oldLoanMoney = "", newLoanTime = "", oldLoanTime = "", newCurrPrice = "", oldCurrPrice = "";
                            string newBargainPrice = "", oldBargainPrice = "";
                            foreach (var extendsOld in itemOld.ListLeadsActionExtends)
                            {
                                var extendsNew = lstActionExtendUpdate.FindAll(x => x.Id == extendsOld.Id).FirstOrDefault();
                                if (extendsNew != null)
                                {
                                    if (extendsOld.SecondHand != extendsNew.SecondHand)
                                    {
                                        oldSecondHand += $", {(extendsOld.SecondHand > 0 ? Utils.GetEnumDescription((Secondhand)extendsOld.SecondHand) : "")}";
                                        newSecondHand += $", {(extendsNew.SecondHand > 0 ? Utils.GetEnumDescription((Secondhand)extendsNew.SecondHand) : "")}";
                                    }
                                    if (extendsOld.CityId != extendsNew.CityId)
                                    {
                                        oldCity += $", {cityList.Find(x => extendsOld.CityId == x.CityID)?.CityName}";
                                        newCity += $", {cityList.Find(x => extendsNew.CityId == x.CityID)?.CityName}";
                                    }

                                    if (extendsOld.Type != extendsNew.Type)
                                    {
                                        oldSecondHand += $", {(extendsOld.SecondHand > 0 ? Utils.GetEnumDescription((LeadActionExtendsType)extendsOld.Type) : "")}";
                                        newSecondHand += $", {(extendsNew.SecondHand > 0 ? Utils.GetEnumDescription((LeadActionExtendsType)extendsNew.Type) : "")}";
                                    }
                                    if (extendsOld.BankId != extendsNew.BankId)
                                    {
                                        oldBank += $", {bankList.Find(x => x.Id == extendsOld.BankId)?.Name}";
                                        newBank += $", {bankList.Find(x => x.Id == extendsNew.BankId)?.Name}";
                                    }
                                    if (extendsOld.LoanMoney != extendsNew.LoanMoney)
                                    {
                                        oldLoanMoney += $", {extendsOld.LoanMoney.ToString("#,###,###")} VNĐ";
                                        newLoanMoney += $", {extendsNew.LoanMoney.ToString("#,###,###")} VNĐ";
                                    }
                                    if (extendsOld.LoanTime != extendsNew.LoanTime)
                                    {
                                        oldLoanTime += $", {extendsOld.LoanTime} tháng";
                                        newLoanTime += $", {extendsNew.LoanTime} tháng";
                                    }
                                    if (extendsOld.BargainCurrentPrices != extendsNew.BargainCurrentPrices)
                                    {
                                        oldCurrPrice += $", {extendsOld.BargainCurrentPrices.ToString("#,###,###")} VNĐ";
                                        newCurrPrice += $", {extendsNew.BargainCurrentPrices.ToString("#,###,###")} VNĐ";
                                    }
                                    if (extendsOld.BargainPrices != extendsNew.BargainPrices)
                                    {
                                        oldBargainPrice += $", {extendsOld.BargainPrices.ToString("#,###,###")} VNĐ";
                                        newBargainPrice += $", {extendsNew.BargainPrices.ToString("#,###,###")} VNĐ";
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(oldSecondHand) && !string.IsNullOrEmpty(newSecondHand))
                            {
                                oldValue += $", Tình trạng: {oldSecondHand.Trim(',').Trim()}";
                                newValue += $", Tình trạng: {newSecondHand.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldCity) && !string.IsNullOrEmpty(newCity))
                            {
                                oldValue += $", TP: {oldCity.Trim(',').Trim()}";
                                newValue += $", TP: {newCity.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldType) && !string.IsNullOrEmpty(newType))
                            {
                                oldValue += $", Box thu lead: {oldType.Trim(',').Trim()}";
                                newValue += $", Box thu lead: {newType.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldBank) && !string.IsNullOrEmpty(newBank))
                            {
                                oldValue += $", Ngân hàng: {oldBank.Trim(',').Trim()}";
                                newValue += $", Ngân hàng: {newBank.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldLoanMoney) && !string.IsNullOrEmpty(newLoanMoney))
                            {
                                oldValue += $", Số tiền vay: {oldLoanMoney.Trim(',').Trim()}";
                                newValue += $", Số tiền vay: {newLoanMoney.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldLoanTime) && !string.IsNullOrEmpty(newLoanTime))
                            {
                                oldValue += $", TG vay: {oldLoanTime.Trim(',').Trim()}";
                                newValue += $", TG vay: {newLoanTime.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldCurrPrice) && !string.IsNullOrEmpty(newCurrPrice))
                            {
                                oldValue += $", Giá xe hiện tại: {oldCurrPrice.Trim(',').Trim()}";
                                newValue += $", Giá xe hiện tại: {newCurrPrice.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(oldBargainPrice) && !string.IsNullOrEmpty(newBargainPrice))
                            {
                                oldValue += $", Giá xe trả giá: {oldBargainPrice.Trim(',').Trim()}";
                                newValue += $", Giá xe trả giá: {newBargainPrice.Trim(',').Trim()}";
                            }
                            if (!string.IsNullOrEmpty(newValue) || !string.IsNullOrEmpty(oldValue))
                            {
                                History history = new History();
                                history.RelationId = newModel.Id;
                                history.Type = (int)HistoryType.Leads;
                                history.FieldName = $"Thông tin xe(Mã: {item.Id})";
                                history.NewValue = newValue.Trim(',');
                                history.OldValue = oldValue.Trim(',');
                                history.CreatedBy = newModel.CreatedBy;
                                history.CreatedDate = DateTime.Now;
                                lstActionHistory.Add(history);
                            }
                        }
                    }
                    if (lstActionHistory.Count > 0)
                    {
                        var dataTableHistory = SqlHelper.GetDataTable(lstActionHistory, "Id");
                        var typeName = "HistoryType_Insert";
                        _repository.CommandDataSet("History_InsertDataSet", dataTableHistory, typeName);
                    }
                    #endregion

                    uow.Commit();
                    return new Response(SystemCode.Success, "Sửa thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public LeadsModel GetById(int id)
        {
            // Make list
            var makeList = _makeServiceCacheBL.ListAll();
            // Model list
            var modelList = _modelServiceCacheBL.ListAll();
            // City list
            var cityList = _cityServiceCacheBL.ListAll();
            // Bank list
            var bankList = _bankServiceCacheBL.ListAll();
            // Campaign
            var task = _campaignBL.ListAllAsync();
            Task.WaitAll(task);
            var campaigns = (List<Campaign>)task.Result;
            // User list
            var lstUsers = _userService.GetGetUserActive().ToList();

            var leadsModel = new LeadsModel();
            leadsModel = _leadsDAL.GetLeadsModelById(id);

            if (leadsModel != null)
            {
                leadsModel.CityName = cityList.Find(x => x.CityID == leadsModel.CityId)?.CityName;
                leadsModel.AssignToName = lstUsers.Find(x => x.UserId == leadsModel.AssignToId)?.UserName;
                leadsModel.ListAction.ForEach(x =>
                {
                    x.MakeName = makeList.Find(item => item.MakeID == x.MakeId)?.MakeName;
                    x.ModelName = modelList.Find(item => item.ModelID == x.ModelId)?.ModelName;
                    x.CampaignName = campaigns.Find(c => c.Id == x.CampaignId)?.Name;
                    x.DealerLeadList = _dealerLeadBL.GetDealerLeadsActionById(x.Id);
                    foreach (var extend in x.ListLeadsActionExtends)
                    {
                        extend.CityName = cityList.Find(c => c.CityID == extend.CityId)?.CityName;
                        extend.BankName = bankList.Find(c => c.Id == extend.BankId)?.Name;
                    }
                });
            }
            return leadsModel;
        }
        public bool CheckExistMobile(string mobile)
        {
            return _leadsDAL.CheckExistMobile(mobile) > 0;
        }
        public Task<string> GetFormFilter()
        {
            return null;
        }
        public List<LeadsModel> GetList(LeadsFilterModel filterModel, out int totalRow)
        {
            filterModel.KeyWord = filterModel.KeyWord.Trim().ToLower();

            // Lọc lấy membershipid bên oto
            // kết nối sang oto để lấy data membership
            List<UserProfile> dealerList = String.IsNullOrEmpty(filterModel.DealerName) ? null : _dealerLeadBL.GetUserByName(
                filterModel.DealerName);

            filterModel.DealerIds = dealerList != null && dealerList.Count > 0 ? String.Join(",", dealerList.Select(x => x.UserId).ToList()) : String.Empty;

            var leadsList = (List<LeadsModel>)_leadsDAL.GetList(filterModel, out totalRow);

            if (leadsList != null)
            {
                foreach (var item in leadsList)
                {
                    item.ListAction = _leadsDAL.GetListActionByLeadId(item.Id).ToList();
                }
            }
            return leadsList;
        }
        public Response UpdateStatus(LeadsModel leadsModel, LeadsChangeStatusModel statusModel)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    _leadsDAL.UpdateStatus(statusModel);
                    ////Lưu note nếu đó bị từ chối
                    //if (statusModel.Status == (int)LeadStatus.Rejected && statusModel.noteModel != null)
                    //{
                    //    var note = new Note();
                    //    note.RelationId = statusModel.LeadsId;
                    //    note.Type = (int)NoteType.Lead;
                    //    note.LeadsStatusAtTime = statusModel.Status;
                    //    note.Notes = statusModel.noteModel.Notes;
                    //    note.CreatedDate = DateTime.Now;
                    //    note.CreatedBy = statusModel.ChangeStatusBy;
                    //    note.LastModifiedDate = DateTime.Now;
                    //    note.LastModifiedBy = statusModel.ChangeStatusBy;
                    //    _repository.Command(note, "Note_Insert", "Id");
                    //}
                    //Lưu history
                    History history = new History();
                    history.RelationId = statusModel.LeadsId;
                    history.Type = (int)HistoryType.Leads;
                    history.FieldName = $"Trạng thái";
                    history.NewValue = Utils.GetEnumDescription((LeadStatus)statusModel.Status);
                    history.OldValue = Utils.GetEnumDescription((LeadStatus)leadsModel.Status);
                    history.CreatedBy = statusModel.ChangeStatusBy;
                    history.CreatedDate = DateTime.Now;
                    _repository.Command(history, "History_Insert", "Id");
                    uow.Commit();
                    return new Response(SystemCode.Success, "Thay đổi trạng thái thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public Response TakeLead(int leadsId)
        {

            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    var lead = _repository.GetById<Leads, int>(leadsId, "Leads_GetById");
                    var currUser = _appService.CurrUser();
                    var lstUsers = _userService.GetGetUserActive().ToList();
                    if (lead != null)
                    {
                        //Lưu history
                        History history = new History();
                        history.RelationId = leadsId;
                        history.Type = (int)HistoryType.Leads;
                        history.CreatedBy = currUser.UserName;
                        history.CreatedDate = DateTime.Now;

                        history.FieldName = $"NV Chăm sóc";
                        history.NewValue = currUser.UserName;
                        history.OldValue = lstUsers.Find(x => x.UserId == lead.AssignToId)?.UserName;
                        _repository.Command(history, "History_Insert", "Id");

                        history.FieldName = $"Ngày bắt đầu CS";
                        history.NewValue = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        history.OldValue = lead.StartCareDate?.ToString("dd/MM/yyyy HH:mm:ss");
                        _repository.Command(history, "History_Insert", "Id");

                        lead.AssignToId = currUser.UserId;
                        lead.StartCareDate = DateTime.Now;
                        _repository.Command(lead, "Leads_Update");
                    }
                    uow.Commit();
                    return new Response(SystemCode.Success, "Nhận Lead thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }

        }
        public Response AssigToLead(int leadsId, int assignToId)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    var lead = _repository.GetById<Leads, int>(leadsId, "Leads_GetById");
                    var currUser = _appService.CurrUser();
                    var lstUsers = _userService.GetGetUserActive().ToList();
                    if (lead != null)
                    {
                        //Lưu history
                        History history = new History();
                        history.RelationId = leadsId;
                        history.Type = (int)HistoryType.Leads;
                        history.CreatedBy = currUser.UserName;
                        history.CreatedDate = DateTime.Now;

                        history.FieldName = $"NV Chăm sóc";
                        history.NewValue = lstUsers.Find(x => x.UserId == assignToId)?.UserName;
                        history.OldValue = lstUsers.Find(x => x.UserId == lead.AssignToId)?.UserName;
                        _repository.Command(history, "History_Insert", "Id");

                        history.FieldName = $"Ngày bắt đầu CS";
                        history.NewValue = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        history.OldValue = lead.StartCareDate?.ToString("dd/MM/yyyy HH:mm:ss");
                        _repository.Command(history, "History_Insert", "Id");

                        lead.AssignToId = assignToId;
                        lead.StartCareDate = DateTime.Now;
                        _repository.Command(lead, "Leads_Update");
                    }
                    uow.Commit();
                    return new Response(SystemCode.Success, "Giao Lead thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public Response AssigLeadToDealer(AssigLeadToDealerModel assigLeadToDealer)
        {

            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());

                    for (int i = 0; i < assigLeadToDealer.LstDealerId.Count; i++)
                    {
                        var condition = new LeadsActionIdCondition();
                        condition.LeadsActionId = assigLeadToDealer.LeadsActionId;
                        var listDealerLeadsAction = _repository.List<DealerLeadsAction, LeadsActionIdCondition>(condition, "DealerLeadsAction_GetByLeadsActionId").ToList();
                        //nếu dealer đã đc gán vào lead này rồi thì không gán nữa
                        if (listDealerLeadsAction.Count > 0 && listDealerLeadsAction.FindAll(x => x.DealerId == assigLeadToDealer.LstDealerId[i]).Count > 0)
                        {
                            continue;
                        }

                        //Lưu dealer
                        var entity = new DealerLeadsAction();
                        entity.DealerId = assigLeadToDealer.LstDealerId[i];
                        entity.LeadsId = assigLeadToDealer.LeadsId;
                        entity.LeadActionId = assigLeadToDealer.LeadsActionId;
                        entity.DealerName = assigLeadToDealer.LstDealerName[i];
                        entity.DealerRecievedDate = DateTime.Now;
                        entity.DealerNote = assigLeadToDealer.DealerNote;
                        entity.Status = (int)DealerLeadActionStatus.WatingContact;
                        entity.AssignType = (int)DealerLeadActionAssignType.SaleSet;
                        _repository.Command(entity, "DealerLeadsAction_Insert_25_5_2021", "Id");
                        //update lại status lead action
                        var leadAction = _repository.GetById<LeadsAction, int>(assigLeadToDealer.LeadsActionId, "LeadsAction_GetById_V1");
                        if (leadAction != null && leadAction.Status != (int)LeadActionStatus.Running)
                        {
                            leadAction.Status = (int)LeadActionStatus.Running;
                            leadAction.PaymentStatus = leadAction.PaymentStatus > 0 ? leadAction.PaymentStatus : assigLeadToDealer.LeadActionPaymentStatus;
                            _repository.Command<LeadsAction>(leadAction, "LeadsAction_Update");
                        }
                        //Lưu history
                        var currUser = _appService.CurrUser();

                        History history = new History();
                        history.RelationId = assigLeadToDealer.LeadsId;
                        history.Type = (int)HistoryType.Leads;
                        history.CreatedBy = currUser.UserName;
                        history.CreatedDate = DateTime.Now;

                        history.FieldName = $"Gán cho dealer";
                        history.NewValue = $"Dealer: {assigLeadToDealer.LstDealerName[i]}, Ngày Dealer nhận lead: {DateTime.Now.ToString("dd/MM/yyyy")}";
                        history.OldValue = "";
                        _repository.Command(history, "History_Insert", "Id");
                    }
                    uow.Commit();
                    return new Response(SystemCode.Success, "Gán Lead cho dealer thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }

        }

        public async Task<LeadsDetailModel> GetDetail(int id)
        {
            // Make list
            var makeList = _makeServiceCacheBL.ListAll();
            // Model list
            var modelList = _modelServiceCacheBL.ListAll();
            // City list
            var cityList = _cityServiceCacheBL.ListAll();
            // Bank list
            var bankList = _bankServiceCacheBL.ListAll();
            // Campaign
            var campaignList = await _campaignBL.ListAllAsync();
            // User list
            var lstUsers = _userService.GetGetUserActive().ToList();

            var leadDetailModel = new LeadsDetailModel();
            var leadsModel = new LeadsModel();
            var LeadNoteList = new List<NoteModel>();
            var LeadHistoryList = new List<HistoryModel>();

            leadsModel = _leadsDAL.GetLeadsModelById(id);

            if (leadsModel != null)
            {
                leadsModel.CityName = cityList.Find(x => x.CityID == leadsModel.CityId)?.CityName;
                leadsModel.AssignToName = lstUsers.Find(x => x.UserId == leadsModel.AssignToId)?.UserName;
                leadsModel.ListAction.ForEach(x =>
                {
                    x.MakeName = makeList.Find(item => item.MakeID == x.MakeId)?.MakeName;
                    x.ModelName = modelList.Find(item => item.ModelID == x.ModelId)?.ModelName;
                    x.CampaignName = campaignList.Find(c => c.Id == x.CampaignId)?.Name;
                    x.DealerLeadList = _dealerLeadBL.GetDealerLeadsActionById(x.Id);
                    foreach (var extend in x.ListLeadsActionExtends)
                    {
                        extend.CityName = cityList.Find(c => c.CityID == extend.CityId)?.CityName;
                        extend.BankName = bankList.Find(c => c.Id == extend.BankId)?.Name;
                    }
                });

                // Note
                LeadNoteList = (List<NoteModel>)_noteBL.GetListByRelationIdAndTypeNoCount(leadsModel.Id, (int)NoteType.Lead);

                // History
                LeadHistoryList = (List<HistoryModel>)_historyBL.GetListByRelationIdAndTypeNoCount(leadsModel.Id, (int)HistoryType.Leads);

                // Gom lại
                leadDetailModel.LeadsModel = leadsModel;
                leadDetailModel.LeadNoteList = LeadNoteList;
                leadDetailModel.LeadHistoryList = LeadHistoryList;
            }
            return leadDetailModel;
        }
        public Response ChangeIsActive(int leadsId, bool isActive)
        {
            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    var leadModel = _repository.GetById<Leads, int>(leadsId, "Leads_GetById");
                    if (leadModel != null)
                    {
                        string sql = $@"UPDATE dbo.Leads SET IsActive = '{isActive}' WHERE Id = {leadsId}";
                        _repository.CommandSql(sql);
                        var currUser = _appService.CurrUser();
                        //Lưu history
                        History history = new History();
                        history.RelationId = leadsId;
                        history.Type = (int)HistoryType.Leads;
                        history.FieldName = $"IsActive, {(isActive ? "Mở khóa lead" : "Khóa lead")}";
                        history.NewValue = isActive.ToString();
                        history.OldValue = leadModel.IsActive.ToString();
                        history.CreatedBy = currUser.UserName;
                        history.CreatedDate = DateTime.Now;
                        _repository.Command(history, "History_Insert", "Id");
                        uow.Commit();
                        var titleSuccess = (isActive ? "Mở khóa lead thành công" : "Khóa lead thành công");
                        return new Response(SystemCode.Success, titleSuccess, null);
                    }
                    else
                    {
                        uow.Rollback();
                        return new Response(SystemCode.DataNull, "Không tìm thấy Lead", null);
                    }
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public Response ImportLeadFromExcel(List<LeadsModel> lstModel)
        {

            using (var uow = new UnitOfWork())
            {
                try
                {
                    uow.Begin();
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    var currUser = _appService.CurrUser();
                    History history = new History();
                    var lstActionExtend = new List<LeadsActionExtends>();
                    foreach (var model in lstModel)
                    {
                        model.LastModifiedDate = DateTime.Now;
                        model.LastModifiedBy = currUser.UserName;
                        model.Status = (int)LeadStatus.New;
                        model.IsActive = true;
                        model.CreatedDate = model.CreatedDate != null ? model.CreatedDate : DateTime.Now;
                        model.CreatedBy = currUser.UserName;
                        var fullNameSearch = Utils.UnicodeToKoDau(model.FullName.Trim()).ToLower();
                        model.FullTextSearch = $"{fullNameSearch} {model.Mobile.Trim()} {(model.Email ?? "").Trim().ToLower()}";
                        var id = _repository.CommandGetId<Leads, int>(model, "Leads_Insert", "Id");
                        if (id > 0)
                        {
                            if (model.ListAction.Count > 0)
                            {
                                foreach (var item in model.ListAction)
                                {
                                    item.LeadsId = id;
                                    item.CreatedBy = model.CreatedBy;
                                    item.CreatedDate = model.CreatedDate;
                                    item.LastModifiedBy = model.CreatedBy;
                                    item.LastModifiedDate = DateTime.Now;
                                    item.Status = (int)LeadActionStatus.New;

                                    int leadActionId = _repository.CommandGetId<LeadsAction, int>(item, "LeadsAction_Insert", "Id");
                                    history.RelationId = id;
                                    history.FieldName = $"Tạo mới box Thông  tin xe. Mã TT: {leadActionId}";
                                    history.NewValue = "";
                                    history.OldValue = "";
                                    history.CreatedBy = model.CreatedBy;
                                    history.CreatedDate = DateTime.Now;
                                    _repository.Command(history, "History_Insert", "Id");

                                    foreach (var y in item.ListLeadsActionExtends)
                                    {
                                        y.LeadsId = id;
                                        y.LeadsActionId = leadActionId;
                                        y.CreatedBy = model.CreatedBy;
                                        y.CreatedDate = DateTime.Now;
                                        y.LastModifiedBy = model.CreatedBy;
                                        y.LastModifiedDate = DateTime.Now;
                                        lstActionExtend.Add(y);
                                    }
                                }
                                if (lstActionExtend.Count > 0)
                                {
                                    var dataTable = SqlHelper.GetDataTable(lstActionExtend, "Id");
                                    var typeName = "LeadsActionExtendsType_Insert";
                                    _repository.CommandDataSet("LeadsActionExtends_InsertDataSet", dataTable, typeName);
                                }
                            }
                            history.RelationId = id;
                            history.Type = (int)HistoryType.Leads;
                            history.FieldName = "Tạo mới";
                            history.NewValue = "";
                            history.OldValue = "";
                            history.CreatedBy = model.CreatedBy;
                            history.CreatedDate = DateTime.Now;
                            _repository.Command(history, "History_Insert", "Id");
                            if (!string.IsNullOrEmpty(model.Note))
                            {
                                var note = new Note();
                                note.RelationId = id;
                                note.Type = (int)NoteType.Lead;
                                note.CurrentRelationStatus = model.Status;
                                note.Notes = model.Note;
                                note.CreatedDate = DateTime.Now;
                                note.CreatedBy = model.CreatedBy;
                                note.LastModifiedDate = DateTime.Now;
                                note.LastModifiedBy = model.CreatedBy;
                                _repository.Command(note, "Note_Insert", "Id");
                            }
                        }
                    }
                    uow.Commit();
                    return new Response(SystemCode.Success, "", null);
                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public Response GetLeadsModelById(int id)
        {
            try
            {
                var leadModel = _leadsDAL.GetLeadsModelById(id);
                return new Response(SystemCode.Success, "", leadModel);

            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }
        }
        public Response UpdateLeadActionStatus(int status, int leadActionid)
        {
            try
            {
                _leadsDAL.UpdateLeadActionStatus(status, leadActionid);
                return new Response(SystemCode.Success, "Đổi TT thành công", null);

            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }
        }
       
    }
}
