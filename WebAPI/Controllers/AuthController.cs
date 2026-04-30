using Application.Abstraction.ISecurityServices;
using Application.Abstraction.ISecurityServices.Tokens;
using Domain.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class AuthController:Controller
{
    private IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
     [HttpPost("login")]
     public IActionResult Login([FromBody] UserForLoginDto userForLoginDto)
     { 
         var user= _authService.Login(userForLoginDto);
         AccessToken accessToken= _authService.CreateAccessToken(user);
         return Ok(new
         {
             message = "Login successful",
             token = accessToken
         });
     }
     
     [HttpPost("register")]
     public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
     {
         var user = await _authService.Register(userForRegisterDto);
         if (user == null)
         {
             return BadRequest("User Already Exist.");
         }
         return user.Type?Ok("Player account created successfully"):Ok("Manager account created successfully");
     }
    
}