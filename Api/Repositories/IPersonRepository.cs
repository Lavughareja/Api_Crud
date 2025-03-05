using Api.Models;

namespace Api.Repositories
{
    public interface IPersonRepository
    {
            List<Person> GetAllPersons(int pageNumber, int pageSize);
            void InsertPerson(Person_2 person);
            void UpdatePerson(Person_2 person);
            string DeletePerson(int personId);
            List<Person> SearchPersons(string searchTerm, int pageNumber, int pageSize);
        }
    }


