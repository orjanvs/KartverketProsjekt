﻿using KartverketProsjekt.Models.DomainModels;
using KartverketProsjekt.Models.ViewModels;

namespace KartverketProsjekt.Repositories
{
    public interface IMapReportRepository
    {
        Task<IEnumerable<ListReportsViewModel>> GetSomeMapReportsAsync(string userId, string userRole, int pageNumber, int pageSize);

        Task<IEnumerable<MapReportModel>> GetAllMapReportsAsync();

        Task<MapReportModel?> GetMapReportByIdAsync(int id);

        Task<MapReportModel> AddMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> UpdateMapReportAsync(MapReportModel mapReport);

        Task<MapReportModel?> DeleteMapReportAsync(int id);
        
    }
}
