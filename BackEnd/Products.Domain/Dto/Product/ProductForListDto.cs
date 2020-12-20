using System;
using System.Collections.Generic;
using System.Text;

namespace Products.Domain.Dto.Product
{
   public class ProductForListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Owner { get; set; }

        public decimal Price { get; set; }
    }
}
