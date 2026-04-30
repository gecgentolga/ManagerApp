namespace Application.Abstraction.ISecurityServices.Tokens;

public class TokenOptions
{
    public string Audience { get; set; } //who is token for
    public string Issuer { get; set; } //who created the token
    public int AccessTokenExpiration { get; set; } //expiration time in minutes
    public string SecurityKey { get; set; }  //How token signature is protected?
}