using APVN.LeadsPlatform.BusinessLayer.IBusiness;
using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.Entity;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using APVN.LeadsPlatform.Utility.GoogleOTP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APVN.LeadsPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        IUserBL _userBL;
        ICheckPermissionBL _checkPermissionBL;

        public UserController(IUserBL userBL, ICheckPermissionBL checkPermissionBL)
        {
            _userBL = userBL;
            _checkPermissionBL = checkPermissionBL;
        }
        [HttpPost]
        [Route("get-list")]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Admin } })]
        public async Task<JsonResult> GetList(UserIndexModel model)
        {
            var result = await _userBL.GetListAsync(model);
            return new JsonResult(result);
        }
        [Route("get-to-edit")]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Admin } })]
        public async Task<JsonResult> GetToEdit(int userId)
        {
            var res = await _userBL.GetForEdit(userId);
            return new JsonResult(res);
        }
        [Route("get-group-roles")]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Admin } })]
        public JsonResult GetLisRole()
        {
            var lstRole = Utils.GetAllEnumValueAndDescription<SystemRole>();
            return new JsonResult(new Response(SystemCode.Success, "", lstRole));
        }

        [HttpPost]
        [CustomizeAttribute]
        [Route("save")]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Admin } })]
        public JsonResult Save(UserUpdateIndexModel model)
        {
            var res = _userBL.Update(model);
            return new JsonResult(res);
        }

        [HttpPost]
        [Route("delete-permission/{id}")]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Admin } })]
        public JsonResult DeletePermission([FromRoute] int id)
        {
            _userBL.DeletePermission(id);
            return new JsonResult(new Response()
            {
                Code = SystemCode.Success,
                Message = $"Bạn xóa quyền thành công"
            });
        }

        [Route("change-password")]
        [HttpPost]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Sales } })]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassWord user)
        {
            try
            {
                var userModel = await _userBL.GetByIdAsync(CurrUser.UserId);
                //check xem mật khẩu cũ có đúng hay không?
                if (userModel.Password != CryptUtils.MD5Hash(user.CurrentPassword))
                {
                    return new JsonResult(new Response(SystemCode.NotValid, "Password cũ không đúng", null));
                }
                //chang pass
                if (!_userBL.ResetPassword(CurrUser.UserName, CryptUtils.MD5Hash(user.NewPassword)))
                {
                    return new JsonResult(new Response(SystemCode.Error, "Có lỗi xảy ra khi cập nhật password của bạn !", null));
                }
                return new JsonResult(new Response(SystemCode.Success, "Thay đổi password thành công", null));
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response(SystemCode.Error, ex.ToString(), null));
            }
        }
        [Route("geturl/{userId}")]
        [HttpPost]
        [CustomizeAttribute]
        [CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Sales } })]
        public IActionResult AllocateOTP(int userId)
        {
            try
            {
                // Cập nhật lại OTP private key cho nhân viên này
                var task = _userBL.GetByIdAsync(userId);
                Task.WaitAll(task);
                var user = task.Result;
                // Sinh mã OTPPrivateKey trong bảng user
                var randomString = Utils.RandomString(5);
                _userBL.UpdateOTPPrivateKey(userId, randomString);
                var secretKey = AppSettings.GetAppKey("OTPSecretKey") + randomString;
                var dataInfo = new
                {
                    UserName = user.UserName,
                    SecretKey = secretKey,
                    ExpiredTime = System.DateTime.Now.AddMinutes(5)
                };
                var data = HttpUtility.UrlEncode(JsonConvert.SerializeObject(dataInfo).Encrypt());
                //var link = string.Format("/User/ShowQRCode?data={0}", data);
                //var fullLink = AppSettings.Instance.GetString("ServerDomain").ToString() + link;
                return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Success, "", new { UrlEndCode = data })));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Error, ex.Message, null)));
            }
        }
        [Route("getotpprivatekey")]
        [HttpPost]
        public IActionResult ShowQRCode(UrlOtpModel urlModel)
        {
            try
            {
                //var realData = HttpUtility.UrlDecode(data);
                //var a = JsonConvert.DeserializeObject<dynamic>(data.Decrypt());
                var dataDecrypt = JsonConvert.DeserializeObject<dynamic>(urlModel.UrlOtp.Decrypt());
                var username = (string)dataDecrypt.UserName;
                var secretKey = (string)dataDecrypt.SecretKey;
                var expiredTime = (System.DateTime)dataDecrypt.ExpiredTime;

                //Sinh ra ảnh QR code
                var googleOPTAuthenticator = new GoogleTOTP();
                var qRCodeImage = googleOPTAuthenticator.GenerateImage(secretKey, "LEADSPLATFORM-" + username);

                var retVal = new OTPManagerModel();
                if (expiredTime > System.DateTime.Now)
                {
                    retVal.Image = qRCodeImage;
                    retVal.Status = 1;
                }
                else
                {
                    retVal.Image = "The QRCode is expired.Please contact IT to be supported.";
                    retVal.Status = 2;
                }
                return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Success, "", new { OTPData = retVal })));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Response(SystemCode.Error, ex.Message, null)));
            }
        }
    }
}
