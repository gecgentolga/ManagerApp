using Application.DataAccess;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfOwnedPlayerDal : EfEntityRepositoryBase<Domain.Entities.Concrete.OwnedPlayer, Context>, IOwnedPlayerDal
{
   
}