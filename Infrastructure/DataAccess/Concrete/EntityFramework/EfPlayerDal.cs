using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfPlayerDal : EfEntityRepositoryBase<Player, Context>, IPlayerDal
{
    public EfPlayerDal(Context context) : base(context)
    {
    }
}