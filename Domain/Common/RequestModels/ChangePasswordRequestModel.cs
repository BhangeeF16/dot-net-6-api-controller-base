namespace Domain.Common.RequestModels;


public class ChangePasswordRequestModel
{
    public string? NewPassword { get; set; }
    public string? PasswordConfirmation { get; set; }
    public string? CurrentPassword { get; set; }
}