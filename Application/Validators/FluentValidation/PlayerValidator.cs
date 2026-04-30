using Domain.Entities.Concrete;
using FluentValidation;

namespace Application.Validators.FluentValidation;

public class PlayerValidator:AbstractValidator<Player>
{
    public PlayerValidator()
    {
        RuleFor(x => x.PlayerName).NotEmpty();
        RuleFor(x => x.PlayerName).Length(3,50);
        RuleFor(x => x.Age).NotEmpty().GreaterThan(15);
        RuleFor(x => x.Position).NotEmpty();
        
    }
    
}