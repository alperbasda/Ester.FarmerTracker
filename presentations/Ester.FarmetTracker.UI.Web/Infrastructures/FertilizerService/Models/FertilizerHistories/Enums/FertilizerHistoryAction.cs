using System.ComponentModel;

namespace Ester.FarmetTracker.UI.Web.Infrastructures.FertilizerService.Models.FertilizerHistories.Enums;

public enum FertilizerHistoryAction
{
    [Description("Bilinmiyor")]
    Unknown,
    [Description("Başkasına Aktarım")]
    Transfer,
    [Description("Kayıp")]
    Loss,
    [Description("Tarlaya Gübre Atımı")]
    Throw
}
