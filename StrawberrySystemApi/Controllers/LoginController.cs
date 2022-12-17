using Microsoft.AspNetCore.Mvc;
using StrawberrySystemApi.DAL;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StrawberrySystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginData _loginData;
        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return id;
        }
        // GET api/<LoginController>/5
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

        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
