using StrawberrySystemApi.Common;
using StrawberrySystemApi.Model.Bet;
using System.Data.SqlClient;
using System;

namespace StrawberrySystemApi.DAL
{
    public class CommonData
    {
        public bool SetBetAmount(string BetAmount)
        {
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"
                    Declare @id int=1
                    set @id = (select  case when max(1) IS NULL then 0 when MAX(id) IS not null then MAX(id) end from tblSystemSetting )+1
IF(  select COUNT(*) from tblSystemSetting  where FunctionName ='BetAmount' ) = 1
begin
update tblSystemSetting set value={BetAmount.FormatDBString()} where FunctionName ='BetAmount'
end
else
begin
insert into tblSystemSetting (ID,FunctionName,Value) values (@id,'BetAmount',{BetAmount.FormatDBString()})
end";
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
        public string GetBetAmount()
        {
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"select * from tblSystemSetting where functionname ='BetAmount'";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    System.Data.SqlClient.SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        return Convert.IsDBNull(rdr["value"]) ? "0" : rdr["value"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "0";
                }
                return "0";
            }
        }
    }
}
