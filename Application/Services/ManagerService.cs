using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;

namespace Application.Services;

public class ManagerService: IManagerService
{
    private IManagerDal _managerDal;
    private IUserDal _userDal;
    private IUserClaimDal _userClaimDal;

    public ManagerService(IManagerDal managerDal, IUserDal userDal, IUserClaimDal userClaimDal)
    {
        _managerDal = managerDal;
        _userDal = userDal;
        _userClaimDal = userClaimDal;
    }
    
    public List<Manager> GetManagers()
    {
        return _managerDal.GetAll();
    }

    public Manager? GetManagerById(int managerId)
    {
        return _managerDal.Get(o=>o.ManagerId==managerId);
    }

    public Manager? GetManagerByName(string name)
    {
        return _managerDal.Get(o=>o.ManagerName==name);
    }

    public async Task CreateManagerAsync(Manager manager)
    {
        _managerDal.Add(manager);
        await _managerDal.SaveAsync();
    }

    public async Task DeleteManagerAsync(int managerId)
    {
        var user = _userDal.Get(u => u.Manager_Id == managerId);
        if (user != null)
        {
            var userclaim = _userClaimDal.Get(uc => uc.UserId == user.Id && uc.OperationClaimId == 2);
            if (userclaim != null)
                _userClaimDal.Delete(userclaim);
        }

        var manager = _managerDal.Get(o => o.ManagerId == managerId);
        if (manager == null)
            throw new InvalidOperationException($"Manager {managerId} not found.");

        _managerDal.Delete(manager);
        await _managerDal.SaveAsync();
    }
}

