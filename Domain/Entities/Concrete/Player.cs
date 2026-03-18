using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Player : IEntity
{
    [Key]
    public string PlayerId { get; set; } = null!;
    public string PlayerName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public int Age { get; set; }
    public double MarketValue { get; set; }
    public int TeamId { get; set; }
    
}