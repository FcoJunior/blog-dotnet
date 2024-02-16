using ErrorOr;
using MediatR;

namespace Blog.Application.Authentication.Commands.SignUp;

public sealed record SignUpCommand(string Email, string Password, string Name, string CoverLetter, string Photo) : IRequest<ErrorOr<Created>>;