using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Team : IEntity
{
    [Key]
    public string TeamId { get; set; } = null!;
    public string TeamName { get; set; } = null!;
    public string LeagueId { get; set; } = null!;
}