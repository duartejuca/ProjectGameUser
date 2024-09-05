using Microsoft.AspNetCore.Mvc;
using USERS.API.Data;
using USERS.API.Models;

namespace USERS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlayerDataController : ControllerBase
    {
        private readonly DataContext context;

        public PlayerDataController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<PlayerData> Get()
        {
            return
                context.PlayerData;

        }

        [HttpGet("{userId:int}")]
        public PlayerData GetByUserId(int userId)
        {
            return context.PlayerData
                    .FirstOrDefault(pd => pd.UserId == userId);
        }


        [HttpPost]
        public IEnumerable<PlayerData> Post(PlayerData playerData)
        {
            context.PlayerData.Add(playerData);
            if (context.SaveChanges() > 0)
            {
                return context.PlayerData;
            }
            else
            {
                throw new Exception("ERRO AO ADICIONAR");
            }
        }

        [HttpPut("{userId:int}")]
        public PlayerData Update(int userId, int points)
        {

            var playerData = context.PlayerData
                    .FirstOrDefault(pd => pd.UserId == userId);
            if (playerData == null)
            {
                throw new Exception("ID NÃO ENCONTRADO");
            }
            playerData.Point = points;
            context.PlayerData.Update(playerData);
            context.SaveChanges();
            return playerData;
        }

        [HttpDelete("{userId:int}")]
        public bool Delete(int userId)
        {
            var playerData = context.PlayerData.FirstOrDefault(playerData => playerData.UserId == userId);
            if (playerData == null)
            {
                throw new Exception("ID NÃO ENCONTRADO");
            }

            context.PlayerData.Remove(playerData);
            return context.SaveChanges() > 0;
        }
    }
}