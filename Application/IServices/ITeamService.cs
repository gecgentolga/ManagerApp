using Domain.Entities.Concrete;

namespace Application.IServices;

public interface ITeamService
{
    List<Team> GetTeams();
    Team? GetTeamById(int teamId);
    List<Team> GetTeamsByLeagueId(string leagueId);
    
    
}