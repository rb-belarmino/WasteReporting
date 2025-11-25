using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WasteReporting.API.Models;

[Table("ColetaResiduos")]
public class CollectionWaste
{
    [Column("ColetaId")]
    public int CollectionId { get; set; }
    public Collection? Collection { get; set; }

    [Column("ResiduoId")]
    public int WasteId { get; set; }
    public Waste? Waste { get; set; }

    [Column("PesoKg")]
    public double WeightKg { get; set; }
}
