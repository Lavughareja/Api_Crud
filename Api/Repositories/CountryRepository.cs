using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;
namespace Api.Repositories
{
    public class CountryRepository: BaseRepository, ICountryRepository
    {
        public CountryRepository(IConfiguration configuration) : base(configuration) { }

        #region SelectAll
        // Get All Countries
        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_MST_Country_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(new Country
                            {
                                Country_id = Convert.ToInt32(reader["Country_id"]),
                                Country_name = reader["Country_name"].ToString()
                            });
                        }
                    }
                }
            }

            return countries;
        }
        #endregion

        #region SelectById
        // Get Country by ID
        public Country GetCountryById(int id)
        {
            Country country = null;

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetCountryByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            country = new Country
                            {
                                Country_id = Convert.ToInt32(reader["Country_id"]),
                                Country_name = reader["Country_name"].ToString()
                            };
                        }
                    }
                }
            }

            return country;
        }

        #endregion

        #region Insert
        // Insert Country
        public void AddCountry(Country country)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Country_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Country_name", country.Country_name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Update
        // Update Country
        public void UpdateCountry(Country country)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Country_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Country_id", country.Country_id);
                    cmd.Parameters.AddWithValue("@Country_name", country.Country_name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Delete
        // Delete Country
        public string DeleteCountry(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Country_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Country_id", id);
                   
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
