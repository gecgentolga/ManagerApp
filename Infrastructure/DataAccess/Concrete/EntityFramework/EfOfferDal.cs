using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfOfferDal : EfEntityRepositoryBase<Offer, Context>, IOfferDal
{
    public EfOfferDal(Context context) : base(context)
    {
    }
}