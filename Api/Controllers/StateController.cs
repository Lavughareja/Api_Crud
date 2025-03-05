using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }
        #region SelectAll
        // GET: api/state
        [HttpGet]
        public ActionResult<IEnumerable<State>> GetStates()
        {
            return _stateRepository.GetStates();
        }
        #endregion

        #region Insert
        // POST: api/state
        [HttpPost]
        public IActionResult AddState(State_2 state)
        {
            _stateRepository.AddState(state);
            return CreatedAtAction(nameof(GetStates), state);
        }
        #endregion

        #region Update
        // PUT: api/state/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, State_2 state)
        {
            if (id != state.state_id)
                return BadRequest();
            _stateRepository.UpdateState(state);
            return NoContent();
        }
        #endregion

        #region Delete
        // DELETE: api/state/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            _stateRepository.DeleteState(id);
            return NoContent();
        }
        #endregion
    }
}
