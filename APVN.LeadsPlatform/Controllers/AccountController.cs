using APVN.LeadsPlatform.BusinessLayer.IBusinessLayer;
using APVN.LeadsPlatform.Models;
using APVN.LeadsPlatform.Utility.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace APVN.LeadsPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IUserService _userService;
        ILogger<AccountController> _logger;

        public AccountController(IUserService userService, ILogger<AccountController> logger) 
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        [Route("login")]
        public async Task<JsonResult> Login(UserLoginModel userLoginModel)
        {
            Utility.Response loginResult = new Utility.Response();
            try
            {
                Console.WriteLine("Welcome to login");
                _logger.LogInformation("Welcome to Account Logging");
                loginResult = await _userService.Login(userLoginModel.Username, userLoginModel.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                Console.WriteLine(ex.Message);
            }
            return new JsonResult(loginResult);
        }

        [HttpPost]
        [Route("do-logout")]
        //[CustomizeAuthorize(Arguments = new object[] { new int[] { (int)SystemRole.Sales } })]
        public ActionResult Logout()
        {
            var result = _userService.Logout();
            return new JsonResult(result);
        }
    }
}
