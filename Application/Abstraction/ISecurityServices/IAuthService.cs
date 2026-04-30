using Application.Abstraction.ISecurityServices.Tokens;
using Domain.Entities.Auth;
using Domain.Entities.DTOs;

namespace Application.Abstraction.ISecurityServices;

public interface IAuthService
{
    Task<User> Register(UserForRegisterDto userForRegisterDto);
    User Login(UserForLoginDto userForLoginDto);
    Boolean UserExists(string email);
    AccessToken CreateAccessToken(User user);
}