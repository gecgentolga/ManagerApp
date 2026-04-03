using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;

namespace Application.Services;

public class TeamService:ITeamService
{
    private ITeamDal _teamDal;

    public TeamService(ITeamDal teamDal)
    {
        _teamDal=teamDal;
    }
    public List<Team> GetTeams()
    {
        return _teamDal.GetAll();
        
    }

    public Team? GetTeamById(int teamId)
    {
        return _teamDal.Get(o => o.TeamId == teamId);
    }

    public List<Team> GetTeamsByLeagueId(string leagueId)
    {
        return _teamDal.GetAll(o => o.LeagueId == leagueId);
    }
}