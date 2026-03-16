using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class League : IEntity
{
    [Key]
    public string LeagueId { get; set; } = null!;
    public string LeagueName { get; set; } = null!;
}