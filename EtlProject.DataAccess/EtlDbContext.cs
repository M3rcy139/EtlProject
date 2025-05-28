using EtlProject.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EtlProject.DataAccess;

public class EtlDbContext : DbContext
{
    public EtlDbContext(DbContextOptions<EtlDbContext> options) : base(options) { }
    
    public DbSet<JsonMessage> JsonMessages { get; set; }
    public DbSet<ProcessingLog> ProcessingLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EtlDbContext).Assembly);
    }
}