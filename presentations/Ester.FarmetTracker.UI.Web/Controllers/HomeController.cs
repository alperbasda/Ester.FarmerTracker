using Ester.FarmetTracker.UI.Web.Controllers._base;
using Microsoft.AspNetCore.Mvc;

namespace Ester.FarmetTracker.UI.Web.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
