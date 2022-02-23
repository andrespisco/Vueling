using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VY.Person.Business.Contract.Services;
using VY.Person.Dtos;
using VY.Person.Infraestructure.Contract;

namespace VY.Person.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
      

        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(IEnumerable<PersonDto>))]
        [ProducesResponseType(500, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<ErrorObject>))]
        [ProducesResponseType(204, Type = typeof(void))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
          var result = await _personService.GetPersonsAsync();
            if (result.HasException())
            {
                return StatusCode(500);
            }
            if (result.HasErrors())
            {
                return BadRequest(result.Errors);
            }
            if (!result.Result.Any())
            {
                return NoContent();
            }
            return Ok(result.Result);
        }
        /// <summary>
        /// Get the data of a person using the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(500, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<ErrorObject>))]
        [ProducesResponseType(404, Type = typeof(void))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _personService.GetPersonAsync(id);
            if (result.HasException())
            {
                return StatusCode(500);
            }
            if (result.HasErrors())
            {
                return BadRequest(result.Result);
            }
            if(result.Result == null)
            {
                return NotFound();
            }
            return Ok(result.Result);
        }

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(PersonDto))]
        [ProducesResponseType(500, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<ErrorObject>))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDto person)
        {
            var result = await _personService.AddPersonAsync(person);
            if (result.HasException())
            {
                return StatusCode(500);
            }
            if (result.HasErrors())
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Result);
        }

        /// <summary>
        /// Updates a person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<ErrorObject>))]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PersonDto person)
        {
            var result = await _personService.UpdatePersonAsync(id, person);
            if (result.HasException())
            {
                return StatusCode(500);
            }
            if (result.HasErrors())
            {
                return BadRequest(result.Errors);
            }
            if (!result.Result)
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Deletes a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(void))]
        [ProducesResponseType(500, Type = typeof(void))]
        [ProducesResponseType(404, Type = typeof(void))]
        [ProducesResponseType(400, Type = typeof(IEnumerable<ErrorObject>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _personService.DeletePersonAsync(id);
            if (result.HasException())
            {
                return StatusCode(500);
            }
            if (result.HasErrors())
            {
                return BadRequest(result.Errors);
            }
            if (!result.Result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
