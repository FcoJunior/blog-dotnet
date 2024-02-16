using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blog.Application.Interfaces.Auth;
using Blog.Domain.AuthenticationContext.AccountAggregate;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blog.Infra.Auth;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _securityKey;

    public JwtProvider(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret));
    }

    public Task<string> GenerateAccessToken(Account account)
    {
        var claims = new ClaimsIdentity();
        claims.AddClaim(new Claim(ClaimTypes.Email, account.Email.Value));
        claims.AddClaim(new Claim("account", account.Id.ToString()));
        
        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature),
            Subject = claims,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            IssuedAt = DateTime.UtcNow,
            TokenType = "at+jwt"
        });
        return Task.FromResult(handler.WriteToken(securityToken));
    }

    public Task<string> GetEmailClaim(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenRead = tokenHandler.ReadJwtToken(token);
        var containsKey = tokenRead.Payload.ContainsKey(JwtRegisteredClaimNames.Email);
        return containsKey ? Task.FromResult((string)tokenRead.Payload[JwtRegisteredClaimNames.Email]) : Task.FromResult(string.Empty);
    }

    public async Task<bool> IsValidJwt(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validatedToken = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _securityKey,
            ValidateIssuer = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = true
        });

        return validatedToken.IsValid;
    }
}