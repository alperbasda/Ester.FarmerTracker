using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Customers.GetById;


public record GetByIdCustomerCommand(Guid Id) : IServiceRequest<GetByIdCustomerResponse>;

public record GetByIdCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address, DateTime CreatedTime, DateTime UpdatedTime);

public class GetByIdCustomerCommandHandler(CustomerBusinessRules _customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<GetByIdCustomerCommand, GetByIdCustomerResponse>
{

    public async Task<Response<GetByIdCustomerResponse>> Handle(GetByIdCustomerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, enableTracking: false, cancellationToken: cancellationToken);

        _customerBusinessRules.ThrowExceptionIfDataNull(data);
        _customerBusinessRules.ThrowExceptionIfCustomerRepresantiveNotLoggedUser(data!);

        return Response<GetByIdCustomerResponse>.Success(_mapper.Map<GetByIdCustomerResponse>(data), HttpStatusCode.OK);
    }
}

public static class GetByICustomerEndpointExtension
{
    public static RouteGroupBuilder GetByIdCustomerEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new GetByIdCustomerCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}

