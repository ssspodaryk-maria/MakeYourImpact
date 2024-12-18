using System.ComponentModel.DataAnnotations;

namespace MakeYourImpact.Models.Requests;

/// <summary>
/// Represents the data required to create or update a project.
/// </summary>
public record ProjectRequestModel
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int VolCount { get; set; }

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string InitiatorFullName { get; set; } = string.Empty;
}
