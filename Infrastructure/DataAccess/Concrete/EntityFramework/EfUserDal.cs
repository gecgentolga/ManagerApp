using Application.DataAccess;
using Domain.Entities.Auth;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfUserDal : EfEntityRepositoryBase<User, Context>, IUserDal
{
    public EfUserDal(Context context) : base(context)
    {
    }

    public List<OperationClaim> GetClaims(int id)
    {
        using (var context = new Context())
        {
            var result = from operationClaim in context.OperationClaims
                join userOperationClaim in context.UserOperationClaims
                    on operationClaim.Id equals userOperationClaim.OperationClaimId
                where userOperationClaim.UserId == id
                select new OperationClaim
                {
                    Id = operationClaim.Id,
                    Name = operationClaim.Name
                };

            return result.ToList();

        }
    }
}
        
    
