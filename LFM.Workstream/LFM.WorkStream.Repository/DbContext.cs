using Microsoft.EntityFrameworkCore;

namespace LFM.WorkStream.Repository;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Core.Models.WorkStream> WorkStreams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Core.Models.WorkStream>().Property(x => x.CreatedAt).HasDefaultValue(DateTimeOffset.Now);
    }
}