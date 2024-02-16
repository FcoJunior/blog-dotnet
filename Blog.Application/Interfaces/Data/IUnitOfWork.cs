namespace Blog.Application.Interfaces.Data;

public interface IUnitOfWork
{
    public Task Commit(CancellationToken cancellationToken);
}