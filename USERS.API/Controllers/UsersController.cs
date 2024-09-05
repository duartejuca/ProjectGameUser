using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USERS.API.Data;
using USERS.API.Models;
using USERS.API.Models.DTO;
using USERS.API.Services;

namespace USERS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;


        public UsersController(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users;
        }

        [HttpGet("{id}")]
        public User GetById(int id)
        {
            return context.Users.Include(x => x.Id).FirstOrDefault(x => x.Id == id);
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserDTO user)
        {

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            string passwordHash = passwordHasher.HashPassword(user.name, user.password);

            User filled = new User()
            {
                Id = user.id,
                Email = user.email,
                Name = user.name,
                Password = user.password,
                Surname = user.surname,
                UserName = user.userName,
                NormalizedUserName = user.userName.Normalize(),
                NormalizedEmail = user.email.Normalize(),
                PhoneNumber = user.phoneNumber,
                AccessFailedCount = 0,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnd = DateTime.Now,
                SecurityStamp = null,
                ConcurrencyStamp = null,
                PasswordHash = passwordHash,
            };

            TokenService ts = new TokenService(filled.Password);

            //var token = await userManager.GenerateUserTokenAsync(filled, ts.CreateToken(filled.UserName, filled.Password).AccessToken, "access_token");
            var result = await userManager.CreateAsync(filled, filled.Password);

            if (result.Succeeded)
            {

                PlayerData PlayerData = new PlayerData(user.id, 0);
                context.PlayerData.Add(PlayerData);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<User> Update(int userId, User user)
        {
            if (user.Id != userId)
            {
                throw new ArgumentException("ID DIFERENTE", nameof(userId));
            }

            var existingUser = await userManager.FindByIdAsync(userId.ToString());
            if (existingUser == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            existingUser.Email = user.Email;
            existingUser.Name = user.Name;
            existingUser.Surname = user.Surname;
            existingUser.UserName = user.UserName;

            await userManager.UpdateAsync(existingUser);

            return existingUser;
        }

        [HttpDelete("{userId}")]
        public bool Delete(int userId)
        {
            var user = context.Users.FirstOrDefault<User>(user => user.Id == userId);
            if (user == null)
            {
                throw new Exception("ID NÃO ENCONTRADO");
            }

            context.Users.Remove(user);
            return context.SaveChanges() > 0;
        }

        [HttpDelete]
        public bool DeleteAllUsers()
        {
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
            return true;
        }
    }
}