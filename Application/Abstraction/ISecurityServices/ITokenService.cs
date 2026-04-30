using Application.Abstraction.ISecurityServices.Tokens;
using Domain.Entities.Auth;

namespace Application.Abstraction.ISecurityServices;

public interface ITokenService
{
    AccessToken CreateAccessToken(User user, List<OperationClaim> claims);
}