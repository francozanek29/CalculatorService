using Microsoft.EntityFrameworkCore;

namespace CalculatorService.Server.Repository.Entities
{
  public class JournalRepositoryDbContext : DbContext
  {
    public JournalRepositoryDbContext(DbContextOptions<JournalRepositoryDbContext> options) : base(options) { }

    public DbSet<OperationEntity> Journeys { get; set; }
  }
}
