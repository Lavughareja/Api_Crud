using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        #region SelectAll
        // GET: api/city
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCities()
        {
            return _cityRepository.GetCities();
        }
        #endregion

        #region SelectByID
        // GET: api/city/{id}
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            var city = _cityRepository.GetCityById(id);
            if (city == null)
                return NotFound();
            return city;
        }
        #endregion

        #region Insert
        // POST: api/city
        [HttpPost]
        public IActionResult AddCity(City_2 city)
        {
            _cityRepository.AddCity(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.city_id }, city); 
            //return Ok("data insert successfully");
        }
        #endregion

        #region Update
        // PUT: api/city/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, City_2 city)
        {
            if (id != city.city_id)
                return BadRequest();
            _cityRepository.UpdateCity(city);
            return Ok("City Updated succcefully");
        }
        #endregion

        #region delete
        // DELETE: api/city/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var result = _cityRepository.DeleteCity(id);
            return Ok(result);
        }
        #endregion
    }
}
