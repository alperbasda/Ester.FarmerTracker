using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Fields._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Entities;
using Ester.FarmerTracker.FieldService.Features.Fields._base.Repositories;
using Ester.FarmetTracker.Common.ApiResults;
using Ester.FarmetTracker.Common.Filters;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Fields.Create;

public record CreateFieldCommand(Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address) : IServiceRequest<CreateFieldResponse>;

public record CreateFieldResponse(Guid Id, Guid CustomerId, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);

public class CreateFieldCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<CreateFieldCommand, CreateFieldResponse>
{
    public async Task<Response<CreateFieldResponse>> Handle(CreateFieldCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<Field>(request);

        await _fieldBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data.CustomerId);
        _fieldBusinessRules.SetId(data);

        await _repository.AddAsync(data);

        return Response<CreateFieldResponse>.Success(_mapper.Map<CreateFieldResponse>(data), HttpStatusCode.OK);
    }
}

public static class CreateFieldEndpointExtension
{
    public static RouteGroupBuilder CreateFieldEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/", async ([FromBody] CreateFieldCommand command, [FromServices] IMediator mediatr) =>
        {

            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<CreateFieldCommand>>();
        return group;
    }
}