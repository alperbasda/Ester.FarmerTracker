using Alp.RepositoryAbstraction.Models.Dynamic;
using AutoMapper;
using Ester.FarmerTracker.FieldService.Features._base;
using Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmerTracker.FieldService.Features.Fields.ListDynamic;
using Ester.FarmetTracker.Common;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Customers.ListDynamic;

public class ListDynamicCustomerCommand : BaseDynamicRequest, IServiceRequest<ListModel<ListDynamicCustomerResponse>>
{

}

public class ListDynamicCustomerResponse
{
    public Guid Id { get; set; }
    public Guid? SalesRepresantativeUserId { get; set; }
    public string? SalesRepresantativeUserName { get; set; }
    public long IdentityNumber { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string MailAddress { get; set; }
    public int CityPlate { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public decimal FieldsSquereMeterSum { get; set; }
    public string FullName { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
}


public class ListDynamicCustomerCommandHandler(CustomerBusinessRules _customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<ListDynamicCustomerCommand, ListModel<ListDynamicCustomerResponse>>
{

    public async Task<Response<ListModel<ListDynamicCustomerResponse>>> Handle(ListDynamicCustomerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.ListDynamicAsync(request.DynamicQuery,
                                                      predicate: w => _customerBusinessRules.IsUserAdmin() ||
                                                      w.SalesRepresantativeUserId == _customerBusinessRules.GetUserId() ||
                                                      w.Id == _customerBusinessRules.GetUserId(),
                                                      size: request.PageRequest.PageSize, index: request.PageRequest.PageIndex, enableTracking: false, cancellationToken: cancellationToken);


        var returnData = _mapper.Map<ListModel<ListDynamicCustomerResponse>>(data);

        _customerBusinessRules.FillFilters(returnData, request.DynamicQuery, request.PageRequest);

        return Response<ListModel<ListDynamicCustomerResponse>>.Success(returnData, HttpStatusCode.OK);
    }

}

public static class ListDynamicCustomerEndpointExtension
{
    public static RouteGroupBuilder ListDynamicCustomerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/list", async ([FromBody] ListDynamicCustomerCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}