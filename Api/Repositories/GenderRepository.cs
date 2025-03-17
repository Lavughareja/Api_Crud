using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;

namespace Api.Repositories
{
    public class GenderRepository : BaseRepository, IGenderRepository
    {
        public GenderRepository(IConfiguration configuration) : base(configuration) { }

        #region SelectAll
        public List<Gender> GetGenders()
        {
            List<Gender> genders = new List<Gender>();

            using (var conn = GetConnection())
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
        #endregion

        #region SelectByID
        public Gender GetGenderById(int id)
        {
            Gender gender = null;

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetGenderByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GenderID", id);

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
        #endregion

        #region Insert
        public void AddGender(Gender gender)
        {
            using (var conn = GetConnection())
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
        #endregion

        #region Update
        public void UpdateGender(Gender gender)
        {
            using (var conn = GetConnection())
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
        #endregion

        #region Delete
        public string DeleteGender(int id)
        {
            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("Gender_delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@gender_id", id);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0
                        ? "Deleted Successfully"
                        : "Delete Failed: No record found for this ID.";
                }
            }
        }
        #endregion
    }
}
