using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
public class TeamController : Controller
{
    private ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet("GetAllTeams")]
    public IActionResult GetAllTeams()
    {
        var teams = _teamService.GetTeams();
        return Ok(teams);
    }

    [HttpGet("TeamById")]
    public IActionResult GetTeamById(int teamId)
    {
        var team = _teamService.GetTeamById(teamId);
        if (team == null)
            return NotFound();
        return Ok(team);
    }

    [HttpGet("TeamsByLeagueId")]
    public IActionResult GetTeamsByLeagueId(string leagueId)
    {
        var teams = _teamService.GetTeamsByLeagueId(leagueId);
        return Ok(teams);
    }
}