using Microsoft.AspNetCore.Mvc;
using StrawberrySystemApi.DAL;
using StrawberrySystemApi.Model.Bet;
using StrawberrySystemApi.Model.Login;

namespace StrawberrySystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetController : ControllerBase
    {
        #region Filed
        private BetData _betData = new BetData();
        #endregion
        [HttpPost]
        public IActionResult SetBetInfo(BetInfo model)
        {
            BetInfo betInfo = new BetInfo();
            if (_betData == null)
                _betData = new BetData();
            var result = _betData.CreateBetInfo(model);
            if (result)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
