using Microsoft.EntityFrameworkCore;

namespace Diving.Models;

public class DivingContext : DbContext
{
    public DivingContext(DbContextOptions<DivingContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
}