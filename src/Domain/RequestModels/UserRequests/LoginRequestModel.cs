#nullable disable


namespace Domain.RequestModels.UserRequests
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class GoogleLoginRequestModel
    {
        public string Token { get; set; }
        public string Code { get; set; }
    }
    public class GoogleConnectionRequestModel
    {
        public string Code { get; set; }
        public bool IsCompanyAccountConnection { get; set; }
    }
}
