using System.Data;
using Microsoft.Data.SqlClient;
using Api.Models;

namespace Api.Repositories
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public StateRepository(IConfiguration configuration) : base(configuration) { }


        #region SelectAll
        // Get All States
        public List<State> GetStates()
        {
            List<State> states = new List<State>();

            using (var conn = GetConnection())
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
        #endregion

        #region SelectByID
        // Get State by ID
        public State GetStateById(int id)
        {
            State state = null;

            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("GetStateByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateID", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            state = new State
                            {
                                state_id = Convert.ToInt32(reader["state_id"]),
                                state_name = reader["state_name"].ToString(),
                                country_name = reader["country_name"].ToString()
                            };
                        }
                    }
                }
            }

            return state;
        }

        #endregion

        #region Insert
        // Insert State
        public void AddState(State_2 state)
        {
            using (var conn = GetConnection())
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

        #endregion

        #region Update
        // Update State
        public void UpdateState(State_2 state)
        {
            using (var conn = GetConnection())
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
        #endregion

        #region Delete
        // Delete State
        public string DeleteState(int id) 
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("State_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@state_id", id);
                    
                    var rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0
                        ? "Deleted Successfully"
                        : "Delete Failed: No record found for this ID.";
                }
            }
        }
        #endregion
    }
}
