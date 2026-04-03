
namespace Domain.Entities.DTOs;

public class OfferDetailDto
{
    public string PlayerId { get; set; }
    public string ManagerId { get; set; }
    public double CommissionRate { get; set; }
    public int ContractPeriodMonths { get; set; }
    
}