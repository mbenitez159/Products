using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Products.Domain.Core;
using Products.Domain.Dto.User;
using Products.Domain.Service;

namespace Products.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;
        public UsersController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto UserForLogin)
        {
            var user = await _userManager.FindByEmailAsync(UserForLogin.Email);

            if (user is null)
                return Unauthorized();

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, UserForLogin.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            return Ok(new UserToReturnForAuthDto
            {
                Token = _jwtGenerator.CreateToken(user),
                Username = user.UserName,
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto UserForRegister)
        {
            var userFromRepo = await _userManager.FindByEmailAsync(UserForRegister.Email);

            if (userFromRepo != null)
                return BadRequest("Username and/or Email already exists");

            var user = new AppUser
            {
                Email = UserForRegister.Email,
            };

            var result = await _userManager.CreateAsync(user, UserForRegister.Password);

            if (!result.Succeeded)
                throw new Exception("Problem creating user");

            return Ok(new UserToReturnForAuthDto
            {
                Token = _jwtGenerator.CreateToken(user),
                Username = user.UserName,
            });
        }

        //[HttpPut]
        //[Authorize]
        //[Route("{UserId}")]
        //public async Task<IActionResult> Update(int UserId,
        //    UserForUpdateDto UserForUpdate)
        //{

        //    return NoContent();
        //}

        [HttpDelete]
        [Authorize]
        [Route("{UserId}")]
        public async Task<IActionResult> Delete(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception("Something went wrong trying to delete user");

            return Ok();

        }

    }
}
