using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            await _kartverketDbContext.MapReport.AddAsync(mapReport);
            await _kartverketDbContext.SaveChangesAsync();
            return mapReport;
        }

        public async Task<MapReportModel?> DeleteMapReportAsync(int id)
        {
            var existingReport = await _kartverketDbContext.MapReport.FindAsync(id);
            if (existingReport != null)
            {
                _kartverketDbContext.MapReport.Remove(existingReport);
                await _kartverketDbContext.SaveChangesAsync();
                return existingReport;
            }
            return null;
        }
            

        public async Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync()
        {
            // Eager loading related data for display purposes
            return await _kartverketDbContext.MapReport
                .Include(m => m.Submitter)
                .Include(m => m.CaseHandler)
                .Include(m => m.MapLayer)
                .Include(m => m.MapReportStatus)
                .Take(50)
                .ToListAsync();
        }

        public async Task<MapReportModel?> GetMapReportByIdAsync(int id)
        {
            // Eager loading related data to ensure all details are available for viewing
            return await _kartverketDbContext.MapReport
                .Include(m => m.Submitter)
                .Include(m => m.CaseHandler)
                .Include(m => m.MapLayer)
                .Include(m => m.MapReportStatus)
                .FirstOrDefaultAsync(m => m.MapReportId == id);

        }

        public async Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport)
        {
            var existingMapReport = await _kartverketDbContext.MapReport.FindAsync(mapReport.MapReportId);

            if (existingMapReport != null) 
            {
                existingMapReport.Description = mapReport.Description;
                existingMapReport.GeoJsonString = mapReport.GeoJsonString;
                existingMapReport.SubmissionDate = mapReport.SubmissionDate;
                existingMapReport.SubmitterId = mapReport.SubmitterId;
                existingMapReport.CaseHandlerId = mapReport.CaseHandlerId;
                existingMapReport.MapLayerId = mapReport.MapLayerId;
                existingMapReport.MapReportStatusId = mapReport.MapReportStatusId;

                await _kartverketDbContext.SaveChangesAsync();
                return existingMapReport;
            }
            return null;
        }
    }
}
