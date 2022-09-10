namespace Domain.Common.DataAccessHelpers
{
    public class QueriesHelper
    {
        public static string CheckAndDropStoredProcedure(string StoredProcedureName)
        {
            return $"IF EXISTS ( SELECT * FROM sysobjects WHERE  id = object_id(N'[dbo].[{StoredProcedureName}]')) BEGIN DROP PROCEDURE [dbo].[{StoredProcedureName}] END";
        }
        public static string GetStoredProcedureQuery(string StoredProcedureName)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"StoredProcedures\{StoredProcedureName}.sql");
            return File.ReadAllText(sqlFile);
        }
    }
}
