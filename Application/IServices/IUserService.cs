using Domain.Entities.Auth;

namespace Application.IServices;

public interface IUserService
{
    List<User> GetAll();
    User? GetById(int id);
    List<OperationClaim> GetUserClaims(int Id);
    User? GetUserByEmail(string email);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
}