using Microsoft.AspNetCore.Mvc;

namespace USERS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Get";
        }

        [HttpPost]
        public string Post(int id)
        {
            return "Adicionado";
        }
        [HttpPut]
        public string Put()
        {
            return "Alterado";
        }
        [HttpDelete]
        public string Delete()
        {
            return "Excluido";
        }
    }
}