using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Products.Domain.Dto.Product
{
    public class ProductForUpdateDto
    {

        [Required(ErrorMessage ="Product Id is required")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage = "Product Name is required")]
        [MaxLength(50,ErrorMessage ="Product name max lenght 50")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Location is required")]
        [MaxLength(50, ErrorMessage = "Product Location max lenght 50")]

        public string Location { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Owner is required")]
        [MaxLength(50, ErrorMessage = "Product Owner max lenght 50")]

        public string Owner { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
