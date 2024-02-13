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
    public DbSet<Course> Instructor { get; set; } = null!;

public DbSet<Diving.Models.Instructor> Instructor_1 { get; set; } = default!;
}