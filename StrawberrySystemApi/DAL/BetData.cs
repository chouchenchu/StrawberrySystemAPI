using StrawberrySystemApi.Model.Bet;
using StrawberrySystemApi.Model.Login;
using System.Data.SqlClient;
using System;
using StrawberrySystemApi.Common;

namespace StrawberrySystemApi.DAL
{
    public class BetData
    {
        public bool CreateBetInfo(BetInfo betInfo)
        {
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"Declare @id int=1
        set @id = (select  case when max(1) IS NULL then 0 when MAX(id) IS not null then MAX(id) end from tblBetRecord )+1
        insert into tblBetRecord (ID,CreateDate,LastUpdateDate,money,memberid) values( @id ,{DateTime.Now.FormatDBDateTime()},{DateTime.Now.FormatDBDateTime()},{betInfo.Amount.FormatDBString()},{betInfo.MemberID.FormatDBString()})";
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
    }
}
