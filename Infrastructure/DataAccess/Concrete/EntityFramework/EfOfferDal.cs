using Application.DataAccess;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfOfferDal : EfEntityRepositoryBase<Domain.Entities.Concrete.Offer, Context>, IOfferDal
{
   
}