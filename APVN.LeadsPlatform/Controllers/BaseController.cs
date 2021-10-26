using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace APVN.LeadsPlatform.API.Controllers
{
    public class CustomizeAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomizeAuthorizeAttribute() : base(typeof(CustomizeAuthorizeFilter))
        {
        }
        public class CustomizeAuthorizeFilter : IAsyncActionFilter
        {
            private readonly IUserService _userService;
            public List<int> Permission { get; set; }
            public CustomizeAuthorizeFilter(IUserService userService, params int[] permissions)
            {
                Permission = new List<int>(permissions);
                _userService = userService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var token = string.Empty;
                if (!TryRetrieveToken(context.HttpContext.Request, out token))
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                    //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                    await context.HttpContext.Response.WriteAsync("Jwt token không hợp lệ!");
                    return;
                }
                try
                {
                    string sec = AppKeys.JWTSecretKey;
                    string issuer = AppKeys.JWTIssuer;
                    string audience = AppKeys.JWTAudience;
                    var now = DateTime.UtcNow;
                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                    // Đối tượng chưa check expire để lấy thông tin 
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = audience,
                        ValidIssuer = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey,
                    };
                    // lấy checksumKey
                    //IdentityModelEventSource.ShowPII = true;
                    var currentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                    if (currentPrincipal != null)
                    {
                        var checksumKey = currentPrincipal.GetClaimValue(ClaimTypes.Sid);
                        if (!string.IsNullOrEmpty(checksumKey))
                        {
                            var expiredValue = currentPrincipal.GetClaimValue(ClaimTypes.Expired);
                            if (!string.IsNullOrEmpty(expiredValue))
                            {
                                // kiểm tra trong cache có tồn tài checksum không
                                if (_userService.ChecksumJWTOnCache(checksumKey))
                                {
                                    var roles = currentPrincipal.GetClaimValue(ClaimTypes.Role);
                                    if (!CheckRoles(roles))
                                    {
                                        context.HttpContext.Response.StatusCode = HttpStatusCode.MethodNotAllowed.GetHashCode();
                                        return;
                                    }
                                    await next();
                                    return;
                                }
                            }
                        }
                    }
                    throw new SecurityTokenValidationException();
                }
                catch (SecurityTokenValidationException e)
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                }
                catch (Exception ex)
                {
                    context.HttpContext.Response.StatusCode = ex.Message.Contains("JWT") ? HttpStatusCode.Unauthorized.GetHashCode() : HttpStatusCode.InternalServerError.GetHashCode();
                }
                return;
            }

            private bool CheckRoles(string roles)
            {
                var arrRoles = roles.Split(",").ToList();
                var lstRoles = new List<int>();
                foreach (var roleItem in arrRoles)
                {
                    int.TryParse(roleItem, out var roleInInt);
                    lstRoles.Add(roleInInt);
                }
                //var lstRoles = roles.Split(",").ToList().Select(s => int.Parse(s));
                if (lstRoles.Contains(SystemRole.Admin.GetHashCode()))
                {
                    return true;
                }

                var isPermit = false;
                foreach (var permit in Permission)
                {
                    if (lstRoles.Contains(permit))
                    {
                        isPermit = true;
                        break;
                    }
                }

                return isPermit;
            }

            private bool LifetimeValidator(System.DateTime? notBefore, System.DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
            {
                if (expires != null)
                {
                    //if (System.DateTime.UtcNow < expires)
                    return true;
                }
                return false;
            }

            private bool TryRetrieveToken(HttpRequest request, out string token)
            {
                token = null;
                StringValues authzHeaders;
                if (!request.Headers.TryGetValue("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
                {
                    return false;
                }
                var bearerToken = authzHeaders.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
                return true;
            }
        }
    }
    public class CustomizeAttribute : TypeFilterAttribute
    {
        public CustomizeAttribute() : base(typeof(CustomizeAttributeFilter))
        {
        }
        public class CustomizeAttributeFilter : IAsyncActionFilter
        {
            private readonly IUserService _userService;
            public CustomizeAttributeFilter(IUserService userService)
            {
                _userService = userService;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var token = string.Empty;
                if (!TryRetrieveToken(context.HttpContext.Request, out token))
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                    //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
                    await context.HttpContext.Response.WriteAsync("Jwt token không hợp lệ!");
                    return;
                }

                try
                {
                    string sec = AppKeys.JWTSecretKey;
                    string issuer = AppKeys.JWTIssuer;
                    string audience = AppKeys.JWTAudience;
                    var now = DateTime.UtcNow;
                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                    // Đối tượng chưa check expire để lấy thông tin 
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = audience,
                        ValidIssuer = issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        LifetimeValidator = this.LifetimeValidator,
                        IssuerSigningKey = securityKey,
                    };

                    // lấy checksumKey
                    //IdentityModelEventSource.ShowPII = true;
                    var currentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
                    if (currentPrincipal != null)
                    {
                        var checksumKey = currentPrincipal.GetClaimValue(ClaimTypes.Sid);
                        if (!string.IsNullOrEmpty(checksumKey))
                        {
                            var expiredValue = currentPrincipal.GetClaimValue(ClaimTypes.Expired);
                            if (!string.IsNullOrEmpty(expiredValue))
                            {
                                // kiểm tra trong cache có tồn tài checksum không
                                if (_userService.ChecksumJWTOnCache(checksumKey))
                                {
                                    var roles = currentPrincipal.GetClaimValue(ClaimTypes.Role);
                                    var username = currentPrincipal.Identity.Name;

                                    // kiểm tra xem token có tồn tại trong cache không
                                    if (_userService.CheckExitstJWTTokenOnCache(checksumKey, token))
                                    {
                                        // Kiểm tra xem token đã bị expire chưa
                                        var expired = expiredValue.To<long>();
                                        var expiredDate = Utils.ConvertFromUnixTime(expired);

                                        // nếu token expired thì gen token mới
                                        if (DateTime.UtcNow >= expiredDate)
                                        {
                                            // tại đây phải lock refresh token đến lúc lock đc giải pháp
                                            while (_userService.CheckLockRefreshTokenOnCache(checksumKey))
                                            {
                                                Thread.Sleep(1000);
                                            }
                                            _userService.SetLockRefreshTokenOnCache(checksumKey);

                                            //set the time when it expires
                                            DateTime expires = DateTime.Now.AddMinutes(AppKeys.JWTTimeout);

                                            var userId = 0;
                                            var userIdObj = currentPrincipal.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault();
                                            if (userIdObj != null && !string.IsNullOrEmpty(userIdObj.Value)) userId = userIdObj.Value.To<int>();

                                            var groupdId = string.Empty;
                                            var departmentIdObj = currentPrincipal.Claims.Where(x => x.Type == ClaimTypes.PrimaryGroupSid).FirstOrDefault();
                                            if (departmentIdObj != null && !string.IsNullOrEmpty(departmentIdObj.Value)) groupdId = departmentIdObj.Value;

                                            var newToken = JWTHelper.Instance.CreateToken(userId, username, groupdId, checksumKey, expires, roles);
                                            _userService.SaveJWTTokenOnCache(checksumKey, newToken);

                                            // lưu thêm token cũ expire 1p. Phòng trường hợp 2 request. Request sau đã bị obsolete token
                                            _userService.SaveObsoleteJWTTokenOnCache(checksumKey, expiredValue, token);

                                            var Identity = currentPrincipal.Identity as ClaimsIdentity;
                                            Identity.AddClaim(new Claim(ClaimTypes.Authentication, newToken));
                                            currentPrincipal.AddIdentity(Identity);
                                        }

                                        context.HttpContext.User = currentPrincipal;
                                        await next();
                                        return;
                                    }
                                    // check thêm obsolete token
                                    else if (_userService.CheckExitstObsoleteJWTTokenOnCache(checksumKey, expiredValue, token))
                                    {
                                        context.HttpContext.User = currentPrincipal;
                                        await next();
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    throw new SecurityTokenValidationException();
                }
                catch (SecurityTokenValidationException e)
                {
                    context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
                }
                catch (Exception ex)
                {
                    context.HttpContext.Response.StatusCode = ex.Message.Contains("JWT") ? HttpStatusCode.Unauthorized.GetHashCode() : HttpStatusCode.InternalServerError.GetHashCode();
                }

                return;
            }
            private bool LifetimeValidator(System.DateTime? notBefore, System.DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
            {
                if (expires != null)
                {
                    //if (System.DateTime.UtcNow < expires)
                    return true;
                }
                return false;
            }
            private bool TryRetrieveToken(HttpRequest request, out string token)
            {
                token = null;
                StringValues authzHeaders;
                if (!request.Headers.TryGetValue("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
                {
                    return false;
                }
                var bearerToken = authzHeaders.ElementAt(0);
                token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
                return true;
            }
        }
    }
    //public class CustomizeAuthorizeAttribute : TypeFilterAttribute
    //{
    //    public CustomizeAuthorizeAttribute() : base(typeof(CustomizeAuthorizeFilter))
    //    {
    //    }
    //    public class CustomizeAuthorizeFilter : IAsyncActionFilter
    //    {
    //        private readonly IUserService _userService;
    //        public List<int> Permission { get; set; }
    //        public CustomizeAuthorizeFilter(IUserService userService, params int[] permissions)
    //        {
    //            Permission = new List<int>(permissions);
    //            _userService = userService;
    //        }
    //        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //        {
    //            var token = string.Empty;
    //            if (!TryRetrieveToken(context.HttpContext.Request, out token))
    //            {
    //                context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
    //                //allow requests with no token - whether a action method needs an authentication can be set with the claimsauthorization attribute
    //                await context.HttpContext.Response.WriteAsync("Jwt token không hợp lệ!");
    //                return;
    //            }

    //            try
    //            {
    //                string sec = AppKeys.JWTSecretKey;
    //                string issuer = AppKeys.JWTIssuer;
    //                string audience = AppKeys.JWTAudience;
    //                var now = DateTime.UtcNow;
    //                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));


    //                SecurityToken securityToken;
    //                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

    //                // Đối tượng chưa check expire để lấy thông tin 
    //                TokenValidationParameters validationParameters = new TokenValidationParameters()
    //                {
    //                    ValidAudience = audience,
    //                    ValidIssuer = issuer,
    //                    ValidateLifetime = true,
    //                    ValidateIssuerSigningKey = true,
    //                    LifetimeValidator = this.LifetimeValidator,
    //                    IssuerSigningKey = securityKey,
    //                };

    //                // lấy checksumKey
    //                //IdentityModelEventSource.ShowPII = true;
    //                var currentPrincipal = handler.ValidateToken(token, validationParameters, out securityToken);
    //                if (currentPrincipal != null)
    //                {
    //                    var checksumKey = currentPrincipal.GetClaimValue(ClaimTypes.Sid);
    //                    if (!string.IsNullOrEmpty(checksumKey))
    //                    {
    //                        var expiredValue = currentPrincipal.GetClaimValue(ClaimTypes.Expired);
    //                        if (!string.IsNullOrEmpty(expiredValue))
    //                        {
    //                            // kiểm tra trong cache có tồn tài checksum không
    //                            if (_userService.ChecksumJWTOnCache(checksumKey))
    //                            {
    //                                var roles = currentPrincipal.GetClaimValue(ClaimTypes.Role);
    //                                var username = currentPrincipal.Identity.Name;

    //                                // kiểm tra xem token có tồn tại trong cache không
    //                                if (_userService.CheckExitstJWTTokenOnCache(checksumKey, token))
    //                                {
    //                                    // Kiểm tra xem token đã bị expire chưa
    //                                    var expired = expiredValue.To<long>();
    //                                    var expiredDate = Utils.ConvertFromUnixTime(expired);

    //                                    // nếu token expired thì gen token mới
    //                                    if (DateTime.UtcNow >= expiredDate)
    //                                    {
    //                                        // tại đây phải lock refresh token đến lúc lock đc giải pháp
    //                                        while (_userService.CheckLockRefreshTokenOnCache(checksumKey))
    //                                        {
    //                                            Thread.Sleep(1000);
    //                                        }
    //                                        _userService.SetLockRefreshTokenOnCache(checksumKey);

    //                                        //set the time when it expires
    //                                        DateTime expires = DateTime.Now.AddMinutes(AppKeys.JWTTimeout);

    //                                        var userId = 0;
    //                                        var userIdObj = currentPrincipal.Claims.Where(x => x.Type == ClaimTypes.SerialNumber).FirstOrDefault();
    //                                        if (userIdObj != null && !string.IsNullOrEmpty(userIdObj.Value)) userId = userIdObj.Value.To<int>();

    //                                        var groupdId = string.Empty;
    //                                        var departmentIdObj = currentPrincipal.Claims.Where(x => x.Type == ClaimTypes.PrimaryGroupSid).FirstOrDefault();
    //                                        if (departmentIdObj != null && !string.IsNullOrEmpty(departmentIdObj.Value)) groupdId = departmentIdObj.Value;

    //                                        var newToken = JWTHelper.Instance.CreateToken(userId, username, groupdId, checksumKey, expires, roles);
    //                                        _userService.SaveJWTTokenOnCache(checksumKey, newToken);

    //                                        // lưu thêm token cũ expire 1p. Phòng trường hợp 2 request. Request sau đã bị obsolete token
    //                                        _userService.SaveObsoleteJWTTokenOnCache(checksumKey, expiredValue, token);

    //                                        var Identity = currentPrincipal.Identity as ClaimsIdentity;
    //                                        Identity.AddClaim(new Claim(ClaimTypes.Authentication, newToken));
    //                                        currentPrincipal.AddIdentity(Identity);
    //                                    }

    //                                    context.HttpContext.User = currentPrincipal;

    //                                    if (!CheckRoles(roles))
    //                                    {
    //                                        context.HttpContext.Response.StatusCode = HttpStatusCode.MethodNotAllowed.GetHashCode();
    //                                        return;
    //                                    }

    //                                    await next();
    //                                    return;
    //                                }
    //                                // check thêm obsolete token
    //                                else if (_userService.CheckExitstObsoleteJWTTokenOnCache(checksumKey, expiredValue, token))
    //                                {
    //                                    context.HttpContext.User = currentPrincipal;

    //                                    if (!CheckRoles(roles))
    //                                    {
    //                                        context.HttpContext.Response.StatusCode = HttpStatusCode.MethodNotAllowed.GetHashCode();
    //                                        return;
    //                                    }

    //                                    await next();
    //                                    return;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                throw new SecurityTokenValidationException();
    //            }
    //            catch (SecurityTokenValidationException e)
    //            {
    //                context.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
    //            }
    //            catch (Exception ex)
    //            {
    //                context.HttpContext.Response.StatusCode = ex.Message.Contains("JWT") ? HttpStatusCode.Unauthorized.GetHashCode() : HttpStatusCode.InternalServerError.GetHashCode();
    //            }

    //            return;
    //        }

    //        private bool CheckRoles(string roles)
    //        {
    //            var arrRoles = roles.Split(",").ToList();
    //            var lstRoles = new List<int>();
    //            foreach (var roleItem in arrRoles)
    //            {
    //                int.TryParse(roleItem, out var roleInInt);
    //                lstRoles.Add(roleInInt);
    //            }
    //            //var lstRoles = roles.Split(",").ToList().Select(s => int.Parse(s));
    //            if (lstRoles.Contains(SystemRole.Admin.GetHashCode()))
    //            {
    //                return true;
    //            }

    //            var isPermit = false;
    //            foreach (var permit in Permission)
    //            {
    //                if (lstRoles.Contains(permit))
    //                {
    //                    isPermit = true;
    //                    break;
    //                }
    //            }

    //            return isPermit;
    //        }

    //        private bool LifetimeValidator(System.DateTime? notBefore, System.DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
    //        {
    //            if (expires != null)
    //            {
    //                //if (System.DateTime.UtcNow < expires)
    //                return true;
    //            }
    //            return false;
    //        }

    //        private bool TryRetrieveToken(HttpRequest request, out string token)
    //        {
    //            token = null;
    //            StringValues authzHeaders;
    //            if (!request.Headers.TryGetValue("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
    //            {
    //                return false;
    //            }
    //            var bearerToken = authzHeaders.ElementAt(0);
    //            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
    //            return true;
    //        }
    //    }
    //}
    public class BaseController : ControllerBase
    {
        public ActivatingUserModel CurrUser
        {
            get
            {
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

}
