using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;
namespace Api.Repositories
{
    public class CountryRepository
    {
        private readonly string _connectionString;

        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myConnectionString");
        }

        // Get All Countries
        public List<Country> GetCountries()
        {
            List<Country> countries = new List<Country>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
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

        // Get Country by ID
        public Country GetCountryById(int id)
        {
            Country country = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Country WHERE Country_id = @Country_id", conn))
                {
                    cmd.Parameters.AddWithValue("@Country_id", id);
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

        // Insert Country
        public void AddCountry(Country country)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
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

        // Update Country
        public void UpdateCountry(Country country)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
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

        // Delete Country
        public void DeleteCountry(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Country_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Country_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
