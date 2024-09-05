using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using USERS.API.Models;

namespace USERS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login model)
        {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var token = await userManager.GenerateUserTokenAsync(user, "Auth", "access_token");
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
