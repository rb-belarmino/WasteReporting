using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("DestinosFinais")]
public class FinalDestination
{
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("Descricao")]
    public string Description { get; set; } = string.Empty;
}
