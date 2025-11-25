using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("Recicladores")]
public class Recycler
{
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("Nome")]
    public string Name { get; set; } = string.Empty;

    [Column("Categoria")]
    public string Category { get; set; } = string.Empty;
}
