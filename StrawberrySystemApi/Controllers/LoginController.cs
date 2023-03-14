using Microsoft.AspNetCore.Mvc;
using StrawberrySystemApi.DAL;
using StrawberrySystemApi.Model.Login;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrawberrySystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginData _loginData;

        [HttpPost]
        public IActionResult LoginCheck(LoginModel model)
        {
            if (_loginData == null)
                _loginData = new LoginData();
            var result = _loginData.CheckMemberAuth(model);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("{account},{password}")]
        public string GetLogin(string account,string password)
        {
            if (_loginData == null)
                _loginData = new LoginData();
            if (_loginData.IsMember(account, password))
                return "登入成功";
            else
                return "登入失敗";

            //return _loginData.GetMemberInfo(account,password);
        }
    }
}
