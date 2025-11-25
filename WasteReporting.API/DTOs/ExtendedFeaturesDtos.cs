using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.DTOs;

public class CreateCollectionPointDto
{
    [Required]
    public string Location { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;
}

public class CreateRecyclerDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class CreateFinalDestinationDto
{
    [Required]
    public string Description { get; set; } = string.Empty;
}

public class CreateWasteDto
{
    [Required]
    public string Type { get; set; } = string.Empty;
}

public class CreateCollectionDto
{
    public int CollectionPointId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int RecyclerId { get; set; }
    public int FinalDestinationId { get; set; }
    public string Status { get; set; } = "AGENDADA";
}

public class UpdateCollectionDto
{
    public int CollectionPointId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int RecyclerId { get; set; }
    public int FinalDestinationId { get; set; }
    public string Status { get; set; } = "AGENDADA";
}

public class CreateCollectionWasteDto
{
    public int CollectionId { get; set; }
    public int WasteId { get; set; }
    public double WeightKg { get; set; }
}

public class CollectionResponseDto
{
    public int Id { get; set; }
    public CollectionPointDto? CollectionPoint { get; set; }
    public DateTime CollectionDate { get; set; }
    public RecyclerDto? Recycler { get; set; }
    public FinalDestinationDto? FinalDestination { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CollectionPointDto { public int Id { get; set; } public string Location { get; set; } = string.Empty; }
public class RecyclerDto { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
public class FinalDestinationDto { public int Id { get; set; } public string Description { get; set; } = string.Empty; }
public class WasteDto { public int Id { get; set; } public string Type { get; set; } = string.Empty; }

public class CollectionWasteResponseDto
{
    public int CollectionId { get; set; }
    public int WasteId { get; set; }
    public double WeightKg { get; set; }
}
