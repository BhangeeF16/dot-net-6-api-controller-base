#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.GeneralModule;

[Table("tbl_AppSetting")]
public class AppSetting
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }

    //[ForeignKey("CompanyInfo")]
    //public int fk_CompanyId { get; set; }
    //public virtual CompanyEntity CompanyInfo { get; set; }
}
