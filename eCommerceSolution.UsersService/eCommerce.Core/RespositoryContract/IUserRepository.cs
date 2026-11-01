

using eCommerce.Core.Entities;

namespace eCommerce.Core.RespositoryContract
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> AddUser(ApplicationUser user);
        Task<ApplicationUser?> GetUserByEmailAsync(string email);
        Task<ApplicationUser?> GetUserByIdAsync(Guid userId);

    }
}
