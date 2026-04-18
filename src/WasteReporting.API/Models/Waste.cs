using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("Residuos")]
public class Waste
{
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("Tipo")]
    public string Type { get; set; } = string.Empty;
}
