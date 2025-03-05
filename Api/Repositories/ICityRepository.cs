
using Api.Models;


namespace Api.Repositories
{
    public interface ICityRepository
    {
        List<City> GetCities();
        City GetCityById(int id);
        void AddCity(City_2 city);
        void UpdateCity(City_2 city);
        string DeleteCity(int id);
    }
}
