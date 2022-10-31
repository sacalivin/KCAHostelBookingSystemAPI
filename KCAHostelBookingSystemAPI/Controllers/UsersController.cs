using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCAHostelUserSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return _userService.GetAll() == null ? Ok("No data in the data base") : Ok(_userService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User value)
        {
            var result = _userService.Create(value);
            return result != null ? Ok(result) : BadRequest(value);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (_userService.GetById(id) == null)
                return NotFound(value);
            var result = _userService.Update(value);
            if (!result)
                BadRequest(value);

            return Ok(_userService.GetById(id));
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
