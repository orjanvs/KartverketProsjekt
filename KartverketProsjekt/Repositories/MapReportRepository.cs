using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync()
        {
            return await _kartverketDbContext.MapReports.ToListAsync();
        }

        public async Task<MapReportModel> GetMapReportByIdAsync(int id)
        {
            throw new NotImplementedException();
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
