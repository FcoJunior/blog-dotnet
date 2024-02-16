using Blog.Application.Interfaces.Auth;
using Blog.Domain.AuthenticationContext.AccountAggregate.Repository;
using Blog.Domain.AuthenticationContext.AccountAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace Blog.Application.Authentication.Commands.SignIn;

public sealed class SignInCommandHandler
    : IRequestHandler<SignInCommand, ErrorOr<SignInResponse>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtProvider _jwtProvider;

    public SignInCommandHandler(IAccountRepository accountRepository, IJwtProvider jwtProvider)
    {
        _accountRepository = accountRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var (email, password) = request;
        var account = await _accountRepository.GetAccountByEmail(Email.Create(email), cancellationToken);
        if (account is null) return Error.NotFound("Invalid email or password");
        if (!account.Password.IsMatchedPassword(password)) return Error.Unauthorized("Invalid email or password");
        var token = await _jwtProvider.GenerateAccessToken(account);
        return new SignInResponse(token);
    }
}