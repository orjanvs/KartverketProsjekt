using KartverketProsjekt.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KartverketProsjekt.Data;

public static class TestMapReportSeedData
{
    public static async Task Initialize(KartverketDbContext context)
    {
        if (await context.MapReport.AnyAsync())
        {
            return; // DB has been seeded
        }

        await AddMapReportsBySubmitter(context, "submitterTestUserId");
        await AddMapReportsByCaseHandler(context, "caseHandlerTestUserId");
    }

    private static async Task AddMapReportsBySubmitter(KartverketDbContext context, string submitterId)
    {
        var mapReports = new List<MapReportModel>();

        for (int i = 1; i <= 30; i++)
        {
            mapReports.Add(new MapReportModel
            {
                Title = $"Map Report {i} by Submitter",
                Description = $"Description for map report {i} by submitter",
                SubmissionDate = DateTime.Now.AddDays(-i),
                GeoJsonString = $"{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{i}, {i}] }} }}",
                MapLayerId = 1, // Assuming you have a MapLayer with ID 1
                SubmitterId = submitterId,
                CaseHandlerId = null,
                MapReportStatusId = 1, // Assuming you have a MapReportStatus with ID 1
                Attachments = new List<AttachmentModel>(), // Add attachments if needed
            });
        }

        await context.MapReport.AddRangeAsync(mapReports);
        await context.SaveChangesAsync();
    }

    private static async Task AddMapReportsByCaseHandler(KartverketDbContext context, string caseHandlerId)
    {
        var mapReports = new List<MapReportModel>();

        for (int i = 31; i <= 60; i++)
        {
            mapReports.Add(new MapReportModel
            {
                Title = $"Map Report {i} by Case Handler",
                Description = $"Description for map report {i} by case handler",
                SubmissionDate = DateTime.Now.AddDays(-i),
                GeoJsonString = $"{{ \"type\": \"Feature\", \"geometry\": {{ \"type\": \"Point\", \"coordinates\": [{i}, {i}] }} }}",
                MapLayerId = 1, // Assuming you have a MapLayer with ID 1
                SubmitterId = caseHandlerId,
                CaseHandlerId = null,
                MapReportStatusId = 1, // Assuming you have a MapReportStatus with ID 1
                Attachments = new List<AttachmentModel>(), // Add attachments if needed
            });
        }

        await context.MapReport.AddRangeAsync(mapReports);
        await context.SaveChangesAsync();
    }
}

