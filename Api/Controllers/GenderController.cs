using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly GenderRepository _genderRepository;

        public GenderController(GenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        #region SelectAll
        // GET: api/gender
        [HttpGet]
        public ActionResult<IEnumerable<Gender>> GetGenders()
        {
            return _genderRepository.GetGenders();
        }
        #endregion

        #region SelectById 
        // GET: api/gender/{id}
        [HttpGet("{id}")]
        public ActionResult<Gender> GetGender(int id)
        {
            var gender = _genderRepository.GetGenderById(id);
            if (gender == null)
                return NotFound();
            return gender;
        }
        #endregion

        #region Insert
        // POST: api/gender
        [HttpPost]
        public IActionResult AddGender(Gender gender)
        {
            _genderRepository.AddGender(gender);
            return CreatedAtAction(nameof(GetGender), new { id = gender.gender_id }, gender);
        }
        #endregion

        #region Update
        // PUT: api/gender/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateGender(int id, Gender gender)
        {
            if (id != gender.gender_id)
                return BadRequest();
            _genderRepository.UpdateGender(gender);
            return NoContent();
        }
        #endregion

        #region Delete
        // DELETE: api/gender/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteGender(int id)
        {
            _genderRepository.DeleteGender(id);
            return NoContent();
        }
        #endregion
    }
}
