using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public string Login(PostLogin user)
        {
            return UserHandler.Login(user);
        }
    }
}
