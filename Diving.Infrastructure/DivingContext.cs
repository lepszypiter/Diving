using Diving.Models;
using Microsoft.EntityFrameworkCore;

namespace Diving.Infrastructure;

public class DivingContext : DbContext
{
    public DivingContext(DbContextOptions<DivingContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\DB\\diving.db");

    
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Instructor> Instructors { get; set; } = null!;
    public DbSet<ClientWithCourse> ClientWithCourses { get; set; } = null!;
}