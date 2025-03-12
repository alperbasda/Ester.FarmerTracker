using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmetTracker.Common;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Customers.Create;

public record CreateCustomerCommand(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address) : IServiceRequest<CreateCustomerResponse>;

public record CreateCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address);

public class CreateCustomerCommandHandler(CustomerBusinessRules customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    public async Task<Response<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Customer>(request);

        customerBusinessRules.SetCustomerRepresantiveInfoIfNotAdmin(data);

        await _repository.AddAsync(data);

        return Response<CreateCustomerResponse>.Success(_mapper.Map<CreateCustomerResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateCustomerEndpointExtension
{
    public static RouteGroupBuilder CreateCustomerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateCustomerCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<CreateCustomerCommand>>();
        return group;
    }
}