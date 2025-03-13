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

namespace Ester.FarmerTracker.FieldService.Features.Fields.UpdateFertilizerAmount;

public record UpdateFieldFertilizerAmountCommand(Guid Id, decimal Increase) : IServiceRequest<UpdateFieldFertilizerAmountResponse>;

public record UpdateFieldFertilizerAmountResponse(Guid Id, decimal Total);

public class UpdateFieldFertilizerAmountCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateFieldFertilizerAmountCommand, UpdateFieldFertilizerAmountResponse>
{

    public async Task<Response<UpdateFieldFertilizerAmountResponse>> Handle(UpdateFieldFertilizerAmountCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _fieldBusinessRules.ThrowExceptionIfDataNull(data);

        data!.CurrentTotalFertilizerAmount += request.Increase;

        await _repository.UpdateAsync(data);

        return Response<UpdateFieldFertilizerAmountResponse>.Success(new UpdateFieldFertilizerAmountResponse(data.Id, data.CurrentTotalFertilizerAmount), HttpStatusCode.OK);
    }
}

public static class UpdateFieldFertilizerAmountEndpointExtension
{
    public static RouteGroupBuilder UpdateFieldFertilizerAmountEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/increasefertilizeramount", async ([FromBody] UpdateFieldFertilizerAmountCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        //.AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<UpdateFieldFertilizerAmountCommand>>();
        return group;
    }
}
