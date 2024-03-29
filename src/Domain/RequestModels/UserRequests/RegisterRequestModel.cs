﻿namespace Domain.RequestModels.UserRequests
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

        public string? Company { get; set; } = string.Empty;
        public string? HeadQuarterName { get; set; }
        public string? HeadQuarterContact { get; set; }

    }
}
