using System.ComponentModel;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.Fertilizers.Enums;

public enum FertilizerStatus
{
    [Description("Aktif")]
    Active = 1,
    [Description("Pasif")]
    Passive = 2
}
