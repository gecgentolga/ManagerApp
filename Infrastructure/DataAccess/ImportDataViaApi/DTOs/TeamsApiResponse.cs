namespace Infrastructure.DataAccess.ImportDataViaApi.DTOs;

public class TeamsApiResponse
{
    public List<TeamApiDto> Teams { get; set; } = new();
}