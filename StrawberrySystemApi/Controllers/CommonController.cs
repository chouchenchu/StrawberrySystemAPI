using Microsoft.AspNetCore.Mvc;
using StrawberrySystemApi.DAL;
using StrawberrySystemApi.Model.Bet;
using StrawberrySystemApi.Model.Common;

namespace StrawberrySystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        #region Filed
        private CommonData _commonData=new CommonData();
        #endregion

        #region Public Method  
        [HttpPost]
        public IActionResult SetBetAmount(BetAmountModel model)
        {
            SetBetInfo betInfo = new SetBetInfo();
            if (_commonData == null)
                _commonData = new CommonData();
            var result = _commonData.SetBetAmount(model.BetAmount);
            if (result)
                return Ok(result);
            return BadRequest(result);
        }
        #endregion

        #region Internal Method  

        #endregion
    }
}
