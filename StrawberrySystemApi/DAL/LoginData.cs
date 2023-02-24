using StrawberrySystemApi.Common;
using StrawberrySystemApi.Model.Login;
using StrawberrySystemApi.Utility;
using System;
using System.Data.SqlClient;

namespace StrawberrySystemApi.DAL
{
    public class LoginData
    {
        public LoginOutputModel CheckMemberAuth(LoginModel loginModel)
        {
            LoginOutputModel loginOutputModel = new LoginOutputModel();
            string temp = "Data Source=192.168.17.100\\SQLEXPRESS;Initial Catalog=Tony_Test;user id=proxsa; password=aMiTu@F0de1a!;";

            using (SqlConnection con = new SqlConnection(/*Entry.SystemConfig.DBPath*/temp))
            {
                try
                {
                    con.Open();
                    string sql = $@"select Name,[Permissions],count(*) as count from MemberData where Account = {loginModel.Account.FormatDBString()} and Password = {loginModel.Password.FormatDBString()}
                    group by name,[Permissions]";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    System.Data.SqlClient.SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (Convert.ToInt32(rdr["count"]) > 0 ? true : false)
                        {
                            loginOutputModel.Name = Convert.IsDBNull(rdr["Name"]) ? "" : rdr["Name"].ToString();
                            loginOutputModel.Auth = (PermissionsEnum)Enum.Parse(typeof(PermissionsEnum), rdr["Permissions"].ToString());
                            loginOutputModel.IsSuccess = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return loginOutputModel;
                }
            }
            return loginOutputModel;
        }
        public bool IsMember(string account, string password)
        {

            //string temp = "Data Source=LAPTOP-JACK\\SQLEXPRESS;Initial Catalog=StrawberrySystem_Test;Integrated Security=True";
            //string temp = "Data Source=rd-db-server\\SQLEXPRESS;Initial Catalog=Tony_Test;user id=proxsa; password=aMiTu@F0de1a!;";
            string temp = "Data Source=192.168.17.100\\SQLEXPRESS;Initial Catalog=Tony_Test;user id=proxsa; password=aMiTu@F0de1a!;";

            using (SqlConnection con = new SqlConnection(/*Entry.SystemConfig.DBPath*/temp))
            {
                try
                {
                    con.Open();
                    string sql = $@"select count(*) as count from MemberData where Account = {account.FormatDBString()} and Password = {password.FormatDBString()}";
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, con);
                    System.Data.SqlClient.SqlDataReader rdr = null;
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        return Convert.ToInt32(rdr["count"]) > 0 ? true : false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
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
