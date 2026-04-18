using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("PontosColeta")]
public class CollectionPoint
{
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("Localizacao")]
    public string Location { get; set; } = string.Empty;

    [Column("Responsavel")]
    public string Responsible { get; set; } = string.Empty;
}
