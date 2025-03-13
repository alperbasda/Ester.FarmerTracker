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

namespace Ester.FarmerTracker.FieldService.Features.Fields.Update;

public record UpdateFieldCommand(Guid Id, Guid CustomerId, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address) : IServiceRequest<UpdateFieldResponse>;

public record UpdateFieldResponse(Guid Id, Guid CustomerId, string? CurrentCropName, decimal CurrentTotalFertilizerAmount, string Name, string Coordinate, decimal SquareMeter, int CityPlate, string City, string Address);

public class UpdateFieldCommandHandler(FieldBusinessRules _fieldBusinessRules, IFieldRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateFieldCommand, UpdateFieldResponse>
{

    public async Task<Response<UpdateFieldResponse>> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id);

        _fieldBusinessRules.ThrowExceptionIfDataNull(data);
        await _fieldBusinessRules.ThrowExceptionIfLoginUserNotWriteAccessToField(data!.CustomerId);
        
        if (request.SquareMeter != data.SquareMeter)
        {
            await _fieldBusinessRules.UpdateCustomerFieldsSquareMeters(data.CustomerId);
        }

        _mapper.Map(request, data);
        data.CurrentCropName = data.CurrentCropName;
        data.CurrentTotalFertilizerAmount = data.CurrentTotalFertilizerAmount;

        await _repository.UpdateAsync(data!);
        
            

        return Response<UpdateFieldResponse>.Success(_mapper.Map<UpdateFieldResponse>(data), HttpStatusCode.OK);
    }
}

public static class UpdateFieldEndpointExtension
{
    public static RouteGroupBuilder UpdateFieldEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/", async ([FromBody] UpdateFieldCommand command, [FromServices] IMediator mediatr) =>
        {
            return (await mediatr.Send(command)).ToResult();
        })
        .AddEndpointFilter(new AuthorizationFilter())
        .AddEndpointFilter<FluentValidationFilter<UpdateFieldCommand>>();
        return group;
    }
}
