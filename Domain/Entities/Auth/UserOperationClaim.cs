using Domain.Entities.Abstract;

namespace Domain.Entities.Auth;

public class UserOperationClaim:IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
    
}