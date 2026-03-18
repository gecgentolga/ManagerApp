using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfLeagueDal : EfEntityRepositoryBase<League, Context>, ILeagueDal
{
    public EfLeagueDal(Context context) : base(context)
    {
    }
}