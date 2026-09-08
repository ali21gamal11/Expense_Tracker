using System.ComponentModel.DataAnnotations;
namespace Expense_Tracker.Data.DTOs

{
    public class ExpenseDto
    {
        
        

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0.01,double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
