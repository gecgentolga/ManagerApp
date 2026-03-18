using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfTeamDal : EfEntityRepositoryBase<Team, Context>, ITeamDal
{
    public EfTeamDal(Context context) : base(context)
    {
    }
}