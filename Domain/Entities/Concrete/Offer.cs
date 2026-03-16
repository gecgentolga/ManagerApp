using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Offer : IEntity
{
    [Key]
    public string OfferId { get; set; } = null!;
    public string PlayerId { get; set; } = null!;
    public string TeamId { get; set; } = null!;
    public double CommissionRate { get; set; }
    public int ContractPeriodMonths { get; set; }
    public bool OfferStatus { get; set; } // false: rejected, true: accepted
    
}