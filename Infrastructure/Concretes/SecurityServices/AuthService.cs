
using Application.Abstraction.ISecurityServices;
using Application.Abstraction.ISecurityServices.Tokens;
using Application.IServices;
using Application.Services;
using Domain.Entities.Auth;
using Domain.Entities.Concrete;
using Domain.Entities.DTOs;
using Infrastructure.Concretes.SecurityServices.Hashing;
using Infrastructure.Concretes.SecurityServices.JWT;

namespace Infrastructure.Concretes.SecurityServices;

public class AuthService:IAuthService
{
    private IUserService _userService;
    private IUserClaimService _userClaimService;
    private IManagerService _managerService;
    private IPlayerService _playerService;
    private ITokenService _tokenService;


    public AuthService(IUserService userService, ITokenService tokenService
        , IManagerService managerService, IPlayerService playerService, IUserClaimService userClaimService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _managerService = managerService;
        _playerService = playerService;
        _userClaimService = userClaimService;
    }

    public async Task<User> Register(UserForRegisterDto userForRegisterDto)
    {
        if (UserExists(userForRegisterDto.Email)) return null;
        string? player_Id = null;
        int? manager_Id = null;
        string fullName = userForRegisterDto.FirstName +" "+ userForRegisterDto.LastName;
        //If user is player, add player to the table and match playerid with user.
        //If player already exists, match playerid with user.
        if (userForRegisterDto.Type)//player
        {
          var player=  _playerService.GetPlayerByName(fullName);
          
          player_Id =player.PlayerId;

        }

        // If user is manager, add manager to the table and match managerid with user.
        if (!userForRegisterDto.Type)
        {
            var manager = new Manager();
            {
                manager.ManagerName= fullName;
            }
            await _managerService.CreateManagerAsync(manager);
           manager_Id = _managerService.GetManagerByName(fullName).ManagerId;

        }
        byte[] passwordHash, passwordSalt;
        HashService.CreatePasswordHash(userForRegisterDto.Password,out passwordHash,out passwordSalt);

        var user = new User();
        {
                user.Email = userForRegisterDto.Email;
                user.FirstName = userForRegisterDto.FirstName;
                user.LastName = userForRegisterDto.LastName;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Type = userForRegisterDto.Type;
                user.Player_Id = userForRegisterDto.Type ? player_Id : null;
                user.Manager_Id = userForRegisterDto.Type ? null : manager_Id;
        }
        
        await _userService.AddUserAsync(user);
        await _userClaimService.AddClaimAsync(user.Id,userForRegisterDto.Type ? 3 : 2);
        return user;
    }

    public User Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetUserByEmail(userForLoginDto.Email);
        if (userToCheck == null)
            throw new InvalidOperationException("User not found");
        if(!HashService.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                userToCheck.PasswordSalt))
        {
            throw new InvalidOperationException("Email/Password is incorrect or User not found");
        }

        return userToCheck;
    }

    public bool UserExists(string email)
    {
        return _userService.GetUserByEmail(email) != null;
    }

    public AccessToken CreateAccessToken(User user)
    {
        var claims = _userService.GetUserClaims(user.Id);
        return _tokenService.CreateAccessToken(user, claims);
    }
}