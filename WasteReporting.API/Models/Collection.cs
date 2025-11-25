using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("Coletas")]
public class Collection
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("PontoColetaId")]
    public int CollectionPointId { get; set; }
    public CollectionPoint? CollectionPoint { get; set; }

    [Column("RecicladorId")]
    public int RecyclerId { get; set; }
    public Recycler? Recycler { get; set; }

    [Column("DestinoFinalId")]
    public int FinalDestinationId { get; set; }
    public FinalDestination? FinalDestination { get; set; }

    [Column("DataColeta")]
    public DateTime CollectionDate { get; set; }

    [Column("Status")]
    public string Status { get; set; } = "AGENDADA"; // AGENDADA, EM_ANDAMENTO, CONCLUIDA, CANCELADA

    public List<CollectionWaste> CollectionWastes { get; set; } = new();
}
