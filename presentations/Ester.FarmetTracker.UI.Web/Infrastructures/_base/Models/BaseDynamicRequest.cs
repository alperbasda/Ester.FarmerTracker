using Alp.RepositoryAbstraction.Models.Dynamic;

namespace Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;

public class BaseDynamicRequest
{
    public PageRequest PageRequest { get; set; } = new PageRequest()
    {
        PageSize = 10,
        PageIndex = 0,
    };

    public DynamicQuery? DynamicQuery { get; set; }
}

