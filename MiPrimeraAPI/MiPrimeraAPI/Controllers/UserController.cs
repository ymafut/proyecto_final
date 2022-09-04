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
            bool userExist = UserHandler.FindUserByUserName(user.UserName);
            if (userExist == true)
            {
                return $"El nombre de usuario \"{user.UserName}\" ya existe.";
            }
            else
            {
                return UserHandler.Add(user);
            }
        }

        [HttpGet]
        public User Get([FromBody] string userName)
        {
            return UserHandler.Get(userName);
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

        #region Este no va porque la BDD no admite "nulls" pero seria recomendable, en vez del Delete.
        //[HttpDelete]
        //public string UpdateToDelete([FromBody] int id)
        //{
        //    return UserHandler.UpdateToDelete(id);
        //}
        #endregion
    }
}
