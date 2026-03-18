namespace Infrastructure.DataAccess.ImportDataViaApi.DTOs;

public class TeamApiDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<PlayerApiDto> Squad { get; set; } = new();
}