using Domain.Entities.Concrete;
using FluentValidation;

namespace Application.Validators.FluentValidation;

public class ManagerValidator:AbstractValidator<Manager>
{
    public ManagerValidator()
    {
        RuleFor(x => x.ManagerName).NotEmpty();
        RuleFor(x => x.ManagerName).Length(3,50);
    }
    
}