using Blog.Application.Interfaces.Data;

namespace Blog.Infra.Persistence.Context;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _applicationContext;

    public UnitOfWork(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public async Task Commit(CancellationToken cancellationToken)
        => await _applicationContext.SaveChangesAsync(cancellationToken);
}