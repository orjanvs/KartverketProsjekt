using KartverketProsjekt.Models.DomainModels;

namespace KartverketProsjekt.Repositories
{
    /// <summary>
    /// Provides an interface for managing map reports, including methods for retrieving, adding, updating, and deleting map reports.
    /// </summary>
    public interface IMapReportRepository
    {


        Task<List<MapReportModel>> GetMapReportsAsync(string userId, string userRole, string? searchQuery = null, int pageNumber = 1, int pageSize = 100);
        Task<List<MapReportModel>> GetAllMapReportsAsync(string userId, string userRole);
        Task<MapReportModel?> GetMapReportByIdAsync(int id);

        Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> DeleteMapReportAsync(int id);

        Task<int> CountAsync(string userId, string userRole, string? searchQuery);
        
    }
}
