
namespace Domain.Entities.DTOs;

public class OfferDetailDto
{
    public string PlayerId { get; set; }
    public int ManagerId { get; set; }
    public double CommissionRate { get; set; }
    public int ContractPeriodMonths { get; set; }
    
}