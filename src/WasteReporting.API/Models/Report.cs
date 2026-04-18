using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("Denuncias")]
public class Report
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    [Column("Localizacao")]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Column("Descricao")]
    public string Description { get; set; } = string.Empty;

    [Column("Status")]
    public string Status { get; set; } = "PENDENTE"; // PENDENTE, EM_ANDAMENTO, RESOLVIDO

    [Column("DataCriacao")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
