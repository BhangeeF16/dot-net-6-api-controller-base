#nullable disable

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.GeneralModule;

[Table("tbl_ApiCallLog")]
public class ApiCallLogs
{
    public int Id { get; set; }
    public string EndPoint { get; set; }
    public string Notes { get; set; }
    public string RequestUrl { get; set; }
    public string Response { get; set; }
    public int ResponseStatusCode { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool IsSuccessfull { get; set; }
    public bool IsException { get; set; }
    public string ExceptionMessage { get; set; }
}
