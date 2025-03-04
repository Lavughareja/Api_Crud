using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;

namespace Api.Repositories
{
    public class GenderRepository
    {
        private readonly string _connectionString;

        public GenderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myConnectionString");
        }

        // Get All Genders
        public List<Gender> GetGenders()
        {
            List<Gender> genders = new List<Gender>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_MST_Gender_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            genders.Add(new Gender
                            {
                                gender_id = reader.GetInt32(reader.GetOrdinal("gender_id")),
                                gender_name = reader.GetString(reader.GetOrdinal("gender_name"))
                            });
                        }
                    }
                }
            }

            return genders;
        }

        // Get Gender by ID
        public Gender GetGenderById(int id)
        {
            Gender gender = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT gender_id, gender_name FROM Gender WHERE gender_id = @gender_id", conn))
                {
                    cmd.Parameters.AddWithValue("@gender_id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gender = new Gender
                            {
                                gender_id = reader.GetInt32(reader.GetOrdinal("gender_id")),
                                gender_name = reader.GetString(reader.GetOrdinal("gender_name"))
                            };
                        }
                    }
                }
            }

            return gender;
        }

        // Insert Gender
        public void AddGender(Gender gender)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Gender_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gender_name", gender.gender_name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update Gender
        public void UpdateGender(Gender gender)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Gender_update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gender_id", gender.gender_id);
                    cmd.Parameters.AddWithValue("@gender_name", gender.gender_name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete Gender
        public void DeleteGender(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Gender_delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gender_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
