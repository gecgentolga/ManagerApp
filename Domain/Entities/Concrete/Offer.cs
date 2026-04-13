using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Offer : IEntity
{
    [Key]
    public int OfferId { get; set; } 
    public string PlayerId { get; set; } = null!;
    
    public int ManagerId { get; set; }
    public double CommissionRate { get; set; }
    public int ContractPeriodMonths { get; set; }
    public bool OfferStatus { get; set; } // false: inactive, true: active
    
}