using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfManagerDal: EfEntityRepositoryBase<Manager, Context>, IManagerDal
{
    public EfManagerDal(Context context) : base(context)
    {
    }
}