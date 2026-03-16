using Application.DataAccess;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfPlayerDal : EfEntityRepositoryBase<Domain.Entities.Concrete.Player, Context>, IPlayerDal
{

}