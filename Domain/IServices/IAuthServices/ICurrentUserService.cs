namespace Domain.IServices.IAuthServices
{
    public interface ICurrentUserService
    {
        int UserID { get; }
        string UserName { get; }
        string FirstName { get; }
        string RoleId { get; }
        bool IsUserCandidate { get; }
    }
}
