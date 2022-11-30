using DAL_CRUD.Models;
using KCAHostelBookingSystemAPI.Auth;
using KCAHostelBookingSystemAPI.Models;
using KCAHostelBookingSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KCAHostelBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;
        public AuthenticateController(
            EmailService emailService,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var LoggedinUser = new Register();
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                LoggedinUser = new Register()
                {
                    Firstname = user.FirstName,
                    Lastname = user.LastName,
                    Email = user.Email,
                    HostelId = user.HostelId ?? 0,
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole)); ;
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiratation = token.ValidTo,
                    roles = userRoles,
                    username = LoggedinUser.Email,
                    appuser = LoggedinUser
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exist" });
            }
            if (string.Compare(model.NewPassword, model.ConfirmPassword) != 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "The new password and the confirm new password do not match" });

            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            }

            return Ok(new Response { Status = "Success", Message = "Password Changed" });
        }

        [HttpPut]
        [Route("change-Username")]
        public async Task<IActionResult> SendChangeEmailToken([FromBody] ChangeUserName model)
        {
            try
            {
                await GenerateChangeEmailTokenAsync(model);

            }
            catch (Exception)
            {

                throw;
            }



            //var user = await _userManager.FindByNameAsync(model.OldEmail);

            //if (user == null)
            //{
            //    return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exist" });
            //}

            //var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, model.Token);

            //if (!result.Succeeded)
            //{
            //    var errors = new List<string>();
            //    foreach (var error in result.Errors)
            //    {
            //        errors.Add(error.Description);
            //    }

            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            //}

            return Ok(new Response { Status = "Success", Message = "Email was Send" });
        }


        [HttpPut]
        [Route("forgot-password")]
        public async Task<IActionResult> SendForgotPasswordToken([FromBody] string email)
        {
            try
            {
                await GenerateForgotPasswordTokenAsync(email);

            }
            catch (Exception)
            {

                throw;
            }


            return Ok(new Response { Status = "Success", Message = "Email was Send" });
        }

        [HttpPut]
        [Route("forgot-password-confirm")]
        public async Task<IActionResult> ForgotPassword(string uid, string token, [FromBody] ForgotPassword model)
        {
            token = token.Replace(' ', '+');

            var user = await _userManager.FindByIdAsync(uid.Trim());
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exist" });
            }

           
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            }
            
            return Ok(new Response { Status = "Success", Message = "Password was changed Successfully" });
        }


        [HttpPut]
        [Route("change-Username-confirmed")]
        public async Task<IActionResult> ChangeUsername(string uid, string token, [FromBody] ChangeUserName model)
        {
            token = token.Replace(' ', '+');

            var user = await _userManager.FindByIdAsync(uid.Trim());
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "User does not exist" });
            }
            user.UserName = model.NewEmail;
            user.Email = model.NewEmail;
            model.OldEmail = user.Email;

            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token); 
            

            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = string.Join(",", errors) });
            }

            await _userManager.UpdateAsync(user);
            ApplicationUser user2 = await _userManager.FindByEmailAsync(model.NewEmail);
            if (user2 == null)
            {
                Debug.WriteLine("no user found");
            }
            return Ok(new Response { Status = "Success", Message = "Email was changed" });
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });


            }

            ApplicationUser user = new()
            {
                FirstName = model.Firstname,
                LastName = model.Lastname,
                HostelId = model.HostelId,
                Email = model.Email.Replace(" ", String.Empty),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            //create role in the data base if they do not exist
            if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (await _roleManager.RoleExistsAsync(UserRoles.Manager))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Manager); ;
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }


            return
                Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpGet]
        [Route("send-testemail")]
        public async Task SendTempMail()
        {
            try
            {
                await _emailService.SendTestEmail(new UserEmailOptions { Body = "Hello", ToEmails = new List<string>() { "sacalivinmocha@gmail.com" } });

            }
            catch (Exception e)
            {

                throw;
            }
        }


        [HttpGet]
        [Route("get-user")]
        public async Task<Register> Get(string email)
        {
            var identityUsers = await _userManager.GetUsersInRoleAsync(UserRoles.User);
            List<Register> users = new List<Register>();
            foreach (var identityUser in identityUsers)
            {
                var user = new Register()
                {
                    Email = identityUser.Email,


                };
                users.Add(user);
            }
            return users.FirstOrDefault(u => u.Email == email);
        }



        [HttpGet]
        public async Task<List<Register>> GetAll()
        {
            var identityUsers = await _userManager.GetUsersInRoleAsync(UserRoles.User);
            List<Register> users = new List<Register>();
            foreach (var identityUser in identityUsers)
            {
                var user = new Register()
                {
                    Email = identityUser.Email,


                };
                users.Add(user);
            }
            return users;
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });


            }

            ApplicationUser user = new()
            {
                FirstName = model.Firstname,
                LastName = model.Lastname,
                HostelId = model.HostelId,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }

            //create role in the data base if they do not exist
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));


            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin); ;
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }


            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        private async Task GenerateChangeEmailTokenAsync(ChangeUserName model)
        {
            var user = await _userManager.FindByNameAsync(model.OldEmail);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            if (!string.IsNullOrEmpty(token))
            {
                await SendChangeEmail(user,model.NewEmail, token);
            }
        }
        private async Task GenerateForgotPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
               await SendForgotPasswordEmail(user, token);
            }
        }

        private async Task SendChangeEmail(ApplicationUser user,string newEmail, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ChangeEmail").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { newEmail },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailChangeEmail(options);
        }

        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailChangeEmail(options);
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
        }
    }
}
