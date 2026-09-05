using Expense_Tracker.Data;
using Microsoft.AspNetCore.Identity;

namespace Expense_Tracker.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<User> _passwordHasher;

    public PasswordService()
    {
        _passwordHasher = new PasswordHasher<User>();
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            null,
            passwordHash,
            password
        );

        return result == PasswordVerificationResult.Success;
    }
}