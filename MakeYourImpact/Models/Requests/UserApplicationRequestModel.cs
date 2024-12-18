using System.ComponentModel.DataAnnotations;

namespace MakeYourImpact.Models.Requests;

/// <summary>
/// Represents the data required to create or update a user application.
/// </summary>
public class UserApplicationRequestModel
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public ProjectStatus Status { get; set; } = ProjectStatus.InReview;
}