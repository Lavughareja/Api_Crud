using Api.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Api.Repositories
{
    public class UserRepository : BaseRepository,IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration) : base(configuration) { }


        public User Login(string email, string password)
        {
            User user = null;
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("User_Login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = (int)reader["UserId"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }

        public bool Register(User user)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("User_Register", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Role", user.Role);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;  // Return true if inserted
                }
            }
        }

        public void SaveRefreshToken(int userId, string refreshToken, DateTime expiryTime)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("User_SaveRefreshToken", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);
                    cmd.Parameters.AddWithValue("@ExpiryTime", expiryTime);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            User user = null;
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [User] WHERE RefreshToken = @RefreshToken", conn))
                {
                    cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = (int)reader["UserId"],
                                Email = reader["Email"].ToString(),
                                RefreshToken = reader["RefreshToken"].ToString()
                            };
                        }
                    }
                }
            }
            return user;
        }
    }
}
