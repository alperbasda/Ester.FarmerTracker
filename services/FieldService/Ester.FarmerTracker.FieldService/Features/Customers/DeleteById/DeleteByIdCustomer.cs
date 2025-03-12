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

namespace Ester.FarmerTracker.FieldService.Features.Customers.DeleteById;

public record DeleteCustomerCommand(Guid Id) : IServiceRequest<DeleteCustomerResponse>;

public record DeleteCustomerResponse(Guid Id);

public class DeleteCustomerCommandHandler(CustomerBusinessRules _customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{

    public async Task<Response<DeleteCustomerResponse>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {

        var data = await _repository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

        _customerBusinessRules.ThrowExceptionIfDataNull(data);
        _customerBusinessRules.ThrowExceptionIfCustomerRepresantiveNotLoggedUser(data!);

        await _repository.DeleteAsync(data!);

        return Response<DeleteCustomerResponse>.Success(_mapper.Map<DeleteCustomerResponse>(data), HttpStatusCode.OK);
    }
}

public static class DeleteCustomerEndpointExtension
{
    public static RouteGroupBuilder DeleteCustomerEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new DeleteCustomerCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}
