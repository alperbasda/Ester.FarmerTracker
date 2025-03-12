using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Helpers.HashHelpers;
using IdentityServer.Application.Features.Users.Commands.ValidateForgetCode;
using IdentityServer.Application.Features.Users.Rules;
using IdentityServer.Application.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Application.Features.Users.Handlers.Commands.ValidateForgetCode;

public class ValidateForgetCodeUserCommandHandler : IRequestHandler<ValidateForgetCodeUserCommand, ValidateForgetCodeUserResponse>
{
    private readonly IUserDal _userDal;
    private readonly IUserPasswordDal _userPasswordDal;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly IMapper _mapper;

    public ValidateForgetCodeUserCommandHandler(IMapper mapper, IUserDal userDal, UserBusinessRules userBusinessRules, IUserPasswordDal userPasswordDal)
    {
        _mapper = mapper;
        _userDal = userDal;
        _userBusinessRules = userBusinessRules;
        _userPasswordDal = userPasswordDal;
    }
    public async Task<ValidateForgetCodeUserResponse> Handle(ValidateForgetCodeUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password.Length < 5)
        {
            throw new BusinessException("Yeni Şifreniz En Az 5 Karakter Olmalı !");
        }
        var data = await _userDal.GetAsync(w => w.NormalizedMailAddress == request.MailAddress.ToLower(), include: w => w.Include(r => r.UserPasswords), cancellationToken: cancellationToken);

        _userBusinessRules.ThrowExceptionIfDataNull(data);

        if (data.CompanyName != request.Code)
        {
            throw new BusinessException("Doğrulama kodunu hatalı girdiniz !");
        }

        byte[] hash, salt;
        HashingHelper.CreatePasswordHash(request.Password, out hash, out salt);

        var selectedPass = data.UserPasswords.FirstOrDefault(w => w.DeletedTime == null);
        if (selectedPass == null)
        {
            throw new BusinessException("Sistemde Hata Oluştu. Lütfen Yeni Hesap Oluşturun !");
        }
        selectedPass.PasswordSalt = salt;
        selectedPass.Password = hash;
        selectedPass.UpdatedTime = DateTime.UtcNow;

        await _userPasswordDal.UpdateAsync(selectedPass);

        data.CompanyName = null;
        await _userDal.UpdateAsync(data);

        return _mapper.Map<ValidateForgetCodeUserResponse>(data);
    }
}
