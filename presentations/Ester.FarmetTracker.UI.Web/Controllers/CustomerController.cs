using Alp.RepositoryAbstraction.Models.Dynamic;
using Ester.FarmetTracker.Common.Models.Responses;
using Ester.FarmetTracker.UI.Web.Controllers._base;
using Ester.FarmetTracker.UI.Web.Extensions.RedirectExt;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService.Models.Customers;
using Ester.FarmetTracker.UI.Web.Infrastructures.FieldService;
using Microsoft.AspNetCore.Mvc;
using Ester.FarmetTracker.UI.Web.Infrastructures._base.Models;
using Ester.FarmetTracker.Common.Settings;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService;
using Ester.FarmetTracker.UI.Web.Infrastructures.IdentityService.Models;

namespace Ester.FarmetTracker.UI.Web.Controllers;

[Route("customer")]
public class CustomerController(IFieldClient fieldClient, IIdentityApiClient identityApiClient, TokenParameters tokenParameters) : BaseController
{
    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {
        var data = await fieldClient.PostAsync<BaseDynamicRequest, Response<ListModel<ListCustomerResponse>>>(GetListRequest<MockCustomer>(), endpoint: "/customers/list");
        ShowErrorIfExists(data);
        return View(data.Data);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        ViewData["TokenParameters"] = tokenParameters;
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(CreateCustomerRequest request)
    {
        var id = Guid.NewGuid();
        Random random = new Random();
        int randomNumber = random.Next(100, 1000);
        var response =await identityApiClient.CreateUser(new CreateUserRequest() { Id= id, DeviceId = "unknown", DeviceToken = "unknown", AgreeContact = true, CompanyName = "unknown", FirstName = request.Name, LastName = request.Surname, Language = "tr", MailAddress = request.MailAddress, UserName = $"{request.Name.ToLower()}{request.Surname.ToLower()}{randomNumber}", Password = request.Password, PasswordConfirm = request.Password, OffsetStr = "00:00:00", PhoneNumber = request.PhoneNumber, PolicyApprove = true, Status = 50 });
        ShowErrorIfExists(response);
        var sendData = request with { Id = response.Data!.Id };

        var res = await fieldClient.PostAsync<CreateCustomerRequest, Response<CreateCustomerResponse>>(sendData, endpoint: "/customers");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Customer").Success("Çiftçi Olusturuldu.");
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update(Guid id)
    {
        ViewData["TokenParameters"] = tokenParameters;
        var data = await fieldClient.GetAsync<Response<GetByIdCustomerResponse>>(endpoint: $"/customers/{id}");
        ShowErrorIfExists(data);
        return View(data.Data);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateCustomerRequest request)
    {
        var id = Guid.NewGuid();
        Random random = new Random();
        int randomNumber = random.Next(100, 1000);
        var response = await identityApiClient.UpdateUser(new UpdateUserRequest(request.Id, $"{request.Name.ToLower()}{request.Surname.ToLower()}{randomNumber}", "unknown", "unknown", request.Name, request.Surname, request.MailAddress, request.PhoneNumber, true));
        ShowErrorIfExists(response);

        var res = await fieldClient.PutAsync<UpdateCustomerRequest, Response<UpdateCustomerResponse>>(request, endpoint: "/customers");
        ShowErrorIfExists(res);
        return RedirectToAction("Index", "Customer").Success("Çiftçi Düzenlendi.");
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await fieldClient.DeleteAsync(endpoint: $"/customers/{id}");
        return Json("OK");
    }
}
