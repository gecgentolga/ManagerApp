using System.Globalization;
using System.Net.Http.Json;
using Domain.Entities.Concrete;
using Infrastructure.DataAccess.Concrete.EntityFramework;
using Infrastructure.DataAccess.ImportDataViaApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.ImportDataViaApi;

public class PlayerImportHandler : IPlayerImport
{
    private readonly HttpClient _httpClient;
    private const string API_KEY = "5413226a84f946e39491a1fdbab01695";
    private readonly Context _context;
    private readonly Random random = new Random();

    public PlayerImportHandler(Context context)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", API_KEY);
        _context = context;
    }

    public async Task ImportPlayersFromApiAsync()
    {
        List<string> leagueIds = await _context.Leagues.AsNoTracking()
            .Select(l => l.LeagueId)
            .ToListAsync();
        foreach (string leagueId in leagueIds)
        {
            var url = $"https://api.football-data.org/v4/competitions/{leagueId}/teams";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<TeamsApiResponse>();

            var teams = new List<Team>();
            foreach (var apiTeam in content.Teams)
            {
                var team = new Team
                {
                    TeamId = apiTeam.Id,
                    TeamName = apiTeam.Name,
                    LeagueId = leagueId
                };
                _context.Teams.Add(team);
                foreach (var playerApiDto in apiTeam.Squad)
                {
                    try
                    {
                        var player = new Player
                        {
                            PlayerId = playerApiDto.Id.ToString(),
                            PlayerName = playerApiDto.Name,
                            Position = playerApiDto.Position,
                            Age = CalculateAge(playerApiDto.DateOfBirth),
                            MarketValue = random.Next(50000, 250001),
                            TeamId = team.TeamId
                        };
                        _context.Players.Add(player);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                   
                    
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    private int CalculateAge(string? dateOfBirth)
    {
        // API bazen dateOfBirth alanini bos veya null dondurebiliyor.
        if (string.IsNullOrWhiteSpace(dateOfBirth))
            return 0;

        if (!DateTime.TryParse(dateOfBirth, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var birthDate))
            return 0;

        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;
        if (birthDate.Date > today.AddYears(-age))
            age--;

        return Math.Max(age, 0);
    }
}
//5413226a84f946e39491a1fdbab01695