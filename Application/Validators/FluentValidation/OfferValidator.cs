using Domain.Entities.Concrete;
using FluentValidation;

namespace Application.Validators.FluentValidation;

public class OfferValidator:AbstractValidator<Offer>
{
    public OfferValidator()
    {
        RuleFor(x => x.PlayerId).NotEmpty();
        RuleFor(x => x.ManagerId).NotEmpty();
    }
    
    
}