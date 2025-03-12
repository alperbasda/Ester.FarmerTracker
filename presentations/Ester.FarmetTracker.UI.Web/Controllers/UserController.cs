using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;
using Microsoft.AspNetCore.Mvc;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("user")]
public class UserController(IIdentityApiClient identityApiClient) : BaseController
{
    
}
