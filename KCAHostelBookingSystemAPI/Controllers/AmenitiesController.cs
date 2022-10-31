using BAL_CRUD.Interfaces;
using BAL_CRUD.Services;
using DAL_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCAHostelArmenitySystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _armenityService;

        public AmenitiesController(IAmenityService armenityService)
        {
            _armenityService = armenityService;
        }
        // GET: api/<ArmenityController>
        [HttpGet]
        public IActionResult Get()
        {
            return _armenityService.GetAll() == null ? Ok("No data in the data base") : Ok(_armenityService.GetAll());
        }

        // GET api/<ArmenityController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var armenity = _armenityService.GetById(id);
            if (armenity == null)
            {
                return NotFound();
            }
            return Ok(armenity);
        }

        // POST api/<ArmenityController>
        [HttpPost]
        public IActionResult Post([FromBody] Armenity value)
        {
            var result = _armenityService.Create(value);
            return result != null ? Ok(result) : BadRequest(value);
        }

        // PUT api/<ArmenityController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Armenity value)
        {
            if (_armenityService.GetById(id) == null)
                return NotFound(value);
            var result = _armenityService.Update(value);
            if (!result)
                BadRequest(value);

            return Ok(_armenityService.GetById(id));
        }

        // DELETE api/<ArmenityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _armenityService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        
    }
}
