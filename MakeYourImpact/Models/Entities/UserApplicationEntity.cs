namespace MakeYourImpact.Models.Entities;

public class UserApplicationEntity : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; } = ProjectStatus.InReview;
}
