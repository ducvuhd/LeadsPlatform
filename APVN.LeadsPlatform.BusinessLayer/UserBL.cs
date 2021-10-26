using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Database;
using APVN.LeadsPlatform.Utility.Database.Interfaces;
using APVN.LeadsPlatform.Utility.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class UserBL : Repository, IUserBL
    {
        private IUsersDAL _userDAL;
        private IUserRoleDAL _userRoleDAL;
        private ICustomAuthenticationAppService _appService;
        private IRepository _repository;

        public UserBL(IUsersDAL userDAL, IUserRoleDAL userRoleDAL, ICustomAuthenticationAppService appService, IRepository repository)
        {
            _userDAL = userDAL;
            _userRoleDAL = userRoleDAL;
            _appService = appService;
            _repository = repository;
        }

        public async Task<IEnumerable<Users>> GetByDepartmentIdAsync(int departmentId, int month)
        {
            return await _userDAL.GetByDepartmentIdAsync(departmentId, month);
        }

        public async Task<Users> GetByIdAsync(int userId)
        {
            try
            {
                return await _userDAL.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                return new Users();
            }
        }

        public async Task<IEnumerable<Users>> GetGetUserActiveAsync()
        {
            try
            {
                return await _userDAL.GetGetUserActiveAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Users> GetLoginAsync(string username, string password)
        {
            try
            {
                return await _userDAL.GetLoginAsync(username, password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserIndexModel> GetListAsync(UserIndexModel filter)
        {
            filter = await _userDAL.SearchAsync(filter);
            return filter;
        }

        public async Task<Users> GetByUserNameAsync(string username)
        {
            return await _userDAL.GetByUserNameAsync(username);
        }

        public Response Save(PermissionSave model)
        {
            var userInfo = _userDAL.GetByUserName(model.UserName);
            var lstRole = new List<int>();
            if (string.IsNullOrEmpty(userInfo.Role))
            {
                //userInfo.Role = model.RoleId.ToString();
                lstRole.Add(model.RoleId);
            }
            else
            {

                var strRole = userInfo.Role.Split(',');
                foreach (var roleItem in strRole)
                {
                    if (int.TryParse(roleItem, out int tempRole))
                    {
                        lstRole.Add(tempRole);
                    }
                }
                if (!lstRole.Contains(model.RoleId))
                {
                    lstRole.Add(model.RoleId);
                }
            }
            _userDAL.UpdateRole(model.UserName, string.Join(",", lstRole));
            _userDAL.InsertPermission(model);
            return new Response()
            {
                Code = SystemCode.Success,
                Message = ""
            };
        }

        public async Task<IEnumerable<UserPermission>> GetPermissionByUserName(string userName)
        {
            return await _userDAL.GetPermissionByUserNameAsync(userName);
        }

        public Response DeletePermission(int id)
        {
            _userDAL.DeletePermission(id);
            return new Response()
            {
                Code = SystemCode.Success,
                Message = ""
            };
        }

        public bool ResetPassword(string userName, string password)
        {
            return _userDAL.ResetPassword(userName, password);
        }
        public async Task<Response> GetForEdit(int userId)
        {
            try
            {
                var user = await _userDAL.GetByIdAsync(userId);
                var lstRole = await _userRoleDAL.GetRoleByUserId(userId);
                return new Response(SystemCode.Success, "", new { user = user, lstRole = lstRole });
            }
            catch (Exception ex)
            {
                return new Response(SystemCode.Error, ex.Message, null);
            }
        }
        public Response Update(UserUpdateIndexModel model)
        {
            using (var uow = new UnitOfWork())
            {
                uow.Begin();
                try
                {
                    _repository.SetWriteDbContext(uow.GetConnection(), uow.GetTransaction());
                    int userId = 0;
                    if (model.UserId > 0)
                    {
                        _repository.Command<UserUpdateModel>(model, "User_Update", "Password", "CreatedDate", "UpdatedBy");
                        userId = model.UserId;
                    }
                    else
                    {
                        model.UpdatedBy = _appService.CurrUser().UserName;
                        model.CreatedDate = DateTime.Now;
                        model.Password = model.Password.ToMD5();
                        userId = _repository.CommandGetId<UserUpdateModel, int>(model, "User_Insert", "UserId");
                    }
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        if (model.UserId > 0)
                        {
                            string sql = $"DELETE dbo.UserRole WHERE PersonId = {model.UserId}";
                            _repository.CommandSql(sql);
                        }
                        var lstRole = model.Role.Split(',');
                        foreach (var role in lstRole)
                        {
                            string sql = $@"
                                    INSERT dbo.UserRole
                                        ( RoleId, PersonId, Type, IsInherit )
                                    VALUES  ( {role}, -- RoleId - int
                                              {userId}, -- PersonId - int
                                              0, -- Type - int
                                              1  -- IsInherit - bit
                                              )
                                ";
                            _repository.CommandSql(sql);
                        }
                    }
                    uow.Commit();
                    return new Response(SystemCode.Success, "Thao tác thành công", null);
                }
                catch (Exception ex)
                {
                    uow.Commit();
                    return new Response(SystemCode.Error, ex.Message, null);
                }
            }
        }
        public void UpdateOTPPrivateKey(int userId, string otpPrivateKey)
        {
            _userDAL.UpdateOTPPrivateKey(userId, otpPrivateKey);
        }
    }
}
