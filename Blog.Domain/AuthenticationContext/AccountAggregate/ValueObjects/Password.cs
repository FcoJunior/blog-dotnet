using System.Security.Cryptography;

namespace Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

public sealed record Password
{
    public byte[] Salt { get; init; }
    public byte[] Hash { get; init; }
    
    private Password(string value)
    {
        using var hmac = new HMACSHA512();
        Salt = hmac.Key;
        Hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
    }
    
    private Password(byte[] salt, byte[] hash)
    {
        Salt = salt;
        Hash = hash;
    }

    public static Password Create(string value) => new(value);
    
    public static Password Create(byte[] salt, byte[] hash) => new(salt, hash);

    public bool IsMatchedPassword(string value)
    {
        using var hmac = new HMACSHA512(Salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value));
        return computedHash.SequenceEqual(Hash);
    }
}