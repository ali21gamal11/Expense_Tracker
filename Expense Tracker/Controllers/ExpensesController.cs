using Expense_Tracker.Data;
using Expense_Tracker.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
namespace Expense_Tracker.Controllers



{

    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetExpenses()
        {
            
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var expenses = await _context.Expenses
                .Where(e => e.UserId == userId)
                 .Include(e => e.User)
                .Include(e => e.Category)
                .Select(e => new ExpenseResponseDto
                {
                    Id = e.Id,
                    UserName = e.User.Name,
                    UserId = e.UserId,
                    CategoryId = e.CategoryId,
                    CategoryName = e.Category.Name,
                    Description = e.Description,
                    Amount = e.Amount,
                    Date = e.Date,
                    CreatedAt = e.CreatedAt

                }).ToArrayAsync();
            return Ok(expenses);

        }

        // POST: api/Expenses
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateExpense(ExpenseDto dto)
        {

            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var expense = new Expense
            {
                UserId = userId,
                CategoryId = dto.CategoryId,
                Amount = dto.Amount,
                Description = dto.Description,
                Date = dto.Date,
                CreatedAt = DateTime.UtcNow
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            await _context.Entry(expense)
                .Reference(e => e.User)
                .LoadAsync();

            await _context.Entry(expense)
                .Reference(e => e.Category)
                .LoadAsync();

            var expenseResponse = new ExpenseResponseDto
            {
                Id = expense.Id,
                UserId = expense.UserId,
                UserName = expense.User.Name,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CreatedAt = expense.CreatedAt
            };
            return CreatedAtAction(nameof(GetExpense),new{id = expense.Id }, expenseResponse);
        }

        // GET: api/Expenses/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetExpense(int id)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var expense = await _context.Expenses
            .Include(e => e.User)
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }
            var expenseResponse = new ExpenseResponseDto
            {
                Id = expense.Id,
                UserId = expense.UserId,
                UserName = expense.User.Name,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CreatedAt = expense.CreatedAt
            };

            return Ok(expenseResponse);
        }

        // PUT: api/Expenses/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateExpense(int id, ExpenseDto dto)
        {
            var userId = int.Parse(
               User.FindFirst(ClaimTypes.NameIdentifier)!.Value);


            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }

            expense.CategoryId = dto.CategoryId;
            expense.Amount = dto.Amount;
            expense.Description = dto.Description;
            expense.Date = dto.Date;

            await _context.SaveChangesAsync();

            await _context.Entry(expense)
                .Reference(e => e.User)
                .LoadAsync();

            await _context.Entry(expense)
                .Reference(e => e.Category)
                .LoadAsync();

            var expenseResponse = new ExpenseResponseDto
            {
                Id = expense.Id,
                UserId = expense.UserId,
                UserName = expense.User.Name,
                CategoryId = expense.CategoryId,
                CategoryName = expense.Category.Name,
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                CreatedAt = expense.CreatedAt
            };

            return Ok(expenseResponse);
        }

        // DELETE: api/Expenses/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);
            if (expense == null)
            {
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
