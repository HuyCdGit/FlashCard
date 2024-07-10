using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.Services;
using FlashCardAppWebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlashCardAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            try
            {
                User? user = await _userService.Register(registerViewModel);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {Messge = ex.Message});
            }
        }

        //http://localhost:5205/api/user/login

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginViewModel loginViewModel)
        {
            try
            {
                var jwtToken = await _userService.Login(loginViewModel);
                return Ok(new {jwtToken});
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new {Messge = ex.Message});
            }
        }
    }
}