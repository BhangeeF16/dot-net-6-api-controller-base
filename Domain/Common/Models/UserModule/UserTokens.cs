namespace Domain.Common.Models.UserModule
{
    public class UserTokens
    {
        public int? UserId
        {
            get;
            set;
        }
        public string? UserName
        {
            get;
            set;
        }
        public string? FullName
        {
            get;
            set;
        }
        public int? RoleId
        {
            get;
            set;
        }
        public string? Email
        {
            get;
            set;
        }
        public string? Token
        {
            get;
            set;
        }
        public DateTime ExpiryTime
        {
            get;
            set;
        }
    }
}
