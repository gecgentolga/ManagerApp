using Application.DataAccess;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfTeamDal : EfEntityRepositoryBase<Domain.Entities.Concrete.Team, Context>, ITeamDal
{
  
}