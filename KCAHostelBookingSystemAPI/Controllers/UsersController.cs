using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using KCAHostelBookingSystemAPI.Auth;
using KCAHostelBookingSystemAPI.Models;
using KCAHostelBookingSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCAHostelUserSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHostelService _hostelService;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        public UsersController(IUserService userService, IHostelService hostelService, IConfiguration configuration, EmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
            _hostelService = hostelService;
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
            if(result == null)
            {
                return BadRequest(value);
            }

            SendBookingSuccessEmail(result);
            return Ok(result) ;
        }

        private async Task SendBookingSuccessEmail(User user)
        {
            Hostel hostel = _hostelService.GetById((int)user.HostelId);

            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:BookingSuccessful").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{hostelName}}", hostel.Name),
                    new KeyValuePair<string, string>("{{location}}", hostel.Location),
                    new KeyValuePair<string, string>("{{rentalCost}}", hostel.RentalCost.ToString()),
                    new KeyValuePair<string, string>("{{imageUrl}}", Path.Combine(Request.Host.Value, hostel.ImageUrl ?? "")),
                   
                }
            };

            await _emailService.SendEmailBookingSuccessful(options);
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
