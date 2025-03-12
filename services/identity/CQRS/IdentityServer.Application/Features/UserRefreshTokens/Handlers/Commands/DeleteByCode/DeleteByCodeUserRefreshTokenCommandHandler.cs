using AutoMapper;
using IdentityServer.Application.Features.UserRefreshTokens.Commands.DeleteByCode;
using IdentityServer.Application.Features.UserRefreshTokens.Rules;
using IdentityServer.Application.Services.Repositories;
using MediatR;

namespace IdentityServer.Application.Features.UserRefreshTokens.Handlers.Commands.DeleteByCode;

public class DeleteByCodeUserRefreshTokenCommandHandler : IRequestHandler<DeleteByCodeUserRefreshTokenCommand, DeleteByCodeUserRefreshTokenResponse>
{

    private readonly IUserRefreshTokenDal _userRefreshTokenDal;
    private readonly UserRefreshTokenBusinessRules _userRefreshTokenBusinessRules;
    private readonly IMapper _mapper;

    public DeleteByCodeUserRefreshTokenCommandHandler(IMapper mapper, IUserRefreshTokenDal userRefreshTokenDal, UserRefreshTokenBusinessRules userRefreshTokenBusinessRules)
    {
        _mapper = mapper;
        _userRefreshTokenDal = userRefreshTokenDal;
        _userRefreshTokenBusinessRules = userRefreshTokenBusinessRules;
    }

    public async Task<DeleteByCodeUserRefreshTokenResponse> Handle(DeleteByCodeUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var data = await _userRefreshTokenDal.GetAsync(w => w.Code == request.Code);

        _userRefreshTokenBusinessRules.ThrowExceptionIfDataNull(data);

        //İş Kurallarınızı Burada Çağırabilirsiniz.

        await _userRefreshTokenDal.DeleteAsync(data!);

        return _mapper.Map<DeleteByCodeUserRefreshTokenResponse>(data);
    }
}
