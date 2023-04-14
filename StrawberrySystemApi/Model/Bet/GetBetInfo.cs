using System.Collections.Generic;

namespace StrawberrySystemApi.Model.Bet
{
    public class GetBetInfo
    {
        public string ProfitLossTotal { get; set; }
        public List<BetInfo> BetInfoList { get; set; }=new List<BetInfo>();
        public class BetInfo
        {
            public string CreateTime { get; set; }
            public string Amount { get; set; }
            public string ProfitLoss { get; set; }
        }
     
    }
}
