namespace WasteReporting.API.DTOs;

public class DenunciaResponseDto
{
    public int Id { get; set; }
    public string Localizacao { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
}
