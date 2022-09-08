using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimplyChatAPI.Data;
using SimplyChatAPI.DTOs;
using SimplyChatAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SimplyChatAPI.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {

            _logger = logger;

        }
        // GET: api/Allusers
        [HttpGet]
        public async Task<IEnumerable<User>> AllUsers()
        {
            return await _context.Users.ToListAsync();
        }



        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var isExist = _context.Users.FirstOrDefault(x => x.Email == register.Email);
                if (isExist != null)
                {
                    var user = new User
                    {
                        FirstName = register.FirstName,
                        LastName = register.LastName,
                        Email = register.Email
                    };
                    var result = await _context.Users.AddAsync(user);
                    return result != null ? Accepted() : (ActionResult)BadRequest(ModelState);

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return StatusCode(500, "Internal server error. Please try again later");
            }



        }


        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest signInRequest)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == signInRequest.Email);
            if (user is null)
            {
                return BadRequest();
            }

            var claims = new List<Claim>
            {
                new Claim(type: ClaimTypes.Email, value: signInRequest.Email),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                });

            return Ok();
        }



        [Authorize]
        [HttpGet("signout")]
        public async Task SignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
