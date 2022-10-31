using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCAHostelRentAlternativeingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentAlternativesController : ControllerBase
    {
        private readonly IRentAlternativeService _rentAlternativeService;

        public RentAlternativesController(IRentAlternativeService rentAlternativeService)
        {
            _rentAlternativeService = rentAlternativeService;
        }
        // GET: api/<RentAlternativeingController>
        [HttpGet]
        public IActionResult Get()
        {
            return _rentAlternativeService.GetAll() == null ? Ok("No data in the data base") : Ok(_rentAlternativeService.GetAll());
        }

        // GET api/<RentAlternativeingController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var rentAlternative = _rentAlternativeService.GetById(id);
            if (rentAlternative == null)
            {
                return NotFound();
            }
            return Ok(rentAlternative);
        }

        // POST api/<RentAlternativeingController>
        [HttpPost]
        public IActionResult Post([FromBody] RentAlternative value)
        {
            var result = _rentAlternativeService.Create(value);
            return result != null ? Ok(result) : BadRequest(value);
        }

        // PUT api/<RentAlternativeingController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RentAlternative value)
        {
            if (_rentAlternativeService.GetById(id) == null)
                return NotFound(value);
            var result = _rentAlternativeService.Update(value);
            if (!result)
                BadRequest(value);

            return Ok(_rentAlternativeService.GetById(id));
        }

        // DELETE api/<RentAlternativeingController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _rentAlternativeService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
