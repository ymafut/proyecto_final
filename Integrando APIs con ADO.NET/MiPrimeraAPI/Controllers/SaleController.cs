using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOs;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        [HttpPost]
        public string Add([FromBody] PostSale sale)
        {
            return SaleHandler.Add(sale);
        }

        [HttpGet]
        public List<Sale> Get()
        {
            return SaleHandler.Get();
        }

        [HttpPut]
        public string Update([FromBody] Sale sale)
        {
            return SaleHandler.Update(sale);
        }

        [HttpDelete]
        public string Delete([FromQuery] int id)
        {
            return SaleHandler.Delete(id);
        }
    }
}
