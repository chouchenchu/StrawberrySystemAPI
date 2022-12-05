using StrawberrySystemApi.Common;
using StrawberrySystemApi.Utility;
using System;
using System.Data.SqlClient;

namespace StrawberrySystemApi.DAL
{
    public class LoginData
    {
        public bool IsMember(string account,string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Entry.SystemConfig.DBPath))
                {
                    con.Open();
                    string sql = $@"select count(*) as count from MemberData where Account = {account.FormatDBString()} and Password = {password.FormatDBString()}";
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con))
                    {
                        System.Data.SqlClient.SqlDataReader rdr = null;
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            return Convert.ToInt32(rdr["count"]) > 0 ? true : false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
    }
}
