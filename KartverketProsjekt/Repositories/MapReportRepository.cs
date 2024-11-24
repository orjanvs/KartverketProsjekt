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
        /// Retrieves the base query for map reports, with filtering based on user role and search query, but without pagination.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the reports.</param>
        /// <param name="userRole">The role of the user, which determines access level to the reports (e.g., "Submitter", "CaseHandler", or "Admin").</param>
        /// <param name="searchQuery">The search term to filter map reports by title, description, county, municipality, or map layer type.</param>
        /// <returns>An IQueryable representing the base query for the map reports.</returns>
        private IQueryable<MapReportModel> QueryMapReportsAsync(string userId, string userRole, string? searchQuery)
        {
            var query = _kartverketDbContext.MapReport
                .Include(m => m.Submitter)
                .Include(m => m.CaseHandler)
                .Include(m => m.MapLayer)
                .Include(m => m.MapReportStatus)
                .Include(m => m.Attachments)
                .AsQueryable();

            // Apply role-based filtering
            if (userRole == "Submitter")
            {
                query = query.Where(m => m.SubmitterId == userId);
            }

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                query = query.Where(m => m.Title.Contains(searchQuery) ||
                                         m.Description.Contains(searchQuery) ||
                                         m.County.Contains(searchQuery) ||
                                         m.Municipality.Contains(searchQuery) ||
                                         m.MapLayer.MapLayerType.Contains(searchQuery));
            }

            return query;
        }

        /// <summary>
        /// Retrieves a list of map reports, filtered by the user's role and ID, with pagination.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the reports.</param>
        /// <param name="userRole">The role of the user, which determines access level to the reports (e.g., "Submitter", "CaseHandler", or "Admin").</param>
        /// <param name="searchQuery">The search term to filter map reports by title, description, county, municipality, or map layer type.</param>
        /// <param name="pageNumber">The page number for pagination (defaults to 1).</param>
        /// <param name="pageSize">The number of results per page (defaults to 100).</param>
        /// <returns>A list of <see cref="MapReportModel"/> objects that match the filters and pagination criteria.</returns>
        public async Task<List<MapReportModel>> GetMapReportsAsync(string userId, string userRole, string? searchQuery, int pageNumber = 1, int pageSize = 100)
        {
            // Get the base query using the shared method
            var query = QueryMapReportsAsync(userId, userRole, searchQuery);

            // Apply pagination
            var skipResults = (pageNumber - 1) * pageSize;
            query = query.Skip(skipResults).Take(pageSize);

            // Execute the query and return the results
            return await query.ToListAsync();
        }

        /// <summary>
        /// Retrieves the count of map reports, filtered by the user's role and ID, without pagination.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the count.</param>
        /// <param name="userRole">The role of the user, which determines access level to the reports (e.g., "Submitter", "CaseHandler", or "Admin").</param>
        /// <param name="searchQuery">The search term to filter map reports by title, description, county, municipality, or map layer type.</param>
        /// <returns>The count of map reports that match the filters.</returns>
        public async Task<int> CountAsync(string userId, string userRole, string? searchQuery)
        {
            // Get the base query using the shared method
            var query = QueryMapReportsAsync(userId, userRole, searchQuery);

            // Return the count of the filtered query
            return await query.CountAsync();
        }

        /// <summary>
        /// Retrieves a list of all map reports based on the user's role and ID.
        /// </summary>
        /// <param name="userId">The unique identifier of the user requesting the reports.</param>
        /// <param name="userRole">The role of the user requesting the reports.</param>
        /// <returns>
        /// List of query items.
        /// </returns>
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

            if (existingMapReport == null) return null;
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
    }
}
