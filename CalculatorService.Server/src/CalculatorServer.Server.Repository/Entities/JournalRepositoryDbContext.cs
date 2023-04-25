using Microsoft.EntityFrameworkCore;

namespace CalculatorService.Server.Repository.Entities
{
    /// <summary>
    /// Class that would be used to handle the DBContext in the application.
    /// </summary>
    public class JournalRepositoryDbContext : DbContext
    {
        public JournalRepositoryDbContext(DbContextOptions<JournalRepositoryDbContext> options) : base(options) { }

        public DbSet<OperationEntity> Journeys { get; set; }
    }
}
