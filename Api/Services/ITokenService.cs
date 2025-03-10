using Api.Models;

namespace Api.Services
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(User user);
        string GenerateRefreshToken();
    }
}
