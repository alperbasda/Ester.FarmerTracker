using AutoMapper;
using Ester.FarmerTracker.FieldService.Features.Customers._base.BusinessRules;
using Ester.FarmerTracker.FieldService.Features.Customers._base.Repositories;
using Ester.FarmetTracker.Common.MediatR;
using Ester.FarmetTracker.Common.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ester.FarmerTracker.FieldService.Features.Customers.UpdateFieldsSquereMeter;

public record UpdateFieldsSquereMeterCommand(Guid Id) : IServiceRequest<UpdateFieldsSquereMeterResponse>;

public record UpdateFieldsSquereMeterResponse(Guid Id, decimal Total);

public class UpdateFieldsSquereMeterCommandHandler(CustomerBusinessRules _customerBusinessRules, ICustomerRepository _repository, IMapper _mapper) : IServiceRequestHandler<UpdateFieldsSquereMeterCommand, UpdateFieldsSquereMeterResponse>
{

    public async Task<Response<UpdateFieldsSquereMeterResponse>> Handle(UpdateFieldsSquereMeterCommand request, CancellationToken cancellationToken)
    {
        var data = await _repository.GetAsync(w => w.Id == request.Id, include: w => w.Include(r => r.Fields));

        _customerBusinessRules.ThrowExceptionIfDataNull(data);
        data!.FieldsSquereMeterSum = data.Fields.Sum(x => x.SquareMeter);

        await _repository.UpdateAsync(data!);

        return Response<UpdateFieldsSquereMeterResponse>.Success(_mapper.Map<UpdateFieldsSquereMeterResponse>(data), HttpStatusCode.OK);
    }
}

