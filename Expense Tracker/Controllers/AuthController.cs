using Expense_Tracker.Data;
using Expense_Tracker.Services;
using Microsoft.EntityFrameworkCore;


namespace Expense_Tracker.Controllers
{
    public class AuthController
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordService _passwordService;
        public AuthController(ApplicationDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }
    }
}
