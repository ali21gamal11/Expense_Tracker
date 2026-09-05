using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.Data.DTOs
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
