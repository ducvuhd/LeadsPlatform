using APVN.LeadsPlatform.BusinessLayer.Cache;
using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.Caching;
using APVN.LeadsPlatform.DataAccessLayer.IDataAccessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using APVN.LeadsPlatform.Utility.GoogleOTP;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer
{
    public class UserService : CacheService, IUserService
    {
        private static readonly object _objLocked = new object();
        IHttpContextAccessor _httpContextAccessor;
        private ICache _cacheService;
        private IUsersDAL _usersDAL;
        private IUserRoleDAL _userRoleDAL;

        public UserService(ICache cache, IHttpContextAccessor httpContextAccessor, IUsersDAL usersDAL, IUserRoleDAL userRoleDAL) : base(cache)
        {
            _cacheService = cache;
            _httpContextAccessor = httpContextAccessor;
            _usersDAL = usersDAL;
            _userRoleDAL = userRoleDAL;
        }

        public async Task<Response> Login(string username, string password, string secureCode = "")
        {
            var response = new Response();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.Message = "Tên đăng nhập hay mật khẩu trống";
                response.Code = SystemCode.DataNull;
                return response;
            }

            var encryptPassword = GenratePassword(password);
            Users user = await _usersDAL.GetLoginAsync(username, encryptPassword);
            if (user == null)
            {
                response.Code = SystemCode.NotValid;
                response.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
                return response;
            }

            if (user.Status == (short)UserStatus.Inactive)
            {
                response.Code = SystemCode.NotPermitted;
                response.Message = "Tài khoản đã bị khóa. Xin vui lòng liên hệ Admin để được giải quyết.";
                return response;
            }
            //Check OTP
            //var lstUserNotNeedOtp = AppSettings.GetAppKey("UserNotNeedOTP").Split(',');
            //if (!lstUserNotNeedOtp.Contains(user.UserName))
            //{
            //    var secretkey = AppSettings.GetAppKey("OTPSecretKey") + user.OTPPrivateKey;
            //    if (!GoogleTOTP.IsVaLid(secretkey, secureCode))
            //    {
            //        return new Response(SystemCode.Error, "Incorrect OTP Key", null);
            //    }
            //}

            List<UserRole> lstUserRole = (await _userRoleDAL.GetRoleByUserId(user.UserId)).ToList();
            user.Role = string.Join(",", lstUserRole.Select(x => x.RoleId).ToList());
            string token = string.Empty;
            // lưu checksumKey và token vào cache
            JwtLogin(user, out token);
            //return the token
            response.Token = token;
            response.Data = user;
            response.Message = "Đăng nhập thành công";

            return response;
        }

        public Response Logout()
        {
            var response = new Response();
            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var checksumKey = identity.Claims.Where(x => x.Type == ClaimTypes.Sid).FirstOrDefault();
            if (checksumKey != null && !string.IsNullOrEmpty(checksumKey.Value))
            {
                var isSuccess = DeleteJWTTokenOnCache(checksumKey.Value);
                if (!isSuccess)
                {
                    response.Code = SystemCode.Error;
                    return response;
                }
                response.Code = SystemCode.Success;
            }
            return response;
        }

        public bool IsLogin()
        {
            try
            {
                var isLogin = _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                // && !string.IsNullOrEmpty (HttpContext.Current.User.Identity.Name);
                return isLogin;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region JWT
        public bool SaveJWTTokenOnCache(string checksumKey, string token)
        {
            string keyCached = FormatKey(CacheConstants.JWTToken, checksumKey);
            return _cacheService.Set(keyCached, token, CacheConstants.ExpireLongTime);

        }
        public bool SaveObsoleteJWTTokenOnCache(string checksumKey, string expires, string token)
        {
            string keyCached = FormatKey(CacheConstants.ObsoleteJWTToken, checksumKey, expires);
            return _cacheService.Set(keyCached, token, 1);
        }
        public bool ChecksumJWTOnCache(string checksumKey)
        {
            string keyCached = FormatKey(CacheConstants.JWTToken, checksumKey);
            return _cacheService.Exists(keyCached);
        }

        public bool CheckExitstJWTTokenOnCache(string checksumKey, string token)
        {
            string keyCached = FormatKey(CacheConstants.JWTToken, checksumKey);
            var val = _cacheService.Get(keyCached);
            if (!string.IsNullOrEmpty(val))
            {
                return token == val;
            }
            return false;
        }

        public bool CheckExitstObsoleteJWTTokenOnCache(string checksumKey, string expires, string token)
        {
            string keyCached = FormatKey(CacheConstants.ObsoleteJWTToken, checksumKey, expires);
            var val = _cacheService.Get(keyCached);
            if (!string.IsNullOrEmpty(val))
            {
                return token == val;
            }
            return false;
        }

        public bool DeleteJWTTokenOnCache(string checksumKey)
        {
            string keyCached = FormatKey(CacheConstants.JWTToken, checksumKey);
            return _cacheService.Delete(keyCached);
        }

        public bool SetLockRefreshTokenOnCache(string checksumKey)
        {
            lock (_objLocked)
            {
                string keyCached = FormatKey(CacheConstants.JWTLockRefreshToken, checksumKey);
                return _cacheService.Set(keyCached, true, 5);
            }
        }

        public bool CheckLockRefreshTokenOnCache(string checksumKey)
        {
            string keyCached = FormatKey(CacheConstants.JWTLockRefreshToken, checksumKey);
            return _cacheService.Get<bool>(keyCached);
        }

        public bool DeleteLockRefreshTokenOnCache(string checksumKey)
        {
            string keyCached = FormatKey(CacheConstants.JWTLockRefreshToken, checksumKey);
            return _cacheService.Delete(keyCached);
        }

        public void DeleteAllChecksumOnCache(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                string prefix = FormatKey(CacheConstants.JWTToken, username + ":");
                _cacheService.DeletePattern(prefix);
            }
        }
        #endregion

        private bool JwtLogin(Users userInfo, out string token)
        {
            //set the time when it expires
            System.DateTime expires = System.DateTime.UtcNow.AddMinutes(AppKeys.JWTTimeout);
            // gen checksumKey
            var checksumKey = JWTHelper.Instance.GenerateChecksumKey(userInfo.UserName);
            token = JWTHelper.Instance.CreateToken(userInfo.UserId, userInfo.UserName, userInfo.GroupId.ToString(), checksumKey, expires, userInfo.Role);

            // lưu checksumKey và token vào cache
            return SaveJWTTokenOnCache(checksumKey, token);

        }


        public string GenratePassword(string originPassword)
        {
            //return CryptUtils.MD5Hash(AppKeys.PasswordSalt + originPassword);
            return CryptUtils.MD5Hash(originPassword);
        }

        public string GenrateHash(string username, string originPassword)
        {
            return CryptUtils.MD5Hash(username + AppKeys.PasswordSalt + originPassword);
        }
        public IEnumerable<Users> GetGetUserActive()
        {
            string keyCached = FormatKey(CacheConstants.LeadUserCache);
            return Execute(() => (List<Users>)_usersDAL.GetGetUserActive(), keyCached, CacheConstants.ExpireLongTime);
        }
    }
}
