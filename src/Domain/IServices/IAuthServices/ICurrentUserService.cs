namespace Domain.IServices.IAuthServices
{
    public interface ICurrentUserService
    {
        int ID { get; }
        int RoleID { get; }
        string Email { get; }
        string FirstName { get; }
    }
}
