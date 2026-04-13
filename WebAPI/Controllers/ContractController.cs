using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class ContractController : Controller
{
    private IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("GetAllContracts")]
    public IActionResult GetAllContracts()
    {
        var contracts = _contractService.GetContracts();
        return Ok(contracts);
    }

    [HttpGet("ContractsManagerId")]
    public IActionResult GetContractsManagerId(int managerId)
    {
        var contracts = _contractService.GetContractsByManagerId(managerId);
        return Ok(contracts);
    }
    
    [HttpGet("ContractsByPlayerId")]
    public IActionResult GetContractsPlayerId(string playerId)
    {
        var contracts = _contractService.GetContractsByPlayerId(playerId);
        return Ok(contracts);
    }

    [HttpGet("ContractById")]
    public IActionResult ContractById(int id)
    {
        var contract = _contractService.GetContractById(id);
        return Ok(contract);
    }

    [HttpPost("CreateContract")]
    public async Task<IActionResult> CreateContract([FromBody] Contract contract)
    {
       await _contractService.CreateContractAsync(contract);
        return Ok("Contract created successfully");
    }

    [HttpPut("UpdateContract")]
    public async Task<IActionResult> UpdateContract([FromBody] Contract contract)
    {
        await _contractService.UpdateContractAsync(contract);
        return Ok("Contract updated successfully");
    }

    [HttpDelete("DeleteContract")]
    public async Task<IActionResult> DeleteContract(int id)
    {
       await _contractService.DeleteContractAsync(id);
        return Ok("Contract deleted successfully");
    }
}