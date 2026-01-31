using ITAsset.Domain.Common;
using ITAsset.Domain.Entities;

namespace ITAsset.WebApp.ViewModels;

public class PcComponentHistoryVm
{
    public string ComponentType { get; set; } = "";
    public string? ITCode { get; set; }
    public string? OldInfo { get; set; }
    public string? NewInfo { get; set; }
    public ChangeType ChangeType { get; set; }
    public string ChangeTypeTitle => ChangeType.GetDisplayName();
    public string ChangedBy { get; set; } = "";
    public DateTime ChangeDate { get; set; }
}