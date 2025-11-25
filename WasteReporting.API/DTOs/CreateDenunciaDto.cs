using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.DTOs;

public class CreateDenunciaDto
{
    [Required]
    public string Localizacao { get; set; } = string.Empty;

    [Required]
    public string Descricao { get; set; } = string.Empty;
}
