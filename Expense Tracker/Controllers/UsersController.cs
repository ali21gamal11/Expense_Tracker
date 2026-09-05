using Expense_Tracker.Data;
using Expense_Tracker.Data.DTOs;
using Expense_Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Expense_Tracker.Controllers


{

    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        public UsersController(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
            .Select(u => new UserResponseDto
            {
                Id = u.Id,
                 Name = u.Name,
                 Email = u.Email
            })
            .ToListAsync();

            return Ok(users);
            
        }

            
            
        

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto dto)
        {
            var user = new User
            {

                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = _passwordService.HashPassword(dto.Password),
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email

            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userResponse);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Ok(userResponse);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.PasswordHash = _passwordService.HashPassword(dto.Password);

            await _context.SaveChangesAsync();

            var userResponse = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };

            return Ok(userResponse);
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
