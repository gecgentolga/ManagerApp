using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class OwnedPlayer : IEntity
{
    [Key]
    public string OwnedPlayerId { get; set; } = null!;
    public string PlayerId { get; set; } = null!;
    public string ContractId { get; set; } = null!;
    public bool IsActive { get; set; } // true: aktif oyuncu, false: sözleşmesi bitti
}