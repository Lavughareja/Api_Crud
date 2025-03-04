using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;

namespace Api.Repositories
{
    public class StateRepository
    {
        private readonly string _connectionString;

        public StateRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myConnectionString");
        }

        // Get All States
        public List<State> GetStates()
        {
            List<State> states = new List<State>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("PR_MST_State_SelectAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            states.Add(new State
                            {
                                state_id = Convert.ToInt32(reader["state_id"]),
                                state_name = reader["state_name"].ToString(),
                                country_name = reader["country_name"].ToString(),
                                
                            });
                        }
                    }
                }
            }

            return states;
        }

        // Get State by ID
        public State GetStateById(int id)
        {
            State state = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM States WHERE state_id = @state_id", conn))
                {
                    cmd.Parameters.AddWithValue("@state_id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            state = new State
                            {
                                state_id = Convert.ToInt32(reader["state_id"]),
                                state_name = reader["state_name"].ToString(),
                                country_name = reader["country_name"].ToString (),
                            };
                        }
                    }
                }
            }

            return state;
        }

        // Insert State
        public void AddState(State_2 state)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("State_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_name", state.state_name);
                    cmd.Parameters.AddWithValue("@country_id", state.Country_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update State
        public void UpdateState(State_2 state)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("State_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_id", state.state_id);
                    cmd.Parameters.AddWithValue("@state_name", state.state_name);
                    cmd.Parameters.AddWithValue("@country_id", state.Country_id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete State
        public void DeleteState(int id) 
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("State_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
