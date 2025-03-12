using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmetTracker.Common;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Customers.Update;

public record UpdateCustomerCommand(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address) : IServiceRequest<UpdateCustomerResponse>;

public record UpdateCustomerResponse(Guid Id, Guid? SalesRepresantativeUserId, string? SalesRepresantativeUserName, long IdentityNumber, string Name, string Surname, string PhoneNumber, string MailAddress, int CityPlate, string City, string Address);

public class UpdateCustomerCommandHandler(CustomerBusinessRules _customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{

    public async Task<Response<UpdateCustomerResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _customerBusinessRules.ThrowExceptionIfDataNull(data);
        _customerBusinessRules.ThrowExceptionIfCustomerRepresantiveNotLoggedUser(data!);

        _mapper.Map(request, data);
        await _repository.UpdateAsync(data!);

        return Response<UpdateCustomerResponse>.Success(_mapper.Map<UpdateCustomerResponse>(data), HttpStatusCode.OK);
    }
}

public static class UpdateCustomerEndpointExtension
{
    public static RouteGroupBuilder UpdateCustomerEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateCustomerCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<UpdateCustomerCommand>>();
        return group;
    }
}
