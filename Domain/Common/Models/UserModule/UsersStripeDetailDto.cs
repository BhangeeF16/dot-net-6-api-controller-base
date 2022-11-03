namespace Domain.Common.Models.UserModule
{
    public class UsersStripeDetailDto
    {
        public int ID { get; set; }
        public string? CustomerId { get; set; }
        public string? ConnectedAccountId { get; set; }
        public int fk_UserID { get; set; }

    }
}
