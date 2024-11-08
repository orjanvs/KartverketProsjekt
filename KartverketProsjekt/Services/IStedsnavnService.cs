using KartverketProsjekt.API_Models;    

namespace KartverketProsjekt.Services
{
    public interface IStedsnavnService
    {
        Task<StedsnavnResponse> GetStedsnavnAsync(string search);
    }
}
