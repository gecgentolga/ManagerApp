using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Auth;

namespace Application.Services;

public class UserService:IUserService
{
    private IUserDal _userDal;
    
    public UserService(IUserDal userDal)
    {
        _userDal = userDal;
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
        var user = _userDal.Get(o => o.Id == userId);
        _userDal.Delete(user);
        await _userDal.SaveAsync();
    }
    
}