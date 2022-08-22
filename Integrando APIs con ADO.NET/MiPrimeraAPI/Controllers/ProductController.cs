using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public string Add([FromBody] PostProduct product)
        {
            return ProductHandler.Add(product);
        }

        [HttpGet]
        public List<Product> Get()
        {
            return ProductHandler.Get();
        }

        [HttpPut]
        public string Udpate([FromBody] Product product)
        {
            return ProductHandler.Update(product);
        }

        [HttpDelete]
        public string Delete([FromQuery] int id)
        {
            return ProductHandler.Delete(id);
        }
    }
}
