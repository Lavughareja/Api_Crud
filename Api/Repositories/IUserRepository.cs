    using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        User Login(string email, string password);
        bool Register(User user);
        void SaveRefreshToken(int userId, string refreshToken, DateTime expiryTime);
        User GetUserByRefreshToken(string refreshToken);
    }
}
