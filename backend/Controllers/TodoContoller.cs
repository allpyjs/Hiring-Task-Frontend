using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;

[ApiController]
[Route("api/v1/todos")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public TodoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult GetTodos()
    {
        var userId = _userManager.GetUserId(User);
        var todos = _context.TodoItems.Where(t => t.UserId == userId).ToList();
        return Ok(todos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] TodoItem todo)
    {
        var userId = _userManager.GetUserId(User);
        todo.UserId = userId;

        _context.TodoItems.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodos), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] TodoItem updatedTodo)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null || todo.UserId != _userManager.GetUserId(User))
            return Unauthorized();

        todo.Title = updatedTodo.Title;
        todo.Description = updatedTodo.Description;
        todo.Status = updatedTodo.Status;
        todo.DueDate = updatedTodo.DueDate;

        _context.TodoItems.Update(todo);
        await _context.SaveChangesAsync();

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null || todo.UserId != _userManager.GetUserId(User))
            return Unauthorized();

        _context.TodoItems.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
