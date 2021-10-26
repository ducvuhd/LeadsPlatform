using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class CheckPermissionBL : ICheckPermissionBL
    {
        private IUsersDAL _usersDAL;
        private IUserPermissionDAL _userPermissionDAL;

        public CheckPermissionBL(IUsersDAL usersDAL, IUserPermissionDAL userPermissionDAL)
        {
            _usersDAL = usersDAL;
            _userPermissionDAL = userPermissionDAL;
        }
        public async Task<List<Users>> GetUsersAsync(ActivatingUserModel currUser, int cityId, int departmentId, List<int> lstRole, bool isAdmin = false, bool isApproved = false, bool isGetLeave = false, int month = 0)
        {
            month = month == 0 ? int.Parse(DateTime.Now.ToString("yyyyMM")) : month;

            var lstAllUsers = new List<Users>();
            if (departmentId > 0)
            {
                if (!isGetLeave)
                {
                    lstAllUsers = (await _usersDAL.GetWorkingByDepartmentAndMonthAsync(departmentId, month)).ToList();
                }
                else
                {
                    lstAllUsers = (await _usersDAL.GetByDepartmentIdAsync(departmentId, month)).ToList();
                }
            }
            else if (cityId > 0)
            {
                if (!isGetLeave)
                {
                    lstAllUsers = (await _usersDAL.GetWorkingByCityAndMonthAsync(cityId, month)).ToList();
                }
                else
                {
                    lstAllUsers = (await _usersDAL.GetByCityIdAsync(cityId)).ToList();
                }
            }
            else
            {
                if (!isGetLeave)
                {
                    lstAllUsers = (await _usersDAL.GetAllWorkingInMonthAsync(month)).ToList();
                }
                else
                {
                    lstAllUsers = (await _usersDAL.GetAllInMonthAsync(month)).ToList();
                }
            }

            if (this.HasPermission(currUser, SystemRole.Admin.GetHashCode()) || isAdmin)
            {
                return lstAllUsers;
            }

            var lstAllUserPermission = (await _userPermissionDAL.GetUserPermissionByUserNameAsync(currUser.UserName)).ToList();
            var lstPermission = new List<UserPermission>();
            if (lstAllUserPermission.Count > 0)
            {
                foreach (var role in lstRole)
                {
                    if (this.HasPermission(currUser, role))
                    {
                        lstPermission.AddRange(lstAllUserPermission.FindAll(p => p.RoleId == role));
                    }
                }
            }

            var lstUsers = new List<Users>();
            foreach (var userPermission in lstPermission)
            {
                if (userPermission.groupId == -1000 && userPermission.CityId == -1000)
                {
                    return lstAllUsers;
                }
                else if (userPermission.groupId > 0)
                {
                    var lst = lstAllUsers.FindAll(u => u.GroupId == userPermission.groupId);
                    lstUsers.AddRange(lst.Except(lstUsers));
                }
            }

            if (lstUsers.Find(u => u.UserName == currUser.UserName) == null && !isApproved)
            {
                lstUsers.Add(new Users
                {
                    //CityId = this.CityId,
                    //DepartmentId = this.DepartmentId,
                    UserName = currUser.UserName,
                    UserId = currUser.UserId,
                    //Status = this.Status,
                    //IPComputer = this.IPComputer,
                    //IsLogOnAll = this.IsLogOnAll,
                    //GroupId = this.GroupId
                });
            }

            return lstUsers;
        }

        public bool HasPermission(ActivatingUserModel currUser, int permission)
        {
            return currUser.Roles.Contains(permission);
        }

        public bool HasPermission(ActivatingUserModel currUser, params int[] permission)
        {
            var ret = false;
            foreach (var per in permission)
            {
                ret = currUser.Roles.Contains(per);
                if (ret) break;
            }
            return ret;
        }
    }
}
