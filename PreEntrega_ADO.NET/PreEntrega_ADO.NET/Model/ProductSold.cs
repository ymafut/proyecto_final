using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_ADO.NET
{
    public class ProductSold
    {
        public long Id { get; set; }
        public int Stock { get; set; }
        public long ProductId { get; set; }
        public long SellId { get; set; }
    }
}


