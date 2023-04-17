using StrawberrySystemApi.Model.Bet;
using StrawberrySystemApi.Model.Login;
using System.Data.SqlClient;
using System;
using StrawberrySystemApi.Common;
using System.Collections.Generic;
using static StrawberrySystemApi.Model.Bet.GetBetInfo;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace StrawberrySystemApi.DAL
{
    public class BetData
    {
        public bool CreateBetInfo(SetBetInfo betInfo)
        {
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"Declare @id int=1
        set @id = (select  case when max(1) IS NULL then 0 when MAX(id) IS not null then MAX(id) end from tblBetRecord )+1
        insert into tblBetRecord (ID,CreateDate,LastUpdateDate,money,memberid,IsSettlement) values( @id ,{DateTime.Now.FormatDBDateTime()},{DateTime.Now.FormatDBDateTime()},{betInfo.Amount.FormatDBString()},{betInfo.MemberID.FormatDBString()},{false.FormatDBBBit()})";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    cmd.ExecuteReader();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public GetBetInfo GetBetRecord(SearchBetRecord searchBetRecord)
        {
            GetBetInfo getinfo = new GetBetInfo();
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@" select * from tblSystemSetting where functionname ='BetAmount' 
select * from tblBetRecord where issettlement is null or issettlement = 0 and memberid = {searchBetRecord.MemberID.FormatDBString()}";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    System.Data.SqlClient.SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    int betAmount = 0;
                    while (rdr.Read())
                    {
                        betAmount = Convert.IsDBNull(rdr["Value"]) ? 0 : Convert.ToInt32(rdr["Value"]);
                    }
                    rdr.NextResult();
                    while (rdr.Read())
                    {
                        BetInfo betInfo = new BetInfo();
                        
                        betInfo.CreateTime = Convert.ToDateTime(rdr["CreateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        betInfo.Amount = rdr["Money"].ToString();
                        betInfo.ProfitLoss = (Convert.ToInt32(betInfo.Amount) - betAmount).ToString();
                        getinfo.BetInfoList.Add(betInfo);
                    }
                    getinfo.ProfitLossTotal = getinfo.BetInfoList.Sum(z => Convert.ToInt32(z.ProfitLoss)).ToString();
                    return getinfo;
                }
                catch (Exception ex)
                {
                    return getinfo;
                }
            }
        }
        public string SettlementAllBet(string memberid)
        {
            SearchBetRecord searchBetRecord = new SearchBetRecord();
            searchBetRecord.MemberID = memberid;
            var amount = GetBetRecord(searchBetRecord).ProfitLossTotal;
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"update tblBetRecord set IsSettlement = 1 ,SettlementDate ={DateTime.Now.FormatDBDateTime()}
where memberid={memberid.FormatDBString()} and IsSettlement=0 or IsSettlement is null";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    cmd.ExecuteReader();
                    return amount;
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
    }
}
