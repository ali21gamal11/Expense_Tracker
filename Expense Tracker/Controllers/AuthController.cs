using Expense_Tracker.Data;
using Expense_Tracker.Data.DTOs;
using Expense_Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Expense_Tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;
        private readonly IPasswordService _passwordService;
        public AuthController(ApplicationDbContext context, IPasswordService passwordService , IJwtService jwtService)
        {
            _context = context;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login( LoginDto dto) {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if(user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var isPasswordValid = _passwordService.VerifyPassword(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                return Unauthorized("Invalid email or password");
            }

            var token = _jwtService.GenerateToken(
                user.Id,
                user.Email,
                user.Role
            );

            return Ok(token);


        }
    }
}
