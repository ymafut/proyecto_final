using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public string Udpate([FromQuery]int id, [FromBody] PostProduct product)
        {
            return ProductHandler.Update(id, product);
        }

        [HttpDelete]
        public string Delete([FromQuery] int id)
        {
            return ProductHandler.Delete(id);
        }

        #region Este no va porque la BDD no admite "nulls" pero seria recomendable, en vez del Delete.
        //[HttpPut]
        //public string UpdateToDelete(int id)
        //{
        //    return ProductHandler.UpdateToDelete(id);
        //}
        #endregion
    }
}
