namespace Domain.Common.DataAccessHelpers
{
    public class ConnectionInfo
    {
        public string? ConnectionString { get; set; }

        public ConnectionInfo(string? connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
