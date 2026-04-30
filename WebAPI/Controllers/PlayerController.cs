using Application.IServices;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class PlayerController : Controller
{
    private IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    [Authorize (Roles="Admin")]
    [HttpGet("GetAllPlayers")]
    public IActionResult GetAllPlayers()
    {
        var players = _playerService.GetPlayers();
        return Ok(players);
    }

    [Authorize (Roles="Admin,Manager")]
    [HttpGet("PlayerById")]
    public IActionResult GetPlayerById(string playerId)
    {
        var player = _playerService.GetPlayerById(playerId);
        if (player == null)
            return NotFound();
        return Ok(player);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("PlayerByPosition")]
    public IActionResult GetPlayerByPosition(string position)
    {
        var players = _playerService.GetPlayersByPosition(position);
        return Ok(players);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpGet("PlayerByTeamId")]
    public IActionResult GetPlayerByTeamId(int teamId)
    {
        var players = _playerService.GetPlayersByTeamId(teamId);
        return Ok(players);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("CreatePlayer")]
    public async Task<IActionResult> CreatePlayer([FromBody] Player player)
    {
        await _playerService.CreatePlayerAsync(player);
        return CreatedAtAction(nameof(GetPlayerById), new { playerId = player.PlayerId }, player);
    }

    [Authorize(Roles = "Admin,Player")]
    [HttpPut("UpdatePlayer")]
    public async Task<IActionResult> UpdatePlayer([FromBody] Player player)
    {
        await _playerService.UpdatePlayerAsync(player);
        return Ok("Player updated successfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeletePlayer")]
    public async Task<IActionResult> DeletePlayer(string playerId)
    {
        await _playerService.DeletePlayerAsync(playerId);
        return Ok("Player deleted successfully");
    }
}