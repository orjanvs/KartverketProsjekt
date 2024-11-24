using KartverketProsjekt.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
namespace KartverketProsjekt.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAll();
        Task<bool> DeleteUserById(string id);
    }

}
