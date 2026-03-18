using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Team : IEntity
{
    [Key]
    public int TeamId { get; set; }
    public string TeamName { get; set; } = null!;
    public string LeagueId { get; set; } = null!;
}