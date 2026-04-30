using Domain.Entities.Auth;

namespace Application.DataAccess;

public interface IUserDal:IEntityRepository<User>
{
    List<OperationClaim> GetClaims(int id);
    
}