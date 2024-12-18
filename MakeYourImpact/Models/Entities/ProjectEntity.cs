namespace MakeYourImpact.Models.Entities;

public class ProjectEntity : EntityBase
{
    public ProjectStatus Status { get; set; } = ProjectStatus.InReview;
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public int VolCount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string InitiatorFullName { get; set; } = string.Empty;
}
