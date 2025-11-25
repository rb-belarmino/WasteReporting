using WasteReporting.API.DTOs;

namespace WasteReporting.API.Services;

public interface IDenunciaService
{
    Task<DenunciaResponseDto> CriarDenunciaAsync(CreateDenunciaDto dto, int userId);
    Task<IEnumerable<DenunciaResponseDto>> ListarMinhasDenunciasAsync(int userId, int page, int pageSize);
    Task<IEnumerable<DenunciaResponseDto>> ListarTodasDenunciasAsync(int page, int pageSize);
    Task<DenunciaResponseDto> AtualizarStatusAsync(int id, string novoStatus);
}
