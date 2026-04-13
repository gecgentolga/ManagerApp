using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Contract : IEntity
{
    [Key]
    public int ContractId { get; set; }
    public string PlayerId { get; set; } = null!;
    public int ManagerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double CommissionRate { get; set; }
}