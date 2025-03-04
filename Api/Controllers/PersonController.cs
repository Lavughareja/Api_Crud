using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Repositories;
using System.Collections.Generic;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonRepository _personRepository;

        public PersonController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: api/Person
        [HttpGet]
        public IActionResult GetAllPersons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            List<Person> persons = _personRepository.GetAllPersons(pageNumber, pageSize);
            return Ok(persons);
        }
        // POST: api/Person
        [HttpPost]
        public ActionResult Post([FromBody] Person_2 person)
        {
            _personRepository.InsertPerson(person);
            return Ok(new { message = "Person added successfully" });
        }

        // PUT: api/Person
        [HttpPut]
        public ActionResult Put([FromBody] Person_2 person)
        {
            _personRepository.UpdatePerson(person);
            return Ok(new { message = "Person updated successfully" });
        }

        // DELETE: api/Person/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _personRepository.DeletePerson(id);
            return Ok(new { message = "Person deleted successfully" });
        }
        [HttpGet("search")]
        public ActionResult<List<Person>> SearchPersons([FromQuery] string searchTerm, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var result = _personRepository.SearchPersons(searchTerm, pageNumber, pageSize);
            return Ok(result);
        }

    }
}
