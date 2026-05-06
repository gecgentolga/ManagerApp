using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Auth;

namespace Application.Services;

public class UserService:IUserService
{
    private IUserDal _userDal;
    private IManagerDal _managerDal;
    private IPlayerDal _playerDal;
    
    public UserService(IUserDal userDal, IManagerDal managerDal, IPlayerDal playerDal)
    {
        _userDal = userDal;
        _managerDal = managerDal;
        _playerDal = playerDal;
    }
    public List<User> GetAll()
    {
        return _userDal.GetAll();
    }

    public User? GetById(int id)
    {
        User? user = _userDal.Get(o=>o.Id==id);
        return user;
    }

    public List<OperationClaim> GetUserClaims(int UserId)
    {
        List<OperationClaim> claims = _userDal.GetClaims(UserId);
        return claims;
    }

    public User? GetUserByEmail(string email)
    {
        return _userDal.Get(o=>o.Email==email);
    }

    public async Task AddUserAsync(User user)
    {
        _userDal.Add(user);
        await _userDal.SaveAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _userDal.Update(user);
        await _userDal.SaveAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = _userDal.Get(u => u.Id == userId);

        if (user == null)
            throw new Exception("User not found");

        if (user.Manager_Id != null)
        {
            var manager = _managerDal.Get(m => m.ManagerId == user.Manager_Id);

            if (manager != null)
                _managerDal.Delete(manager);
        }

        if (user.Player_Id != null)
        {
            var player = _playerDal.Get(p => p.PlayerId == user.Player_Id);

            if (player != null)
                _playerDal.Delete(player);
        }

        _userDal.Delete(user);

        await _userDal.SaveAsync();
    }
    
}