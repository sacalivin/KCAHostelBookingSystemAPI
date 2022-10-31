using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KCAHostelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookingController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // GET: api/<BookingController>
        [HttpGet]
        public IActionResult Get()
        {
             return _bookService.GetAll() == null ? Ok("No data in the data base") : Ok(_bookService.GetAll());
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookService.GetById(id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok( book);
        }

        // POST api/<BookingController>
        [HttpPost]
        public IActionResult Post([FromBody] Book value)
        {
            var result = _bookService.Create(value);
            return result != null ? Ok(result) : BadRequest(value);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book value)
        {
            if(_bookService.GetById(id) == null)
                return NotFound(value);
            var result = _bookService.Update(value);
            if(!result)
                BadRequest(value);

            return Ok(_bookService.GetById(id));
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _bookService.Delete(id);
            if(!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
