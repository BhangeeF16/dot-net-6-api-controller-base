namespace Domain.RequestModels.UserRequests;


public class UpdatePasswordRequestModel
{
    public string? NewPassword { get; set; }
    public string? PasswordConfirmation { get; set; }
    public string? CurrentPassword { get; set; }
}