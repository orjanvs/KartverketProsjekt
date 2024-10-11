using KartverketProsjekt.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace KartverketProsjekt.Data
{
    public class KartverketDbContext : DbContext
    {
        public KartverketDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<MapReportModel> MapReports { get; set; }
    }
    
    
}

