

using eCommerce.Core.Entities;

namespace eCommerce.Core.ServicesContract
{
    public  interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user);
    }
}
