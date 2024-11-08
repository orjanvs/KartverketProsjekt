using KartverketProsjekt.API_Models;

namespace KartverketProsjekt.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);
    }
}
