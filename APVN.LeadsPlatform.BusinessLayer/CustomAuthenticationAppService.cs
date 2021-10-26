using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class CustomAuthenticationAppService : ICustomAuthenticationAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationAppService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ActivatingUserModel CurrUser()
        {
            var User = this._httpContextAccessor.HttpContext.User;
            if (User == null)
            {
                return null;
            }
            var userId = User.FindFirst(o => o.Type == ClaimTypes.SerialNumber).Value;
            var userName = User.FindFirst(o => o.Type == ClaimTypes.Name).Value;
            var role = User.FindFirst(o => o.Type == ClaimTypes.Role).Value;
            var strGroupId = User.FindFirst(o => o.Type == ClaimTypes.PrimaryGroupSid).Value;
            int.TryParse(strGroupId, out var groupId);
            var lstRoles = new List<int>();
            if (string.IsNullOrEmpty(role))
            {
                lstRoles.Add(SystemRole.Sales.GetHashCode());
            }
            else
            {
                var allRoleInSystem = Utils.GetAllEnumValueAndDescription<SystemRole>();

                var arrRoles = role.Split(',').ToList();
                foreach (var roleItem in arrRoles)
                {
                    var roleIntOutput = 0;
                    if (int.TryParse(roleItem, out roleIntOutput))
                    {
                        var roleInSystem = allRoleInSystem.Find(o => o.Key == roleIntOutput);
                        if (roleInSystem.Key > 0)
                        {
                            lstRoles.Add(roleInSystem.Key);
                        }
                    }
                }
            }
            return new ActivatingUserModel()
            {
                UserId = int.Parse(userId),
                UserName = userName,
                Roles = lstRoles,
                GroupId = groupId
            };
        }
    }
}
