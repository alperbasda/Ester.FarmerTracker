using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Fields._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Fields.DeleteById;

public record DeleteFieldCommand(Guid Id) : IServiceRequest<DeleteFieldResponse>;

public record DeleteFieldResponse(Guid Id);

public class DeleteFieldCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<DeleteFieldCommand, DeleteFieldResponse>
{

    public async Task<Response<DeleteFieldResponse>> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
    {

        var data = await _repository.GetAsync(w => w.Id == request.Id, cancellationToken: cancellationToken);

        _fieldBusinessRules.ThrowExceptionIfDataNull(data);
        await _fieldBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.CustomerId);

        await _repository.DeleteAsync(data!);

        return Response<DeleteFieldResponse>.Success(_mapper.Map<DeleteFieldResponse>(data), HttpStatusCode.OK);
    }
}

public static class DeleteFieldEndpointExtension
{
    public static RouteGroupBuilder DeleteFieldEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id:Guid}", async ([FromRoute] Guid id, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(new DeleteFieldCommand(id))).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter());
        return group;
    }
}
