using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.AttributeCustoms;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace APVN.LeadsPlatform.Models
{
    public class LeadsListModel : LeadsModel
    {
        public string CampName { get; set; }
        public string UserName { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string SecondHandStr { get; set; }
        public string LeadsBoxStr { get; set; }

        public LeadsListModel() { }

        public LeadsListModel(
            LeadsModel entity, List<Makes> makes,
            List<APVN.LeadsPlatform.Entity.Models> models,
            List<Entity.Campaign> campaignList, List<Users> userList)
        {
            if (entity != null)
            {
                var entityType = entity.GetType();
                foreach (var pModel in this.GetType().GetProperties())
                {
                    var pEntityName = entityType.GetProperty(pModel.Name);
                    var listBreack = new List<string>()
                    {
                        "StartCareDateName",
                        "SourceLeadName",
                        "StatusName",
                        "ActiveName",
                        "PaymentStatusName",
                        "CreatedDateName",
                        "LastModifiedDateName",
                        "ChangeCarTimeName",
                        "LoanMoneyName",
                        "StableIncomeName",
                        "GoodAddressName",
                        "IncomeName",
                        "LookingOtherCarName",
                        "GoodActionDealerName",
                        "IsChangeCarName",
                        "ChangeCarTimeName",
                        "IsSoldCarName",
                        "HasWifeName",
                        "HasChildName",
                        "FamilyIncomeName"
                    };
                    if (pEntityName != null && !listBreack.Contains(pEntityName.Name))
                    {
                        var value = pEntityName.GetValue(entity, null);
                        pModel.SetValue(this, value);
                    }
                }

                this.MakeName = "";
                this.ModelName = "";
                this.SecondHandStr = "";
                this.LeadsBoxStr = "";
                this.CampName = "";

                List<string> makeNameList = new List<string>();
                List<string> modelNameList = new List<string>();
                List<string> SecondHandList = new List<string>();
                List<string> leadsBoxList = new List<string>();
                List<string> campaignNameList = new List<string>();

                if (this.ListAction != null)
                {
                    foreach (var item in this.ListAction)
                    {
                        if (item.MakeId > 0 && !makeNameList.Contains(makes.Where(x => x.MakeID == item.MakeId)?.FirstOrDefault()?.MakeName))
                        {
                            makeNameList.Add(makes.Where(x => x.MakeID == item.MakeId)?.FirstOrDefault()?.MakeName);
                        }
                        if (item.ModelId > 0 && !modelNameList.Contains(models.Where(x => x.ModelID == item.ModelId)?.FirstOrDefault()?.ModelName))
                        {
                            modelNameList.Add(models.Where(x => x.ModelID == item.ModelId)?.FirstOrDefault()?.ModelName);
                        }
                        if (item.Type > 0)
                        {
                            foreach (KeyValuePair<int, string> entry in Utils.GetAllEnumValueAndDescription<LeadActionType>())
                            {
                                if (Utils.HasState(item.Type, (int)entry.Key) && !leadsBoxList.Contains(entry.Value))
                                {
                                    leadsBoxList.Add(entry.Value);
                                }
                            }
                        }
                        //if (item.SecondHand > 0 && !SecondHandList.Contains(Utils.GetEnumDescription((Secondhand)item.SecondHand)))
                        //{
                        //    SecondHandList.Add(Utils.GetEnumDescription((Secondhand)item.SecondHand));
                        //}
                        if (item.CampaignId > 0)
                        {
                            if (!campaignNameList.Contains(campaignList.Where(x => x.Id == item.CampaignId)?.FirstOrDefault()?.Name))
                            {
                                campaignNameList.Add(campaignList.Where(x => x.Id == item.CampaignId)?.FirstOrDefault()?.Name);
                            }
                        }
                        else
                        {
                            if (!campaignNameList.Contains("Hàng ngày"))
                            {
                                campaignNameList.Add("Hàng ngày");
                            }
                        }
                    }
                }

                if (makeNameList.Count > 0)
                {
                    if (makeNameList.Count > 3)
                    {
                        this.MakeName = String.Join(",", makeNameList.Take(3)) + "...";
                    }
                    else
                    {
                        this.MakeName = String.Join(",", makeNameList);
                    }
                }
                if (modelNameList.Count > 0)
                {
                    if (modelNameList.Count > 3)
                    {
                        this.ModelName = String.Join(",", modelNameList.Take(3)) + "...";
                    }
                    else
                    {
                        this.ModelName = String.Join(",", modelNameList);
                    }
                }
                if (leadsBoxList.Count > 0)
                {
                    if (leadsBoxList.Count > 3)
                    {
                        this.LeadsBoxStr = String.Join(",", leadsBoxList.Take(3)) + "...";
                    }
                    else
                    {
                        this.LeadsBoxStr = String.Join(",", leadsBoxList);
                    }
                }
                if (SecondHandList.Count > 0)
                {
                    if (SecondHandList.Count > 3)
                    {
                        this.SecondHandStr = String.Join(",", SecondHandList.Take(3)) + "...";
                    }
                    else
                    {
                        this.SecondHandStr = String.Join(",", SecondHandList);
                    }
                }
                // Campaign
                if (campaignNameList.Count > 3)
                {
                    this.CampName = String.Join(",", campaignNameList.Take(3)) + "...";
                }
                else
                {
                    this.CampName = String.Join(",", campaignNameList);
                }

                //this.CampName = campaignList.Where(x => x.Id == this.CampaignType)?.FirstOrDefault()?.Name;
                this.UserName = userList.Where(x => x.UserId == this.AssignToId)?.FirstOrDefault()?.UserName;

                // Status
                /*string jobStr = String.Empty;
                AuthorEnum.Jobs.TryGetValue(entity.Job, out jobStr);
                this.JobStr = jobStr;

                this.CateGroupStr = AuthorHelper.GetCateGroupByEnum(entity.CateGroup);

                this.Path = AuthorHelper.BuildLinkAuthor(entity.Alias);*/
            }
        }
    }
}
