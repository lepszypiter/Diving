using Microsoft.EntityFrameworkCore;
using Diving.Models;

namespace Diving.Models;

public class DivingContext : DbContext
{
    public DivingContext(DbContextOptions<DivingContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Instructor> Instructors { get; set; } = null!;
    public DbSet<ClientCourse> ClientCourses { get; set; } = null!;
}