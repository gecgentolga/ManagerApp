using Domain.Entities.Abstract;

namespace Domain.Entities.Auth;

public class OperationClaim:IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
}