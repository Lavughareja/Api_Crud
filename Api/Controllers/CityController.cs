using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        // GET: api/city
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            return _cityRepository.GetCities();
        }

        // GET: api/city/{id}
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            var city = _cityRepository.GetCityById(id);
            if (city == null)
                return NotFound();
            return city;
        }

        // POST: api/city
        [HttpPost]
        public IActionResult AddCity(City_2 city)
        {
            _cityRepository.AddCity(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.city_id }, city);
        }

        // PUT: api/city/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, City_2 city)
        {
            if (id != city.city_id)
                return BadRequest();
            _cityRepository.UpdateCity(city);
            return NoContent();
        }

        // DELETE: api/city/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            _cityRepository.DeleteCity(id);
            return NoContent();
        }
    }
}
