using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesManagementSystem.Dtos.Account;
using SalesManagementSystem.Models;
using System.Reflection.Metadata;

namespace SalesManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(new { Message = "User registered successfully" });
                    }
                    return StatusCode(500, new { Message = "User created but failed to assign role", Details = roleResult.Errors });
                }
                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Details = ex.Message });
            }
        }
    }
}
