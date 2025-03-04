using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryController(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        // GET: api/country
        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            return _countryRepository.GetCountries();
        }

        // GET: api/country/{id}
        [HttpGet("{id}")]
        public ActionResult<Country> GetCountry(int id)
        {
            var country = _countryRepository.GetCountryById(id);
            if (country == null)
                return NotFound();
            return country;
        }

        // POST: api/country
        [HttpPost]
        public IActionResult AddCountry(Country country)
        {
            _countryRepository.AddCountry(country);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Country_id }, country);
        }

        // PUT: api/country/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, Country country)
        {
            if (id != country.Country_id)
                return BadRequest();
            _countryRepository.UpdateCountry(country);
            return NoContent();
        }

        // DELETE: api/country/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            _countryRepository.DeleteCountry(id);
            return NoContent();
        }
    }
}
