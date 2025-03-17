using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="admin")] // Requires authentication for all endpoints
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;

        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        #region SelectAll
        [HttpGet]
        public ActionResult<IEnumerable<Gender>> GetGenders()
        {
            return _genderRepository.GetGenders();
        }
        #endregion

        #region SelectById
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
        [HttpPost]
        public IActionResult AddGender(Gender gender)
        {
            _genderRepository.AddGender(gender);
            return CreatedAtAction(nameof(GetGender), new { id = gender.gender_id }, gender);
        }
        #endregion

        #region Update
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
        [HttpDelete("{id}")]
        public IActionResult DeleteGender(int id)
        {
            var result = _genderRepository.DeleteGender(id);
            return Ok(result);
        }
        #endregion
    }
}
