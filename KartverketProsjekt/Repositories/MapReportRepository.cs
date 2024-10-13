using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace KartverketProsjekt.Repositories
{
    public class MapReportRepository : IMapReportRepository
    {
        private readonly KartverketDbContext _kartverketDbContext;

        public MapReportRepository(KartverketDbContext kartverketDbContext)
        {
            _kartverketDbContext = kartverketDbContext;
        }
        public async Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport)
        {
            await _kartverketDbContext.MapReports.AddAsync(mapReport);
            await _kartverketDbContext.SaveChangesAsync();
            return mapReport;
        }

        public async Task<MapReportModel?> DeleteMapReportAsync(int id)
        {
            var existingReport = await _kartverketDbContext.MapReports.FindAsync(id);
            if (existingReport != null)
            {
                _kartverketDbContext.MapReports.Remove(existingReport);
                await _kartverketDbContext.SaveChangesAsync();
                return existingReport;
            }
            return null;
        }
            

        public async Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync()
        {
            return await _kartverketDbContext.MapReports.Take(50).ToListAsync();
        }

        public async Task<MapReportModel?> GetMapReportByIdAsync(int id)
        {
            return await _kartverketDbContext.MapReports.FirstOrDefaultAsync(m => m.MapReportId == id);

        }

        public async Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport)
        {
            var existingMapReport = await _kartverketDbContext.MapReports.FindAsync(mapReport.MapReportId);

            if (existingMapReport != null) 
            {
                existingMapReport.Description = mapReport.Description;
                existingMapReport.Category = mapReport.Category;
                existingMapReport.GeoJson = mapReport.GeoJson;
                existingMapReport.Attachment = mapReport.Attachment;
                existingMapReport.CaseStatus = mapReport.CaseStatus;
                existingMapReport.SubmissionDate = mapReport.SubmissionDate;

                await _kartverketDbContext.SaveChangesAsync();
                return existingMapReport;
            }
            return null;
        }
    }
}
