
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

using AutoMapper;
using IdentityServer.Application.Features.Clients.Commands.Update;
using IdentityServer.Application.Features.Clients.Rules;
using IdentityServer.Application.Services.Repositories;
using IdentityServer.Domain.Entities;
using MediatR;
namespace IdentityServer.Application.Features.Clients.Handlers.Commands.Update;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
{
    private readonly IClientDal _clientDal;
    private readonly ClientBusinessRules _clientBusinessRules;
    private readonly IMapper _mapper;

    public UpdateClientCommandHandler(IMapper mapper, IClientDal clientDal, ClientBusinessRules clientBusinessRules)
    {
        _mapper = mapper;
        _clientDal = clientDal;
        _clientBusinessRules = clientBusinessRules;
    }

    public async Task<UpdateClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var data = await _clientDal.GetAsync(w => w.Id == request.Id);

        _clientBusinessRules.ThrowExceptionIfDataNull(data);
        
        //İş Kurallarınızı Burada Çağırabilirsiniz.

        _mapper.Map(request, data);
        await _clientDal.UpdateAsync(data);

        return _mapper.Map<UpdateClientResponse>(data);
    }
}



