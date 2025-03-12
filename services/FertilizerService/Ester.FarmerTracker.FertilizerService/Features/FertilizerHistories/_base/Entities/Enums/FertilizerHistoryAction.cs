using System.ComponentModel;

namespace Ester.FarmerTracker.FertilizerService.Features.FertilizerHistories._base.Entities.Enums;

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
