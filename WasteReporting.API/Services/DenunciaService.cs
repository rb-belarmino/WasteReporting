using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Data;
using WasteReporting.API.DTOs;
using WasteReporting.API.Models;

namespace WasteReporting.API.Services;

public class DenunciaService : IDenunciaService
{
    private readonly AppDbContext _context;

    public DenunciaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DenunciaResponseDto> CriarDenunciaAsync(CreateDenunciaDto dto, int userId)
    {
        var denuncia = new Denuncia
        {
            UserId = userId,
            Localizacao = dto.Localizacao,
            Descricao = dto.Descricao,
            Status = "PENDENTE",
            DataCriacao = DateTime.UtcNow
        };

        _context.Denuncias.Add(denuncia);
        await _context.SaveChangesAsync();

        // Load user to return username
        await _context.Entry(denuncia).Reference(d => d.User).LoadAsync();

        return MapToDto(denuncia);
    }

    public async Task<IEnumerable<DenunciaResponseDto>> ListarMinhasDenunciasAsync(int userId, int page, int pageSize)
    {
        var denuncias = await _context.Denuncias
            .Include(d => d.User)
            .Where(d => d.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return denuncias.Select(MapToDto);
    }

    public async Task<IEnumerable<DenunciaResponseDto>> ListarTodasDenunciasAsync(int page, int pageSize)
    {
        var denuncias = await _context.Denuncias
            .Include(d => d.User)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return denuncias.Select(MapToDto);
    }

    public async Task<DenunciaResponseDto> AtualizarStatusAsync(int id, string novoStatus)
    {
        var denuncia = await _context.Denuncias.Include(d => d.User).FirstOrDefaultAsync(d => d.Id == id);

        if (denuncia == null)
        {
            throw new Exception("Denúncia não encontrada.");
        }

        denuncia.Status = novoStatus;
        await _context.SaveChangesAsync();

        return MapToDto(denuncia);
    }

    private static DenunciaResponseDto MapToDto(Denuncia denuncia)
    {
        return new DenunciaResponseDto
        {
            Id = denuncia.Id,
            Localizacao = denuncia.Localizacao,
            Descricao = denuncia.Descricao,
            Status = denuncia.Status,
            DataCriacao = denuncia.DataCriacao,
            UsuarioNome = denuncia.User?.Username ?? "Desconhecido"
        };
    }
}
