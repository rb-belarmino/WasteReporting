using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.ViewModels;

public class CreateReportViewModel
{
    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;
}
