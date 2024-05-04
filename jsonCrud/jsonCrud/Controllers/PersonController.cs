
using Microsoft.AspNetCore.Mvc;

//using MvcCrudJson.Models;
using jsonCrud.Models;
//using jsonCrud.Data;

using jsonCrud.Repository;

namespace jsonCrud.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IDbRepo _context;

        public PersonController(IDbRepo context)
        {
            _context = context;
        }

        // GET: api/Person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonModel>>> Get()
        {
            var personModels = await _context.GetPersonDetails();
            return Ok(personModels);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonModel>> Get(int id)
        {
            var people = await _context.GetPersonDetails();
            var personModel = people.FirstOrDefault(p => p.ID == id);

            if (personModel == null)
            {
                return NotFound();
            }

            return Ok(personModel);
        }

        // POST: api/Person
        [HttpPost]
        
        public async Task<ActionResult<PersonModel>> Post([FromBody] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.InsertPerson(personModel);
                if (result)
                {
                    return CreatedAtAction(nameof(Get), new { id = personModel.ID }, personModel);
                }
                else
                {
                    return StatusCode(500); // or any other appropriate error response
                }
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromQuery] PersonModel personModel)
        {
            if (id != personModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _context.UpdatePerson(personModel);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500); // or any other appropriate error response
                }
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _context.DeletePerson(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500); // or any other appropriate error response
            }
        }

    }
}

