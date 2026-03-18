using Application.DataAccess;
using Domain.Entities.Concrete;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfContractDal : EfEntityRepositoryBase<Contract, Context>, IContractDal
{
    public EfContractDal(Context context) : base(context)
    {
    }
}