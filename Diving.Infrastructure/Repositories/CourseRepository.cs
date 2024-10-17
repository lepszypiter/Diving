using Diving.Domain.Course;
using Diving.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Diving.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly DivingContext _context;
    private readonly ILogger _logger;

    public CourseRepository(DivingContext context, ILogger<CourseRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Course>> ReadAllCourses(CancellationToken cancellationToken)
    {
        var result = await _context.Courses
            .Include(x => x.Subjects)
            .Take(15)
            .ToListAsync();
        _logger.LogTrace("GetAllCourses {Count}",  result.Count);
        return result;
    }

    public async Task<Course?> GetById(long id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Id {}", id);// log id
        return await _context
            .Courses
            .Include(x => x.Subjects)
            .FirstOrDefaultAsync(x => x.CourseId == id);
    }

    public async Task Add(Course course)
    {
        await _context.Courses.AddAsync(course);
    }

    public async Task Save()
    {
        var ret = await _context.SaveChangesAsync();
        _logger.LogInformation("Record changed {Count}", ret);// log changed record
    }

    public Task Delete(long id, CancellationToken cancellationToken)
    {
        _context.Courses.Remove(_context.Courses.Single(x => x.CourseId == id));
        return Task.CompletedTask;
    }

    public void Remove(Course course)
    {
        _context.Courses.Remove(course);
    }
}
