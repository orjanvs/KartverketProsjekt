using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.IdeaSchemeModels;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Data
{
    public class KartverketDbContext : DbContext
    {
        public KartverketDbContext(DbContextOptions<KartverketDbContext> options) : base(options)
        {
            
        }

        public DbSet<MapReportModel> MapReports { get; set; }
        public DbSet<UsersIdeaScheme> UserTips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapReportModel>()
                .HasKey(m => m.MapReportId);
        }
    }
    
    
}

