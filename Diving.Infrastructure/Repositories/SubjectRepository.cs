using Diving.Domain.Course;
using Diving.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diving.Infrastructure.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly DivingContext _context;
    private readonly ILogger _logger;

    public SubjectRepository(DivingContext context, ILogger<SubjectRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Subject?>> ReadAllSubjects(CancellationToken cancellationToken)
    {
        var result = await _context.Subject.ToListAsync(cancellationToken);
        _logger.LogTrace("GetAllSubjects {Count}",  result.Count);
        return result;
    }

    public async Task<Subject?> GetById(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Id {}", id);// log id
        return await _context.Subject.FindAsync([ id ], cancellationToken);
    }

    public async Task Add(Subject subject)
    {
        await _context.Subject.AddAsync(subject);
    }

    public Task Delete(long id, CancellationToken cancellationToken)
    {
        _context.Subject.Remove(_context.Subject.Single(x => x.SubjectId == id));
        return Task.CompletedTask;
    }
}
