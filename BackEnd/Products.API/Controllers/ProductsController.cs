using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Products.Domain.Core;
using Products.Domain.Dto.Product;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var products = await UnitOfWork.Products.GetAll();

            var productsToReturn = Mapper.Map<IEnumerable<ProductForListDto>>(products);

            return Ok(productsToReturn);
        }


        [HttpGet]
        [Route("{ProductId}", Name = "ProductLink")]
        public async Task<IActionResult> Get(int ProductId)
        {
            var product = await UnitOfWork.Products.Get(ProductId);

            if (product is null)
                return NotFound();

            var productToReturn = Mapper.Map<ProductForDetailDto>(product);

            return Ok(productToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductForCreationDto ProductDto)
        {
            var user = Mapper.Map<Product>(ProductDto);

            UnitOfWork.Products.Add(user);

            if (await UnitOfWork.Complete())
                return CreatedAtRoute("ProductLink", new { ProductId = user.Id }, ProductDto);

            throw new Exception($"Something went wrong trying to create an product");
        }

        [HttpPut]
        [Route("{ProductId}")]
        public async Task<IActionResult> Update(int ProductId,
            ProductForUpdateDto ProductForUpdate)
        {
            ProductForUpdate.Id = ProductId;

            var productFromRepo = await UnitOfWork.Products.Get(ProductId);

            Mapper.Map(ProductForUpdate, productFromRepo);

            if (await UnitOfWork.Complete())
                return Ok();

            throw new Exception($"Update Product {ProductId} failed on save");
        }

        [HttpDelete]
        [Route("{ProductId}")]
        public async Task<IActionResult> Delete(int ProductId)
        {

            var productFromRepo = await UnitOfWork.Products.Get(ProductId);

            UnitOfWork.Products.Remove(productFromRepo);

            if (await UnitOfWork.Complete())
                return Ok();

            throw new Exception($"Update Product {ProductId} failed on delete");
        }

    }
}
