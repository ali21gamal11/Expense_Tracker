namespace Expense_Tracker.Data.DTOs
{
    public class ExpenseResponseDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
