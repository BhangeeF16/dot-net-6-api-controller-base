namespace Domain.Common.RequestModels.UserRequests
{
    public class RegisterRequestModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime DOB { get; set; }
        public string? PhoneNumber { get; set; }
        public int RoleID { get; set; }

    }
}
