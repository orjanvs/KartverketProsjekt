using KartverketProsjekt.Models.DomainModels;

namespace KartverketProsjekt.Repositories
{
    public interface IMapReportRepository
    {
        Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync();

        Task<MapReportModel?> GetMapReportByIdAsync(int id);

        Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> DeleteMapReportAsync(int id);


    }
}
