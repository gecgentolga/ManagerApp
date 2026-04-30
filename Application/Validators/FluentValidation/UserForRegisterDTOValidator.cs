using Domain.Entities.Auth;
using Domain.Entities.DTOs;
using FluentValidation;

namespace Application.Validators.FluentValidation;

public class UserForRegisterDtoValidator:AbstractValidator<UserForRegisterDto>
{
    public UserForRegisterDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Password).Length(6,100);
        RuleFor(x => x.Password).NotEmpty();
    }
    
}