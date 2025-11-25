using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.Models;

public class Denuncia
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public string Localizacao { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;

    public string Status { get; set; } = "PENDENTE"; // PENDENTE, EM_ANDAMENTO, RESOLVIDO

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
