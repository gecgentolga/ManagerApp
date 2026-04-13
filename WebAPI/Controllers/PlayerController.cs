using Application.IServices;
using Domain.Entities.Concrete;
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

    [HttpGet("GetAllPlayers")]
    public IActionResult GetAllPlayers()
    {
        var players = _playerService.GetPlayers();
        return Ok(players);
    }

    [HttpGet("PlayerById")]
    public IActionResult GetPlayerById(string playerId)
    {
        var player = _playerService.GetPlayerById(playerId);
        if (player == null)
            return NotFound();
        return Ok(player);
    }

    [HttpGet("PlayerByPosition")]
    public IActionResult GetPlayerByPosition(string position)
    {
        var players = _playerService.GetPlayersByPosition(position);
        return Ok(players);
    }

    [HttpGet("PlayerByTeamId")]
    public IActionResult GetPlayerByTeamId(int teamId)
    {
        var players = _playerService.GetPlayersByTeamId(teamId);
        return Ok(players);
    }

    [HttpPost("CreatePlayer")]
    public async Task<IActionResult> CreatePlayer([FromBody] Player player)
    {
        await _playerService.CreatePlayerAsync(player);
        return CreatedAtAction(nameof(GetPlayerById), new { playerId = player.PlayerId }, player);
    }

    [HttpPut("UpdatePlayer")]
    public async Task<IActionResult> UpdatePlayer([FromBody] Player player)
    {
        await _playerService.UpdatePlayerAsync(player);
        return Ok("Player updated successfully");
    }

    [HttpDelete("DeletePlayer")]
    public async Task<IActionResult> DeletePlayer(string playerId)
    {
        await _playerService.DeletePlayerAsync(playerId);
        return Ok("Player deleted successfully");
    }
}