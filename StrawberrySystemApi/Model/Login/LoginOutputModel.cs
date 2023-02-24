namespace StrawberrySystemApi.Model.Login
{
    public class LoginOutputModel
    {
        public bool IsSuccess { get; set; }=false;
        public string Name { get; set; } = "";
        public PermissionsEnum Auth { get; set; }
    }
}
