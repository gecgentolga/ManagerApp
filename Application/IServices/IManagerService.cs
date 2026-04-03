using Domain.Entities.Concrete;

namespace Application.IServices;

public interface IManagerService
{
    List<Manager> GetManagers();
    Manager? GetManagerById(int managerId);
    Task CreateManagerAsync(Manager manager);
    Task DeleteManagerAsync(int managerId);
    
}