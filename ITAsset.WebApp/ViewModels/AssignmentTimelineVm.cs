namespace ITAsset.WebApp.ViewModels;

public class AssignmentTimelineVm
{
    public string Action { get; set; } = "";
    public string EmployeeName { get; set; } = "-";
    public string Condition { get; set; } = "-";
    public string ChangedBy { get; set; } = "-";
    public DateTime Date { get; set; }
    public string? Note { get; set; }
}