using Boilerplate.PublicApi.Features.Todo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Boilerplate.PublicApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Todo> Todo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>()
            .Property(e => e.Status)
            .HasConversion(new EnumToStringConverter<TodoStatusEnum>())
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}