namespace Common.Authorization.JWT
{
    public interface IDatetimeProvider
    {
        DateTime UtcNow { get; }
    }
    public class DatetimeProvider : IDatetimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
