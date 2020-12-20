using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;


using Microsoft.AspNetCore.Mvc;

using Products.Domain.Core;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await UnitOfWork.Products.GetAll();

            var usersToReturn = Mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }


        [HttpGet]
        [Route("{UserId}", Name = "UserLink")]
        public async Task<IActionResult> Get(int ProductId)
        {
            var user = await UnitOfWork.Products.Get(ProductId);

            if (user is null)
                return NotFound();

            var userToReturn = Mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserForCreationDto ProductDto)
        {
            var user = Mapper.Map<Product>(ProductDto);

            UnitOfWork.Products.Add(user);

            if (await UnitOfWork.Complete())
                return CreatedAtRoute("UserLink", new { UserId = user.Id }, ProductDto);

            throw new Exception($"Something went wrong trying to create an product");
        }

        [HttpPut]
        [Route("{UserId}")]
        public async Task<IActionResult> Update(int UserId,
            UserForUpdateDto ProductForUpdate)
        {
            ProductForUpdate.Id = UserId;

            var userFromRepo = await UnitOfWork.Products.Get(UserId);

            Mapper.Map(ProductForUpdate, userFromRepo);

            if (await UnitOfWork.Complete())
                return Ok();

            throw new Exception($"Update Product {UserId} failed on save");
        }

        [HttpDelete]
        [Route("{UserId}")]
        public async Task<IActionResult> Delete(int UserId)
        {

            var userFromRepo = await UnitOfWork.Products.Get(UserId);

            UnitOfWork.Products.Remove(userFromRepo);

            if (await UnitOfWork.Complete())
                return Ok();

            throw new Exception($"Update Product {UserId} failed on save");
        }

    }
}
