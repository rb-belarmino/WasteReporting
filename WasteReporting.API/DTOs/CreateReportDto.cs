using System.ComponentModel.DataAnnotations;

namespace WasteReporting.API.DTOs;

public class CreateReportDto
{
    [Required]
    public string Location { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;
}
