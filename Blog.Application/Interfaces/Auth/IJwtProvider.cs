using Blog.Domain.AuthenticationContext.AccountAggregate;

namespace Blog.Application.Interfaces.Auth;

public interface IJwtProvider
{
    Task<string> GenerateAccessToken(Account account);
    Task<string> GetEmailClaim(string token);
    Task<bool> IsValidJwt(string token);
}