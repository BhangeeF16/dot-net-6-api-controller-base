namespace Domain.Common.DTO.UsersModule
{
    public class UsersShippingAddressDto
    {
        public int ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? StreetAddress { get; set; }
        public string? TownCity { get; set; }
        public string? PostCode { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public int fk_UserID { get; set; }

    }
}
