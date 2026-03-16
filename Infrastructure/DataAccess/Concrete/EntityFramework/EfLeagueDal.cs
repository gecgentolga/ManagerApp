using Application.DataAccess;

namespace Infrastructure.DataAccess.Concrete.EntityFramework;

public class EfLeagueDal : EfEntityRepositoryBase<Domain.Entities.Concrete.League, Context>, ILeagueDal
{

}