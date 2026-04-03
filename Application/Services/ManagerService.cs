using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;

namespace Application.Services;

public class ManagerService: IManagerService
{
    private IManagerDal _managerDal;

    public ManagerService(IManagerDal managerDal)
    {
        _managerDal = managerDal;
    }
    
    public List<Manager> GetManagers()
    {
        return _managerDal.GetAll();
    }

    public Manager? GetManagerById(int managerId)
    {
        return _managerDal.Get(o=>o.ManagerId==managerId);
    }

    public async Task CreateManagerAsync(Manager manager)
    {
        _managerDal.Add(manager);
        await _managerDal.SaveAsync();
    }

    public async Task DeleteManagerAsync(int managerId)
    {
        var manager = _managerDal.Get(o => o.ManagerId == managerId);
        if (manager == null)
            throw new InvalidOperationException($"Manager {managerId} not found.");

        _managerDal.Delete(manager);
        await _managerDal.SaveAsync();
    }
}

