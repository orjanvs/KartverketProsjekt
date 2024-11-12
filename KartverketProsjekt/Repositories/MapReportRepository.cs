using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KartverketProsjekt.Models.ViewModels;


namespace KartverketProsjekt.Repositories
{
    public class MapReportRepository : IMapReportRepository
    { 
        private readonly KartverketDbContext _kartverketDbContext;

        public MapReportRepository(KartverketDbContext kartverketDbContext)
        {
            _kartverketDbContext = kartverketDbContext;
        }

		/// <summary>
		/// Add a new map report to the database
		/// </summary>
		/// <param name="mapReport"> The map report to be added to the database. </param>
		/// /// <returns>The <see cref="MapReportModel"/> object that was added, with updated information from the database (e.g., generated ID).</returns>
		/// <returns></returns>
		public async Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport)
        {
            await _kartverketDbContext.MapReport.AddAsync(mapReport);
            await _kartverketDbContext.SaveChangesAsync();
            return mapReport;
        }

		/// <summary>
		/// Deletes an existing map report from the database by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the map report to be deleted.</param>
		/// <returns>
		/// The deleted <see cref="MapReportModel"/> object if it was found and removed; 
		/// otherwise, <c>null</c> if no map report with the specified ID was found.
		/// </returns>
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


		/// <summary>
		/// Retrieves a list of all map reports, filtered by the user's role and ID if applicable.
		/// </summary>
		/// <param name="userId">The unique identifier of the user requesting the reports.</param>
		/// <param name="userRole">The role of the user, which determines access level to the reports (e.g., "Submitter", "CaseHandler", or "Admin").</param>
		/// <returns>A list of <see cref="MapReportModel"/> objects that the user has access to view.</returns>

		public async Task<List<MapReportModel>> GetAllMapReportsAsync(string userId, string userRole)
        {
            var query = _kartverketDbContext.MapReport
                .Include(m => m.Submitter)
                .Include(m => m.CaseHandler)
                .Include(m => m.MapLayer)
                .Include(m => m.MapReportStatus)
                .Include(m => m.Attachments)
                .AsQueryable();

            if (userRole == "Submitter")
            {
                query = query.Where(m => m.SubmitterId == userId);
            }

            return await query.ToListAsync();
        }
		//public async Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync()
		//{
		//    // Eager loading related data for display purposes
		//    return await _kartverketDbContext.MapReport
		//        .Include(m => m.Submitter)
		//        .Include(m => m.CaseHandler)
		//        .Include(m => m.MapLayer)
		//        .Include(m => m.MapReportStatus)
		//        .ToListAsync();
		//}



		/// <summary>
		/// Retrieves a specific map report by its unique identifier, including all related data for complete viewing.
		/// </summary>
		/// <param name="id">The unique identifier of the map report to retrieve.</param>
		/// <returns>
		/// The <see cref="MapReportModel"/> object with all related data eagerly loaded, if found; 
		/// otherwise, <c>null</c> if no map report with the specified ID exists.
		/// </returns>

		public async Task<MapReportModel?> GetMapReportByIdAsync(int id)
        {
            // Eager loading related data to ensure all details are available for viewing
            return await _kartverketDbContext.MapReport
                .Include(m => m.Submitter)
                .Include(m => m.CaseHandler)
                .Include(m => m.MapLayer)
                .Include(m => m.MapReportStatus)
                .Include(m => m.Attachments) // Legg til denne linjen for å inkludere vedleggene
                .FirstOrDefaultAsync(m => m.MapReportId == id);
        }

		/// <summary>
		/// Updates an existing map report in the database with new values.
		/// </summary>
		/// <param name="mapReport">The <see cref="MapReportModel"/> object containing updated data to be saved.</param>
		/// <returns>
		/// The updated <see cref="MapReportModel"/> object if the update is successful; 
		/// otherwise, <c>null</c> if no map report with the specified ID exists.
		/// </returns>

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
