namespace Blog.Domain.BlogContext.WriterAggregate.Repository;

public interface IWriterRepository
{
    public Task CreateAsync(Writer writer, CancellationToken cancellationToken);
}