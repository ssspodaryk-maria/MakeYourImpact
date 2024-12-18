using System.ComponentModel.DataAnnotations;

namespace MakeYourImpact.Models.Requests;

/// <summary>
/// Represents the data required to create or update a user.
/// </summary>
public record UserRequestModel
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string MobNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Range(16, 100)]
    public int Age { get; set; }

    [MaxLength(500)]
    public string Bio { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; } = UserRole.Volunteer;
}
