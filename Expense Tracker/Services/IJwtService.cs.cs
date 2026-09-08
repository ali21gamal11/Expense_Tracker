namespace Expense_Tracker.Services;

public interface IJwtService
{
    string GenerateToken(int userId, string email);
}