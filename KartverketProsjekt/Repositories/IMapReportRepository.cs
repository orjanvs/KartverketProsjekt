using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;

namespace KartverketProsjekt.Repositories
{
    public interface IMapReportRepository
    {


        Task<List<MapReportModel>> GetAllMapReportsAsync(string userId, string userRole);

        Task<MapReportModel?> GetMapReportByIdAsync(int id);

        Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> DeleteMapReportAsync(int id);
        
    }
}
