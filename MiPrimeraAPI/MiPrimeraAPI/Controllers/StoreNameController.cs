using Microsoft.AspNetCore.Mvc;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreNameController : ControllerBase
    {
        [HttpGet]
        public string GetStoreName()
        {
            return "Mi Primera API";
        }
    }
}
