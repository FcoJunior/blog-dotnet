using Blog.Application.Interfaces.Data;
using Blog.Domain.BlogContext.WriterAggregate;
using Blog.Domain.BlogContext.WriterAggregate.Repository;
using Blog.Infra.Persistence.Context;

namespace Blog.Infra.Persistence.Repository;

public sealed class WriterRepository : IWriterRepository
{
    private readonly IApplicationContext _context;

    public WriterRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task CreateAsync(Writer writer, CancellationToken cancellationToken)
        => await _context.Writers.AddAsync(writer, cancellationToken);
}