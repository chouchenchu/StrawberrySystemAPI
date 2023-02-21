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
                //string temp = "Data Source=LAPTOP-JACK\\SQLEXPRESS;Initial Catalog=StrawberrySystem_Test;Integrated Security=True";
                string temp = "Data Source=rd-db-server\\SQLEXPRESS;Initial Catalog=Tony_Test;user id=sa; password=aMiTu@F0de1a!;";

                using (SqlConnection con = new SqlConnection(/*Entry.SystemConfig.DBPath*/temp))
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
        public string GetMemberInfo(string account, string password)
        {
            string temp = "Data Source=LAPTOP-JACK\\SQLEXPRESS;Initial Catalog=StrawberrySystem_Test;Integrated Security=True";
            try
            {
                using (SqlConnection con = new SqlConnection(/*Entry.SystemConfig.DBPath*/temp))
                {
                    con.Open();
                    string sql = $@"select * from MemberData where Account = '123' and Password = '123'";
                    using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con))
                    {
                        System.Data.SqlClient.SqlDataReader rdr = null;
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            return rdr["Name"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
    }
}
