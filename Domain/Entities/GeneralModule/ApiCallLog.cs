#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.GeneralModule;

[Table("ApiCallLog")]
public class ApiCallLog
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string EndPoint { get; set; }
    [MaxLength(450)]
    public string Notes { get; set; }
    [MaxLength(1000)]
    public string RequestUrl { get; set; }
    public string Response { get; set; }
    public int ResponseStatusCode { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public bool IsSuccessfull { get; set; }
    public bool IsException { get; set; }
    public string ExceptionMessage { get; set; }
}
