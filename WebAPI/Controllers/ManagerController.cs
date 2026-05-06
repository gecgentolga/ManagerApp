using Application.IServices;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class ManagerController : Controller
{
 private IManagerService _managerService;
 private IPlayerService _playerService;
 private IUserClaimService _userClaimService;

 public ManagerController(IManagerService managerService, IPlayerService playerService, IUserClaimService userClaimService)
 {
     _managerService = managerService;
     _playerService = playerService;
     _userClaimService = userClaimService;
 }
 [Authorize(Roles = "Admin,Player")]
 [HttpGet("GetAllManagers")]
 public IActionResult GetAllManagers()
 {
    var managers= _managerService.GetManagers();
        return Ok(managers);
 }
 [Authorize(Roles = "Admin,Player")]
 [HttpGet("ManagerById")]
 public IActionResult GetManagerById(int managerId)
 {
     var manager = _managerService.GetManagerById(managerId);
        if (manager == null)
            return NotFound();
        return Ok(manager);
 }
 [Authorize(Roles = "Admin,Manager,Player")]
 [HttpGet("ManagerPlayersByManagerId")]
 public IActionResult GetManagerPlayersByManagerId(int managerId)
 {
     var players = _playerService.GetPlayersByManagerId(managerId);
     return Ok(players);
 }
 [Authorize(Roles = "Admin")]
 [HttpPost("CreateManager")]
 public async Task<IActionResult> CreateManager([FromBody] Manager manager)
 {
     await _managerService.CreateManagerAsync(manager);
      return Ok($"Manager {manager.ManagerName}created successfully");
 }

 [Authorize(Roles="Admin")]
 [HttpDelete("DeleteManager")]
 public async Task<IActionResult> DeleteManager(int managerId)
 {
    await _managerService.DeleteManagerAsync(managerId);
        return Ok($"Manager with id {managerId} deleted successfully");
 }
 
 
}
