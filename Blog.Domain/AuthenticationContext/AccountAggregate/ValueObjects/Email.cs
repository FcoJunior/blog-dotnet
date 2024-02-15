using System.Text.RegularExpressions;
using Blog.Domain.Abstraction;

namespace Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;

public sealed record Email : ValueObject
{
    private const string Pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public string Value { get; init; }
    
    private Email(string value)
    {
        if (!Regex.IsMatch(value, Pattern)) throw new Exception("Invalid email");
        Value = value;
    }

    public static Email Create(string value) => new(value);
}