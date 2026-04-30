using Application.DataAccess;
using Domain.Entities.Auth;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfOperationDal : EfEntityRepositoryBase<OperationClaim, Context>, IOperationClaimDal
{
    public EfOperationDal(Context context) : base(context)
    {

    }
}