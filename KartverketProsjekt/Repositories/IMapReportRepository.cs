using KartverketProsjekt.Models.DomainModels;

namespace KartverketProsjekt.Repositories
{
    public interface IMapReportRepository
    {
        IEnumerable<MapReportModel> GetAllMapReports();

        MapReportModel GetMapReportById(int id);

        MapReportModel AddMapReport(MapReportModel mapReport);

        MapReportModel? UpdateMapReport(MapReportModel mapReport);

        MapReportModel? DeleteMapReport(int id);


    }
}
