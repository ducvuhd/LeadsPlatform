using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.BusinessLayer.IBusinessLayer
{
    public interface IUserService
    {
        Task<Response> Login(string username, string password, string secureCode = "");
        Response Logout();

        bool IsLogin();

        #region JWT
        bool SaveJWTTokenOnCache(string checksumKey, string token);
        bool SaveObsoleteJWTTokenOnCache(string checksumKey, string expires, string token);
        bool ChecksumJWTOnCache(string checksumKey);
        bool CheckExitstJWTTokenOnCache(string checksumKey, string token);
        bool CheckExitstObsoleteJWTTokenOnCache(string checksumKey, string expires, string token);
        bool DeleteJWTTokenOnCache(string checksumKey);
        bool SetLockRefreshTokenOnCache(string checksumKey);
        bool CheckLockRefreshTokenOnCache(string checksumKey);
        bool DeleteLockRefreshTokenOnCache(string checksumKey);
        void DeleteAllChecksumOnCache(string username);
        #endregion
        string GenratePassword(string originPassword);
        string GenrateHash(string username, string originPassword);
        //IEnumerable<Users> GetGetUserActiveAsync();
        IEnumerable<Users> GetGetUserActive();
    }
}
