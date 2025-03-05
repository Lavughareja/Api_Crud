using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;

namespace Api.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(IConfiguration configuration) : base(configuration) { }

        #region SelectAll
        // Get All Cities
        public List<City> GetCities()
        {
            List<City> cities = new List<City>();

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_MST_City_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cities.Add(new City
                            {
                                city_id = reader.GetInt32(reader.GetOrdinal("city_id")),
                                city_name = reader.GetString(reader.GetOrdinal("city_name")),
                                state_name = reader.GetString("state_name").ToString(),
                                country_name = reader.GetString("country_name").ToString()
                            });
                        }
                    }
                }
            }

            return cities;
        }
        #endregion

        #region SelectById
        // Get City by ID
        public City GetCityById(int id)
        {
            City city = null;

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetCityByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CityID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            city = new City
                            {
                                city_id = reader.GetInt32(reader.GetOrdinal("city_id")),
                                city_name = reader.GetString(reader.GetOrdinal("city_name")),
                                state_name = reader.GetString(reader.GetOrdinal("state_name")),
                                country_name = reader.GetString(reader.GetOrdinal("country_name"))
                            };
                        }
                    }
                }
            }

            return city;
        }

        #endregion

        #region Insert
        // Insert City
        public void AddCity(City_2 city)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("City_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@city_name", city.city_name);
                    cmd.Parameters.AddWithValue("@state_id", city.state_id);
                    cmd.Parameters.AddWithValue("@country_id", city.country_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region Update
        // Update City
        public void UpdateCity(City_2 city)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("City_update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@city_id", city.city_id);
                    cmd.Parameters.AddWithValue("@city_name", city.city_name);
                    cmd.Parameters.AddWithValue("@state_id", city.state_id);
                    cmd.Parameters.AddWithValue("@country_id", city.country_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Delete
        // Delete City
        public string DeleteCity(int id)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("City_delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@city_id", id);
                    cmd.ExecuteNonQuery(); 
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0
                        ? "Deleted Successfully"
                        : "Delete Failed: No record found for this ID.";
                }
            }
            #endregion
        }
    }
}