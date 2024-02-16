namespace Blog.Infra.Auth;

public sealed record JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; } = null!;
    public int ExpiryMinutes { get; init; }
    public int ExpiryDays { get; init; }
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
};