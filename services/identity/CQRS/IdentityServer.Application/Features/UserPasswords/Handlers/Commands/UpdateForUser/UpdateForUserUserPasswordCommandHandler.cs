using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Helpers.HashHelpers;
using IdentityServer.Application.Features.UserPasswords.Commands.UpdateForUser;
using IdentityServer.Application.Features.UserPasswords.Rules;
using IdentityServer.Application.Services.Repositories;
using MediatR;

namespace IdentityServer.Application.Features.UserPasswords.Handlers.Commands.UpdateForUser;

public class UpdateForUserUserPasswordCommandHandler : IRequestHandler<UpdateForUserUserPasswordCommand, UpdateForUserUserPasswordResponse>
{
    private readonly IUserPasswordDal _userPasswordDal;
    private readonly UserPasswordBusinessRules _userPasswordBusinessRules;
    private readonly IMapper _mapper;

    public UpdateForUserUserPasswordCommandHandler(IMapper mapper, IUserPasswordDal userPasswordDal, UserPasswordBusinessRules userPasswordBusinessRules)
    {
        _mapper = mapper;
        _userPasswordDal = userPasswordDal;
        _userPasswordBusinessRules = userPasswordBusinessRules;
    }

    public async Task<UpdateForUserUserPasswordResponse> Handle(UpdateForUserUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var data = await _userPasswordDal.GetAsync(w => w.UserId == request.UserId);

        _userPasswordBusinessRules.ThrowExceptionIfDataNull(data);

        if (!HashingHelper.VerifyPasswordHash(request.OldPassword, data.Password, data.PasswordSalt))
        {
            throw new BusinessException("Eski Şifrenizi Hatalı Girdiniz.");
        }

        byte[] hash, salt;
        HashingHelper.CreatePasswordHash(request.NewPassword, out hash, out salt);
        data.PasswordSalt = salt;
        data.Password = hash;

        await _userPasswordDal.UpdateAsync(data);

        return _mapper.Map<UpdateForUserUserPasswordResponse>(data);

    }
}
