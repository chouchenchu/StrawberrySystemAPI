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
        public IActionResult SetBetInfo(SetBetInfo model)
        {
            SetBetInfo betInfo = new SetBetInfo();
            if (_betData == null)
                _betData = new BetData();
            var result = _betData.CreateBetInfo(model);
            if (result)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("GetInfo")]
        public IActionResult GetBetInfo(SearchBetRecord model)
        {
            SetBetInfo betInfo = new SetBetInfo();
            if (_betData == null)
                _betData = new BetData();
            var result = _betData.GetBetRecord(model);
            return Ok(result);
            // return BadRequest(result);
        }
    }
}
