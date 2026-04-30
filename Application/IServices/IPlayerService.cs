using Domain.Entities.Concrete;

namespace Application.IServices;

public interface IPlayerService
{
    List<Player> GetPlayers();
    Player? GetPlayerById(string playerId);
    Player? GetPlayerByName(string name);
    List<Player> GetPlayersByTeamId(int teamId);
    List<Player> GetPlayersByPosition(string position);
    Task CreatePlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task DeletePlayerAsync(string playerId);
    
}