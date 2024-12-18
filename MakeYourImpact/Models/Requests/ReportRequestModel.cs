using System.ComponentModel.DataAnnotations;

namespace MakeYourImpact.Models.Requests;

/// <summary>
/// Represents the data required to create or update a report.
/// </summary>
public record ReportRequestModel
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Results { get; set; } = string.Empty;
}
