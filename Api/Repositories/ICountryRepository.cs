using Api.Models;


namespace Api.Repositories
{
    public interface ICountryRepository
    {
        List<Country> GetCountries();
        Country GetCountryById(int id);
        void AddCountry(Country country);
        void UpdateCountry(Country country);
        string DeleteCountry(int id);
    }
}
