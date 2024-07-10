using FlashCardAppWebApi.Attribute;
using FlashCardAppWebApi.Extensions;
using FlashCardAppWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCardAppWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //http://localhost:5205/api/UserCategory/AddUserCategory
    public class UserCategoryController : ControllerBase
    {
        private readonly IUserCategoryService _userCategoryService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public UserCategoryController(IUserCategoryService userCategoryService
            , IUserService userService
            , ICategoryService categoryService)
        {
            _userCategoryService = userCategoryService;
            _userService = userService;
            _categoryService = categoryService;
        }

        [HttpPost("AddUserCategory/{categoryId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddUserCategory(int categoryId)
        {
            var userId = HttpContext.GetUserId();
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                var category = await _categoryService.GetByIdAsync(categoryId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }
                if (category == null)
                {
                    return NotFound("Category not found.");
                }
                var userCategory = await _userCategoryService.GetUserCategorylist(userId, categoryId);
                if (userCategory != null)
                {
                    return BadRequest("UserCategory is already in exists");
                }
                await _userCategoryService.AddAsync(userId, categoryId);
                return Ok(new { message = "Insert UserCategory is successfully" });
            }
            catch (System.Exception)
            {
                return Unauthorized();
            }
        }
    }
}