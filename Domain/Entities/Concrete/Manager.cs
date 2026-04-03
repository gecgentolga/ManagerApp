using System.ComponentModel.DataAnnotations;
using Domain.Entities.Abstract;

namespace Domain.Entities.Concrete;

public class Manager: IEntity
{
    [Key]
    public int ManagerId { get; set; }
    public string ManagerName { get; set; } = null!;
    
}