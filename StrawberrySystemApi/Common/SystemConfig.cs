namespace StrawberrySystemApi.Common
{
    public class SystemConfig
    {
        public string DBPath { get; set; } = "Data Source=192.168.17.100\\SQLEXPRESS;Initial Catalog=Tony_Test;user id=proxsa; password=aMiTu@F0de1a!;";
        public string Version { get; set; } = "V1.0.0.220912";//大版號+db版號+小版號+異動日期
    }
}
