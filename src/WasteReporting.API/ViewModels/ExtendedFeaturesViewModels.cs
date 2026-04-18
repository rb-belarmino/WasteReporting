using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.ViewModels;

public class CreateCollectionPointViewModel
{
    [Required]
    public string Location { get; set; } = string.Empty;
    public string Responsible { get; set; } = string.Empty;
}

public class CreateRecyclerViewModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}

public class CreateFinalDestinationViewModel
{
    [Required]
    public string Description { get; set; } = string.Empty;
}

public class CreateWasteViewModel
{
    [Required]
    public string Type { get; set; } = string.Empty;
}

public class CreateCollectionViewModel
{
    public int CollectionPointId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int RecyclerId { get; set; }
    public int FinalDestinationId { get; set; }
    public string Status { get; set; } = "AGENDADA";
}

public class UpdateCollectionViewModel
{
    public int CollectionPointId { get; set; }
    public DateTime CollectionDate { get; set; }
    public int RecyclerId { get; set; }
    public int FinalDestinationId { get; set; }
    public string Status { get; set; } = "AGENDADA";
}

public class CreateCollectionWasteViewModel
{
    public int CollectionId { get; set; }
    public int WasteId { get; set; }
    public double WeightKg { get; set; }
}

public class CollectionResponseViewModel
{
    public int Id { get; set; }
    public CollectionPointViewModel? CollectionPoint { get; set; }
    public DateTime CollectionDate { get; set; }
    public RecyclerViewModel? Recycler { get; set; }
    public FinalDestinationViewModel? FinalDestination { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class CollectionPointViewModel { public int Id { get; set; } public string Location { get; set; } = string.Empty; }
public class RecyclerViewModel { public int Id { get; set; } public string Name { get; set; } = string.Empty; }
public class FinalDestinationViewModel { public int Id { get; set; } public string Description { get; set; } = string.Empty; }
public class WasteViewModel { public int Id { get; set; } public string Type { get; set; } = string.Empty; }

public class CollectionWasteResponseViewModel
{
    public int CollectionId { get; set; }
    public int WasteId { get; set; }
    public double WeightKg { get; set; }
}
