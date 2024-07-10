using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace FlashCardAppWebApi.Filters
{
    public class JwtAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public JwtAuthorizeFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Kiểm tra JWT token ở đây
            var token = context.HttpContext.Request
                .Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //Nếu token hết hạn,
                    //thì khi gọi phương thức ValidateToken,
                    //mã lỗi SecurityTokenExpiredException sẽ được throw ra
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    //Token hết hạn
                    //... chờ xử lý code
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var userId = int.Parse(jwtToken.Claims.First().Value);
                context.HttpContext.Items["userId"] = userId;
            }
            catch (System.Exception)
            {
                // Nếu không hợp lệ, bạn có thể trả về UnauthorizedResult hoặc RedirectResult
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}