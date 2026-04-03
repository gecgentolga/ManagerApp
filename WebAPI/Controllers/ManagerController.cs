using Application.IServices;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class ManagerController : Controller
{
 private IManagerService _managerService;

 public ManagerController(IManagerService managerService)
 {
     _managerService = managerService;
 }

 [HttpGet("GetAllManagers")]
 public IActionResult GetAllManagers()
 {
    var managers= _managerService.GetManagers();
        return Ok(managers);
 }

 [HttpGet("ManagerById")]
 public IActionResult GetManagerById(int managerId)
 {
     var manager = _managerService.GetManagerById(managerId);
        if (manager == null)
            return NotFound();
        return Ok(manager);
 }

 [HttpPost("CreateManager")]
 public async Task<IActionResult> CreateManager([FromBody] Manager manager)
 {
     await _managerService.CreateManagerAsync(manager);
      return Ok($"Manager {manager.ManagerName}created successfully");
 }

 [HttpDelete("DeleteManager")]
 public async Task<IActionResult> DeleteManager(int managerId)
 {
    await _managerService.DeleteManagerAsync(managerId);
        return Ok($"Manager with id {managerId} deleted successfully");
 }
 
 
}