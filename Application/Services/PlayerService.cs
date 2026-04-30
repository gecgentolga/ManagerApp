using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Concrete;

namespace Application.Services;

public class PlayerService : IPlayerService
{
    private IPlayerDal _playerDal;

    public PlayerService(IPlayerDal playerDal)
    {
        _playerDal = playerDal;
    }

    
    public List<Player> GetPlayers()
    {
        return _playerDal.GetAll();
    }

    public Player? GetPlayerById(string playerId)
    {
        return _playerDal.Get(o => o.PlayerId == playerId);
    }

    public Player? GetPlayerByName(string name)
    {
        return _playerDal.Get(x => x.PlayerName == name);
    }

    public List<Player> GetPlayersByTeamId(int teamId)
    {
        return _playerDal.GetAll(o => o.TeamId == teamId);
    }

    public List<Player> GetPlayersByPosition(string position)
    {
        return _playerDal.GetAll(o => o.Position == position);
    }

    public async Task CreatePlayerAsync(Player player)
    {
        _playerDal.Add(player);
        await _playerDal.SaveAsync();
    }

    public async Task UpdatePlayerAsync(Player player)
    {
        _playerDal.Update(player);
        await _playerDal.SaveAsync();
    }

    public async Task DeletePlayerAsync(string playerId)
    {
        var player = _playerDal.Get(o => o.PlayerId == playerId);
        if (player == null)
            throw new InvalidOperationException($"Player {playerId} not found.");

        _playerDal.Delete(player);
        await _playerDal.SaveAsync();
    }
}