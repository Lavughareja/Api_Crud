using System.Data;
using Api.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Api.Repositories
{
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("myConnectionString");
        }

        // Get All Persons with Pagination
        public List<Person> GetAllPersons(int pageNumber, int pageSize)
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_MST_Person_SelectAll", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add pagination parameters
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    persons.Add(new Person
                    {
                        PersonId = (int)reader["person_id"],
                        FirstName = reader["first_name"].ToString(),
                        MiddleName = reader["middle_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        GenderName = reader["Gender_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Address = reader["address"].ToString(),
                        CityName = reader["city_name"].ToString(),
                        StateName = reader["state_name"].ToString(),
                        CountryName = reader["country_name"].ToString(),
                        CheckBox = (bool)reader["CheckBox"]
                    });
                }
            }
            return persons;
        }

       

        // Insert Person
        public void InsertPerson(Person_2 person)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Person_insert", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@first_name", person.FirstName);
                cmd.Parameters.AddWithValue("@middle_name", person.MiddleName);
                cmd.Parameters.AddWithValue("@last_name", person.LastName);
                cmd.Parameters.AddWithValue("@Gender_id", person.GenderId);
                cmd.Parameters.AddWithValue("@City_id", person.CityId);
                cmd.Parameters.AddWithValue("@State_id", person.StateId);
                cmd.Parameters.AddWithValue("@Country_id", person.CountryId);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                cmd.Parameters.AddWithValue("@Address", person.Address);
                cmd.Parameters.AddWithValue("@CheckBox", person.CheckBox);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Update Person
        public void UpdatePerson(Person_2 person)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Update_Person", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Person_id", person.PersonId);
                cmd.Parameters.AddWithValue("@first_name", person.FirstName);
                cmd.Parameters.AddWithValue("@middle_name", person.MiddleName);
                cmd.Parameters.AddWithValue("@last_name", person.LastName);
                cmd.Parameters.AddWithValue("@Gender_id", person.GenderId);
                cmd.Parameters.AddWithValue("@City_id", person.CityId);
                cmd.Parameters.AddWithValue("@State_id", person.StateId);
                cmd.Parameters.AddWithValue("@Country_id", person.CountryId);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Phone", person.Phone);
                cmd.Parameters.AddWithValue("@Address", person.Address);
                cmd.Parameters.AddWithValue("@CheckBox", person.CheckBox);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete Person
        public void DeletePerson(int personId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("Person_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Person_id", personId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        //serch
        public List<Person> SearchPersons(string searchTerm, int pageNumber, int pageSize)
        {
            List<Person> persons = new List<Person>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetPersons_Search", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    persons.Add(new Person
                    {
                        PersonId = (int)reader["person_id"],
                        FirstName = reader["first_name"].ToString(),
                        MiddleName = reader["middle_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        GenderName = reader["gender_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Address = reader["address"].ToString(),
                        CityName = reader["city_name"].ToString(),
                        StateName = reader["state_name"].ToString(),
                        CountryName = reader["country_name"].ToString()
                    });
                }
            }
            return persons;
        }

    }
}
