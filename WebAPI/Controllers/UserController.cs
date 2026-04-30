using Application.IServices;
using Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
public class UserController : Controller
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
        
    }
    [Authorize(Roles = "Admin")]
    [HttpGet ("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        return Ok(_userService.GetAll());
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("UserById")]
    public IActionResult GetUserById(int userId)
    {
        var user=_userService.GetById(userId);
        return Ok(user);

    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("UserByEmail")]
    public IActionResult GetUserByEmail(string email)
    {
        var user = _userService.GetUserByEmail(email);
        return Ok(user);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("UserClaims")]
    public IActionResult GetUserClaims(int userId)
    {
        var claims = _userService.GetUserClaims(userId);
        return Ok(claims);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateUser")]
    public async Task<IActionResult>  UpdateUser([FromBody] User user)
    {
       await _userService.UpdateUserAsync(user);
        return Ok("User updated successfully");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteUser")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userService.DeleteUserAsync(userId);
        return Ok("User deleted successfully");
    }
}