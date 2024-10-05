using LFM.WorkStream.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LFM.WorkStream.Repository;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Core.Models.WorkStream> WorkStreams { get; init; }
    public DbSet<Project> Projects { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Models.WorkStream>().Property(x => x.CreatedAt).HasDefaultValue(DateTimeOffset.Now);
        modelBuilder.Entity<Project>().Property(x => x.CreatedAt).HasDefaultValue(DateTimeOffset.Now);
    }
}