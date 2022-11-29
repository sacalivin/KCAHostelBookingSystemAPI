using BAL_CRUD.Interfaces;
using DAL_CRUD.Models;
using KCAHostelBookingSystemAPI.Auth;
using kcauHosteslAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KCAHostelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelsController : ControllerBase
    {
        private readonly IHostelService _hostelService;
        
        private readonly IWebHostEnvironment _hostingEnvironment;

     
        public HostelsController(IHostelService hostelService, IWebHostEnvironment hostingEnvironment)
        {
            _hostelService = hostelService;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: api/<HostelController>
        [HttpGet]
        public IActionResult Get()
        {
            
            return _hostelService.GetAll() == null ? Ok("No data in the data base") 
                : Ok(_hostelService
                .GetAll().Select(x => { x.ImageUrl = Path.Combine(Request.Host.Value, x.ImageUrl ?? ""); return x; })
                .ToList());
        }

        // GET api/<HostelController>/5
        [HttpGet("get-by-id/{id}")]
        public IActionResult Get(int id)
        {
            var hostel = _hostelService.GetById(id);
            if (hostel == null)
            {
                return NotFound();
            }
            return Ok(hostel);
        }

        [HttpGet("get-by-name/{name}")]
        public IActionResult GetByName(string name)
        {
            var hostel = _hostelService.GetHostelByHostelName(name);
            if (hostel == null)
            {
                return NotFound();
            }
            return Ok(hostel);
        }

        // POST api/<HostelController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Hostel value)
        {
           var filename = Path.GetFileName(value.Image.FileName);
            var filePath= Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images",filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                value.Image.CopyTo(stream);
            }
            //image.
            value.ImageUrl = Path.Combine(@"images", filename);
            var result = _hostelService.Create(value);

            //create an account for the manager
            await CreateHostelManagerAccount(result);
            return result != null ? Ok(result) : BadRequest(value);
        }

        private async Task<bool> CreateHostelManagerAccount(Hostel hostel)
        {
            var email = hostel.Name.Trim(' ');
            Register manager = new Register()
            {
                Firstname = "manager",
                Lastname = hostel.Name,
                Email =$"{email}@example.com",
                Password ="Manager@123",
                HostelId = hostel.Id
            };

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://localhost:7119/api/Authenticate/");

                    var response = await client.PostAsJsonAsync("register", manager);

                    if (response.IsSuccessStatusCode)
                    {
                        string results = response.Content.ReadAsStringAsync().Result;
                        var registeredDetails = JsonConvert.DeserializeObject<Register>(results);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    return false;
                }
              

            }
        }

        // PUT api/<HostelController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Hostel value)
        {
            
            var filename = Path.GetFileName(value.Image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                value.Image.CopyTo(stream);
            }
            //image.
            value.ImageUrl = Path.Combine(@"images", filename);
            value.Id = id;
            if (_hostelService.GetById(id) == null)
                return NotFound(value);
            var result = _hostelService.Update(value);
            if (!result)
                BadRequest(value);

            return Ok(_hostelService.GetById(id));
        }

        // DELETE api/<HostelController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _hostelService.Delete(id);
            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
