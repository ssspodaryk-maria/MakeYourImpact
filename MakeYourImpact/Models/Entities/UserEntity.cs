namespace MakeYourImpact.Models.Entities;

public class UserEntity : EntityBase
{
    public UserRole Role { get; set; } = UserRole.Volunteer;
    public string Name { get; set; } = string.Empty;
    public string MobNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
