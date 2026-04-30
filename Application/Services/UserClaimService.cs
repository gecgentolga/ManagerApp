using Application.DataAccess;
using Application.IServices;
using Domain.Entities.Auth;

namespace Application.Services;

public class UserClaimService: IUserClaimService
{
    private readonly IUserClaimDal _userClaimDal;
    private readonly IOperationClaimDal _operationClaimDal;

    public UserClaimService(IUserClaimDal userClaimDal, IOperationClaimDal operationClaimDal)
    {
        _userClaimDal= userClaimDal;
        _operationClaimDal = operationClaimDal;
        
    }
    
    public List<OperationClaim> GetClaims(int userId)
    {
        List<OperationClaim> userClaims = _userClaimDal.GetAll(o => o.UserId == userId)
            .Select(uc => _operationClaimDal.Get(c => c.Id == uc.OperationClaimId))
            .ToList();
        return userClaims;
        
    }

    public async Task AddClaimAsync(int userId, int operationClaimId)
    {
        UserOperationClaim userClaim = new UserOperationClaim
        {
            UserId = userId,
            OperationClaimId = operationClaimId
        };
        _userClaimDal.Add(userClaim);
        await _userClaimDal.SaveAsync();
        
    }

    public async Task RemoveClaimAsync(int userId, int operationClaimId)
    {
        UserOperationClaim userClaim =
            _userClaimDal.Get(uc => uc.UserId == userId && uc.OperationClaimId == operationClaimId);
        if (userClaim != null)
        {
            _userClaimDal.Delete(userClaim);
            await _userClaimDal.SaveAsync();
        }
    }

    public async Task RemoveAllClaimsOfUserAsync(int userId)
    {
        List<UserOperationClaim> userClaims = _userClaimDal.GetAll(o => o.UserId == userId);
        foreach (var claim in userClaims)
        {
            _userClaimDal.Delete(claim);
        }
        await _userClaimDal.SaveAsync();
        
    }

}