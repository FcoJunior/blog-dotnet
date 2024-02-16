using ErrorOr;
using MediatR;

namespace Blog.Application.Authentication.Commands.SignIn;

public sealed record SignInCommand(string Email, string Password) : IRequest<ErrorOr<string>>;