using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlashCardAppWebApi.Models;
using FlashCardAppWebApi.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FlashCardAppWebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashCardAppContext _context; // Assuming you have a DbContext named FlashcardAppContext
        private readonly IConfiguration _configuration;

        public UserRepository(FlashCardAppContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
        }

        public async Task<User?> AddAsync(RegisterViewModel registerViewModel)
        {
            string sql = "EXEC dbo.RegisterUser @username, @password, @email, @phone, @full_name, @date_of_birth, @country";
            IEnumerable<User?> users = await _context.users
            .FromSqlRaw(sql,
                new SqlParameter("@username", registerViewModel.Username ?? ""),
                new SqlParameter("@password", registerViewModel.Password),
                new SqlParameter("@email", registerViewModel.Email),
                new SqlParameter("@phone", registerViewModel.Phone ?? ""),
                new SqlParameter("@full_name", registerViewModel.FullName ?? ""),
                new SqlParameter("@date_of_birth", ""),
                new SqlParameter("@country", registerViewModel.Country)
            )
            .ToListAsync();

            var user = users.FirstOrDefault();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user != null)
            {
                _context.users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Username == userName);
        }

        public async Task<string> LoginUser(LoginViewModel loginViewModel)
        {
            string sql = "EXEC dbo.CheckLogin @email, @password";
            IEnumerable<User?> users = await _context.users
            .FromSqlRaw(sql,
                new SqlParameter("@email", loginViewModel.Email),
                new SqlParameter("@password", loginViewModel.Password))
            .ToListAsync();

            var user = users.FirstOrDefault();
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"] ?? "");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        // new Claim(ClaimTypes.NameIdentifier, user.Username),
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);
                return jwtToken;
            }
            else
            {
                throw new ArgumentException("wrong password or email");
            }
        }

        public async Task UpdateAsync(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}