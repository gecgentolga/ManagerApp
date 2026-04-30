using System.Security.Claims;
using Domain.Entities.Auth;

namespace Application.IServices;

public interface IUserClaimService
{
    List<OperationClaim> GetClaims(int userId); 
    Task AddClaimAsync(int userId, int operationClaimId);
    Task RemoveClaimAsync(int userId, int operationClaimId);
    Task RemoveAllClaimsOfUserAsync(int userId);

}