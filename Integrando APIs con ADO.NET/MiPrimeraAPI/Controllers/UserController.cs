using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public string Add([FromBody] PostUser user)
        {
            return UserHandler.Add(user);
        }

        [HttpGet]
        public List<User> Get()
        {
            return UserHandler.Get();
        }

        [HttpPut]
        public string Update([FromBody] User user)
        {
            return UserHandler.Update(user);
        }

        [HttpDelete]
        public string Delete([FromQuery] int id)
        {
            return UserHandler.Delete(id);
        }

        //[HttpDelete]
        //public string UpdateToDelete([FromBody] int id)
        //{
        //    return UserHandler.UpdateToDelete(id);
        //}
    }
}
