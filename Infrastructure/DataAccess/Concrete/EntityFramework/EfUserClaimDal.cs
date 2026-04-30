using Application.DataAccess;
using Domain.Entities.Auth;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfUserClaimDal: EfEntityRepositoryBase<UserOperationClaim, Context>, IUserClaimDal
{
    public EfUserClaimDal(Context context) : base(context)
    {
    }
}