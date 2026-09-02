using Expense_Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Expense_Tracker.Data.DTOs;

namespace Expense_Tracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public CategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }


    // GET: api/Categories
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();
        return Ok(categories);
    }


    // POST: api/Categories
    [HttpPost]
    public async Task<IActionResult> CreateCategories(CategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return Ok(category);
    }


    // GET: api/Categories/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategories(int id)
    {
        var category= await _context.Categories.FindAsync(id);
        if(category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    // PUT: api/Categories/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategories(int id,CategoryDto dto )
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = dto.Name;
        await _context.SaveChangesAsync();
        return Ok(category);
    }

    // DELETE: api/Categories/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
        {
            return NotFound();
        }
         _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}