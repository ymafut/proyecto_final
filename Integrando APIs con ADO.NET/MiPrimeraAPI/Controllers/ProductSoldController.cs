using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductSoldController : ControllerBase
    {
        [HttpPost]
        public string Add([FromBody] PostProductSold productSold)
        {
            return ProductSoldHandler.Add(productSold);
        }

        [HttpGet]
        public List<ProductSold> Get()
        {
            return ProductSoldHandler.Get();
        }

        [HttpPut]
        public string Update([FromBody] ProductSold productSold)
        {
            return ProductSoldHandler.Update(productSold);
        }

        [HttpDelete]
        public string Delete([FromQuery] int id)
        {
            return ProductSoldHandler.Delete(id);
        }
    }
}
