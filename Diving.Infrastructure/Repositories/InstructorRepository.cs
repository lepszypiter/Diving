using Diving.Domain.Instructor;
using Diving.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diving.Infrastructure.Repositories;

internal class InstructorRepository : IInstructorRepository
{
    private readonly DivingContext _context;
    private readonly ILogger _logger;

    public InstructorRepository(DivingContext context, ILogger<InstructorRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Instructor>> GetAllInstructors()
    {
        var result = await _context.Instructors.Take(15).ToListAsync();
        _logger.LogTrace("GetAllInstructors {Count}",  result.Count);
        return result;
    }

    public async Task<Instructor?> GetById(long id)
    {
        _logger.LogInformation("Id {}", id);// log id
        return await _context.Instructors.FindAsync(id);
    }

    public async Task Add(Instructor instructor)
    {
        await _context.Instructors.AddAsync(instructor);
    }

    public async Task Save()
    {
        var ret = await _context.SaveChangesAsync();
        _logger.LogInformation("Record changed {Count}", ret);// log changed record
    }

    public void Remove(Instructor instructor)
    {
        _context.Instructors.Remove(instructor);
    }
}
