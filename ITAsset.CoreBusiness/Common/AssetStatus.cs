using System.ComponentModel.DataAnnotations;

namespace ITAsset.Domain.Common;

public enum AssetStatus
{
    [Display(Name = "آماده تحویل")]
    Available = 1,

    [Display(Name = "تحویل داده شده")]
    Assigned = 2,

    [Display(Name = "در تعمیر")]
    InRepair = 3,

    [Display(Name = "اسقاط شده")]
    Retired = 4
}