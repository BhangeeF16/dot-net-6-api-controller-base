using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Domain.Entities.GeneralModule;

[Table("tbl_MiddlewareLog")]
public class MiddlewareLogs
{
    public int Id { get; set; }
    public string RequestURL { get; set; }
    [MaxLength(50)]
    public string IPAddress { get; set; }
    public string RequestByURL { get; set; }
    public string RequestBody { get; set; }
    public string Response { get; set; }
    public int ResponseStatusCode { get; set; }
    public DateTime RequestAt { get; set; }
    public DateTime? ResponseAt { get; set; }
}
