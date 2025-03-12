using Alp.RepositoryAbstraction.Models.Dynamic;

namespace Ester.FarmerTracker.FertilizerService.Features._base;

public class BaseDynamicRequest
{
    public PageRequest PageRequest { get; set; } = new PageRequest()
    {
        PageSize = 10,
        PageIndex = 0,
    };

    public DynamicQuery? DynamicQuery { get; set; }
}
