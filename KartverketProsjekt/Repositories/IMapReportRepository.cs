using KartverketProsjekt.Models.DomainModels;

namespace KartverketProsjekt.Repositories
{
    /// <summary>
    /// Provides an interface for managing map reports, including methods for retrieving, adding, updating, and deleting map reports.
    /// </summary>
    public interface IMapReportRepository
    {


        Task<List<MapReportModel>> GetAllMapReportsAsync(string userId, string userRole);

        Task<MapReportModel?> GetMapReportByIdAsync(int id);

        Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> DeleteMapReportAsync(int id);
        
    }
}
