namespace MakeYourImpact.Models.Entities;

public class ReportEntity : EntityBase
{
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Results { get; set; } = string.Empty;
}
