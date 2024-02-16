using Blog.Application.Interfaces.Data;
using Blog.Domain.AuthenticationContext.AccountAggregate;
using Blog.Domain.AuthenticationContext.AccountAggregate.Repository;
using Blog.Domain.BlogContext.WriterAggregate;
using Blog.Domain.BlogContext.WriterAggregate.Repository;
using ErrorOr;
using MediatR;

namespace Blog.Application.Authentication.Commands.SignUp;

public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, ErrorOr<Created>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IWriterRepository _writerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SignUpCommandHandler(IAccountRepository accountRepository, IWriterRepository writerRepository, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _writerRepository = writerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Created>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var account = Account.Create(request.Email, request.Password);
        var writer = Writer.Create(request.Name, request.CoverLetter, request.Photo, account.Id);

        await _accountRepository.CreateAsync(account, cancellationToken);
        await _writerRepository.CreateAsync(writer, cancellationToken);

        await _unitOfWork.Commit(cancellationToken);
        
        return Result.Created;
    }
}