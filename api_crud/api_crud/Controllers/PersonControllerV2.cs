using Microsoft.AspNetCore.Mvc;
using jsonCrud.Models;
using jsonCrud.Repository;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using api_crud.validation_handler;

namespace jsonCrud.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/person")]
    [Route("api/{version:apiVersion}/person")]
    [Authorize(Policy = "DynamicRolePolicy")]
    public class PersonControllerV2 : ControllerBase
    {
        private readonly IDbRepo _context;
        private readonly DynamicValidationService _validationService;

        public PersonControllerV2(IDbRepo context, DynamicValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
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
        public async Task<ActionResult<PersonModel>> GetV2(int id)
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
        [Authorize(Policy = "DynamicRolePolicy")]
        public async Task<ActionResult<PersonModel>> PostV2([FromBody] PersonModel personModel)
        {
            if (ModelState.IsValid)
            {
                _validationService.ApplyValidations(personModel);

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
        [Authorize(Policy = "DynamicRolePolicy")]
        public async Task<IActionResult> PutV2(int id, [FromBody] PersonModel personModel)
        {
            if (id != personModel.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _validationService.ApplyValidations(personModel);

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
        [Authorize(Policy = "DynamicRolePolicy")]
        public async Task<IActionResult> DeleteV2(int id)
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
