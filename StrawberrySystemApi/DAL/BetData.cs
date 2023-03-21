using StrawberrySystemApi.Model.Bet;
using StrawberrySystemApi.Model.Login;
using System.Data.SqlClient;
using System;
using StrawberrySystemApi.Common;
using System.Collections.Generic;

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
        public List<GetBetInfo>  GetBetRecord(SearchBetRecord searchBetRecord)
        {
            List<GetBetInfo> getinfoList = new List<GetBetInfo>();
            using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
            {
                try
                {
                    con.Open();
                    string sql = $@"select * from tblBetRecord where issettlement is null or issettlement = 0";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    System.Data.SqlClient.SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        GetBetInfo getBetInfo = new GetBetInfo();
                        getBetInfo.CreateTime = Convert.ToDateTime(rdr["CreateDate"]).ToString("yyyy/MM/dd HH:mm:ss");
                        getBetInfo.Amount = rdr["Money"].ToString();
                        getinfoList.Add(getBetInfo);
                    }
                        return getinfoList;
                }
                catch (Exception ex)
                {
                    return getinfoList;
                }
            }
        }
    }
}
