
//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper tool. 
//      Runtime version : 1.0
//      Generation Time : 23.10.2023 11:28
//---------------------------------------------------------------------------------------

using FluentValidation;
namespace IdentityServer.Application.Features.UserPasswords.Commands.Update;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
         
		RuleFor(w => w.Password).NotEmpty().NotNull().WithMessage("Lütfen Password Alanını Doldurun veya Seçin.");
		RuleFor(w => w.PasswordSalt).NotEmpty().NotNull().WithMessage("Lütfen PasswordSalt Alanını Doldurun veya Seçin.");

        
    }
}





