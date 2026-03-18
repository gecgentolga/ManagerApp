using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfOwnedPlayerDal : EfEntityRepositoryBase<OwnedPlayer, Context>, IOwnedPlayerDal
{
    public EfOwnedPlayerDal(Context context) : base(context)
    {
    }
}