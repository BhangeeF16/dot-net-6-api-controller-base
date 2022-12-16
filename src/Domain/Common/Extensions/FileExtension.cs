namespace Domain.Common.Extensions
{
    public class FileExtensions
    {
        public static string GetExcelFileTemplate(string TemplateName)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Common\ExcelTemplates\{TemplateName}.xlsx");
        }
    }
}
