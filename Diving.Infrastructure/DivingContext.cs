using Diving.Domain.Clients;
using Diving.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Diving.Infrastructure;

public class DivingContext : DbContext
{
    public DivingContext(DbContextOptions<DivingContext> options)
        : base(options)
    {
    }

    public DivingContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=C:\\DB\\diving.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Instructor> Instructors { get; set; } = null!;
    public DbSet<ClientWithCourse> ClientWithCourses { get; set; } = null!;
}
