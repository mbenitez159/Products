
using Products.Domain.Core;

namespace Products.Domain.Service
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
